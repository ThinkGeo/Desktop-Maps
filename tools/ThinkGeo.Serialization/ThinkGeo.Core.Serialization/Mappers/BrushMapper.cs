using System;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Dtos;

namespace ThinkGeo.Core.Serialization.Mappers;

/// <summary>
/// Maps between <see cref="GeoBrush"/> and its polymorphic descriptor hierarchy.
/// Supported: <see cref="GeoSolidBrush"/>, <see cref="GeoLinearGradientBrush"/>,
/// <see cref="GeoHatchBrush"/>, <see cref="GeoTextureBrush"/>.
/// </summary>
public static class BrushMapper
{
    public static GeoBrushDescriptor ToDescriptor(GeoBrush brush) => brush switch
    {
        GeoSolidBrush s => new GeoSolidBrushDescriptor(ColorMapper.ToHexString(s.Color)),
        GeoLinearGradientBrush lg => new GeoLinearGradientBrushDescriptor(
            ColorMapper.ToHexString(lg.StartColor),
            ColorMapper.ToHexString(lg.EndColor),
            lg.DirectionAngle),
        GeoHatchBrush hb => new GeoHatchBrushDescriptor(
            ColorMapper.ToHexString(hb.ForegroundColor),
            ColorMapper.ToHexString(hb.BackgroundColor),
            hb.HatchStyle.ToString()),
        GeoTextureBrush tb => new GeoTextureBrushDescriptor(tb.GeoImage?.PathFilename ?? string.Empty),
        _ => throw new NotSupportedException(
            $"Brush type '{brush.GetType().FullName}' is not supported by {nameof(BrushMapper)}."),
    };

    public static GeoBrush FromDescriptor(GeoBrushDescriptor descriptor) => descriptor switch
    {
        GeoSolidBrushDescriptor s => new GeoSolidBrush(ColorMapper.FromHexString(s.Color)),
        GeoLinearGradientBrushDescriptor lg => new GeoLinearGradientBrush(
            ColorMapper.FromHexString(lg.StartColor),
            ColorMapper.FromHexString(lg.EndColor),
            lg.DirectionAngle),
        GeoHatchBrushDescriptor hb => new GeoHatchBrush(
            Enum.TryParse<GeoHatchStyle>(hb.HatchStyle, ignoreCase: true, out var parsed)
                ? parsed
                : GeoHatchStyle.Horizontal,
            ColorMapper.FromHexString(hb.ForegroundColor),
            ColorMapper.FromHexString(hb.BackgroundColor)),
        GeoTextureBrushDescriptor tb => new GeoTextureBrush(
            string.IsNullOrEmpty(tb.ImagePath) ? new GeoImage() : new GeoImage(tb.ImagePath)),
        _ => throw new NotSupportedException(
            $"Brush descriptor '{descriptor.GetType().FullName}' is not supported."),
    };
}
