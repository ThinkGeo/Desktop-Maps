using System.Text.Json;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Dtos;
using ThinkGeo.Core.Serialization.Mappers;
using Xunit;

namespace ThinkGeo.UI.Wpf.Serialization.UnitTest;

public class StyleMapperTests
{
    [Fact]
    public void AreaStyle_RoundTrips_PreservingFillAndOutline()
    {
        var original = new AreaStyle
        {
            Name = "city-blocks",
            FillBrush = new GeoSolidBrush(GeoColors.LightGray),
            OutlinePen = new GeoPen(GeoColors.Black, 1.5f),
        };

        var dto = StyleMapper.ToDescriptor(original);
        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<StyleDescriptor>(json)!;
        var roundTripped = (AreaStyle)StyleMapper.FromDescriptor(deserialized);

        Assert.Equal("city-blocks", roundTripped.Name);
        Assert.Equal(((GeoSolidBrush)original.FillBrush).Color.R, ((GeoSolidBrush)roundTripped.FillBrush).Color.R);
        Assert.Equal(original.OutlinePen.Color.R, roundTripped.OutlinePen.Color.R);
        Assert.Equal(original.OutlinePen.Width, roundTripped.OutlinePen.Width);
    }

    [Fact]
    public void LineStyle_RoundTrips_PreservingAllThreePens()
    {
        var original = new LineStyle(
            outerPen: new GeoPen(GeoColors.Black, 4f),
            innerPen: new GeoPen(GeoColors.White, 2f),
            centerPen: new GeoPen(GeoColors.Red, 1f));

        var dto = StyleMapper.ToDescriptor(original);
        var roundTripped = (LineStyle)StyleMapper.FromDescriptor(dto);

        Assert.Equal(original.OuterPen.Width, roundTripped.OuterPen.Width);
        Assert.Equal(original.InnerPen.Width, roundTripped.InnerPen.Width);
        Assert.Equal(original.CenterPen.Width, roundTripped.CenterPen.Width);
        Assert.Equal(original.CenterPen.Color.R, roundTripped.CenterPen.Color.R);
    }

    [Fact]
    public void PointStyle_RoundTrips_PreservingSymbolAndSize()
    {
        var original = PointStyle.CreateSimpleCircleStyle(GeoColors.Red, 10f, GeoColors.Black, 1f);
        original.Name = "pois";

        var dto = StyleMapper.ToDescriptor(original);
        var roundTripped = (PointStyle)StyleMapper.FromDescriptor(dto);

        Assert.Equal("pois", roundTripped.Name);
        Assert.Equal(original.SymbolType, roundTripped.SymbolType);
        Assert.Equal(original.SymbolSize, roundTripped.SymbolSize);
        Assert.Equal(((GeoSolidBrush)original.FillBrush).Color.R, ((GeoSolidBrush)roundTripped.FillBrush).Color.R);
    }

    [Fact]
    public void TextStyle_RoundTrips_PreservingFontAndColumn()
    {
        var original = TextStyle.CreateSimpleTextStyle(
            "name", "Arial", 12f, DrawingFontStyles.Bold, GeoColors.Black, GeoColors.White, 2f);

        var dto = StyleMapper.ToDescriptor(original);
        var roundTripped = (TextStyle)StyleMapper.FromDescriptor(dto);

        Assert.Equal("name", roundTripped.TextColumnName);
        Assert.Equal("Arial", roundTripped.Font.FontName);
        Assert.Equal(12f, roundTripped.Font.Size);
        Assert.True(roundTripped.Font.IsBold);
        Assert.Equal(((GeoSolidBrush)original.TextBrush).Color.R, ((GeoSolidBrush)roundTripped.TextBrush).Color.R);
        Assert.Equal(original.HaloPen.Width, roundTripped.HaloPen.Width);
    }

    [Fact]
    public void CompositeStyle_RoundTrips_PreservingChildStylesInOrder()
    {
        var original = new CompositeStyle();
        original.Styles.Add(new AreaStyle { FillBrush = new GeoSolidBrush(GeoColors.Red) });
        original.Styles.Add(new LineStyle(new GeoPen(GeoColors.Black, 1f)));
        original.Styles.Add(PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 5f));

        var dto = StyleMapper.ToDescriptor(original);
        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<StyleDescriptor>(json)!;
        var roundTripped = (CompositeStyle)StyleMapper.FromDescriptor(deserialized);

