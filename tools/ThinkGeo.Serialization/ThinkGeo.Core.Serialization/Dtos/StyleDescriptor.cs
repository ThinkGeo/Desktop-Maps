using System.Text.Json.Serialization;

namespace ThinkGeo.Core.Serialization.Dtos;

/// <summary>
/// Base descriptor for any ThinkGeo.Core <c>Style</c>. Concrete subclasses describe a specific
/// style kind (area, line, point, text, class-break, …). Polymorphic JSON round-trips via the
/// <c>kind</c> discriminator.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "kind")]
[JsonDerivedType(typeof(AreaStyleDescriptor), "area")]
[JsonDerivedType(typeof(LineStyleDescriptor), "line")]
[JsonDerivedType(typeof(PointStyleDescriptor), "point")]
[JsonDerivedType(typeof(TextStyleDescriptor), "text")]
[JsonDerivedType(typeof(ClassBreakStyleDescriptor), "classBreak")]
[JsonDerivedType(typeof(ValueStyleDescriptor), "value")]
[JsonDerivedType(typeof(CompositeStyleDescriptor), "composite")]
[JsonDerivedType(typeof(RegexStyleDescriptor), "regex")]
[JsonDerivedType(typeof(FilterStyleDescriptor), "filter")]
[JsonDerivedType(typeof(DotDensityStyleDescriptor), "dotDensity")]
[JsonDerivedType(typeof(IconStyleDescriptor), "icon")]
[JsonDerivedType(typeof(HeatStyleDescriptor), "heat")]
public abstract record StyleDescriptor
{
    /// <summary>Optional user-facing name (maps to <c>Style.Name</c>).</summary>
    public string Name { get; init; } = "";

    /// <summary>Whether the style actively participates in drawing (maps to <c>Style.IsActive</c>).</summary>
    public bool IsActive { get; init; } = true;
}

/// <summary>Describes an area (polygon fill + outline) style.</summary>
public sealed record AreaStyleDescriptor : StyleDescriptor
{
    public GeoBrushDescriptor? FillBrush { get; init; }
    public GeoPenDescriptor? OutlinePen { get; init; }
}

/// <summary>Describes a line style (outer/inner/center pens).</summary>
public sealed record LineStyleDescriptor : StyleDescriptor
{
    public GeoPenDescriptor? OuterPen { get; init; }
    public GeoPenDescriptor? InnerPen { get; init; }
    public GeoPenDescriptor? CenterPen { get; init; }
}

/// <summary>Describes a point style (symbol type, size, fill, outline).</summary>
public sealed record PointStyleDescriptor : StyleDescriptor
{
    /// <summary>Symbol kind: <c>"circle"</c>, <c>"square"</c>, <c>"triangle"</c>, <c>"star"</c>, <c>"diamond"</c>, …</summary>
    public string Symbol { get; init; } = "circle";
    public float SymbolSize { get; init; }
    public GeoBrushDescriptor? FillBrush { get; init; }
    public GeoPenDescriptor? OutlinePen { get; init; }
    /// <summary>Optional path to an image for icon point styles.</summary>
    public string? IconPath { get; init; }
}

/// <summary>Describes a text / label style.</summary>
public sealed record TextStyleDescriptor : StyleDescriptor
{
    public string TextColumnName { get; init; } = "";
    public string FontFamily { get; init; } = "Arial";
    public float FontSize { get; init; } = 10f;
    public string FontStyle { get; init; } = "Regular";
    public string TextColor { get; init; } = "#FF000000";
    public string? HaloColor { get; init; }
    public float HaloPenWidth { get; init; }
}

/// <summary>Describes a class-break style — applies different child styles per attribute range.</summary>
public sealed record ClassBreakStyleDescriptor : StyleDescriptor
{
    public string ColumnName { get; init; } = "";
    public string BreakValueInclusion { get; init; } = "IncludeValue";
    public List<ClassBreakEntryDescriptor> Breaks { get; init; } = new();
}

/// <summary>
/// One entry in a <see cref="ClassBreakStyleDescriptor"/> — a break value plus the styles that
/// apply at or above this value (until the next break). Styles list can mix Area/Line/Point/Text.
/// </summary>
public sealed record ClassBreakEntryDescriptor(double Value, List<StyleDescriptor> Styles);

/// <summary>Describes a value style — applies different child styles per exact attribute value.</summary>
public sealed record ValueStyleDescriptor : StyleDescriptor
{
    public string ColumnName { get; init; } = "";
    public List<ValueStyleEntryDescriptor> ValueItems { get; init; } = new();
    public StyleDescriptor? DefaultStyle { get; init; }
}

/// <summary>One entry in a <see cref="ValueStyleDescriptor"/> — the attribute value and the styles to apply.</summary>
public sealed record ValueStyleEntryDescriptor(string Value, List<StyleDescriptor> Styles);

