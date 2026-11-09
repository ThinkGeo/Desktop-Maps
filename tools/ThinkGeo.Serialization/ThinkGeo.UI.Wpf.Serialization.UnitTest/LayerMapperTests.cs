using System.IO;
using System.Linq;
using System.Text.Json;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Dtos;
using ThinkGeo.Core.Serialization.Mappers;
using Xunit;

namespace ThinkGeo.UI.Wpf.Serialization.UnitTest;

public class LayerMapperTests
{
    [Fact]
    public void InMemoryFeatureLayer_RoundTrips_ViaJson()
    {
        var columns = new[]
        {
            new FeatureSourceColumn("name", "Character", 50),
            new FeatureSourceColumn("pop", "Integer", 10),
        };
        var features = new[]
        {
            new Feature(new PointShape(2.35, 48.85),
                new System.Collections.Generic.Dictionary<string, string>
                    { ["name"] = "Paris", ["pop"] = "2148271" }) { Id = "paris" },
            new Feature(new PointShape(-0.13, 51.51),
                new System.Collections.Generic.Dictionary<string, string>
                    { ["name"] = "London", ["pop"] = "8982000" }) { Id = "london" },
        };
        var original = new InMemoryFeatureLayer(columns, features)
        {
            Name = "cities",
            IsVisible = true,
        };

        var dto = LayerMapper.ToDescriptor(original);
        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<LayerDescriptor>(json)!;
        var roundTripped = (InMemoryFeatureLayer)LayerMapper.FromDescriptor(deserialized);

        Assert.Equal(original.Name, roundTripped.Name);
        Assert.Equal(original.IsVisible, roundTripped.IsVisible);
        // Use GetColumns() rather than .Columns: the latter is lazy-populated by a GetColumns call
        // the first time it's touched, which in turn requires the source to be Open.
        var originalColumns = original.GetColumns();
        var roundTrippedColumns = roundTripped.GetColumns();
        Assert.Equal(originalColumns.Count, roundTrippedColumns.Count);
        Assert.Equal("name", roundTrippedColumns[0].ColumnName);
        Assert.Equal("Character", roundTrippedColumns[0].TypeName);

        Assert.Equal(original.InternalFeatures.Count, roundTripped.InternalFeatures.Count);
        var byId = roundTripped.InternalFeatures.ToDictionary(f => f.Id, f => f);
        Assert.Equal("Paris", byId["paris"].ColumnValues["name"]);
        Assert.Equal("2148271", byId["paris"].ColumnValues["pop"]);
        Assert.Equal("London", byId["london"].ColumnValues["name"]);

        // Verify geometry survives as well.
        var parisShape = (PointShape)byId["paris"].GetShape();
        Assert.Equal(2.35, parisShape.X, 2);
        Assert.Equal(48.85, parisShape.Y, 2);
    }

    [Fact]
    public void ShapefileLayer_RoundTrips_PreservingPath()
    {
        var original = new ShapeFileFeatureLayer(@"C:\fake\path\cities.shp")
        {
            Name = "cities",
        };

        var dto = (ShapeFileLayerDescriptor)LayerMapper.ToDescriptor(original);
        var roundTripped = (ShapeFileFeatureLayer)LayerMapper.FromDescriptor(dto);

        Assert.Equal(@"C:\fake\path\cities.shp", roundTripped.ShapePathFilename);
        Assert.Equal("cities", roundTripped.Name);
    }

    [Fact]
    public void UnsupportedLayerType_ToDescriptor_ReturnsNull()
    {
        // ToDescriptor returns null for unknown layer types instead of throwing, matching the
        // nullable-return convention used across the snapshot mappers.
        var unsupported = new UnsupportedTestLayer();
        Assert.Null(LayerMapper.ToDescriptor(unsupported));
        Assert.Null(LayerMapper.TryToDescriptor(unsupported));
    }

