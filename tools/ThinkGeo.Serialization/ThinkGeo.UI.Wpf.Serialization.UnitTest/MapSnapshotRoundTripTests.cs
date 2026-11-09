using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.UI.Wpf.Serialization;
using ThinkGeo.UI.Wpf.Serialization.Dtos;
using ThinkGeo.UI.Wpf.Serialization.Mappers;
using Xunit;

namespace ThinkGeo.UI.Wpf.Serialization.UnitTest;

/// <summary>
/// End-to-end round-trip tests for <see cref="MapSerializer"/>. Each test:
///   1. builds a source MapView in a known state,
///   2. serializes it to JSON via <see cref="MapSerializer.ToJson"/>,
///   3. applies the JSON to a fresh MapView via <see cref="MapSerializer.ApplyJson"/>,
///   4. asserts the two are structurally equivalent.
/// </summary>
public class MapSnapshotRoundTripTests
{
    [Fact]
    public void EmptyMapView_RoundTrips_ViewStateOnly()
    {
        StaRunner.Run(async () =>
        {
            var source = new MapView
            {
                MapUnit = GeographyUnit.Meter,
                CenterPoint = new PointShape(-10_000_000, 5_000_000),
                CurrentScale = 125_000,
                RotationAngle = 15f,
            };

            var json = MapSerializer.Serialize(source);
            var target = new MapView();
            MapSerializer.Deserialize(target, json);

            Assert.Equal(source.MapUnit, target.MapUnit);
            Assert.Equal(source.CenterPoint.X, target.CenterPoint.X);
            Assert.Equal(source.CenterPoint.Y, target.CenterPoint.Y);
            Assert.Equal(source.CurrentScale, target.CurrentScale);
            Assert.Equal(source.RotationAngle, target.RotationAngle);

            await Task.CompletedTask;
        });
    }

    [Fact]
    public void MapViewWithInMemoryLayer_RoundTrips_FeaturesAndStyle()
    {
        StaRunner.Run(async () =>
        {
            var source = BuildSampleMapView();
            var json = MapSerializer.Serialize(source);

            var target = new MapView();
            MapSerializer.Deserialize(target, json);

            Assert.Single(target.Overlays);
            var targetOverlay = Assert.IsType<LayerOverlay>(target.Overlays[0]);
            var targetLayer = Assert.IsType<InMemoryFeatureLayer>(targetOverlay.Layers[0]);

            Assert.Equal("cities", targetLayer.Name);
            Assert.Equal(3, targetLayer.InternalFeatures.Count);
            Assert.Equal("Paris", targetLayer.InternalFeatures.First(f => f.Id == "paris").ColumnValues["name"]);

            await Task.CompletedTask;
        });
    }

    [Fact]
    public void MapView_RoundTripsViaFile_ProducesIdenticalSnapshot()
    {
        StaRunner.Run(async () =>
        {
            var source = BuildSampleMapView();
            var tempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(),
                $"snapshot-{System.Guid.NewGuid():N}.json");
            try
            {
                MapSerializer.Save(source, tempFile);
                var target = new MapView();
                MapSerializer.Load(target, tempFile);

                // Re-snapshotting target should produce the same JSON as the source snapshot.
                var sourceJson = MapSerializer.Serialize(source);
                var targetJson = MapSerializer.Serialize(target);
                Assert.Equal(sourceJson, targetJson);
            }
            finally
            {
                if (System.IO.File.Exists(tempFile)) System.IO.File.Delete(tempFile);
            }

            await Task.CompletedTask;
        });
    }

    [Fact]
    public void Load_WithUnsupportedSchemaVersion_InvokesMigrationHook()
    {
        StaRunner.Run(async () =>
        {
            var originalHook = MapSerializer.MigrationHook;
            try
            {
                var hookInvoked = false;
                MapSerializer.MigrationHook = snapshot =>
                {
                    hookInvoked = true;
                    return snapshot with { SchemaVersion = 1 };
                };

                var futureJson =
                    "{\"schemaVersion\":999,\"centerX\":0,\"centerY\":0,\"currentScale\":1," +
                    "\"rotationAngle\":0,\"mapUnit\":\"DecimalDegree\",\"overlays\":[]}";

                var mapView = new MapView();
                MapSerializer.Deserialize(mapView, futureJson);

                Assert.True(hookInvoked, "migration hook should have been called for schemaVersion != 1");
            }
            finally
            {
                MapSerializer.MigrationHook = originalHook;
            }

            await Task.CompletedTask;
        });
    }

    private static MapView BuildSampleMapView()
    {
        var columns = new[]
        {
            new FeatureSourceColumn("name", "Character", 50),
            new FeatureSourceColumn("pop", "Integer", 10),
        };
        var features = new[]
        {
            new Feature(new PointShape(2.35, 48.85),
                new Dictionary<string, string> { ["name"] = "Paris", ["pop"] = "2148271" })
                { Id = "paris" },
            new Feature(new PointShape(-0.13, 51.51),
                new Dictionary<string, string> { ["name"] = "London", ["pop"] = "8982000" })
                { Id = "london" },
            new Feature(new PointShape(139.69, 35.68),
                new Dictionary<string, string> { ["name"] = "Tokyo", ["pop"] = "13960000" })
                { Id = "tokyo" },
        };
        var layer = new InMemoryFeatureLayer(columns, features) { Name = "cities", IsVisible = true };

        var overlay = new LayerOverlay { Name = "cities-overlay" };
        overlay.Layers.Add(layer);

        var mapView = new MapView
        {
            MapUnit = GeographyUnit.DecimalDegree,
            CenterPoint = new PointShape(0, 0),
            CurrentScale = 100_000_000,
        };
        mapView.Overlays.Add(overlay);
        return mapView;
    }
}
