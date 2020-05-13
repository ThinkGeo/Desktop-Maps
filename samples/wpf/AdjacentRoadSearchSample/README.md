# Adjacent Road Search Sample for Wpf

### Description

This WPF project shows how to get a route between two adjacent roads, even if they don't intersect within an allowable tolerance. 

It’s based on Map Suite Geometry Topology module and does not require the Map Suite Routing Extension.  
              
Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/AdjacentRoadSearchSample-ForWpf/blob/master/Screenshot.gif)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
double tolerance = double.Parse(txtTolerance.Text, CultureInfo.InvariantCulture);
PolygonShape bufferedPolygon = endSegment.Buffer(tolerance, GeographyUnit.DecimalDegree, DistanceUnit.Meter).Polygons[0];
MultilineShape intersectedLines = startSegment.GetIntersection(bufferedPolygon) as MultilineShape;
if (intersectedLines.Lines.Count > 0)     // Just check the intersected within allowed tolerance.
{
    PointShape intersectedPoint = intersectedLines.Lines[0].GetCenterPoint();

    PointShape nearestPointOnStart = intersectedPoint.GetClosestPointTo(startSegment, GeographyUnit.DecimalDegree);
    PointShape nearestPointOnEnd = intersectedPoint.GetClosestPointTo(endSegment, GeographyUnit.DecimalDegree);

    LineShape routeSegmentOnStart = startSegment.GetLineOnALine(start, nearestPointOnStart) as LineShape;
    LineShape routeSegmentOnEnd = endSegment.GetLineOnALine(end, nearestPointOnEnd) as LineShape;

    IEnumerable<Vertex> vertexs = route.Vertices;
    int index = IndexOf(routeSegmentOnStart.Vertices, new Vertex(nearestPointOnStart));
    if (index == 0)
    {
        vertexs = vertexs.Concat(routeSegmentOnStart.Vertices.Reverse());
    }
    else
    {
        vertexs = vertexs.Concat(routeSegmentOnStart.Vertices);
    }

    index = IndexOf(routeSegmentOnEnd.Vertices, new ThinkGeo.MapSuite.Shapes.Vertex(nearestPointOnEnd));
    if (index == 0)
    {
        vertexs = vertexs.Concat(routeSegmentOnEnd.Vertices);
    }
    else
    {
        vertexs = vertexs.Concat(routeSegmentOnEnd.Vertices.Reverse());
    }

    route = new LineShape(vertexs);
}
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Shapes.PolygonShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.polygonshape)
- [ThinkGeo.MapSuite.GeographyUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.geographyunit)
- [ThinkGeo.MapSuite.Shapes.DistanceUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.distanceunit)
- [ThinkGeo.MapSuite.Shapes.MultilineShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.multilineshape)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.Shapes.LineShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.lineshape)
- [ThinkGeo.MapSuite.Shapes.Vertex](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.vertex)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
