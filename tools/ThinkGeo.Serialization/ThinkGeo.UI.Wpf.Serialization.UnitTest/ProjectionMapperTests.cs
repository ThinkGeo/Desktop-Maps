using System.Text.Json;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Dtos;
using ThinkGeo.Core.Serialization.Mappers;
using Xunit;

namespace ThinkGeo.UI.Wpf.Serialization.UnitTest;

public class ProjectionMapperTests
{
    [Fact]
    public void ProjectionConverter_WithEpsgSrids_RoundTrips()
    {
        var original = new ProjectionConverter(internalSrid: 4326, externalSrid: 3857);

        var dto = ProjectionMapper.ToDescriptor(original)!;
        Assert.Equal(4326, dto.InternalSrid);
        Assert.Equal(3857, dto.ExternalSrid);

        var roundTripped = ProjectionMapper.FromDescriptor(dto)!;
        Assert.Equal(4326, roundTripped.InternalProjection.Srid);
        Assert.Equal(3857, roundTripped.ExternalProjection.Srid);
    }

    [Fact]
    public void ProjectionConverter_WithProjStrings_RoundTrips()
    {
        var wgs84 = "+proj=longlat +datum=WGS84 +no_defs";
        var mercator = "+proj=merc +lon_0=0 +k=1 +x_0=0 +y_0=0 +datum=WGS84 +units=m +no_defs";
        var original = new ProjectionConverter(wgs84, mercator);

        var dto = ProjectionMapper.ToDescriptor(original)!;
        Assert.NotNull(dto.InternalProjString);
        Assert.NotNull(dto.ExternalProjString);
        Assert.Contains("longlat", dto.InternalProjString);

        var roundTripped = ProjectionMapper.FromDescriptor(dto)!;
        Assert.Contains("longlat", roundTripped.InternalProjection.ProjString);
        Assert.Contains("merc", roundTripped.ExternalProjection.ProjString);
    }

    [Fact]
    public void NullConverter_CapturesAsNull()
    {
        Assert.Null(ProjectionMapper.ToDescriptor(null));
        Assert.Null(ProjectionMapper.FromDescriptor(null));
    }

    [Fact]
    public void ShapeFileLayer_WithProjection_RoundTripsViaLayerMapper()
    {
        var original = new ShapeFileFeatureLayer(@"C:\fake\cities.shp") { Name = "cities" };
        original.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);

        var dto = LayerMapper.ToDescriptor(original)!;
        Assert.NotNull(dto.Projection);
        Assert.Equal(4326, dto.Projection!.InternalSrid);
        Assert.Equal(3857, dto.Projection!.ExternalSrid);

        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<LayerDescriptor>(json)!;
        var roundTripped = (ShapeFileFeatureLayer)LayerMapper.FromDescriptor(deserialized)!;

        Assert.NotNull(roundTripped.FeatureSource.ProjectionConverter);
        Assert.Equal(4326, roundTripped.FeatureSource.ProjectionConverter.InternalProjection.Srid);
        Assert.Equal(3857, roundTripped.FeatureSource.ProjectionConverter.ExternalProjection.Srid);
    }

    [Fact]
    public void InMemoryLayer_WithoutProjection_HasNullProjectionDescriptor()
    {
        var layer = new InMemoryFeatureLayer();
        var dto = LayerMapper.ToDescriptor(layer)!;
        Assert.Null(dto.Projection);
    }

    [Fact]
    public void WmsLayer_WithProjection_RoundTrips()
    {
        var original = new WmsAsyncLayer(new System.Uri("https://example.org/wms?"))
        {
            ProjectionConverter = new ProjectionConverter(4326, 3857),
        };

        var dto = LayerMapper.ToDescriptor(original)!;
        Assert.NotNull(dto.Projection);

        var roundTripped = (WmsAsyncLayer)LayerMapper.FromDescriptor(dto)!;
        Assert.NotNull(roundTripped.ProjectionConverter);
        Assert.Equal(3857, roundTripped.ProjectionConverter!.ExternalProjection.Srid);
    }

    [Fact]
    public void GeoTiffLayer_WithProjection_RoundTrips()
    {
        var original = new GeoTiffRasterLayer(@"C:\fake\dem.tif");
        original.ImageSource.ProjectionConverter = new ProjectionConverter(32633, 3857); // UTM 33N → WebMercator

        var dto = LayerMapper.ToDescriptor(original)!;
        Assert.Equal(32633, dto.Projection!.InternalSrid);
        Assert.Equal(3857, dto.Projection!.ExternalSrid);

        var roundTripped = (GeoTiffRasterLayer)LayerMapper.FromDescriptor(dto)!;
        Assert.NotNull(roundTripped.ImageSource.ProjectionConverter);
        Assert.Equal(32633, roundTripped.ImageSource.ProjectionConverter.InternalProjection.Srid);
    }
}
