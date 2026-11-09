using System;
using System.Linq;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Dtos;

namespace ThinkGeo.Core.Serialization.Mappers;

/// <summary>
/// Maps between <see cref="ZoomLevel"/> / <see cref="ZoomLevelSet"/> and their descriptors.
/// </summary>
public static class ZoomLevelMapper
{
    public static ZoomLevelDescriptor ToDescriptor(ZoomLevel zoomLevel)
    {
        if (zoomLevel == null) throw new ArgumentNullException(nameof(zoomLevel));

        return new ZoomLevelDescriptor
        {
            Name = zoomLevel.Name ?? string.Empty,
            Scale = zoomLevel.Scale,
            ApplyUntilZoomLevel = zoomLevel.ApplyUntilZoomLevel.ToString(),
            DefaultAreaStyle = zoomLevel.DefaultAreaStyle is AreaStyle a ? StyleMapper.ToDescriptor(a) : null,
            DefaultLineStyle = zoomLevel.DefaultLineStyle is LineStyle l ? StyleMapper.ToDescriptor(l) : null,
            DefaultPointStyle = zoomLevel.DefaultPointStyle is PointStyle p ? StyleMapper.ToDescriptor(p) : null,
            DefaultTextStyle = zoomLevel.DefaultTextStyle is TextStyle t ? StyleMapper.ToDescriptor(t) : null,
            CustomStyles = zoomLevel.CustomStyles
                .Select(StyleMapper.TryToDescriptor)
                .Where(d => d != null)
                .ToList()!,
        };
    }

    public static ZoomLevel FromDescriptor(ZoomLevelDescriptor descriptor)
    {
        if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));

        var zoomLevel = new ZoomLevel(descriptor.Scale)
        {
            Name = descriptor.Name,
            ApplyUntilZoomLevel = Enum.TryParse<ApplyUntilZoomLevel>(descriptor.ApplyUntilZoomLevel, out var aul)
                ? aul
                : ApplyUntilZoomLevel.Level20,
        };

        if (descriptor.DefaultAreaStyle != null)
            zoomLevel.DefaultAreaStyle = StyleMapper.FromDescriptor(descriptor.DefaultAreaStyle) as AreaStyle;
        if (descriptor.DefaultLineStyle != null)
            zoomLevel.DefaultLineStyle = StyleMapper.FromDescriptor(descriptor.DefaultLineStyle) as LineStyle;
        if (descriptor.DefaultPointStyle != null)
            zoomLevel.DefaultPointStyle = StyleMapper.FromDescriptor(descriptor.DefaultPointStyle) as PointStyle;
        if (descriptor.DefaultTextStyle != null)
            zoomLevel.DefaultTextStyle = StyleMapper.FromDescriptor(descriptor.DefaultTextStyle) as TextStyle;

        foreach (var style in descriptor.CustomStyles)
        {
            zoomLevel.CustomStyles.Add(StyleMapper.FromDescriptor(style));
        }

        return zoomLevel;
    }

    public static ZoomLevelSetDescriptor ToDescriptor(ZoomLevelSet zoomLevelSet)
    {
        if (zoomLevelSet == null) throw new ArgumentNullException(nameof(zoomLevelSet));

        // ZoomLevelSet exposes its 20 built-in zoom levels as properties (ZoomLevel01..ZoomLevel20)
        // plus CustomZoomLevels. We just snapshot the full list via .GetZoomLevels() which callers
        // can override but the built-in lookup covers standard cases.
        return new ZoomLevelSetDescriptor
        {
            ZoomLevels = zoomLevelSet.GetZoomLevels().Select(ToDescriptor).ToList(),
        };
    }

    public static ZoomLevelSet FromDescriptor(ZoomLevelSetDescriptor descriptor)
    {
        if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));

        // Callers typically don't round-trip full ZoomLevelSets via this path; we expose the API
        // for completeness but filling the built-in 20 slots one-by-one requires index matching
        // which is awkward. For now, return a fresh ZoomLevelSet with CustomZoomLevels populated
        // from everything beyond the first 20.
        var zoomLevelSet = new ZoomLevelSet();
        foreach (var levelDescriptor in descriptor.ZoomLevels)
        {
            zoomLevelSet.CustomZoomLevels.Add(FromDescriptor(levelDescriptor));
        }
        return zoomLevelSet;
    }
}
