using System;
using System.Linq;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Mappers;
using ThinkGeo.UI.WinForms.Serialization.Dtos;

namespace ThinkGeo.UI.WinForms.Serialization.Mappers;

/// <summary>
/// Maps between <see cref="AdornmentLayer"/> instances and <see cref="AdornmentLayerDescriptor"/>.
/// Implemented: Logo, ScaleLine, ScaleText, ScaleBar, Legend, MagneticDeclination.
/// </summary>
public static class AdornmentMapper
{
    public static AdornmentOverlaySnapshot ToSnapshot(AdornmentOverlay overlay)
    {
        if (overlay == null) throw new ArgumentNullException(nameof(overlay));

        return new AdornmentOverlaySnapshot
        {
            IsEditable = overlay.IsEditable,
            IsVisible = overlay.IsVisible,
            Opacity = overlay.Opacity,
            Layers = overlay.Layers.Select(ToDescriptor).Where(d => d != null).ToList()!,
        };
    }

    /// <summary>
    /// Applies a snapshot to an existing <see cref="AdornmentOverlay"/> by replacing its
    /// <c>Layers</c> collection in place. The overlay instance is not re-created — its
    /// internal Canvas wiring is kept intact.
    /// </summary>
    public static void ApplySnapshot(AdornmentOverlay overlay, AdornmentOverlaySnapshot snapshot)
    {
        if (overlay == null) throw new ArgumentNullException(nameof(overlay));
        if (snapshot == null) throw new ArgumentNullException(nameof(snapshot));

        overlay.IsEditable = snapshot.IsEditable;
        overlay.IsVisible = snapshot.IsVisible;
        overlay.Opacity = snapshot.Opacity;

        overlay.Layers.Clear();
        foreach (var descriptor in snapshot.Layers)
        {
            var layer = FromDescriptor(descriptor);
            if (layer != null) overlay.Layers.Add(layer);
        }
    }

    public static AdornmentLayerDescriptor? ToDescriptor(AdornmentLayer layer)
    {
        if (layer == null) throw new ArgumentNullException(nameof(layer));

        AdornmentLayerDescriptor? descriptor = layer switch
        {
            LogoAdornmentLayer logo => new LogoAdornmentLayerDescriptor
            {
                ImagePath = logo.Image?.PathFilename,
            },
            ScaleLineAdornmentLayer sl => new ScaleLineAdornmentLayerDescriptor
            {
                MaxBarLength = sl.MaxBarLength,
                Margin = sl.Margin,
                UnitSystem = sl.UnitSystem.ToString(),
                AboveLabelTextStyle = sl.AboveLabelTextStyle is TextStyle at ? StyleMapper.TryToDescriptor(at) : null,
                BelowLabelTextStyle = sl.BelowLabelTextStyle is TextStyle bt ? StyleMapper.TryToDescriptor(bt) : null,
            },
            ScaleTextAdornmentLayer st => new ScaleTextAdornmentLayerDescriptor
            {
                ScreenUnit = st.ScreenUnit.ToString(),
                WorldUnit = st.WorldUnit.ToString(),
                // Font defaults:
                FontFamily = st.Font?.FontName ?? "Arial",
                FontSize = st.Font?.Size ?? 10f,
                FontStyle = st.Font?.Style.ToString() ?? "Regular",
                TextColor = (st.TextBrush as GeoSolidBrush)?.Color is GeoColor tc ? ColorMapper.ToHexString(tc) : "#FF000000",
            },
            ScaleBarAdornmentLayer sb => CaptureScaleBar(sb),
            LegendAdornmentLayer lg => CaptureLegend(lg),
            MagneticDeclinationAdornmentLayer md => new MagneticDeclinationAdornmentLayerDescriptor
            {
                MagneticFieldPathFilename = md.MagneticFieldPathFilename,
                Elevation = md.Elevation,
                ElevationUnit = md.ElevationUnit.ToString(),
                SampleDateTime = ClampSampleDateTime(md.SampleDateTime),
            },
            _ => null,
        };

        if (descriptor == null) return null;

        return descriptor with
        {
            Name = layer.Name ?? string.Empty,
            IsVisible = layer.IsVisible,
            Location = layer.Location.ToString(),
            Width = layer.Width,
            Height = layer.Height,
            XOffsetInPixel = layer.XOffsetInPixel,
            YOffsetInPixel = layer.YOffsetInPixel,
            ResizeScale = layer.ResizeScale,
        };
    }

