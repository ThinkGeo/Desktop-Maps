namespace ThinkGeo.Core.Serialization.Dtos;

/// <summary>Describes a <c>GeoPen</c> — colour, width, dash pattern, line join/cap.</summary>
public sealed record GeoPenDescriptor
{
    /// <summary>ARGB hex string, e.g. <c>#FF0080FF</c>.</summary>
    public string Color { get; init; } = "#FF000000";
    public float Width { get; init; } = 1f;
    /// <summary>Optional brush (for gradient/hatch pens). If null, <see cref="Color"/> is used.</summary>
    public GeoBrushDescriptor? Brush { get; init; }
    /// <summary>Dash pattern values, e.g. <c>[4, 2]</c>. Null or empty = solid.</summary>
    public float[]? DashPattern { get; init; }
    /// <summary>Line join: <c>"Miter" | "Round" | "Bevel"</c>.</summary>
    public string LineJoin { get; init; } = "Miter";
    /// <summary>Start cap: <c>"Flat" | "Round" | "Square" | "Triangle"</c>.</summary>
    public string StartCap { get; init; } = "Flat";
    /// <summary>End cap: same options as <see cref="StartCap"/>.</summary>
    public string EndCap { get; init; } = "Flat";
}
