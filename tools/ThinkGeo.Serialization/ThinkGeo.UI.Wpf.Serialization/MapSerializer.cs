using System;
using System.IO;
using System.Text.Json;
using ThinkGeo.UI.Wpf.Serialization.Dtos;
using ThinkGeo.UI.Wpf.Serialization.Mappers;

namespace ThinkGeo.UI.Wpf.Serialization;

/// <summary>
/// Top-level facade for JSON-based MapView persistence. Hides the JSON serializer choice and
/// the schema migration hook so callers only need Save / Load / Serialize / Deserialize.
/// </summary>
/// <remarks>
/// <para>Captures: overlays (incl. interactive Edit/Track/Extent), layers, styles, features,
/// projection converters, adornments, extent, scale, rotation, zoom scales.</para>
/// <para>Does <b>not</b> capture: WPF <c>UIElement</c> content on markers/popups,
/// <c>AzureMapsRasterOverlay.SubscriptionKey</c>, event subscriptions, or the subclass identity
/// of a <c>ProjectionConverter</c> (a <c>GdalProjectionConverter</c> round-trips as a plain
/// <c>Projection</c> built from the same SRID).</para>
/// <para>See <c>tools/README.md</c> for the full known-loss list and tested coverage.</para>
/// <example>
/// <code>
/// var json = MapSerializer.Serialize(mapView);
/// File.WriteAllText("workspace.json", json);
/// // ...later, on a fresh MapView:
/// MapSerializer.Deserialize(freshMapView, File.ReadAllText("workspace.json"));
/// </code>
/// </example>
/// </remarks>
public static class MapSerializer
{
    /// <summary>JSON options used by all helpers — exposed for tests / custom pipelines.</summary>
    public static JsonSerializerOptions DefaultJsonOptions { get; } = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
    };

    /// <summary>Writes the current MapView state to <paramref name="filePath"/> as JSON.</summary>
    public static void Save(MapView mapView, string filePath)
    {
        File.WriteAllText(filePath, Serialize(mapView));
    }

    /// <summary>Reads JSON from <paramref name="filePath"/> and applies it to <paramref name="mapView"/>.</summary>
    public static void Load(MapView mapView, string filePath)
    {
        Deserialize(mapView, File.ReadAllText(filePath));
    }

    /// <summary>Serializes a MapView state to a JSON string.</summary>
    public static string Serialize(MapView mapView)
    {
        var snapshot = mapView.CreateSnapshot();
        return JsonSerializer.Serialize(snapshot, DefaultJsonOptions);
    }

    /// <summary>Applies a JSON-serialized state to a MapView.</summary>
    public static void Deserialize(MapView mapView, string json)
    {
        var snapshot = JsonSerializer.Deserialize<MapViewSnapshot>(json, DefaultJsonOptions)
            ?? throw new InvalidOperationException("Deserialized MapViewSnapshot was null.");

        if (snapshot.SchemaVersion != 1)
        {
            snapshot = MigrationHook(snapshot);
        }

        mapView.ApplySnapshot(snapshot);
    }

    /// <summary>
    /// Hook for version-tolerant load. Called when <see cref="MapViewSnapshot.SchemaVersion"/>
    /// differs from the current supported version. Default implementation throws — override by
    /// swapping this delegate.
    /// </summary>
    public static Func<MapViewSnapshot, MapViewSnapshot> MigrationHook { get; set; } =
        snapshot => throw new NotSupportedException(
            $"MapView snapshot schema version {snapshot.SchemaVersion} is not supported.");
}