    public static AdornmentLayer? FromDescriptor(AdornmentLayerDescriptor descriptor)
    {
        if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));

        AdornmentLayer? layer = descriptor switch
        {
            LogoAdornmentLayerDescriptor logo => BuildLogo(logo),
            ScaleLineAdornmentLayerDescriptor sl => BuildScaleLine(sl),
            ScaleTextAdornmentLayerDescriptor st => BuildScaleText(st),
            ScaleBarAdornmentLayerDescriptor sb => BuildScaleBar(sb),
            LegendAdornmentLayerDescriptor lg => BuildLegend(lg),
            MagneticDeclinationAdornmentLayerDescriptor md => BuildMagneticDeclination(md),
            _ => null,
        };

        if (layer == null) return null;

        layer.Name = descriptor.Name;
        layer.IsVisible = descriptor.IsVisible;
        layer.Location = Enum.TryParse<AdornmentLocation>(descriptor.Location, ignoreCase: true, out var loc)
            ? loc
            : AdornmentLocation.LowerRight;
        layer.Width = descriptor.Width;
        layer.Height = descriptor.Height;
        layer.XOffsetInPixel = descriptor.XOffsetInPixel;
        layer.YOffsetInPixel = descriptor.YOffsetInPixel;
        layer.ResizeScale = descriptor.ResizeScale;
        return layer;
    }

    private static ScaleBarAdornmentLayerDescriptor CaptureScaleBar(ScaleBarAdornmentLayer sb)
    {
        return new ScaleBarAdornmentLayerDescriptor
        {
            UnitFamily = sb.UnitFamily.ToString(),
            Thickness = sb.Thickness,
            MaxWidth = sb.MaxWidth,
            Margin = sb.Margin,
            BarBrushColor = (sb.BarBrush as GeoSolidBrush)?.Color is GeoColor bbc ? ColorMapper.ToHexString(bbc) : "#FF000000",
            LabelBrushColor = (sb.LabelBrush as GeoSolidBrush)?.Color is GeoColor lbc ? ColorMapper.ToHexString(lbc) : "#FF000000",
            AlternateBarBrushColor = (sb.AlternateBarBrush as GeoSolidBrush)?.Color is GeoColor abc ? ColorMapper.ToHexString(abc) : null,
            MaskBrushColor = (sb.MaskBrush as GeoSolidBrush)?.Color is GeoColor mbc ? ColorMapper.ToHexString(mbc) : "#FFFFFFFF",
            HasMask = sb.HasMask,
            MaskContourColor = sb.MaskContour?.Color is GeoColor mcc ? ColorMapper.ToHexString(mcc) : "#FF000000",
            BarPenColor = sb.BarPen?.Color is GeoColor bpc ? ColorMapper.ToHexString(bpc) : "#FF000000",
            LabelFontFamily = sb.LabelFont?.FontName ?? "Microsoft Sans Serif",
            LabelFontSize = sb.LabelFont?.Size ?? 14f,
            LabelFontStyle = sb.LabelFont?.Style.ToString() ?? "Bold",
        };
    }

    private static LegendAdornmentLayerDescriptor CaptureLegend(LegendAdornmentLayer lg)
    {
        return new LegendAdornmentLayerDescriptor
        {
            Title = lg.Title != null ? CaptureLegendItem(lg.Title) : null,
            Footer = lg.Footer != null ? CaptureLegendItem(lg.Footer) : null,
            LegendItems = lg.LegendItems.Select(CaptureLegendItem).ToList(),
            ResizeMode = lg.ResizeMode.ToString(),
        };
    }

    private static LegendItemDescriptor CaptureLegendItem(LegendItem item)
    {
        return new LegendItemDescriptor
        {
            Width = item.Width,
            Height = item.Height,
            ImageWidth = item.ImageWidth,
            ImageHeight = item.ImageHeight,
            ImageStyle = item.ImageStyle != null ? StyleMapper.TryToDescriptor(item.ImageStyle) : null,
            TextStyle = item.TextStyle != null ? StyleMapper.TryToDescriptor(item.TextStyle) : null,
        };
    }

    private static LogoAdornmentLayer BuildLogo(LogoAdornmentLayerDescriptor descriptor)
    {
        if (string.IsNullOrEmpty(descriptor.ImagePath)) return new LogoAdornmentLayer();
        return new LogoAdornmentLayer(new GeoImage(descriptor.ImagePath));
    }

    private static ScaleLineAdornmentLayer BuildScaleLine(ScaleLineAdornmentLayerDescriptor descriptor)
    {
        var layer = new ScaleLineAdornmentLayer
        {
            MaxBarLength = descriptor.MaxBarLength,
            Margin = descriptor.Margin,
        };
        if (Enum.TryParse<ScaleLineUnitSystem>(descriptor.UnitSystem, ignoreCase: true, out var sys))
        {
            layer.UnitSystem = sys;
        }
        if (descriptor.AboveLabelTextStyle != null && StyleMapper.FromDescriptor(descriptor.AboveLabelTextStyle) is TextStyle at)
        {
            layer.AboveLabelTextStyle = at;
        }
        if (descriptor.BelowLabelTextStyle != null && StyleMapper.FromDescriptor(descriptor.BelowLabelTextStyle) is TextStyle bt)
        {
            layer.BelowLabelTextStyle = bt;
        }
        return layer;
    }

    private static ScaleTextAdornmentLayer BuildScaleText(ScaleTextAdornmentLayerDescriptor descriptor)
    {
        var screen = Enum.TryParse<ScaleTextScreenUnit>(descriptor.ScreenUnit, ignoreCase: true, out var su)
            ? su
            : ScaleTextScreenUnit.Inch;
        var world = Enum.TryParse<DistanceUnit>(descriptor.WorldUnit, ignoreCase: true, out var wu)
            ? wu
            : DistanceUnit.Feet;
        var font = new GeoFont(descriptor.FontFamily, descriptor.FontSize,
            Enum.TryParse<DrawingFontStyles>(descriptor.FontStyle, ignoreCase: true, out var fs) ? fs : DrawingFontStyles.Regular);
        var brush = new GeoSolidBrush(ColorMapper.FromHexString(descriptor.TextColor));
        return new ScaleTextAdornmentLayer(screen, world, font, brush);
    }

    private static ScaleBarAdornmentLayer BuildScaleBar(ScaleBarAdornmentLayerDescriptor descriptor)
    {
        var layer = new ScaleBarAdornmentLayer
        {
            Thickness = descriptor.Thickness,
            MaxWidth = descriptor.MaxWidth,
            Margin = descriptor.Margin,
            BarBrush = new GeoSolidBrush(ColorMapper.FromHexString(descriptor.BarBrushColor)),
            LabelBrush = new GeoSolidBrush(ColorMapper.FromHexString(descriptor.LabelBrushColor)),
            MaskBrush = new GeoSolidBrush(ColorMapper.FromHexString(descriptor.MaskBrushColor)),
            HasMask = descriptor.HasMask,
            MaskContour = new GeoPen(ColorMapper.FromHexString(descriptor.MaskContourColor), 1f),
            BarPen = new GeoPen(ColorMapper.FromHexString(descriptor.BarPenColor), 1f),
            LabelFont = new GeoFont(descriptor.LabelFontFamily, descriptor.LabelFontSize,
                Enum.TryParse<DrawingFontStyles>(descriptor.LabelFontStyle, ignoreCase: true, out var lfs) ? lfs : DrawingFontStyles.Bold),
        };
        if (Enum.TryParse<UnitSystem>(descriptor.UnitFamily, ignoreCase: true, out var uf))
        {
            layer.UnitFamily = uf;
        }
        if (!string.IsNullOrEmpty(descriptor.AlternateBarBrushColor))
        {
            layer.AlternateBarBrush = new GeoSolidBrush(ColorMapper.FromHexString(descriptor.AlternateBarBrushColor!));
        }
        return layer;
    }

    private static LegendAdornmentLayer BuildLegend(LegendAdornmentLayerDescriptor descriptor)
    {
        var layer = new LegendAdornmentLayer();
        if (descriptor.Title != null) layer.Title = BuildLegendItem(descriptor.Title);
        if (descriptor.Footer != null) layer.Footer = BuildLegendItem(descriptor.Footer);
        foreach (var itemDescriptor in descriptor.LegendItems)
        {
            layer.LegendItems.Add(BuildLegendItem(itemDescriptor));
        }
        if (Enum.TryParse<AdornmentResizeMode>(descriptor.ResizeMode, ignoreCase: true, out var arm))
        {
            layer.ResizeMode = arm;
        }
        return layer;
    }

    private static LegendItem BuildLegendItem(LegendItemDescriptor descriptor)
    {
        var item = new LegendItem
        {
            Width = descriptor.Width,
            Height = descriptor.Height,
            ImageWidth = descriptor.ImageWidth,
            ImageHeight = descriptor.ImageHeight,
        };
        if (descriptor.ImageStyle != null) item.ImageStyle = StyleMapper.FromDescriptor(descriptor.ImageStyle);
        if (descriptor.TextStyle != null && StyleMapper.FromDescriptor(descriptor.TextStyle) is TextStyle ts) item.TextStyle = ts;
        return item;
    }

    private static MagneticDeclinationAdornmentLayer BuildMagneticDeclination(MagneticDeclinationAdornmentLayerDescriptor descriptor)
    {
        // MagneticFieldPathFilename is read-only and must be passed via the (path, location) ctor.
        // Use AdornmentLocation.LowerRight as the seed; the caller overrides Location later.
        var layer = string.IsNullOrEmpty(descriptor.MagneticFieldPathFilename)
            ? new MagneticDeclinationAdornmentLayer()
            : new MagneticDeclinationAdornmentLayer(descriptor.MagneticFieldPathFilename!, AdornmentLocation.LowerRight);

        layer.Elevation = descriptor.Elevation;
        if (Enum.TryParse<DistanceUnit>(descriptor.ElevationUnit, ignoreCase: true, out var eu))
        {
            layer.ElevationUnit = eu;
        }
        layer.SampleDateTime = ClampSampleDateTime(descriptor.SampleDateTime);
        return layer;
    }

    // MagneticDeclinationAdornmentLayer.SampleDateTime setter rejects anything outside
    // 1900..2015 (the magnetic-field data coverage window). Live samples — including the stock
    // HowDoI MagneticNorth — routinely carry DateTime.UtcNow, which is out of range. Clamp on
    // both capture and apply so (a) rebuild doesn't throw and (b) round-trip is a fixed point.
    private static DateTime ClampSampleDateTime(DateTime value)
    {
        var minValid = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var maxValid = new DateTime(2015, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        if (value < minValid) return minValid;
        if (value > maxValid) return maxValid;
        return value;
    }
}
