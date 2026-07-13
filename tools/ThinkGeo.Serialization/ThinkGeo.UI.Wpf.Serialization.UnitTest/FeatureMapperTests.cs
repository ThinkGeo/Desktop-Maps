using System.Collections.Generic;
using System.Linq;
using ThinkGeo.Core;
using ThinkGeo.Core.Serialization.Mappers;
using Xunit;

namespace ThinkGeo.UI.Wpf.Serialization.UnitTest;

public class FeatureMapperTests
{
    [Fact]
    public void PointFeature_RoundTrips_ThroughWkt()
    {
        var original = new Feature(new PointShape(5.0, 10.0));

        var dto = FeatureMapper.ToDescriptor(original);
        var roundTripped = FeatureMapper.FromDescriptor(dto);

        var originalShape = (PointShape)original.GetShape();
        var roundTrippedShape = (PointShape)roundTripped.GetShape();
        Assert.Equal(originalShape.X, roundTrippedShape.X);
        Assert.Equal(originalShape.Y, roundTrippedShape.Y);
    }

    [Fact]
    public void LineFeature_RoundTrips_ThroughWkt()
    {
        var line = new LineShape(new[] { new Vertex(0, 0), new Vertex(1, 1), new Vertex(2, 0) });
        var original = new Feature(line);

        var dto = FeatureMapper.ToDescriptor(original);
        var roundTripped = FeatureMapper.FromDescriptor(dto);

        var roundTrippedLine = (LineShape)roundTripped.GetShape();
        Assert.Equal(3, roundTrippedLine.Vertices.Count);
        Assert.Equal(0, roundTrippedLine.Vertices[0].X);
        Assert.Equal(1, roundTrippedLine.Vertices[1].X);
        Assert.Equal(2, roundTrippedLine.Vertices[2].X);
    }

    [Fact]
    public void PolygonFeature_RoundTrips_ThroughWkt()
    {
        var ring = new RingShape(new[]
        {
            new Vertex(0, 0), new Vertex(10, 0), new Vertex(10, 10), new Vertex(0, 10), new Vertex(0, 0),
        });
        var polygon = new PolygonShape(ring);
        var original = new Feature(polygon);

        var dto = FeatureMapper.ToDescriptor(original);
        var roundTripped = FeatureMapper.FromDescriptor(dto);

        var roundTrippedPolygon = (PolygonShape)roundTripped.GetShape();
        Assert.Equal(5, roundTrippedPolygon.OuterRing.Vertices.Count);
    }

    [Fact]
    public void Feature_PreservesColumnValues()
    {
        var columns = new Dictionary<string, string>
        {
            ["name"] = "Paris",
            ["population"] = "2148271",
            ["country"] = "FR",
        };
        var original = new Feature(new PointShape(2.35, 48.85), columns) { Id = "city-paris" };

        var dto = FeatureMapper.ToDescriptor(original);
        var roundTripped = FeatureMapper.FromDescriptor(dto);

        Assert.Equal("city-paris", roundTripped.Id);
        Assert.Equal(columns.Count, roundTripped.ColumnValues.Count);
        foreach (var kv in columns)
        {
            Assert.Equal(kv.Value, roundTripped.ColumnValues[kv.Key]);
        }
    }

    [Fact]
    public void Batch_RoundTrips()
    {
        var features = new List<Feature>
        {
            new Feature(new PointShape(1, 1)) { Id = "a" },
            new Feature(new PointShape(2, 2)) { Id = "b" },
            new Feature(new PointShape(3, 3)) { Id = "c" },
        };

        var dtos = FeatureMapper.ToDescriptors(features);
        var roundTripped = FeatureMapper.FromDescriptors(dtos);

        Assert.Equal(features.Count, roundTripped.Count);
        Assert.Equal(features.Select(f => f.Id), roundTripped.Select(f => f.Id));
    }
}
