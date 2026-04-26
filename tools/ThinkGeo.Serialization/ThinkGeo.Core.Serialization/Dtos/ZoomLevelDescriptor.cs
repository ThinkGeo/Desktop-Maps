namespace ThinkGeo.Core.Serialization.Dtos;

/// <summary>
/// Describes a single <c>ZoomLevel</c> — at the given scale range, these styles apply.
/// </summary>
public sealed record ZoomLevelDescriptor
{
    public string Name { get; init; } = "";
    public double Scale { get; init; }
    public string ApplyUntilZoomLevel { get; init; } = "Level20";
    public List<StyleDescriptor> CustomStyles { get; init; } = new();
    public StyleDescriptor? DefaultAreaStyle { get; init; }
    public StyleDescriptor? DefaultLineStyle { get; init; }
    public StyleDescriptor? DefaultPointStyle { get; init; }
    public StyleDescriptor? DefaultTextStyle { get; init; }
}

/// <summary>
/// Describes a full <c>ZoomLevelSet</c> — the 20-level zoom ladder + custom levels.
/// </summary>
public sealed record ZoomLevelSetDescriptor
{
    public List<ZoomLevelDescriptor> ZoomLevels { get; init; } = new();
}
