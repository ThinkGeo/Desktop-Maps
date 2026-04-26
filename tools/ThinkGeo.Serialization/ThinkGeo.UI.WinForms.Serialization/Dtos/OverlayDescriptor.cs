using System.Text.Json.Serialization;
using ThinkGeo.Core.Serialization.Dtos;

namespace ThinkGeo.UI.WinForms.Serialization.Dtos;

/// <summary>
/// Base descriptor for any <c>Overlay</c> on a WPF MapView. Polymorphic JSON via <c>kind</c>.
/// Add a new <see cref="JsonDerivedTypeAttribute"/> when supporting another overlay.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "kind")]
[JsonDerivedType(typeof(LayerOverlayDescriptor), "layerOverlay")]
[JsonDerivedType(typeof(BackgroundOverlayDescriptor), "background")]
[JsonDerivedType(typeof(SimpleMarkerOverlayDescriptor), "simpleMarker")]
[JsonDerivedType(typeof(ThinkGeoCloudVectorMapsOverlayDescriptor), "thinkGeoCloudVector")]
[JsonDerivedType(typeof(ThinkGeoCloudRasterMapsOverlayDescriptor), "thinkGeoCloudRaster")]
[JsonDerivedType(typeof(OpenStreetMapOverlayDescriptor), "openStreetMap")]
[JsonDerivedType(typeof(WmsOverlayDescriptor), "wmsOverlay")]
[JsonDerivedType(typeof(WmtsOverlayDescriptor), "wmtsOverlay")]
[JsonDerivedType(typeof(BingMapsOverlayDescriptor), "bingMaps")]
[JsonDerivedType(typeof(GoogleMapsOverlayDescriptor), "googleMaps")]
[JsonDerivedType(typeof(AzureMapsRasterOverlayDescriptor), "azureMapsRaster")]
[JsonDerivedType(typeof(HereMapsRasterTileOverlayDescriptor), "hereMapsRaster")]
[JsonDerivedType(typeof(MapBoxStaticTilesOverlayDescriptor), "mapBoxStatic")]
[JsonDerivedType(typeof(PopupOverlayDescriptor), "popup")]
[JsonDerivedType(typeof(EditInteractiveOverlayDescriptor), "editInteractive")]
[JsonDerivedType(typeof(TrackInteractiveOverlayDescriptor), "trackInteractive")]
[JsonDerivedType(typeof(ExtentInteractiveOverlayDescriptor), "extentInteractive")]
public abstract record OverlayDescriptor
{
    public string Name { get; init; } = "";
    public bool IsVisible { get; init; } = true;
    public double Opacity { get; init; } = 1.0;
}

/// <summary>Generic layer overlay — a container for one or more <see cref="LayerDescriptor"/>.</summary>
public sealed record LayerOverlayDescriptor : OverlayDescriptor
{
    public List<LayerDescriptor> Layers { get; init; } = new();
}

/// <summary>Background overlay (single solid fill / gradient).</summary>
public sealed record BackgroundOverlayDescriptor : OverlayDescriptor
{
    public string BackgroundBrushColor { get; init; } = "#FFFFFFFF";
}

/// <summary>
/// Marker overlay backed by <c>SimpleMarkerOverlay</c>. Each marker carries its own world
/// position + optional image / tooltip.
/// </summary>
public sealed record SimpleMarkerOverlayDescriptor : OverlayDescriptor
{
    public List<MarkerDescriptor> Markers { get; init; } = new();
    public string DragMode { get; init; } = "None";
}

/// <summary>Individual marker inside a <see cref="SimpleMarkerOverlayDescriptor"/>.</summary>
public sealed record MarkerDescriptor(
    double WorldX,
    double WorldY,
    double XOffset = 0,
    double YOffset = 0,
    double RotationAngle = 0,
    string? ImagePath = null,
    string? ToolTip = null);

/// <summary>ThinkGeo Cloud vector map service.</summary>
public sealed record ThinkGeoCloudVectorMapsOverlayDescriptor : OverlayDescriptor
{
    public string ClientId { get; init; } = "";
    public string ClientSecret { get; init; } = "";
    public string MapType { get; init; } = "Light";
}

