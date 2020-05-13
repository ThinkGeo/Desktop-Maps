# Vertex Tolerance Sample for WinForms

### Description

Basically, this project shows the opposite of yesterday’s project “Snap To Layer”. Instead of having the dragged control point snapping to a vertex if within a tolerance, we show how to not allow a control point get within a set tolerance. This technique can be handy to implement if you have a requirement to have vertices of a shape being no less than a certain distance between each others. To implement that technique, again we use the power and flexibility of the EditInteractiveOverlay of the Desktop edition.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/VertexToleranceSample-ForWinForms/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0.0](https://www.nuget.org/packages?q=thinkgeo)

### About the Code
```csharp
protected override Feature MoveVertexCore(Feature sourceFeature, PointShape sourceControlPoint, PointShape targetControlPoint)
{
    VertexMovingEditInteractiveOverlayEventArgs vertexMovingEditInteractiveOverlayEventArgs = new VertexMovingEditInteractiveOverlayEventArgs(false, sourceFeature, new Vertex(targetControlPoint));

    ExistingControlPointsLayer.Open();
    Collection<Feature> controlPoints = ExistingControlPointsLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);

    EllipseShape toleranceEllipse = null;
    foreach (Feature feature in controlPoints)
    {
        PointShape pointShape = (PointShape)feature.GetShape();
        double Dist = pointShape.GetDistanceTo(targetControlPoint, geographyUnit, toleranceUnit);

        if (Dist <= Tolerance)
        {
            toleranceEllipse = new EllipseShape(pointShape, tolerance, geographyUnit, toleranceUnit);
            break;
        }
    }

    if (toleranceEllipse != null)
    {
        EllipseShape[] neighborEllipseShapes = GetNeighborEllipseShapes(toleranceEllipse);
        MultipolygonShape unionMultiPolygonShape = PolygonShape.Union(neighborEllipseShapes);
        LineShape ellipseLineShape = ToLineShape(unionMultiPolygonShape);
        PointShape onEllipsePointShape = ellipseLineShape.GetClosestPointTo(targetControlPoint, geographyUnit);

        vertexMovingEditInteractiveOverlayEventArgs.MovingVertex = new Vertex(onEllipsePointShape);
    }
    return base.MoveVertexCore(sourceFeature, sourceControlPoint, new PointShape(vertexMovingEditInteractiveOverlayEventArgs.MovingVertex));
}
```
### Getting Help

[Map Suite Desktop for WinForms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for WinForms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Shapes.Feature](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.feature)
- [ThinkGeo.MapSuite.Shapes.EllipseShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.ellipseshape)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.Shapes.MultipolygonShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.multipolygonshape)
- [ThinkGeo.MapSuite.Shapes.LineShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.lineshape)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
