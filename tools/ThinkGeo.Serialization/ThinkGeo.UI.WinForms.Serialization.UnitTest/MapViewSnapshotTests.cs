using System.Threading.Tasks;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms.Serialization;
using Xunit;

namespace ThinkGeo.UI.WinForms.Serialization.UnitTest;

/// <summary>
/// Minimal sanity coverage for the WinForms snapshot package. Intentionally narrow: just enough
/// to confirm the MapView-level round-trip works, plus a representative overlay-through-MapView
/// path. The deep property-by-property checking is already done by the 78-test WPF unit suite,
/// and the mappers here are copy-paste of the WPF ones with namespace swaps only — any regression
/// there shows up in the WPF suite too.
///
/// Each test hosts the MapView inside an off-screen Form via <see cref="MapViewHost"/> because
/// <c>Overlays.Clear()</c> on an un-parented WinForms MapView dereferences unset fields.
/// </summary>
public class MapViewSnapshotTests
{
    [Fact]
    public void EmptyMapView_RoundTrips_ExtentAndScale()
    {
        StaRunner.Run(async () =>
        {
            using var src = new MapViewHost();
            src.Map.MapUnit = GeographyUnit.Meter;
            src.Map.CenterPoint = new PointShape(-10777420, 3911000);
            src.Map.CurrentScale = 49_300;
            src.Map.RotationAngle = -15;

            var json = MapSerializer.Serialize(src.Map);

            using var tgt = new MapViewHost();
            MapSerializer.Deserialize(tgt.Map, json);

            Assert.Equal(GeographyUnit.Meter, tgt.Map.MapUnit);
            Assert.Equal(-10777420, tgt.Map.CenterPoint.X);
            Assert.Equal(3911000, tgt.Map.CenterPoint.Y);
            Assert.Equal(49_300, tgt.Map.CurrentScale);
            Assert.Equal(-15f, tgt.Map.RotationAngle);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void MapView_WithOverlayAndAdornment_RoundTrips()
    {
        StaRunner.Run(async () =>
        {
            using var src = new MapViewHost();
            src.Map.MapUnit = GeographyUnit.Meter;
            src.Map.CenterPoint = new PointShape(0, 0);
            src.Map.CurrentScale = 1_000_000;
            src.Map.Overlays.Add("osm", new OpenStreetMapOverlay());

            var inMemory = new InMemoryFeatureLayer();
            inMemory.InternalFeatures.Add(new Feature("POINT(1 2)") { Id = "f1" });
            var layerOverlay = new LayerOverlay { Name = "features" };
            layerOverlay.Layers.Add(inMemory);
            src.Map.Overlays.Add(layerOverlay);

            src.Map.AdornmentOverlay.Layers.Add(new LogoAdornmentLayer
            {
                Location = AdornmentLocation.UpperLeft,
                Width = 120,
                Height = 40,
            });
            src.Map.AdornmentOverlay.Layers.Add(new ScaleLineAdornmentLayer
            {
                Location = AdornmentLocation.LowerLeft,
                UnitSystem = ScaleLineUnitSystem.ImperialAndMetric,
            });

            var json = MapSerializer.Serialize(src.Map);

            using var tgt = new MapViewHost();
            MapSerializer.Deserialize(tgt.Map, json);

            Assert.Equal(2, tgt.Map.Overlays.Count);
            Assert.IsType<OpenStreetMapOverlay>(tgt.Map.Overlays[0]);
            var rebuiltLayerOverlay = Assert.IsType<LayerOverlay>(tgt.Map.Overlays[1]);
            Assert.Equal("features", rebuiltLayerOverlay.Name);
            Assert.Single(rebuiltLayerOverlay.Layers);
            var rebuiltLayer = Assert.IsType<InMemoryFeatureLayer>(rebuiltLayerOverlay.Layers[0]);
            Assert.Single(rebuiltLayer.InternalFeatures);
            Assert.Equal("f1", rebuiltLayer.InternalFeatures[0].Id);

            Assert.Equal(2, tgt.Map.AdornmentOverlay.Layers.Count);
            Assert.IsType<LogoAdornmentLayer>(tgt.Map.AdornmentOverlay.Layers[0]);
            Assert.IsType<ScaleLineAdornmentLayer>(tgt.Map.AdornmentOverlay.Layers[1]);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void MapView_FixedPoint_SnapshotEqualsReSnapshot()
    {
        StaRunner.Run(async () =>
        {
            // Fixed-point: ToJson(mv) == ToJson(ApplyJson(fresh, ToJson(mv))). If any mapper
            // drops info asymmetrically the two JSON strings diverge. Same check the WPF
            // integration sweep uses against every HowDoI sample.
            using var src = new MapViewHost();
            src.Map.MapUnit = GeographyUnit.Meter;
            src.Map.CenterPoint = new PointShape(100, 200);
            src.Map.CurrentScale = 500_000;
            src.Map.Overlays.Add(new OpenStreetMapOverlay());
            src.Map.AdornmentOverlay.Layers.Add(new LogoAdornmentLayer
            {
                Location = AdornmentLocation.LowerRight,
            });

            var first = MapSerializer.Serialize(src.Map);

            using var tgt = new MapViewHost();
            MapSerializer.Deserialize(tgt.Map, first);
            var second = MapSerializer.Serialize(tgt.Map);

            Assert.Equal(first, second);
            await Task.CompletedTask;
        });
    }
}
