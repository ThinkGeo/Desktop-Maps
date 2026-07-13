using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Mappers;
using Xunit;

namespace ThinkGeo.UI.Wpf.Serialization.UnitTest;

public class ColorMapperTests
{
    [Theory]
    [InlineData(0xFF, 0x00, 0x80, 0xFF)]      // fully opaque, teal-ish
    [InlineData(0x80, 0xAA, 0xBB, 0xCC)]      // semi-transparent
    [InlineData(0x00, 0x00, 0x00, 0x00)]      // fully transparent
    [InlineData(0xFF, 0xFF, 0xFF, 0xFF)]      // opaque white
    public void Color_RoundTrips_ThroughArgbHex(byte a, byte r, byte g, byte b)
    {
        var original = new GeoColor(a, r, g, b);

        var hex = ColorMapper.ToHexString(original);
        var roundTripped = ColorMapper.FromHexString(hex);

        Assert.Equal(a, roundTripped.A);
        Assert.Equal(r, roundTripped.R);
        Assert.Equal(g, roundTripped.G);
        Assert.Equal(b, roundTripped.B);
    }

    [Fact]
    public void FromHexString_AcceptsSixDigitFormat_AssumingFullAlpha()
    {
        var color = ColorMapper.FromHexString("#0080FF");
        Assert.Equal(0xFF, color.A);
        Assert.Equal(0x00, color.R);
        Assert.Equal(0x80, color.G);
        Assert.Equal(0xFF, color.B);
    }

    [Fact]
    public void FromHexString_EmptyOrNull_ReturnsTransparent()
    {
        Assert.Equal(0, ColorMapper.FromHexString(null).A);
        Assert.Equal(0, ColorMapper.FromHexString("").A);
    }
}
