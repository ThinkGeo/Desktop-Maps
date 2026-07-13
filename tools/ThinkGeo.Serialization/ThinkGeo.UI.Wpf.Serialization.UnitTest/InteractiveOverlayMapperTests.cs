using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Dtos;
using ThinkGeo.Core.Serialization.Mappers;
using ThinkGeo.UI.Wpf;
using ThinkGeo.UI.Wpf.Serialization.Dtos;
using ThinkGeo.UI.Wpf.Serialization.Mappers;
using Xunit;

namespace ThinkGeo.UI.Wpf.Serialization.UnitTest;

public class InteractiveOverlayMapperTests
{
    [Fact]
    public void EditInteractiveOverlay_RoundTrips_WithEditShapesAndToggles()
    {
        StaRunner.Run(async () =>
        {
            var original = new EditInteractiveOverlay
            {
                Name = "edit",
                CanDrag = false,
                CanReshape = true,
                CanResize = false,
                CanRotate = true,
                CanAddVertex = false,
                CanRemoveVertex = true,
            };
            original.EditShapesLayer.InternalFeatures.Add(new Feature("POINT(10 20)") { Id = "f1" });
            original.EditShapesLayer.InternalFeatures.Add(new Feature("POLYGON((0 0,10 0,10 10,0 10,0 0))") { Id = "f2" });

            var dto = (EditInteractiveOverlayDescriptor)OverlayMapper.ToDescriptor(original)!;
            var json = JsonSerializer.Serialize<OverlayDescriptor>(dto);
            var deserialized = (EditInteractiveOverlayDescriptor)JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (EditInteractiveOverlay)OverlayMapper.FromDescriptor(deserialized)!;

            Assert.Equal("edit", roundTripped.Name);
            Assert.False(roundTripped.CanDrag);
            Assert.True(roundTripped.CanReshape);
            Assert.False(roundTripped.CanResize);
            Assert.True(roundTripped.CanRotate);
            Assert.False(roundTripped.CanAddVertex);
            Assert.True(roundTripped.CanRemoveVertex);
            Assert.Equal(2, roundTripped.EditShapesLayer.InternalFeatures.Count);
            Assert.Equal("f1", roundTripped.EditShapesLayer.InternalFeatures[0].Id);
            Assert.Equal("f2", roundTripped.EditShapesLayer.InternalFeatures[1].Id);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void TrackInteractiveOverlay_RoundTrips_WithTrackModeAndShapes()
    {
        StaRunner.Run(async () =>
        {
            var original = new TrackInteractiveOverlay
            {
                Name = "track",
                TrackMode = TrackMode.Polygon,
                VertexCountInQuarter = 8,
            };
            original.TrackShapeLayer.InternalFeatures.Add(
                new Feature("POLYGON((0 0,1 0,1 1,0 1,0 0))") { Id = "poly1" });
            // Sentinel in-progress feature — should be dropped on round-trip.
            original.TrackShapeLayer.InternalFeatures.Add(
                new Feature("POINT(5 5)") { Id = "InTrackingFeature" });

            var dto = (TrackInteractiveOverlayDescriptor)OverlayMapper.ToDescriptor(original)!;
            Assert.Single(dto.TrackShapes);
            Assert.Equal("poly1", dto.TrackShapes[0].Id);

            var json = JsonSerializer.Serialize<OverlayDescriptor>(dto);
            var deserialized = (TrackInteractiveOverlayDescriptor)JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (TrackInteractiveOverlay)OverlayMapper.FromDescriptor(deserialized)!;

            Assert.Equal(TrackMode.Polygon, roundTripped.TrackMode);
            Assert.Equal(8, roundTripped.VertexCountInQuarter);
            Assert.Single(roundTripped.TrackShapeLayer.InternalFeatures);
            Assert.Equal("poly1", roundTripped.TrackShapeLayer.InternalFeatures[0].Id);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void ExtentInteractiveOverlay_RoundTrips_WithModes()
    {
        StaRunner.Run(async () =>
        {
            var original = new ExtentInteractiveOverlay
            {
                Name = "extent",
                PanMode = MapPanMode.Disabled,
                MapZoomMode = MapZoomMode.Default,
                MouseWheelMode = MapMouseWheelMode.Disabled,
                DoubleLeftClickMode = MapDoubleClickMode.Disabled,
                DoubleRightClickMode = MapDoubleClickMode.Default,
                ZoomSnapDirection = ZoomSnapDirection.UpperScale,
                ZoomPercentage = 75,
                MinimumTrackZoomInExtentInPixels = 20f,
            };

            var dto = (ExtentInteractiveOverlayDescriptor)OverlayMapper.ToDescriptor(original)!;
            var json = JsonSerializer.Serialize<OverlayDescriptor>(dto);
            var deserialized = (ExtentInteractiveOverlayDescriptor)JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (ExtentInteractiveOverlay)OverlayMapper.FromDescriptor(deserialized)!;

            Assert.Equal("extent", roundTripped.Name);
            Assert.Equal(MapPanMode.Disabled, roundTripped.PanMode);
            Assert.Equal(MapMouseWheelMode.Disabled, roundTripped.MouseWheelMode);
            Assert.Equal(MapDoubleClickMode.Disabled, roundTripped.DoubleLeftClickMode);
            Assert.Equal(MapDoubleClickMode.Default, roundTripped.DoubleRightClickMode);
            Assert.Equal(75, roundTripped.ZoomPercentage);
            Assert.Equal(20f, roundTripped.MinimumTrackZoomInExtentInPixels);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void InteractiveOverlays_FullRoundTrip_ViaMapSerializer()
    {
        StaRunner.Run(async () =>
        {
            var source = new MapView
            {
                MapUnit = GeographyUnit.Meter,
                CenterPoint = new PointShape(0, 0),
                CurrentScale = 500_000,
            };
            var edit = new EditInteractiveOverlay { Name = "myEdit", CanDrag = false };
            edit.EditShapesLayer.InternalFeatures.Add(new Feature("POINT(1 2)") { Id = "a" });
            source.Overlays.Add(edit);

            var track = new TrackInteractiveOverlay { Name = "myTrack", TrackMode = TrackMode.Circle };
            source.Overlays.Add(track);

            var extent = new ExtentInteractiveOverlay { Name = "myExtent", ZoomPercentage = 60 };
            source.Overlays.Add(extent);

            var json = MapSerializer.Serialize(source);
            var target = new MapView();
            MapSerializer.Deserialize(target, json);

            Assert.Equal(3, target.Overlays.Count);
            var editRT = target.Overlays.OfType<EditInteractiveOverlay>().Single(o => o.Name == "myEdit");
            Assert.False(editRT.CanDrag);
            Assert.Single(editRT.EditShapesLayer.InternalFeatures);

            var trackRT = target.Overlays.OfType<TrackInteractiveOverlay>().Single(o => o.Name == "myTrack");
            Assert.Equal(TrackMode.Circle, trackRT.TrackMode);

            var extentRT = target.Overlays.OfType<ExtentInteractiveOverlay>().Single(o => o.Name == "myExtent");
            Assert.Equal(60, extentRT.ZoomPercentage);
            await Task.CompletedTask;
        });
    }
}
