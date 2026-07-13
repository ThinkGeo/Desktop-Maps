using System.Text.Json;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Dtos;
using ThinkGeo.Core.Serialization.Mappers;
using Xunit;

namespace ThinkGeo.UI.Wpf.Serialization.UnitTest;

public class BrushMapperTests
{
    [Fact]
    public void SolidBrush_RoundTrips()
    {
        var original = new GeoSolidBrush(GeoColors.Red);

        var dto = BrushMapper.ToDescriptor(original);
        var roundTripped = (GeoSolidBrush)BrushMapper.FromDescriptor(dto);

        Assert.Equal(original.Color.R, roundTripped.Color.R);
        Assert.Equal(original.Color.G, roundTripped.Color.G);
        Assert.Equal(original.Color.B, roundTripped.Color.B);
        Assert.Equal(original.Color.A, roundTripped.Color.A);
    }

    [Fact]
    public void LinearGradientBrush_RoundTrips_ViaJson()
    {
        var original = new GeoLinearGradientBrush(GeoColors.Red, GeoColors.Blue, 45f);

        var dto = BrushMapper.ToDescriptor(original);
        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<GeoBrushDescriptor>(json)!;
        var roundTripped = (GeoLinearGradientBrush)BrushMapper.FromDescriptor(deserialized);

        Assert.Equal(original.StartColor.R, roundTripped.StartColor.R);
        Assert.Equal(original.EndColor.B, roundTripped.EndColor.B);
        Assert.Equal(original.DirectionAngle, roundTripped.DirectionAngle);
    }

    [Fact]
    public void HatchBrush_RoundTrips()
    {
        var original = new GeoHatchBrush(GeoHatchStyle.DiagonalCross, GeoColors.Red, GeoColors.White);

        var dto = BrushMapper.ToDescriptor(original);
        var roundTripped = (GeoHatchBrush)BrushMapper.FromDescriptor(dto);

        Assert.Equal(original.HatchStyle, roundTripped.HatchStyle);
        Assert.Equal(original.ForegroundColor.R, roundTripped.ForegroundColor.R);
        Assert.Equal(original.BackgroundColor.R, roundTripped.BackgroundColor.R);
    }
}