/// <summary>ThinkGeo Cloud raster map service.</summary>
public sealed record ThinkGeoCloudRasterMapsOverlayDescriptor : OverlayDescriptor
{
    public string ClientId { get; init; } = "";
    public string ClientSecret { get; init; } = "";
    public string MapType { get; init; } = "Light";
}

/// <summary>OpenStreetMap tile overlay.</summary>
public sealed record OpenStreetMapOverlayDescriptor : OverlayDescriptor
{
    public string? UserAgent { get; init; }
}

/// <summary>WMS tile overlay — server URL + layers + params.</summary>
public sealed record WmsOverlayDescriptor : OverlayDescriptor
{
    public string ServerUri { get; init; } = "";
    public List<string> ActiveLayerNames { get; init; } = new();
    public List<string> ActiveStyleNames { get; init; } = new();
    public string OutputFormat { get; init; } = "image/png";
    public bool IsTransparent { get; init; } = true;
    public Dictionary<string, string> Parameters { get; init; } = new();
}

/// <summary>WMTS tile overlay — server URL + active layer/style + tile matrix.</summary>
public sealed record WmtsOverlayDescriptor : OverlayDescriptor
{
    public string ServerUri { get; init; } = "";
    public string ActiveLayerName { get; init; } = "";
    public string? ActiveStyleName { get; init; }
    public string? OutputFormat { get; init; }
    /// <summary><c>"Kvp" | "Rest"</c>.</summary>
    public string? WmtsServerEncodingType { get; init; }
}

/// <summary>Bing Maps tile overlay — application id + map type.</summary>
public sealed record BingMapsOverlayDescriptor : OverlayDescriptor
{
    public string ApplicationId { get; init; } = "";
    /// <summary><c>"Road" | "Aerial" | "AerialWithLabels"</c>.</summary>
    public string MapType { get; init; } = "Road";
}

/// <summary>Google Maps tile overlay — API key (or client id + private key) + map type.</summary>
public sealed record GoogleMapsOverlayDescriptor : OverlayDescriptor
{
    public string? ApiKey { get; init; }
    public string? ClientId { get; init; }
    public string? PrivateKey { get; init; }
    public string? UriSigningSecret { get; init; }
    /// <summary><c>"RoadMap" | "Satellite" | "Terrain" | "Hybrid"</c>.</summary>
    public string MapType { get; init; } = "RoadMap";
}

/// <summary>
/// Azure Maps raster tile overlay. <c>SubscriptionKey</c> is not exposed publicly by the overlay,
/// so the capture path returns <c>null</c>; callers must set it before applying.
/// </summary>
public sealed record AzureMapsRasterOverlayDescriptor : OverlayDescriptor
{
    public string? SubscriptionKey { get; init; }
    /// <summary><c>"BaseRoad" | "BaseHybridRoad" | "BaseDarkGrey" | "Imagery" | …</c>.</summary>
    public string RasterTileSet { get; init; } = "BaseRoad";
    public string? ApiVersion { get; init; }
}

/// <summary>HERE Maps raster tile overlay — API key + map type + format.</summary>
public sealed record HereMapsRasterTileOverlayDescriptor : OverlayDescriptor
{
    public string ApiKey { get; init; } = "";
    /// <summary><c>"BaseMap" | "Aerial" | "Hybrid"</c>.</summary>
    public string MapType { get; init; } = "BaseMap";
    /// <summary><c>"Png" | "Jpeg"</c>.</summary>
    public string Format { get; init; } = "Png";
}

/// <summary>Mapbox static tiles overlay — access token + style id.</summary>
public sealed record MapBoxStaticTilesOverlayDescriptor : OverlayDescriptor
{
    public string AccessToken { get; init; } = "";
    /// <summary>A <c>MapBoxStyleId</c> enum name: <c>"Streets" | "Outdoors" | "Light" | "Dark" | "Satellite" | "SatelliteStreets"</c>.</summary>
    public string StyleId { get; init; } = "Streets";
}

