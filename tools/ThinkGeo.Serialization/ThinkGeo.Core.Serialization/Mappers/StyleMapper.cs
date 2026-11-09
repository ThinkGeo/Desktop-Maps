using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Dtos;

namespace ThinkGeo.Core.Serialization.Mappers;

/// <summary>
/// Maps between <see cref="Style"/> instances and <see cref="StyleDescriptor"/> DTOs.
/// Implemented: AreaStyle, LineStyle, PointStyle, TextStyle, CompositeStyle, ClassBreakStyle,
/// ValueStyle, RegexStyle, FilterStyle, DotDensityStyle.
/// Unknown style types throw <see cref="NotSupportedException"/>.
/// </summary>
public static class StyleMapper
{
    /// <summary>Produces a descriptor for a known style type; returns <c>null</c> for unknown types.</summary>
    public static StyleDescriptor? ToDescriptor(Style style)
    {
        if (style == null) throw new ArgumentNullException(nameof(style));

        StyleDescriptor? descriptor = style switch
        {
            AreaStyle a => new AreaStyleDescriptor
            {
                FillBrush = a.FillBrush is GeoBrush fb ? BrushMapper.ToDescriptor(fb) : null,
                OutlinePen = a.OutlinePen is GeoPen op ? PenMapper.ToDescriptor(op) : null,
            },
            LineStyle l => new LineStyleDescriptor
            {
                OuterPen = l.OuterPen is GeoPen op ? PenMapper.ToDescriptor(op) : null,
                InnerPen = l.InnerPen is GeoPen ip ? PenMapper.ToDescriptor(ip) : null,
                CenterPen = l.CenterPen is GeoPen cp ? PenMapper.ToDescriptor(cp) : null,
            },
            PointStyle p => new PointStyleDescriptor
            {
                Symbol = p.SymbolType.ToString(),
                SymbolSize = p.SymbolSize,
                FillBrush = p.FillBrush is GeoBrush fb ? BrushMapper.ToDescriptor(fb) : null,
                OutlinePen = p.OutlinePen is GeoPen op ? PenMapper.ToDescriptor(op) : null,
                IconPath = p.Image?.PathFilename,
            },
            IconStyle ic => new IconStyleDescriptor
            {
                IconPath = ic.IconPathFilename ?? string.Empty,
                IconImageScale = ic.IconImageScale,
                TextColumnName = ic.TextColumnName ?? string.Empty,
                FontFamily = ic.Font?.FontName ?? "Arial",
                FontSize = ic.Font?.Size ?? 10f,
                FontStyle = ic.Font?.Style.ToString() ?? "Regular",
                TextColor = ColorMapper.ToHexString((ic.TextBrush as GeoSolidBrush)?.Color ?? GeoColors.Black),
                HaloColor = ic.HaloPen?.Color is GeoColor hc2 ? ColorMapper.ToHexString(hc2) : null,
                HaloPenWidth = ic.HaloPen?.Width ?? 0f,
            },
            HeatStyle hs => new HeatStyleDescriptor
            {
                PointIntensity = hs.PointIntensity,
                Alpha = hs.Alpha,
                IntensityColumnName = hs.IntensityColumnName ?? string.Empty,
                IntensityRangeStart = hs.IntensityRangeStart,
                IntensityRangeEnd = hs.IntensityRangeEnd,
                PointRadius = hs.PointRadius,
                PointRadiusUnit = hs.PointRadiusUnit.ToString(),
                ColorPalette = hs.ColorPalette.Select(ColorMapper.ToHexString).ToList(),
            },
            TextStyle t => new TextStyleDescriptor
            {
                TextColumnName = t.TextColumnName ?? string.Empty,
                FontFamily = t.Font?.FontName ?? "Arial",
                FontSize = t.Font?.Size ?? 10f,
                FontStyle = t.Font?.Style.ToString() ?? "Regular",
                TextColor = ColorMapper.ToHexString((t.TextBrush as GeoSolidBrush)?.Color ?? GeoColors.Black),
                HaloColor = t.HaloPen?.Color is GeoColor hc ? ColorMapper.ToHexString(hc) : null,
                HaloPenWidth = t.HaloPen?.Width ?? 0f,
            },
            CompositeStyle c => new CompositeStyleDescriptor
            {
                Styles = c.Styles.Select(ToDescriptor).ToList(),
            },
            ClassBreakStyle cb => new ClassBreakStyleDescriptor
            {
                ColumnName = cb.ColumnName ?? string.Empty,
                BreakValueInclusion = cb.BreakValueInclusion.ToString(),
                Breaks = cb.ClassBreaks.Select(CaptureClassBreak).ToList(),
            },
            ValueStyle vs => new ValueStyleDescriptor
            {
                ColumnName = vs.ColumnName ?? string.Empty,
                ValueItems = vs.ValueItems.Select(CaptureValueItem).ToList(),
                DefaultStyle = vs.DefaultStyle is Style ds ? ToDescriptor(ds) : null,
            },
            RegexStyle rx => new RegexStyleDescriptor
            {
                ColumnName = rx.ColumnName ?? string.Empty,
                RegexMatchingRule = rx.RegexMatchingRule.ToString(),
                Items = rx.RegexItems.Select(CaptureRegexItem).ToList(),
            },
            FilterStyle fs => new FilterStyleDescriptor
            {
                Styles = fs.Styles.Select(TryToDescriptor).Where(d => d != null).ToList()!,
                Conditions = fs.Conditions.Select(CaptureFilterCondition).ToList(),
            },
            DotDensityStyle dd => new DotDensityStyleDescriptor
            {
                ColumnName = dd.ColumnName ?? string.Empty,
                PointToValueRatio = dd.PointToValueRatio,
                PointSize = dd.PointSize,
                PointColor = ColorMapper.ToHexString(dd.PointColor),
                CustomPointStyle = dd.CustomPointStyle is PointStyle cps ? (PointStyleDescriptor)ToDescriptor(cps) : null,
            },
            _ => null,
        };

        if (descriptor == null) return null;

        return descriptor with { Name = style.Name ?? string.Empty, IsActive = style.IsActive };
    }

