using System;
using System.Linq;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Dtos;

namespace ThinkGeo.Core.Serialization.Mappers;

/// <summary>
/// Maps between <see cref="GeoPen"/> and <see cref="GeoPenDescriptor"/>.
/// </summary>
public static class PenMapper
{
    public static GeoPenDescriptor ToDescriptor(GeoPen pen)
    {
        if (pen == null) throw new ArgumentNullException(nameof(pen));

        return new GeoPenDescriptor
        {
            Color = ColorMapper.ToHexString(pen.Color),
            Width = pen.Width,
            Brush = pen.Brush is GeoBrush brush and not GeoSolidBrush
                ? BrushMapper.ToDescriptor(brush)
                : null,
            DashPattern = pen.DashPattern?.ToArray(),
            LineJoin = pen.LineJoin.ToString(),
            StartCap = pen.StartCap.ToString(),
            EndCap = pen.EndCap.ToString(),
        };
    }

    public static GeoPen FromDescriptor(GeoPenDescriptor descriptor)
    {
        if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));

        var pen = descriptor.Brush != null
            ? new GeoPen(BrushMapper.FromDescriptor(descriptor.Brush), descriptor.Width)
            : new GeoPen(ColorMapper.FromHexString(descriptor.Color), descriptor.Width);

        if (descriptor.DashPattern is { Length: > 0 })
        {
            pen.DashPattern.Clear();
            foreach (var segment in descriptor.DashPattern)
            {
                pen.DashPattern.Add(segment);
            }
        }

        if (Enum.TryParse<DrawingLineJoin>(descriptor.LineJoin, out var lineJoin)) pen.LineJoin = lineJoin;
        if (Enum.TryParse<DrawingLineCap>(descriptor.StartCap, out var startCap)) pen.StartCap = startCap;
        if (Enum.TryParse<DrawingLineCap>(descriptor.EndCap, out var endCap)) pen.EndCap = endCap;

        return pen;
    }
}
