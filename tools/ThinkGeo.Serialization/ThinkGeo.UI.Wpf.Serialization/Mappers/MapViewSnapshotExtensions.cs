using System.Linq;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Mappers;
using ThinkGeo.UI.Wpf.Serialization.Dtos;

namespace ThinkGeo.UI.Wpf.Serialization.Mappers;

/// <summary>
/// Extension methods on <see cref="MapView"/> to produce and apply <see cref="MapViewSnapshot"/>.
/// </summary>
public static class MapViewSnapshotExtensions
{
    /// <summary>Produces a snapshot of the current MapView state.</summary>
    public static MapViewSnapshot CreateSnapshot(this MapView mapView)
    {
        if (mapView == null) throw new System.ArgumentNullException(nameof(mapView));

        return new MapViewSnapshot
        {
            SchemaVersion = 1,
            CenterX = mapView.CenterPoint?.X ?? 0,
            CenterY = mapView.CenterPoint?.Y ?? 0,
            CurrentScale = mapView.CurrentScale,
            RotationAngle = (float)mapView.RotationAngle,
            MapUnit = mapView.MapUnit.ToString(),
            ZoomScales = mapView.ZoomScales?.ToArray(),
            Overlays = mapView.Overlays
                .Select(OverlayMapper.TryToDescriptor)
                .Where(d => d != null)
                .ToList()!,
            BackgroundOverlay = mapView.BackgroundOverlay is BackgroundOverlay bg
                ? OverlayMapper.TryToDescriptor(bg)
                : null,
            AdornmentOverlay = mapView.AdornmentOverlay is AdornmentOverlay ao
                ? AdornmentMapper.ToSnapshot(ao)
                : null,
        };
    }

    /// <summary>
    /// Applies a snapshot to <paramref name="mapView"/>, replacing its current overlays,
    /// extent, scale, and rotation with the snapshotted values.
    /// </summary>
    public static void ApplySnapshot(this MapView mapView, MapViewSnapshot snapshot)
    {
        if (mapView == null) throw new System.ArgumentNullException(nameof(mapView));
        if (snapshot == null) throw new System.ArgumentNullException(nameof(snapshot));

        mapView.MapUnit = System.Enum.TryParse<GeographyUnit>(snapshot.MapUnit, out var mu)
            ? mu
            : GeographyUnit.DecimalDegree;

        mapView.Overlays.Clear();
        foreach (var descriptor in snapshot.Overlays)
        {
            // FromDescriptor returns null for descriptors that cannot be constructed outside the
            // MapView (currently only BackgroundOverlay). Skip those — background is applied below.
            var overlay = OverlayMapper.FromDescriptor(descriptor);
            if (overlay != null) mapView.Overlays.Add(overlay);
        }

        if (snapshot.BackgroundOverlay is BackgroundOverlayDescriptor bg && mapView.BackgroundOverlay != null)
        {
            // BackgroundOverlay has an internal ctor, so we mutate the instance MapView already owns.
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(ColorMapper.FromHexString(bg.BackgroundBrushColor));
            mapView.BackgroundOverlay.IsVisible = bg.IsVisible;
            mapView.BackgroundOverlay.Opacity = bg.Opacity;
        }

        if (snapshot.AdornmentOverlay != null && mapView.AdornmentOverlay != null)
        {
            // AdornmentOverlay is owned by MapView; mutate layers in place to preserve its Canvas wiring.
            AdornmentMapper.ApplySnapshot(mapView.AdornmentOverlay, snapshot.AdornmentOverlay);
        }

        if (snapshot.ZoomScales is { Length: > 0 })
        {
            mapView.ZoomScales.Clear();
            foreach (var scale in snapshot.ZoomScales)
            {
                mapView.ZoomScales.Add(scale);
            }
        }

        mapView.CenterPoint = new PointShape(snapshot.CenterX, snapshot.CenterY);
        mapView.CurrentScale = snapshot.CurrentScale;
        mapView.RotationAngle = snapshot.RotationAngle;
    }
}
