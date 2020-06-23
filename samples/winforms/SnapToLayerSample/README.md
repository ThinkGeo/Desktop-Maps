# Snap To Layer Sample for WinForms

### Description

This project shows how you can apply snapping to an EditInteractiveOverlay. There are many aspects to snapping. Previously, we showed a project with the mouse pointer snapping to the closest vertex of an editable polygon. Today, we show the technique to drag a control point and have it snap to the closest vertex of a layer if within tolerance. The tolerance can be set in world (meter, feet etc) or screen (pixels) coordinates. Notice that we are also using the technique showed in the previous project “Dragged Point Style”.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
class SnapToLayerEditInteractiveOverlay : EditInteractiveOverlay
{
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
}
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.EditInteractiveOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.editinteractiveoverlay)
- [ThinkGeo.MapSuite.WinForms.VertexMovingEditInteractiveOverlayEventArgs](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.vertexmovingeditinteractiveoverlayeventargs)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