/// <summary>
/// Popup overlay — point-anchored popups. Each popup carries its position, arrow height,
/// rotation, and (optionally) a string <c>Content</c>. Richer WPF <c>Content</c> values
/// (<c>UIElement</c>, bindings, etc.) are <b>not</b> captured by the snapshot and are lossy.
/// </summary>
public sealed record PopupOverlayDescriptor : OverlayDescriptor
{
    public List<PopupDescriptor> Popups { get; init; } = new();
}

/// <summary>Individual popup inside a <see cref="PopupOverlayDescriptor"/>.</summary>
public sealed record PopupDescriptor(
    double WorldX,
    double WorldY,
    int ArrowHeight = 8,
    double RotateAngle = 0,
    string? Content = null);

/// <summary>
/// Edit overlay — maps to <c>EditInteractiveOverlay</c>. Captures the editable feature set plus
/// the six edit-ability toggles. Styling is not captured; the overlay's built-in edit visuals
/// (control points, etc.) are restored from SDK defaults.
/// </summary>
public sealed record EditInteractiveOverlayDescriptor : OverlayDescriptor
{
    public bool CanDrag { get; init; } = true;
    public bool CanReshape { get; init; } = true;
    public bool CanResize { get; init; } = true;
    public bool CanRotate { get; init; } = true;
    public bool CanAddVertex { get; init; } = true;
    public bool CanRemoveVertex { get; init; } = true;
    /// <summary>Features currently editable (<c>EditShapesLayer.InternalFeatures</c>).</summary>
    public List<ThinkGeo.Core.Serialization.Dtos.FeatureDescriptor> EditShapes { get; init; } = new();
}

/// <summary>
/// Track overlay — maps to <c>TrackInteractiveOverlay</c>. Captures the current track mode plus
/// any features already committed to <c>TrackShapeLayer.InternalFeatures</c>. The in-progress
/// "InTrackingFeature" sentinel is intentionally dropped (it represents a mid-drag state).
/// </summary>
public sealed record TrackInteractiveOverlayDescriptor : OverlayDescriptor
{
    /// <summary>
    /// Any <c>TrackMode</c> enum name: <c>"None" | "Point" | "Line" | "Polygon" | "Rectangle" |
    /// "Square" | "Circle" | "Ellipse" | "StraightLine" | "Freehand" | "Custom" | "Multipoint"</c>.
    /// </summary>
    public string TrackMode { get; init; } = "None";
    public int VertexCountInQuarter { get; init; } = 6;
    public List<ThinkGeo.Core.Serialization.Dtos.FeatureDescriptor> TrackShapes { get; init; } = new();
}

/// <summary>
/// Extent overlay — maps to <c>ExtentInteractiveOverlay</c>. Captures pan/zoom/wheel modes plus
/// the double-click behaviors. Mouse-button and key bindings (<c>RotationMouseButton</c>,
/// <c>RotationKey</c>, etc.) are not captured — they're rarely customized and pin the snapshot to WPF.
/// </summary>
public sealed record ExtentInteractiveOverlayDescriptor : OverlayDescriptor
{
    /// <summary>Any <c>MapPanMode</c> enum name: <c>"Default" | "Disabled"</c>.</summary>
    public string PanMode { get; init; } = "Default";
    /// <summary>Any <c>MapZoomMode</c> enum name.</summary>
    public string MapZoomMode { get; init; } = "Default";
    /// <summary>Any <c>MapMouseWheelMode</c> enum name.</summary>
    public string MouseWheelMode { get; init; } = "Default";
    /// <summary>Any <c>MapDoubleClickMode</c> enum name: <c>"Default" | "Disabled"</c>.</summary>
    public string DoubleLeftClickMode { get; init; } = "Default";
    public string DoubleRightClickMode { get; init; } = "Default";
    /// <summary>Any <c>ExtentChangedType</c> enum name.</summary>
    public string ExtentChangedType { get; init; } = "Default";
    /// <summary>Any <c>ZoomSnapDirection</c> enum name: <c>"UpperScale" | "LowerScale"</c>.</summary>
    public string ZoomSnapDirection { get; init; } = "LowerScale";
    public int ZoomPercentage { get; init; } = 50;
    public float MinimumTrackZoomInExtentInPixels { get; init; } = 10f;
}
