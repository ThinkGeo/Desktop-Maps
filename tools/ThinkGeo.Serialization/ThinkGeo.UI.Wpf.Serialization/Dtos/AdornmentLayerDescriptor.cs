using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ThinkGeo.Core.Serialization.Dtos;

namespace ThinkGeo.UI.Wpf.Serialization.Dtos;

/// <summary>
/// Base descriptor for any <c>AdornmentLayer</c> hosted on a MapView's <c>AdornmentOverlay</c>.
/// Polymorphic JSON via <c>kind</c>.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "kind")]
[JsonDerivedType(typeof(LogoAdornmentLayerDescriptor), "logo")]
[JsonDerivedType(typeof(ScaleLineAdornmentLayerDescriptor), "scaleLine")]
[JsonDerivedType(typeof(ScaleTextAdornmentLayerDescriptor), "scaleText")]
[JsonDerivedType(typeof(ScaleBarAdornmentLayerDescriptor), "scaleBar")]
[JsonDerivedType(typeof(LegendAdornmentLayerDescriptor), "legend")]
[JsonDerivedType(typeof(MagneticDeclinationAdornmentLayerDescriptor), "magneticDeclination")]
public abstract record AdornmentLayerDescriptor
{
    public string Name { get; init; } = "";
    public bool IsVisible { get; init; } = true;

    /// <summary>Corner anchor: <c>"UpperLeft" | "UpperCenter" | "UpperRight" | "CenterLeft" | "Center" | "CenterRight" | "LowerLeft" | "LowerCenter" | "LowerRight"</c>.</summary>
    public string Location { get; init; } = "LowerRight";
    public float Width { get; init; }
    public float Height { get; init; }
    public float XOffsetInPixel { get; init; }
    public float YOffsetInPixel { get; init; }
    public float ResizeScale { get; init; } = 1f;
}

/// <summary>Logo adornment — an image pinned to a corner. Image path is optional (null uses default image).</summary>
public sealed record LogoAdornmentLayerDescriptor : AdornmentLayerDescriptor
{
    public string? ImagePath { get; init; }
}

/// <summary>Scale line adornment — a distance bar labelled in the configured unit system.</summary>
public sealed record ScaleLineAdornmentLayerDescriptor : AdornmentLayerDescriptor
{
    public float MaxBarLength { get; init; } = 150;
    public float Margin { get; init; } = 10;
    /// <summary><c>"Metric" | "Imperial" | "ImperialAndMetric"</c>.</summary>
    public string UnitSystem { get; init; } = "ImperialAndMetric";
    public StyleDescriptor? AboveLabelTextStyle { get; init; }
    public StyleDescriptor? BelowLabelTextStyle { get; init; }
}

/// <summary>Scale text adornment — textual scale readout, e.g. "1 inch = 2000 feet".</summary>
public sealed record ScaleTextAdornmentLayerDescriptor : AdornmentLayerDescriptor
{
    /// <summary><c>"Inch" | "Centimeter"</c>.</summary>
    public string ScreenUnit { get; init; } = "Inch";
    /// <summary>Any <see cref="DistanceUnit"/> enum name.</summary>
    public string WorldUnit { get; init; } = "Feet";
    public string FontFamily { get; init; } = "Arial";
    public float FontSize { get; init; } = 10f;
    public string FontStyle { get; init; } = "Regular";
    public string TextColor { get; init; } = "#FF000000";
}

/// <summary>Scale bar adornment — graphic scale bar with alternating segments.</summary>
public sealed record ScaleBarAdornmentLayerDescriptor : AdornmentLayerDescriptor
{
    /// <summary><c>"Metric" | "Imperial"</c>.</summary>
    public string UnitFamily { get; init; } = "Metric";
    public int Thickness { get; init; } = 10;
    public int MaxWidth { get; init; } = 250;
    public float Margin { get; init; } = 10f;
    public string BarBrushColor { get; init; } = "#FF000000";
    public string LabelBrushColor { get; init; } = "#FF000000";
    public string? AlternateBarBrushColor { get; init; }
    public string MaskBrushColor { get; init; } = "#FFFFFFFF";
    public bool HasMask { get; init; }
    public string MaskContourColor { get; init; } = "#FF000000";
    public string BarPenColor { get; init; } = "#FF000000";
    public string LabelFontFamily { get; init; } = "Microsoft Sans Serif";
    public float LabelFontSize { get; init; } = 14f;
    public string LabelFontStyle { get; init; } = "Bold";
}

/// <summary>Legend adornment — title + optional footer + collection of legend items.</summary>
public sealed record LegendAdornmentLayerDescriptor : AdornmentLayerDescriptor
{
    public LegendItemDescriptor? Title { get; init; }
    public LegendItemDescriptor? Footer { get; init; }
    public List<LegendItemDescriptor> LegendItems { get; init; } = new();
    /// <summary><c>"Fixed" | "MaintainAspectRatio"</c> (maps to <c>AdornmentResizeMode</c>).</summary>
    public string ResizeMode { get; init; } = "Fixed";
}

/// <summary>One row in a <see cref="LegendAdornmentLayerDescriptor"/>.</summary>
public sealed record LegendItemDescriptor
{
    public float Width { get; init; }
    public float Height { get; init; }
    public float ImageWidth { get; init; }
    public float ImageHeight { get; init; }
    public StyleDescriptor? ImageStyle { get; init; }
    public StyleDescriptor? TextStyle { get; init; }
}

/// <summary>Magnetic-declination adornment — north-arrow showing magnetic vs true vs grid north.</summary>
public sealed record MagneticDeclinationAdornmentLayerDescriptor : AdornmentLayerDescriptor
{
    public string? MagneticFieldPathFilename { get; init; }
    public double Elevation { get; init; }
    /// <summary>Any <see cref="DistanceUnit"/> enum name.</summary>
    public string ElevationUnit { get; init; } = "Meter";
    /// <summary>Must fall within the SDK's supported range (1900–2015).</summary>
    public DateTime SampleDateTime { get; init; } = new DateTime(2015, 1, 1, 0, 0, 0, DateTimeKind.Utc);
}

/// <summary>
/// The root <c>AdornmentOverlay</c> owned by a MapView. Contains the ordered list of adornment
/// layers plus the overlay's editability toggle. Like BackgroundOverlay, the overlay instance
/// itself is owned by the MapView — <see cref="Mappers.MapViewSnapshotExtensions.ApplySnapshot"/>
/// clears and refills the existing <c>mapView.AdornmentOverlay.Layers</c> in place.
/// </summary>
public sealed record AdornmentOverlaySnapshot
{
    public List<AdornmentLayerDescriptor> Layers { get; init; } = new();
    public bool IsEditable { get; init; }
    public double Opacity { get; init; } = 1.0;
    public bool IsVisible { get; init; } = true;
}