    /// <summary>Alias for <see cref="ToDescriptor"/> retained for API symmetry; both now return null on unknown types.</summary>
    public static StyleDescriptor? TryToDescriptor(Style style) => ToDescriptor(style);

    public static Style FromDescriptor(StyleDescriptor descriptor)
    {
        if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));

        Style style = descriptor switch
        {
            AreaStyleDescriptor a => new AreaStyle
            {
                FillBrush = a.FillBrush != null ? BrushMapper.FromDescriptor(a.FillBrush) : new GeoSolidBrush(GeoColors.Transparent),
                OutlinePen = a.OutlinePen != null ? PenMapper.FromDescriptor(a.OutlinePen) : new GeoPen(GeoColors.Black, 1f),
            },
            LineStyleDescriptor l => new LineStyle
            {
                OuterPen = l.OuterPen != null ? PenMapper.FromDescriptor(l.OuterPen) : new GeoPen(GeoColors.Transparent, 0f),
                InnerPen = l.InnerPen != null ? PenMapper.FromDescriptor(l.InnerPen) : new GeoPen(GeoColors.Transparent, 0f),
                CenterPen = l.CenterPen != null ? PenMapper.FromDescriptor(l.CenterPen) : new GeoPen(GeoColors.Transparent, 0f),
            },
            PointStyleDescriptor p => BuildPointStyle(p),
            IconStyleDescriptor ic => BuildIconStyle(ic),
            HeatStyleDescriptor hs => BuildHeatStyle(hs),
            TextStyleDescriptor t => new TextStyle
            {
                TextColumnName = t.TextColumnName,
                Font = new GeoFont(t.FontFamily, t.FontSize,
                    Enum.TryParse<DrawingFontStyles>(t.FontStyle, ignoreCase: true, out var fs) ? fs : DrawingFontStyles.Regular),
                TextBrush = new GeoSolidBrush(ColorMapper.FromHexString(t.TextColor)),
                HaloPen = !string.IsNullOrEmpty(t.HaloColor)
                    ? new GeoPen(ColorMapper.FromHexString(t.HaloColor!), t.HaloPenWidth)
                    : new GeoPen(GeoColors.Transparent, 0f),
            },
            CompositeStyleDescriptor c => BuildCompositeStyle(c),
            ClassBreakStyleDescriptor cb => BuildClassBreakStyle(cb),
            ValueStyleDescriptor vs => BuildValueStyle(vs),
            RegexStyleDescriptor rx => BuildRegexStyle(rx),
            FilterStyleDescriptor fs => BuildFilterStyle(fs),
            DotDensityStyleDescriptor dd => BuildDotDensityStyle(dd),
            _ => throw new NotSupportedException(
                $"Style descriptor '{descriptor.GetType().FullName}' is not supported yet."),
        };

        style.Name = descriptor.Name;
        style.IsActive = descriptor.IsActive;
        return style;
    }

    private static ClassBreakEntryDescriptor CaptureClassBreak(ClassBreak classBreak)
    {
        var styles = new List<StyleDescriptor>();
        if (classBreak.DefaultAreaStyle is AreaStyle a) styles.Add(ToDescriptor(a));
        if (classBreak.DefaultLineStyle is LineStyle l) styles.Add(ToDescriptor(l));
        if (classBreak.DefaultPointStyle is PointStyle p) styles.Add(ToDescriptor(p));
        if (classBreak.DefaultTextStyle is TextStyle t) styles.Add(ToDescriptor(t));
        foreach (var custom in classBreak.CustomStyles)
        {
            var descriptor = TryToDescriptor(custom);
            if (descriptor != null) styles.Add(descriptor);
        }
        return new ClassBreakEntryDescriptor(classBreak.Value, styles);
    }

    private static ValueStyleEntryDescriptor CaptureValueItem(ValueItem valueItem)
    {
        var styles = new List<StyleDescriptor>();
        if (valueItem.DefaultAreaStyle is AreaStyle a) styles.Add(ToDescriptor(a));
        if (valueItem.DefaultLineStyle is LineStyle l) styles.Add(ToDescriptor(l));
        if (valueItem.DefaultPointStyle is PointStyle p) styles.Add(ToDescriptor(p));
        if (valueItem.DefaultTextStyle is TextStyle t) styles.Add(ToDescriptor(t));
        foreach (var custom in valueItem.CustomStyles)
        {
            var descriptor = TryToDescriptor(custom);
            if (descriptor != null) styles.Add(descriptor);
        }
        return new ValueStyleEntryDescriptor(valueItem.Value ?? string.Empty, styles);
    }

    private static CompositeStyle BuildCompositeStyle(CompositeStyleDescriptor descriptor)
    {
        var composite = new CompositeStyle();
        foreach (var childDescriptor in descriptor.Styles)
        {
            composite.Styles.Add(FromDescriptor(childDescriptor));
        }
        return composite;
    }

    private static RegexStyleEntryDescriptor CaptureRegexItem(RegexItem item)
    {
        var styles = new List<StyleDescriptor>();
        if (item.DefaultAreaStyle is AreaStyle a) styles.Add(ToDescriptor(a));
        if (item.DefaultLineStyle is LineStyle l) styles.Add(ToDescriptor(l));
        if (item.DefaultPointStyle is PointStyle p) styles.Add(ToDescriptor(p));
        if (item.DefaultTextStyle is TextStyle t) styles.Add(ToDescriptor(t));
        foreach (var custom in item.CustomStyles)
        {
            var descriptor = TryToDescriptor(custom);
            if (descriptor != null) styles.Add(descriptor);
        }
        return new RegexStyleEntryDescriptor(item.RegularExpression ?? string.Empty, styles);
    }

    private static FilterConditionDescriptor CaptureFilterCondition(FilterCondition condition)
    {
        return new FilterConditionDescriptor
        {
            Name = condition.Name ?? string.Empty,
            ColumnName = condition.ColumnName ?? string.Empty,
            Expression = condition.Expression ?? string.Empty,
            Logical = condition.Logical,
            IsLeftBracket = condition.IsLeftBracket,
            IsRightBracket = condition.IsRightBracket,
            RegexOptions = condition.RegexOptions.ToString(),
        };
    }

    private static RegexStyle BuildRegexStyle(RegexStyleDescriptor descriptor)
    {
        var style = new RegexStyle
        {
            ColumnName = descriptor.ColumnName,
            RegexMatchingRule = Enum.TryParse<RegexMatching>(descriptor.RegexMatchingRule, ignoreCase: true, out var rm)
                ? rm
                : RegexMatching.MatchFirstOnly,
        };
        foreach (var entry in descriptor.Items)
        {
            var item = new RegexItem { RegularExpression = entry.RegularExpression };
            AssignStylesToRegexItem(item, entry.Styles);
            style.RegexItems.Add(item);
        }
        return style;
    }

    private static FilterStyle BuildFilterStyle(FilterStyleDescriptor descriptor)
    {
        var style = new FilterStyle();
        foreach (var childDescriptor in descriptor.Styles)
        {
            style.Styles.Add(FromDescriptor(childDescriptor));
        }
        foreach (var c in descriptor.Conditions)
        {
            var condition = new FilterCondition(c.ColumnName, c.Expression)
            {
                Name = c.Name,
                Logical = c.Logical,
                IsLeftBracket = c.IsLeftBracket,
                IsRightBracket = c.IsRightBracket,
            };
            if (Enum.TryParse<System.Text.RegularExpressions.RegexOptions>(c.RegexOptions, ignoreCase: true, out var ro))
            {
                condition.RegexOptions = ro;
            }
            style.Conditions.Add(condition);
        }
        return style;
    }

    private static DotDensityStyle BuildDotDensityStyle(DotDensityStyleDescriptor descriptor)
    {
        if (descriptor.CustomPointStyle != null)
        {
            var custom = (PointStyle)FromDescriptor(descriptor.CustomPointStyle);
            return new DotDensityStyle(descriptor.ColumnName, descriptor.PointToValueRatio, custom);
        }
        return new DotDensityStyle(
            descriptor.ColumnName,
            descriptor.PointToValueRatio,
            descriptor.PointSize,
            ColorMapper.FromHexString(descriptor.PointColor));
    }

    private static void AssignStylesToRegexItem(RegexItem regexItem, IEnumerable<StyleDescriptor> styles)
    {
        bool areaAssigned = false, lineAssigned = false, pointAssigned = false, textAssigned = false;
        foreach (var descriptor in styles)
        {
            var style = FromDescriptor(descriptor);
            switch (style)
            {
                case AreaStyle a when !areaAssigned: regexItem.DefaultAreaStyle = a; areaAssigned = true; break;
                case LineStyle l when !lineAssigned: regexItem.DefaultLineStyle = l; lineAssigned = true; break;
                case PointStyle p when !pointAssigned: regexItem.DefaultPointStyle = p; pointAssigned = true; break;
                case TextStyle t when !textAssigned: regexItem.DefaultTextStyle = t; textAssigned = true; break;
                default: regexItem.CustomStyles.Add(style); break;
            }
        }
    }

    private static ClassBreakStyle BuildClassBreakStyle(ClassBreakStyleDescriptor descriptor)
    {
        var inclusion = Enum.TryParse<BreakValueInclusion>(descriptor.BreakValueInclusion, ignoreCase: true, out var bvi)
            ? bvi
            : BreakValueInclusion.IncludeValue;

        var style = new ClassBreakStyle(descriptor.ColumnName, inclusion);
        foreach (var breakDescriptor in descriptor.Breaks)
        {
            var classBreak = new ClassBreak { Value = breakDescriptor.Value };
            AssignStylesToClassBreak(classBreak, breakDescriptor.Styles);
            style.ClassBreaks.Add(classBreak);
        }
        return style;
    }

    private static ValueStyle BuildValueStyle(ValueStyleDescriptor descriptor)
    {
        var style = new ValueStyle { ColumnName = descriptor.ColumnName };
        if (descriptor.DefaultStyle != null)
        {
            style.DefaultStyle = FromDescriptor(descriptor.DefaultStyle);
        }
        foreach (var entry in descriptor.ValueItems)
        {
            var item = new ValueItem { Value = entry.Value };
            AssignStylesToValueItem(item, entry.Styles);
            style.ValueItems.Add(item);
        }
        return style;
    }

    // ClassBreak / ValueItem parameterless ctors auto-populate DefaultAreaStyle, DefaultLineStyle,
    // DefaultPointStyle and DefaultTextStyle with empty-but-non-null placeholders. We track which
    // slots the descriptor explicitly populated and route the second+ occurrences of the same type
    // into CustomStyles so callers get predictable round-trip behaviour.

    private static void AssignStylesToClassBreak(ClassBreak classBreak, IEnumerable<StyleDescriptor> styles)
    {
        bool areaAssigned = false, lineAssigned = false, pointAssigned = false, textAssigned = false;
        foreach (var descriptor in styles)
        {
            var style = FromDescriptor(descriptor);
            switch (style)
            {
                case AreaStyle a when !areaAssigned: classBreak.DefaultAreaStyle = a; areaAssigned = true; break;
                case LineStyle l when !lineAssigned: classBreak.DefaultLineStyle = l; lineAssigned = true; break;
                case PointStyle p when !pointAssigned: classBreak.DefaultPointStyle = p; pointAssigned = true; break;
                case TextStyle t when !textAssigned: classBreak.DefaultTextStyle = t; textAssigned = true; break;
                default: classBreak.CustomStyles.Add(style); break;
            }
        }
    }

    private static void AssignStylesToValueItem(ValueItem valueItem, IEnumerable<StyleDescriptor> styles)
    {
        bool areaAssigned = false, lineAssigned = false, pointAssigned = false, textAssigned = false;
        foreach (var descriptor in styles)
        {
            var style = FromDescriptor(descriptor);
            switch (style)
            {
                case AreaStyle a when !areaAssigned: valueItem.DefaultAreaStyle = a; areaAssigned = true; break;
                case LineStyle l when !lineAssigned: valueItem.DefaultLineStyle = l; lineAssigned = true; break;
                case PointStyle p when !pointAssigned: valueItem.DefaultPointStyle = p; pointAssigned = true; break;
                case TextStyle t when !textAssigned: valueItem.DefaultTextStyle = t; textAssigned = true; break;
                default: valueItem.CustomStyles.Add(style); break;
            }
        }
    }

    private static IconStyle BuildIconStyle(IconStyleDescriptor descriptor)
    {
        var icon = new IconStyle
        {
            IconImageScale = descriptor.IconImageScale,
            TextColumnName = descriptor.TextColumnName,
            Font = new GeoFont(descriptor.FontFamily, descriptor.FontSize,
                Enum.TryParse<DrawingFontStyles>(descriptor.FontStyle, ignoreCase: true, out var fs) ? fs : DrawingFontStyles.Regular),
            TextBrush = new GeoSolidBrush(ColorMapper.FromHexString(descriptor.TextColor)),
            HaloPen = !string.IsNullOrEmpty(descriptor.HaloColor)
                ? new GeoPen(ColorMapper.FromHexString(descriptor.HaloColor!), descriptor.HaloPenWidth)
                : new GeoPen(GeoColors.Transparent, 0f),
        };
        if (!string.IsNullOrEmpty(descriptor.IconPath))
        {
            // IconPathFilename setter eagerly constructs a GeoImage — catch file-not-found so an
            // invalid path in a descriptor doesn't abort the whole snapshot load.
            try { icon.IconPathFilename = descriptor.IconPath; } catch { /* best-effort */ }
        }
        return icon;
    }

    private static HeatStyle BuildHeatStyle(HeatStyleDescriptor descriptor)
    {
        var unit = Enum.TryParse<DistanceUnit>(descriptor.PointRadiusUnit, ignoreCase: true, out var u)
            ? u
            : DistanceUnit.Meter;
        var heat = string.IsNullOrEmpty(descriptor.IntensityColumnName)
            ? new HeatStyle(descriptor.PointIntensity, descriptor.Alpha, descriptor.PointRadius, unit)
            : new HeatStyle(descriptor.Alpha, descriptor.IntensityColumnName,
                descriptor.IntensityRangeStart, descriptor.IntensityRangeEnd,
                descriptor.PointRadius, unit);
        if (descriptor.ColorPalette.Count > 0)
        {
            heat.ColorPalette.Clear();
            foreach (var hex in descriptor.ColorPalette)
            {
                heat.ColorPalette.Add(ColorMapper.FromHexString(hex));
            }
        }
        return heat;
    }

    private static PointStyle BuildPointStyle(PointStyleDescriptor descriptor)
    {
        var symbolType = Enum.TryParse<PointSymbolType>(descriptor.Symbol, ignoreCase: true, out var s)
            ? s
            : PointSymbolType.Circle;

        var pointStyle = new PointStyle
        {
            SymbolType = symbolType,
            SymbolSize = descriptor.SymbolSize,
            FillBrush = descriptor.FillBrush != null
                ? BrushMapper.FromDescriptor(descriptor.FillBrush)
                : new GeoSolidBrush(GeoColors.Transparent),
            OutlinePen = descriptor.OutlinePen != null
                ? PenMapper.FromDescriptor(descriptor.OutlinePen)
                : new GeoPen(GeoColors.Transparent, 0f),
        };

        if (!string.IsNullOrEmpty(descriptor.IconPath))
        {
            pointStyle.Image = new GeoImage(descriptor.IconPath);
        }

        return pointStyle;
    }
}