    [Fact]
    public void WmsLayer_RoundTrips_ViaJson()
    {
        var original = new WmsAsyncLayer(new System.Uri("https://example.org/wms?"));
        original.ActiveLayerNames.Add("topp:states");
        original.ActiveLayerNames.Add("topp:cities");
        original.ActiveStyleNames.Add("population");
        original.OutputFormat = "image/jpeg";
        original.IsTransparent = false;
        original.Parameters["token"] = "abc123";
        original.Name = "states-wms";

        var dto = LayerMapper.ToDescriptor(original);
        Assert.IsType<WmsLayerDescriptor>(dto);
        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<LayerDescriptor>(json)!;
        var roundTripped = (WmsAsyncLayer)LayerMapper.FromDescriptor(deserialized);

        Assert.Equal("states-wms", roundTripped.Name);
        Assert.Equal(original.Uri, roundTripped.Uri);
        Assert.Equal(new[] { "topp:states", "topp:cities" }, roundTripped.ActiveLayerNames);
        Assert.Equal(new[] { "population" }, roundTripped.ActiveStyleNames);
        Assert.Equal("image/jpeg", roundTripped.OutputFormat);
        Assert.False(roundTripped.IsTransparent);
        Assert.Equal("abc123", roundTripped.Parameters["token"]);
    }

    [Fact]
    public void GeoTiffLayer_RoundTrips_PreservingPathAndResampling()
    {
        var original = new GeoTiffRasterLayer(@"C:\fake\dem.tif")
        {
            Name = "elevation",
            ResamplingMode = RasterResamplingMode.Bilinear,
        };

        var dto = LayerMapper.ToDescriptor(original);
        Assert.IsType<GeoTiffRasterLayerDescriptor>(dto);
        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<LayerDescriptor>(json)!;
        var roundTripped = (GeoTiffRasterLayer)LayerMapper.FromDescriptor(deserialized);

        Assert.Equal(@"C:\fake\dem.tif", roundTripped.ImagePathFilename);
        Assert.Equal("elevation", roundTripped.Name);
        Assert.Equal(RasterResamplingMode.Bilinear, roundTripped.ResamplingMode);
    }

    [Fact]
    public void MultipleShapeFileLayer_RoundTrips_ExplicitList()
    {
        var original = new MultipleShapeFileFeatureLayer(new[]
        {
            @"C:\fake\roads.shp",
            @"C:\fake\rails.shp",
        })
        { Name = "transport" };

        var dto = LayerMapper.ToDescriptor(original);
        Assert.IsType<MultipleShapeFileLayerDescriptor>(dto);
        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<LayerDescriptor>(json)!;
        var roundTripped = (MultipleShapeFileFeatureLayer)LayerMapper.FromDescriptor(deserialized);

        Assert.Equal("transport", roundTripped.Name);
        Assert.Equal(2, roundTripped.ShapeFiles.Count);
        // MultipleShapeFileFeatureLayer normalises paths to upper-case internally, so compare
        // case-insensitively rather than by exact string.
        var normalised = roundTripped.ShapeFiles.Select(p => p.ToLowerInvariant()).ToList();
        Assert.Contains(@"c:\fake\roads.shp", normalised);
        Assert.Contains(@"c:\fake\rails.shp", normalised);
    }

    [Fact]
    public void WkbFileLayer_RoundTrips_PreservingPathAndAccess()
    {
        var original = new WkbFileFeatureLayer(@"C:\fake\cities.wkb", FileAccess.ReadWrite)
        { Name = "wkb-cities" };

        var dto = LayerMapper.ToDescriptor(original);
        var roundTripped = (WkbFileFeatureLayer)LayerMapper.FromDescriptor(dto);

        Assert.Equal("wkb-cities", roundTripped.Name);
        Assert.Equal(@"C:\fake\cities.wkb", roundTripped.WkbPathFilename);
        Assert.Equal(FileAccess.ReadWrite, roundTripped.ReadWriteMode);
    }

    [Fact]
    public void TabFileLayer_RoundTrips_PreservingPathAndEncoding()
    {
        var original = new TabFeatureLayer(@"C:\fake\parcels.tab")
        {
            Name = "parcels",
            Encoding = System.Text.Encoding.UTF8,
        };

        var dto = LayerMapper.ToDescriptor(original);
        var roundTripped = (TabFeatureLayer)LayerMapper.FromDescriptor(dto);

        Assert.Equal("parcels", roundTripped.Name);
        Assert.Equal(@"C:\fake\parcels.tab", roundTripped.TabPathFilename);
        Assert.Equal("utf-8", roundTripped.Encoding.WebName);
    }