/// <summary>Describes a composite style — renders an ordered list of child styles together.</summary>
public sealed record CompositeStyleDescriptor : StyleDescriptor
{
    public List<StyleDescriptor> Styles { get; init; } = new();
}

/// <summary>
/// Describes a regex style — applies different child styles per regex match on an attribute.
/// Maps to <c>ThinkGeo.Core.RegexStyle</c>.
/// </summary>
public sealed record RegexStyleDescriptor : StyleDescriptor
{
    public string ColumnName { get; init; } = "";
    /// <summary><c>"MatchFirstOnly" | "MatchAll"</c>.</summary>
    public string RegexMatchingRule { get; init; } = "MatchFirstOnly";
    public List<RegexStyleEntryDescriptor> Items { get; init; } = new();
}

/// <summary>One entry in a <see cref="RegexStyleDescriptor"/> — a regular expression and the styles to apply when it matches.</summary>
public sealed record RegexStyleEntryDescriptor(string RegularExpression, List<StyleDescriptor> Styles);

/// <summary>
/// Describes a filter style — renders child styles whenever ALL conditions evaluate true for a feature.
/// Maps to <c>ThinkGeo.Core.FilterStyle</c>.
/// </summary>
public sealed record FilterStyleDescriptor : StyleDescriptor
{
    public List<StyleDescriptor> Styles { get; init; } = new();
    public List<FilterConditionDescriptor> Conditions { get; init; } = new();
}

/// <summary>One boolean expression inside a <see cref="FilterStyleDescriptor"/>.</summary>
public sealed record FilterConditionDescriptor
{
    public string Name { get; init; } = "";
    public string ColumnName { get; init; } = "";
    public string Expression { get; init; } = "";
    public bool Logical { get; init; } = true;
    public bool IsLeftBracket { get; init; }
    public bool IsRightBracket { get; init; }
    /// <summary><c>"None" | "IgnoreCase" | "Multiline" | …</c> (<see cref="System.Text.RegularExpressions.RegexOptions"/> flags, comma-separated).</summary>
    public string RegexOptions { get; init; } = "None";
}

/// <summary>
/// Describes an icon-with-label style — an image anchored to a point, optionally labelled by an
/// attribute column. Maps to <c>ThinkGeo.Core.IconStyle</c> (which derives from <c>TextStyle</c>,
/// so it carries the same font/color surface).
/// </summary>
public sealed record IconStyleDescriptor : StyleDescriptor
{
    public string IconPath { get; init; } = "";
    public double IconImageScale { get; init; } = 1.0;
    /// <summary>Optional column whose value is rendered over/next to the icon.</summary>
    public string TextColumnName { get; init; } = "";
    public string FontFamily { get; init; } = "Arial";
    public float FontSize { get; init; } = 10f;
    public string FontStyle { get; init; } = "Regular";
    public string TextColor { get; init; } = "#FF000000";
    public string? HaloColor { get; init; }
    public float HaloPenWidth { get; init; }
}

/// <summary>
/// Describes a heat-map style — paints density gradients from scattered point features.
/// Maps to <c>ThinkGeo.Core.HeatStyle</c>.
/// </summary>
public sealed record HeatStyleDescriptor : StyleDescriptor
{
    public int PointIntensity { get; init; } = 10;
    public int Alpha { get; init; } = 255;
    /// <summary>Optional — when set, intensity is sampled from this column (<see cref="IntensityRangeStart"/>..<see cref="IntensityRangeEnd"/>).</summary>
    public string IntensityColumnName { get; init; } = "";
    public double IntensityRangeStart { get; init; }
    public double IntensityRangeEnd { get; init; }
    public double PointRadius { get; init; } = 15;
    /// <summary>Any <c>DistanceUnit</c> enum name.</summary>
    public string PointRadiusUnit { get; init; } = "Meter";
    /// <summary>Ordered list of ARGB hex colors used for the gradient; captured from the live <c>ColorPalette</c>.</summary>
    public List<string> ColorPalette { get; init; } = new();
}

/// <summary>
/// Describes a dot-density style — scatters points across a polygon proportional to an attribute value.
/// Maps to <c>ThinkGeo.Core.DotDensityStyle</c>.
/// </summary>
public sealed record DotDensityStyleDescriptor : StyleDescriptor
{
    public string ColumnName { get; init; } = "";
    public double PointToValueRatio { get; init; } = 1;
    public int PointSize { get; init; } = 3;
    public string PointColor { get; init; } = "#FF000000";
    /// <summary>Optional custom point style. If supplied, it overrides <see cref="PointSize"/> / <see cref="PointColor"/>.</summary>
    public PointStyleDescriptor? CustomPointStyle { get; init; }
}