        Assert.Equal(3, roundTripped.Styles.Count);
        Assert.IsType<AreaStyle>(roundTripped.Styles[0]);
        Assert.IsType<LineStyle>(roundTripped.Styles[1]);
        Assert.IsType<PointStyle>(roundTripped.Styles[2]);
    }

    [Fact]
    public void ClassBreakStyle_RoundTrips_WithThreeBreaksAndDefaultAreaStyles()
    {
        var original = new ClassBreakStyle("population", BreakValueInclusion.IncludeValue);
        original.ClassBreaks.Add(new ClassBreak(0, new AreaStyle { FillBrush = new GeoSolidBrush(GeoColors.LightGreen) }));
        original.ClassBreaks.Add(new ClassBreak(100_000, new AreaStyle { FillBrush = new GeoSolidBrush(GeoColors.Yellow) }));
        original.ClassBreaks.Add(new ClassBreak(1_000_000, new AreaStyle { FillBrush = new GeoSolidBrush(GeoColors.Red) }));

        var dto = StyleMapper.ToDescriptor(original);
        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<StyleDescriptor>(json)!;
        var roundTripped = (ClassBreakStyle)StyleMapper.FromDescriptor(deserialized);

        Assert.Equal("population", roundTripped.ColumnName);
        Assert.Equal(BreakValueInclusion.IncludeValue, roundTripped.BreakValueInclusion);
        Assert.Equal(3, roundTripped.ClassBreaks.Count);
        Assert.Equal(100_000, roundTripped.ClassBreaks[1].Value);
        var yellowFill = (GeoSolidBrush)roundTripped.ClassBreaks[1].DefaultAreaStyle.FillBrush;
        Assert.Equal(GeoColors.Yellow.R, yellowFill.Color.R);
        Assert.Equal(GeoColors.Yellow.G, yellowFill.Color.G);
    }

    [Fact]
    public void RegexStyle_RoundTrips_WithTwoItems()
    {
        var original = new RegexStyle { ColumnName = "type", RegexMatchingRule = RegexMatching.MatchFirstOnly };
        original.RegexItems.Add(new RegexItem("^highway$",
            new LineStyle(new GeoPen(GeoColors.Red, 3f))));
        original.RegexItems.Add(new RegexItem("^river|creek$",
            new LineStyle(new GeoPen(GeoColors.Blue, 2f))));

        var dto = StyleMapper.ToDescriptor(original);
        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<StyleDescriptor>(json)!;
        var roundTripped = (RegexStyle)StyleMapper.FromDescriptor(deserialized);

        Assert.Equal("type", roundTripped.ColumnName);
        Assert.Equal(RegexMatching.MatchFirstOnly, roundTripped.RegexMatchingRule);
        Assert.Equal(2, roundTripped.RegexItems.Count);
        Assert.Equal("^highway$", roundTripped.RegexItems[0].RegularExpression);
        Assert.Equal("^river|creek$", roundTripped.RegexItems[1].RegularExpression);
        Assert.Equal(3f, roundTripped.RegexItems[0].DefaultLineStyle.OuterPen.Width);
        Assert.Equal(2f, roundTripped.RegexItems[1].DefaultLineStyle.OuterPen.Width);
    }

    [Fact]
    public void FilterStyle_RoundTrips_WithMultipleConditions()
    {
        var original = new FilterStyle();
        original.Styles.Add(new AreaStyle { FillBrush = new GeoSolidBrush(GeoColors.Orange) });
        original.Conditions.Add(new FilterCondition("population", "> 1000000")
            { Name = "big-cities" });
        original.Conditions.Add(new FilterCondition("country", "= 'US'")
            { Name = "us-only", Logical = true });

        var dto = StyleMapper.ToDescriptor(original);
        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<StyleDescriptor>(json)!;
        var roundTripped = (FilterStyle)StyleMapper.FromDescriptor(deserialized);

        Assert.Single(roundTripped.Styles);
        Assert.IsType<AreaStyle>(roundTripped.Styles[0]);
        Assert.Equal(2, roundTripped.Conditions.Count);
        Assert.Equal("big-cities", roundTripped.Conditions[0].Name);
        Assert.Equal("population", roundTripped.Conditions[0].ColumnName);
        Assert.Equal("> 1000000", roundTripped.Conditions[0].Expression);
        Assert.Equal("us-only", roundTripped.Conditions[1].Name);
    }

    [Fact]
    public void DotDensityStyle_RoundTrips_WithSimpleAndCustomPointStyle()
    {
        var simple = new DotDensityStyle("population", 0.001, 4, GeoColors.Red);
        var simpleDto = StyleMapper.ToDescriptor(simple);
        var simpleRoundTripped = (DotDensityStyle)StyleMapper.FromDescriptor(simpleDto);
        Assert.Equal("population", simpleRoundTripped.ColumnName);
        Assert.Equal(0.001, simpleRoundTripped.PointToValueRatio);
        Assert.Equal(4, simpleRoundTripped.PointSize);
        Assert.Equal(GeoColors.Red.R, simpleRoundTripped.PointColor.R);

        var custom = new DotDensityStyle("population", 0.01,
            PointStyle.CreateSimpleCircleStyle(GeoColors.Green, 3f, GeoColors.Black, 1f));
        var customDto = StyleMapper.ToDescriptor(custom);
        var json = JsonSerializer.Serialize(customDto);
        var deserialized = JsonSerializer.Deserialize<StyleDescriptor>(json)!;
        var customRoundTripped = (DotDensityStyle)StyleMapper.FromDescriptor(deserialized);
        Assert.NotNull(customRoundTripped.CustomPointStyle);
        Assert.Equal(PointSymbolType.Circle, customRoundTripped.CustomPointStyle.SymbolType);
    }

    [Fact]
    public void IconStyle_RoundTrips_WithPathAndLabel()
    {
        var original = new IconStyle
        {
            Name = "poi",
            IconImageScale = 1.5,
            TextColumnName = "label",
            Font = new GeoFont("Segoe UI", 12f, DrawingFontStyles.Bold),
            TextBrush = new GeoSolidBrush(GeoColors.DarkRed),
            HaloPen = new GeoPen(GeoColors.White, 2f),
        };

        var dto = (IconStyleDescriptor)StyleMapper.ToDescriptor(original)!;
        var json = JsonSerializer.Serialize<StyleDescriptor>(dto);
        var deserialized = JsonSerializer.Deserialize<StyleDescriptor>(json)!;
        var roundTripped = (IconStyle)StyleMapper.FromDescriptor(deserialized);

        Assert.Equal("poi", roundTripped.Name);
        Assert.Equal(1.5, roundTripped.IconImageScale);
        Assert.Equal("label", roundTripped.TextColumnName);
        Assert.Equal("Segoe UI", roundTripped.Font.FontName);
        Assert.Equal(12f, roundTripped.Font.Size);
        Assert.Equal(DrawingFontStyles.Bold, roundTripped.Font.Style);
        Assert.Equal(GeoColors.DarkRed.R, ((GeoSolidBrush)roundTripped.TextBrush).Color.R);
        Assert.Equal(2f, roundTripped.HaloPen.Width);
    }

    [Fact]
    public void HeatStyle_RoundTrips_WithPaletteAndIntensityColumn()
    {
        var original = new HeatStyle(128, "density", 0.0, 100.0, 20.0, DistanceUnit.Kilometer);
        original.ColorPalette.Clear();
        original.ColorPalette.Add(GeoColors.Blue);
        original.ColorPalette.Add(GeoColors.Green);
        original.ColorPalette.Add(GeoColors.Yellow);
        original.ColorPalette.Add(GeoColors.Red);

        var dto = (HeatStyleDescriptor)StyleMapper.ToDescriptor(original)!;
        var json = JsonSerializer.Serialize<StyleDescriptor>(dto);
        var deserialized = JsonSerializer.Deserialize<StyleDescriptor>(json)!;
        var roundTripped = (HeatStyle)StyleMapper.FromDescriptor(deserialized);

        Assert.Equal(128, roundTripped.Alpha);
        Assert.Equal("density", roundTripped.IntensityColumnName);
        Assert.Equal(100.0, roundTripped.IntensityRangeEnd);
        Assert.Equal(20.0, roundTripped.PointRadius);
        Assert.Equal(DistanceUnit.Kilometer, roundTripped.PointRadiusUnit);
        Assert.Equal(4, roundTripped.ColorPalette.Count);
        Assert.Equal(GeoColors.Blue.R, roundTripped.ColorPalette[0].R);
        Assert.Equal(GeoColors.Red.R, roundTripped.ColorPalette[3].R);
    }

    [Fact]
    public void ValueStyle_RoundTrips_WithValueItemsAndDefaultStyle()
    {
        var original = new ValueStyle
        {
            ColumnName = "type",
            DefaultStyle = new AreaStyle { FillBrush = new GeoSolidBrush(GeoColors.Gray) },
        };
        original.ValueItems.Add(new ValueItem("highway", new LineStyle(new GeoPen(GeoColors.Red, 3f))));
        original.ValueItems.Add(new ValueItem("street", new LineStyle(new GeoPen(GeoColors.Yellow, 1.5f))));

        var dto = StyleMapper.ToDescriptor(original);
        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<StyleDescriptor>(json)!;
        var roundTripped = (ValueStyle)StyleMapper.FromDescriptor(deserialized);

        Assert.Equal("type", roundTripped.ColumnName);
        Assert.IsType<AreaStyle>(roundTripped.DefaultStyle);
        Assert.Equal(2, roundTripped.ValueItems.Count);
        Assert.Equal("highway", roundTripped.ValueItems[0].Value);
        Assert.Equal(3f, roundTripped.ValueItems[0].DefaultLineStyle.OuterPen.Width);
        Assert.Equal(1.5f, roundTripped.ValueItems[1].DefaultLineStyle.OuterPen.Width);
    }
}