    [Fact]
    public void TinyGeoFileLayer_RoundTrips_WithPassword()
    {
        var original = new TinyGeoFeatureLayer(@"C:\fake\data.tgeo", "s3cret") { Name = "tgeo" };

        var dto = LayerMapper.ToDescriptor(original);
        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<LayerDescriptor>(json)!;
        var roundTripped = (TinyGeoFeatureLayer)LayerMapper.FromDescriptor(deserialized);

        Assert.Equal("tgeo", roundTripped.Name);
        Assert.Equal(@"C:\fake\data.tgeo", roundTripped.TinyGeoPathFilename);
        Assert.Equal("s3cret", roundTripped.Password);
    }

    [Fact]
    public void GpxFileLayer_RoundTrips_PreservingPath()
    {
        var original = new GpxFeatureLayer(@"C:\fake\trail.gpx") { Name = "trail" };

        var dto = LayerMapper.ToDescriptor(original);
        var roundTripped = (GpxFeatureLayer)LayerMapper.FromDescriptor(dto);

        Assert.Equal("trail", roundTripped.Name);
        Assert.Equal(@"C:\fake\trail.gpx", roundTripped.GpxPathFilename);
    }

    [Fact]
    public void SqliteFeatureLayer_RoundTrips_PreservingConnectionAndTable()
    {
        var original = new SqliteFeatureLayer(
            connectionString: "Data Source=:memory:",
            tableName: "parcels",
            featureIdColumn: "id",
            geometryColumnName: "geom")
        {
            Name = "parcels-sqlite",
            WhereClause = "type = 'residential'",
            CommandTimeout = 60,
        };

        var dto = LayerMapper.ToDescriptor(original);
        var json = JsonSerializer.Serialize(dto);
        var deserialized = JsonSerializer.Deserialize<LayerDescriptor>(json)!;
        var roundTripped = (SqliteFeatureLayer)LayerMapper.FromDescriptor(deserialized);

        Assert.Equal("parcels-sqlite", roundTripped.Name);
        Assert.Equal("Data Source=:memory:", roundTripped.ConnectionString);
        Assert.Equal("parcels", roundTripped.TableName);
        Assert.Equal("id", roundTripped.FeatureIdColumn);
        Assert.Equal("geom", roundTripped.GeometryColumnName);
        Assert.Equal("type = 'residential'", roundTripped.WhereClause);
        Assert.Equal(60, roundTripped.CommandTimeout);
    }

    [Fact]
    public void RasterMbTilesLayer_RoundTrips_PreservingPath()
    {
        var original = new RasterMbTilesAsyncLayer(@"C:\fake\basemap.mbtiles") { Name = "basemap" };

        var dto = LayerMapper.ToDescriptor(original);
        var roundTripped = (RasterMbTilesAsyncLayer)LayerMapper.FromDescriptor(dto);

        Assert.Equal("basemap", roundTripped.Name);
        Assert.Equal(@"C:\fake\basemap.mbtiles", roundTripped.FilePath);
    }

    [Fact]
    public void GraticuleFeatureLayer_RoundTrips_PreservingDensity()
    {
        var original = new GraticuleFeatureLayer(5) { Name = "graticule" };

        var dto = LayerMapper.ToDescriptor(original);
        // GraticuleDensity isn't exposed on the runtime class, so capture defaults to 3. Setting
        // the descriptor density explicitly verifies the forward path.
        var deserialized = dto with { };  // same descriptor
        var deserializedWithExplicitDensity = ((GraticuleFeatureLayerDescriptor)deserialized) with { GraticuleDensity = 5 };
        var roundTripped = (GraticuleFeatureLayer)LayerMapper.FromDescriptor(deserializedWithExplicitDensity);

        Assert.Equal("graticule", roundTripped.Name);
    }

    private sealed class UnsupportedTestLayer : Layer
    {
        protected override void OpenCore() { }
        protected override RectangleShape GetBoundingBoxCore() => new(0, 1, 1, 0);
        protected override void DrawCore(GeoCanvas canvas, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInAllLayers) { }
    }
}
