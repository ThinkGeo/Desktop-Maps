namespace ThinkGeo.UI.WinForms.Serialization.Dtos;

/// <summary>
/// Root snapshot for a WPF <c>MapView</c>. Safe to JSON-serialize via <c>System.Text.Json</c>
/// with polymorphic overlay/layer support enabled.
/// </summary>
public sealed record MapViewSnapshot
{
    /// <summary>Bumps on incompatible format changes; consult on load to decide migration.</summary>
    public int SchemaVersion { get; init; } = 1;

    public double CenterX { get; init; }
    public double CenterY { get; init; }
    public double CurrentScale { get; init; }
    public float RotationAngle { get; init; }
    public string MapUnit { get; init; } = "DecimalDegree";

    /// <summary>Optional list of zoom scales (MapView.ZoomScales).</summary>
    public double[]? ZoomScales { get; init; }

    /// <summary>Visible overlays, in insertion order.</summary>
    public List<OverlayDescriptor> Overlays { get; init; } = new();

    /// <summary>Background overlay (rendered behind all other overlays).</summary>
    public OverlayDescriptor? BackgroundOverlay { get; init; }

    /// <summary>
    /// Adornments (logo, scale bar, legend, etc.) rendered on top of all other overlays. Like the
    /// BackgroundOverlay, the overlay instance itself is owned by the MapView — apply via
    /// <see cref="Mappers.MapViewSnapshotExtensions.ApplySnapshot"/>, which clears and refills the
    /// existing <c>mapView.AdornmentOverlay.Layers</c> in place.
    /// </summary>
    public AdornmentOverlaySnapshot? AdornmentOverlay { get; init; }
}
