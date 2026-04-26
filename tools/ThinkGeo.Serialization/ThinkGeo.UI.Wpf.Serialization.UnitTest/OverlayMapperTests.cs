using System.Text.Json;
using System.Threading.Tasks;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.UI.Wpf.Serialization.Dtos;
using ThinkGeo.UI.Wpf.Serialization.Mappers;
using Xunit;

namespace ThinkGeo.UI.Wpf.Serialization.UnitTest;

public class OverlayMapperTests
{
    [Fact]
    public void ThinkGeoCloudVectorOverlay_RoundTrips_ViaJson()
    {
        StaRunner.Run(async () =>
        {
            var original = new ThinkGeoCloudVectorMapsOverlay("client-id", "client-secret",
                ThinkGeoCloudVectorMapsMapType.Dark)
            { Name = "tg-vector" };

            var dto = OverlayMapper.ToDescriptor(original);
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (ThinkGeoCloudVectorMapsOverlay)OverlayMapper.FromDescriptor(deserialized);

            Assert.Equal("tg-vector", roundTripped.Name);
            Assert.Equal("client-id", roundTripped.ClientId);
            Assert.Equal("client-secret", roundTripped.ClientSecret);
            Assert.Equal(ThinkGeoCloudVectorMapsMapType.Dark, roundTripped.MapType);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void ThinkGeoCloudRasterOverlay_RoundTrips_ViaJson()
    {
        StaRunner.Run(async () =>
        {
            var original = new ThinkGeoCloudRasterMapsOverlay("client-id", "client-secret",
                ThinkGeoCloudRasterMapsMapType.Dark_V2_X2)
            { Name = "tg-raster" };

            var dto = OverlayMapper.ToDescriptor(original);
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (ThinkGeoCloudRasterMapsOverlay)OverlayMapper.FromDescriptor(deserialized);

            Assert.Equal("tg-raster", roundTripped.Name);
            Assert.Equal("client-id", roundTripped.ClientId);
            Assert.Equal(ThinkGeoCloudRasterMapsMapType.Dark_V2_X2, roundTripped.MapType);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void OpenStreetMapOverlay_RoundTrips_ViaJson()
    {
        StaRunner.Run(async () =>
        {
            var original = new OpenStreetMapOverlay("my-app/1.0") { Name = "osm" };

            var dto = OverlayMapper.ToDescriptor(original);
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (OpenStreetMapOverlay)OverlayMapper.FromDescriptor(deserialized);

            Assert.Equal("osm", roundTripped.Name);
            Assert.Equal("my-app/1.0", roundTripped.UserAgent);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void WmsOverlay_RoundTrips_ViaJson()
    {
        StaRunner.Run(async () =>
        {
            var original = new WmsOverlay(new System.Uri("https://example.org/wms?")) { Name = "wms" };
            original.ActiveLayerNames.Add("topp:states");
            original.ActiveStyleNames.Add("population");
            original.OutputFormat = "image/jpeg";
            original.IsTransparent = false;
            original.Parameters["token"] = "xyz";

            var dto = OverlayMapper.ToDescriptor(original);
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (WmsOverlay)OverlayMapper.FromDescriptor(deserialized);

            Assert.Equal("wms", roundTripped.Name);
            Assert.Equal(original.Uri, roundTripped.Uri);
            Assert.Equal(new[] { "topp:states" }, roundTripped.ActiveLayerNames);
            Assert.Equal(new[] { "population" }, roundTripped.ActiveStyleNames);
            Assert.Equal("image/jpeg", roundTripped.OutputFormat);
            Assert.False(roundTripped.IsTransparent);
            Assert.Equal("xyz", roundTripped.Parameters["token"]);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void SimpleMarkerOverlay_RoundTrips_PreservingMarkersAndDragMode()
    {
        StaRunner.Run(async () =>
        {
            var original = new SimpleMarkerOverlay { Name = "pois", DragMode = MarkerDragMode.Drag };
            original.Markers.Add(new Marker(2.35, 48.85)
                { XOffset = -5, YOffset = -10, RotationAngle = 45, ToolTip = "Paris" });
            original.Markers.Add(new Marker(-0.13, 51.51) { ToolTip = "London" });

            var dto = OverlayMapper.ToDescriptor(original);
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (SimpleMarkerOverlay)OverlayMapper.FromDescriptor(deserialized);

            Assert.Equal("pois", roundTripped.Name);
            Assert.Equal(MarkerDragMode.Drag, roundTripped.DragMode);
            Assert.Equal(2, roundTripped.Markers.Count);
            Assert.Equal(2.35, roundTripped.Markers[0].Position.X, 2);
            Assert.Equal(48.85, roundTripped.Markers[0].Position.Y, 2);
            Assert.Equal(-5, roundTripped.Markers[0].XOffset);
            Assert.Equal(45, roundTripped.Markers[0].RotationAngle);
            Assert.Equal("Paris", roundTripped.Markers[0].ToolTip);
            Assert.Equal("London", roundTripped.Markers[1].ToolTip);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void BackgroundOverlay_FromDescriptor_ReturnsNull_InsteadOfThrowing()
    {
        // BackgroundOverlay's ctor is internal — it's owned by MapView, not standalone-constructible.
        // FromDescriptor signals this by returning null so callers iterating an overlay list can
        // silently skip the entry and apply the background via ApplySnapshot instead.
        var descriptor = new BackgroundOverlayDescriptor { BackgroundBrushColor = "#FF102030" };
        var result = OverlayMapper.FromDescriptor(descriptor);
        Assert.Null(result);
    }

    [Fact]
    public void ApplySnapshot_BackgroundDescriptorInOverlaysList_IsSkippedGracefully()
    {
        StaRunner.Run(async () =>
        {
            // Hand-craft a snapshot that accidentally has a BackgroundOverlayDescriptor nested in
            // the Overlays list (shouldn't happen from a capture, but can happen if a user mutates
            // the snapshot DTO). ApplySnapshot should skip it rather than blow up.
            var snapshot = new Dtos.MapViewSnapshot
            {
                CenterX = 0,
                CenterY = 0,
                CurrentScale = 1,
                MapUnit = "DecimalDegree",
                Overlays = { new BackgroundOverlayDescriptor { BackgroundBrushColor = "#FF102030" } },
            };

            var mapView = new MapView();
            mapView.ApplySnapshot(snapshot);

            Assert.Empty(mapView.Overlays);
            await System.Threading.Tasks.Task.CompletedTask;
        });
    }

#pragma warning disable CS0618 // BingMapsOverlay is marked Obsolete in 14.5 (Bing maps service retiring)
    [Fact]
    public void BingMapsOverlay_RoundTrips_ViaJson()
    {
        StaRunner.Run(async () =>
        {
            var original = new BingMapsOverlay("bing-app-id", BingMapsMapType.Aerial) { Name = "bing" };

            var dto = OverlayMapper.ToDescriptor(original);
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (BingMapsOverlay)OverlayMapper.FromDescriptor(deserialized);

            Assert.Equal("bing", roundTripped.Name);
            Assert.Equal("bing-app-id", roundTripped.ApplicationId);
            Assert.Equal(BingMapsMapType.Aerial, roundTripped.MapType);
            await Task.CompletedTask;
        });
    }
#pragma warning restore CS0618

    [Fact]
    public void GoogleMapsOverlay_RoundTrips_ViaApiKey()
    {
        StaRunner.Run(async () =>
        {
            var original = new GoogleMapsOverlay("google-api-key", "signing-secret")
            {
                Name = "google",
                MapType = GoogleMapsMapType.Satellite,
            };

            var dto = OverlayMapper.ToDescriptor(original);
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (GoogleMapsOverlay)OverlayMapper.FromDescriptor(deserialized);

            Assert.Equal("google", roundTripped.Name);
            Assert.Equal("google-api-key", roundTripped.ApiKey);
            Assert.Equal("signing-secret", roundTripped.UriSigningSecret);
            Assert.Equal(GoogleMapsMapType.Satellite, roundTripped.MapType);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void AzureMapsRasterOverlay_RoundTrips_ViaJson_NotingSubscriptionKeyNotCaptured()
    {
        StaRunner.Run(async () =>
        {
            var original = new AzureMapsRasterOverlay("azure-sub-key", AzureMapsRasterTileSet.Imagery)
            {
                Name = "azure",
                ApiVersion = "2024-04-01",
            };

            var dto = (AzureMapsRasterOverlayDescriptor)OverlayMapper.ToDescriptor(original);
            // Capture path can't reach the SubscriptionKey (stored on the internal async layer).
            Assert.Null(dto.SubscriptionKey);
            // Caller must fill it in before persisting/applying.
            var restored = dto with { SubscriptionKey = "azure-sub-key" };
            var json = JsonSerializer.Serialize<OverlayDescriptor>(restored);
            var deserialized = JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (AzureMapsRasterOverlay)OverlayMapper.FromDescriptor(deserialized);

            Assert.Equal("azure", roundTripped.Name);
            Assert.Equal(AzureMapsRasterTileSet.Imagery, roundTripped.RasterTileSet);
            Assert.Equal("2024-04-01", roundTripped.ApiVersion);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void HereMapsRasterTileOverlay_RoundTrips_ViaJson()
    {
        StaRunner.Run(async () =>
        {
            var original = new HereMapsRasterTileOverlay("here-api-key", HereMapsRasterType.Aerial)
            {
                Name = "here",
                Format = HereMapsRasterTileFormat.Jpg,
            };

            var dto = OverlayMapper.ToDescriptor(original);
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (HereMapsRasterTileOverlay)OverlayMapper.FromDescriptor(deserialized);

            Assert.Equal("here", roundTripped.Name);
            Assert.Equal("here-api-key", roundTripped.ApiKey);
            Assert.Equal(HereMapsRasterType.Aerial, roundTripped.MapType);
            Assert.Equal(HereMapsRasterTileFormat.Jpg, roundTripped.Format);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void MapBoxStaticTilesOverlay_RoundTrips_ViaJson()
    {
        StaRunner.Run(async () =>
        {
            var original = new MapBoxStaticTilesOverlay("mapbox-token", MapBoxStyleId.Satellite)
            {
                Name = "mapbox",
            };

            var dto = OverlayMapper.ToDescriptor(original);
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (MapBoxStaticTilesOverlay)OverlayMapper.FromDescriptor(deserialized);

            Assert.Equal("mapbox", roundTripped.Name);
            Assert.Equal("mapbox-token", roundTripped.AccessToken);
            Assert.Equal(MapBoxStyleId.Satellite, roundTripped.StyleId);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void PopupOverlay_RoundTrips_WithTwoPopups()
    {
        StaRunner.Run(async () =>
        {
            var original = new PopupOverlay { Name = "info" };
            original.Popups.Add(new Popup(2.35, 48.85)
                { ArrowHeight = 12, RotateAngle = 10, Content = "Paris" });
            original.Popups.Add(new Popup(-0.13, 51.51)
                { Content = "London" });

            var dto = OverlayMapper.ToDescriptor(original);
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (PopupOverlay)OverlayMapper.FromDescriptor(deserialized);

            Assert.Equal("info", roundTripped.Name);
            Assert.Equal(2, roundTripped.Popups.Count);
            Assert.Equal(2.35, roundTripped.Popups[0].Position.X, 2);
            Assert.Equal(12, roundTripped.Popups[0].ArrowHeight);
            Assert.Equal(10, roundTripped.Popups[0].RotateAngle);
            Assert.Equal("Paris", roundTripped.Popups[0].Content);
            Assert.Equal("London", roundTripped.Popups[1].Content);
            await Task.CompletedTask;
        });
    }

    [Fact]
    public void WmtsOverlay_RoundTrips_ViaJson()
    {
        StaRunner.Run(async () =>
        {
            var original = new WmtsOverlay(new System.Uri("https://example.org/wmts?"))
            {
                Name = "wmts",
                ActiveLayerName = "topp:states",
                ActiveStyleName = "population",
                OutputFormat = "image/png",
            };

            var dto = OverlayMapper.ToDescriptor(original);
            var json = JsonSerializer.Serialize(dto);
            var deserialized = JsonSerializer.Deserialize<OverlayDescriptor>(json)!;
            var roundTripped = (WmtsOverlay)OverlayMapper.FromDescriptor(deserialized);

            Assert.Equal("wmts", roundTripped.Name);
            Assert.Equal("topp:states", roundTripped.ActiveLayerName);
            Assert.Equal("population", roundTripped.ActiveStyleName);
            Assert.Equal("image/png", roundTripped.OutputFormat);
            Assert.Equal(original.ServerUri, roundTripped.ServerUri);
            await Task.CompletedTask;
        });
    }
}
