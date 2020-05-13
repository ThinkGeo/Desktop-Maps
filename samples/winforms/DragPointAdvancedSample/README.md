# Drag Point Advanced Sample for WinForms

### Description
This project is an example of how extensible **EditInteractiveOverlay** is for the editing needs of the user. You can see how to change the styles of the vertex being dragged based on the Shift key, and how to have the vertex snap to any feature. 

Thanks to the protected override functions such as **KeyDownCore**, **KeyUpCore**, **MouseUpCore** and **DrawCore**, these functionalities can easily be implemented by inheriting from **EditInteractiveOverlay**.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://raw.githubusercontent.com/ThinkGeo/DragPointAdvancedSample-ForWinForms/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
PolygonShape newPolygonShape = (PolygonShape)EditShapesLayer.InternalFeatures[0].GetShape();
RingShape ringShape = new RingShape();

foreach (Feature feature in controlPoints)
{
    PointShape controlPointShape = (PointShape)feature.GetShape();
    if (feature.ColumnValues["nodetype"] == "special")
    {
        BaseShape baseShape = snappingFeature.GetShape();
        PointShape pointShape = baseShape.GetClosestPointTo(controlPointShape, GeographyUnit.DecimalDegree);
        ringShape.Vertices.Add(new Vertex(pointShape.X, pointShape.Y));
    }
    else
    {
        ringShape.Vertices.Add(new Vertex(controlPointShape.X, controlPointShape.Y));
    }
}
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Shapes.PolygonShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.polygonshape)
- [ThinkGeo.MapSuite.Shapes.RingShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.ringshape)
- [ThinkGeo.MapSuite.Shapes.Feature](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.feature)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.Shapes.BaseShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.baseshape)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
