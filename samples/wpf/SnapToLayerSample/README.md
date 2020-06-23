# Snap To Layer Sample for Wpf

### Description
The purpose of this Wpf project is to address some limitations of the class SnapToLayerEditInteractiveOverlay found in a previous sample Snap To Layer.  

This class allowed the snapping of the mouse pointer to the closest vertex of a polygon if it is within a set tolerance. While this worked great for simple polygons, there was a performance limitation with complex polygons made of many vertices. This Wpf sample addresses this limitation and allows responsive dragging and snapping of vertex regardless of the size of polygon to snap to.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/SnapToLayerSample/ScreenShot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
protected override Feature MoveVertexCore(Feature sourceFeature, PointShape sourceControlPoint, PointShape targetControlPoint)
{
    VertexMovingEditInteractiveOverlayEventArgs vertexMovingEditInteractiveOverlayEventArgs = new VertexMovingEditInteractiveOverlayEventArgs(false, sourceFeature, new Vertex(targetControlPoint));

    PointShape snapPointShape = null;
    if (toleranceType == ToleranceCoordinates.Screen)
    {
        snapPointShape = FindNearestSnappingPointPixel(targetControlPoint);
    }
    else
    {
        snapPointShape = FindNearestSnappingPoint(targetControlPoint);
    }

    if (snapPointShape != null)
    {
        vertexMovingEditInteractiveOverlayEventArgs.MovingVertex = new Vertex(snapPointShape);
    }

    return base.MoveVertexCore(sourceFeature, sourceControlPoint, new PointShape(vertexMovingEditInteractiveOverlayEventArgs.MovingVertex));
}
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Shapes.Feature](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.feature)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.Wpf.VertexMovingEditInteractiveOverlayEventArgs](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.vertexmovingeditinteractiveoverlayeventargs)
- [ThinkGeo.MapSuite.Shapes.Vertex](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.vertex)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
