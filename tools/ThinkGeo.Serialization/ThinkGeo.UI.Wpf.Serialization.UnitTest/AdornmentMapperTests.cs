using System.Text.Json;
using System.Threading.Tasks;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.UI.Wpf.Serialization;
using ThinkGeo.UI.Wpf.Serialization.Dtos;
using ThinkGeo.UI.Wpf.Serialization.Mappers;
using Xunit;

namespace ThinkGeo.UI.Wpf.Serialization.UnitTest;

public class AdornmentMapperTests
{
    [Fact]
    public void LogoAdornmentLayer_RoundTrips_WithImagePath()
    {
        StaRunner.Run(async () =>
        {
            var original = new LogoAdornmentLayer
            {
                Name = "watermark",
                Location = AdornmentLocation.UpperLeft,
                Width = 120,
                Height = 40,
            };

            var dto = AdornmentMapper.ToDescriptor(original)!;
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<AdornmentLayerDescriptor>(json)!;
            var roundTripped = (LogoAdornmentLayer)AdornmentMapper.FromDescriptor(deserialized)!;

            Assert.Equal("watermark", roundTripped.Name);
            Assert.Equal(AdornmentLocation.UpperLeft, roundTripped.Location);
            Assert.Equal(120, roundTripped.Width);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void ScaleLineAdornmentLayer_RoundTrips_WithUnitSystem()
    {
        StaRunner.Run(async () =>
        {
            var original = new ScaleLineAdornmentLayer
            {
                Name = "scaleline",
                MaxBarLength = 200,
                Margin = 20,
                UnitSystem = ScaleLineUnitSystem.MetricAndNauticalMile,
                Location = AdornmentLocation.LowerLeft,
            };

            var dto = AdornmentMapper.ToDescriptor(original)!;
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<AdornmentLayerDescriptor>(json)!;
            var roundTripped = (ScaleLineAdornmentLayer)AdornmentMapper.FromDescriptor(deserialized)!;

            Assert.Equal("scaleline", roundTripped.Name);
            Assert.Equal(200, roundTripped.MaxBarLength);
            Assert.Equal(20, roundTripped.Margin);
            Assert.Equal(ScaleLineUnitSystem.MetricAndNauticalMile, roundTripped.UnitSystem);
            Assert.Equal(AdornmentLocation.LowerLeft, roundTripped.Location);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void ScaleTextAdornmentLayer_RoundTrips_WithScreenAndWorldUnit()
    {
        StaRunner.Run(async () =>
        {
            var original = new ScaleTextAdornmentLayer(
                ScaleTextScreenUnit.Inch,
                DistanceUnit.Kilometer,
                new GeoFont("Calibri", 14f, DrawingFontStyles.Italic),
                new GeoSolidBrush(GeoColors.DarkBlue))
            { Name = "scaletext" };

            var dto = AdornmentMapper.ToDescriptor(original)!;
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<AdornmentLayerDescriptor>(json)!;
            var roundTripped = (ScaleTextAdornmentLayer)AdornmentMapper.FromDescriptor(deserialized)!;

            Assert.Equal("scaletext", roundTripped.Name);
            Assert.Equal(ScaleTextScreenUnit.Inch, roundTripped.ScreenUnit);
            Assert.Equal(DistanceUnit.Kilometer, roundTripped.WorldUnit);
            Assert.Equal("Calibri", roundTripped.Font.FontName);
            Assert.Equal(14f, roundTripped.Font.Size);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void ScaleBarAdornmentLayer_RoundTrips_WithColorsAndUnitFamily()
    {
        StaRunner.Run(async () =>
        {
            var original = new ScaleBarAdornmentLayer
            {
                Name = "scalebar",
                UnitFamily = UnitSystem.Imperial,
                Thickness = 15,
                MaxWidth = 300,
                Margin = 20,
                BarBrush = new GeoSolidBrush(GeoColors.DarkRed),
                LabelBrush = new GeoSolidBrush(GeoColors.Black),
                AlternateBarBrush = new GeoSolidBrush(GeoColors.White),
                HasMask = true,
            };

            var dto = AdornmentMapper.ToDescriptor(original)!;
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<AdornmentLayerDescriptor>(json)!;
            var roundTripped = (ScaleBarAdornmentLayer)AdornmentMapper.FromDescriptor(deserialized)!;

            Assert.Equal("scalebar", roundTripped.Name);
            Assert.Equal(UnitSystem.Imperial, roundTripped.UnitFamily);
            Assert.Equal(15, roundTripped.Thickness);
            Assert.Equal(300, roundTripped.MaxWidth);
            Assert.Equal(GeoColors.DarkRed.R, ((GeoSolidBrush)roundTripped.BarBrush).Color.R);
            Assert.NotNull(roundTripped.AlternateBarBrush);
            Assert.True(roundTripped.HasMask);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void LegendAdornmentLayer_RoundTrips_WithTitleAndItems()
    {
        StaRunner.Run(async () =>
        {
            var original = new LegendAdornmentLayer
            {
                Name = "legend",
                Title = new LegendItem
                {
                    Width = 200,
                    Height = 30,
                    TextStyle = TextStyle.CreateSimpleTextStyle("", "Arial", 14f,
                        DrawingFontStyles.Bold, GeoColors.Black),
                },
            };
            original.LegendItems.Add(new LegendItem(140, 20, 20, 20,
                new AreaStyle { FillBrush = new GeoSolidBrush(GeoColors.Red) },
                TextStyle.CreateSimpleTextStyle("", "Arial", 10f, DrawingFontStyles.Regular, GeoColors.Black)));
            original.LegendItems.Add(new LegendItem(140, 20, 20, 20,
                new AreaStyle { FillBrush = new GeoSolidBrush(GeoColors.Green) },
                TextStyle.CreateSimpleTextStyle("", "Arial", 10f, DrawingFontStyles.Regular, GeoColors.Black)));

            var dto = AdornmentMapper.ToDescriptor(original)!;
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<AdornmentLayerDescriptor>(json)!;
            var roundTripped = (LegendAdornmentLayer)AdornmentMapper.FromDescriptor(deserialized)!;

            Assert.Equal("legend", roundTripped.Name);
            Assert.NotNull(roundTripped.Title);
            Assert.Equal(200, roundTripped.Title!.Width);
            Assert.Equal(2, roundTripped.LegendItems.Count);
            Assert.Equal(140, roundTripped.LegendItems[0].Width);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void MagneticDeclinationAdornmentLayer_RoundTrips_WithElevation()
    {
        StaRunner.Run(async () =>
        {
            var original = new MagneticDeclinationAdornmentLayer
            {
                Name = "mag-decl",
                Elevation = 500,
                ElevationUnit = DistanceUnit.Meter,
                SampleDateTime = new System.DateTime(2014, 6, 15, 12, 0, 0, System.DateTimeKind.Utc),
            };

            var dto = AdornmentMapper.ToDescriptor(original)!;
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<AdornmentLayerDescriptor>(json)!;
            var roundTripped = (MagneticDeclinationAdornmentLayer)AdornmentMapper.FromDescriptor(deserialized)!;

            Assert.Equal("mag-decl", roundTripped.Name);
            Assert.Equal(500, roundTripped.Elevation);
            Assert.Equal(DistanceUnit.Meter, roundTripped.ElevationUnit);
            Assert.Equal(original.SampleDateTime, roundTripped.SampleDateTime);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void AdornmentOverlay_FullRoundTrip_ViaMapSerializer()
    {
        StaRunner.Run(async () =>
        {
            var source = new MapView
            {
                MapUnit = GeographyUnit.Meter,
                CenterPoint = new PointShape(0, 0),
                CurrentScale = 1_000_000,
            };
            source.AdornmentOverlay.Layers.Add(new LogoAdornmentLayer
                { Location = AdornmentLocation.UpperLeft, Width = 100, Height = 30 });
            source.AdornmentOverlay.Layers.Add(new ScaleLineAdornmentLayer
                { Location = AdornmentLocation.LowerLeft, UnitSystem = ScaleLineUnitSystem.ImperialAndMetric });

            var json = MapSerializer.Serialize(source);
            var target = new MapView();
            MapSerializer.Deserialize(target, json);

            Assert.Equal(2, target.AdornmentOverlay.Layers.Count);
            Assert.IsType<LogoAdornmentLayer>(target.AdornmentOverlay.Layers[0]);
            Assert.IsType<ScaleLineAdornmentLayer>(target.AdornmentOverlay.Layers[1]);
            Assert.Equal(AdornmentLocation.UpperLeft, target.AdornmentOverlay.Layers[0].Location);
            await Task.CompletedTask;
        });
    }
}
