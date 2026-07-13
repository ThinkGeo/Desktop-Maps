using System.Text.Json;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Dtos;
using ThinkGeo.Core.Serialization.Mappers;
using Xunit;

namespace ThinkGeo.UI.Wpf.Serialization.UnitTest;

public class PenMapperTests
{
    [Fact]
    public void SolidPen_RoundTrips_ViaDtoInMemory()
    {
        var original = new GeoPen(GeoColors.Red, 2.5f)
        {
            LineJoin = DrawingLineJoin.Round,
            StartCap = DrawingLineCap.Round,
            EndCap = DrawingLineCap.Flat,
        };

        var dto = PenMapper.ToDescriptor(original);
        var roundTripped = PenMapper.FromDescriptor(dto);

        Assert.Equal(original.Color.A, roundTripped.Color.A);
        Assert.Equal(original.Color.R, roundTripped.Color.R);
        Assert.Equal(original.Color.G, roundTripped.Color.G);
        Assert.Equal(original.Color.B, roundTripped.Color.B);
        Assert.Equal(original.Width, roundTripped.Width);
        Assert.Equal(original.LineJoin, roundTripped.LineJoin);
        Assert.Equal(original.StartCap, roundTripped.StartCap);
        Assert.Equal(original.EndCap, roundTripped.EndCap);
    }

    [Fact]
    public void Pen_RoundTrips_ThroughJson()
    {
        var original = new GeoPen(GeoColors.Green, 1.5f);

        var dto = PenMapper.ToDescriptor(original);
        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<GeoPenDescriptor>(json)!;
        var roundTripped = PenMapper.FromDescriptor(deserialized);

        Assert.Equal(original.Color.R, roundTripped.Color.R);
        Assert.Equal(original.Color.G, roundTripped.Color.G);
        Assert.Equal(original.Color.B, roundTripped.Color.B);
        Assert.Equal(original.Width, roundTripped.Width);
    }

    [Fact]
    public void DashedPen_PreservesDashPattern()
    {
        var original = new GeoPen(GeoColors.Blue, 1f);
        original.DashPattern.Add(4f);
        original.DashPattern.Add(2f);
        original.DashPattern.Add(1f);

        var dto = PenMapper.ToDescriptor(original);
        var roundTripped = PenMapper.FromDescriptor(dto);

        Assert.Equal(original.DashPattern.Count, roundTripped.DashPattern.Count);
        for (int i = 0; i < original.DashPattern.Count; i++)
        {
            Assert.Equal(original.DashPattern[i], roundTripped.DashPattern[i]);
        }
    }
}
