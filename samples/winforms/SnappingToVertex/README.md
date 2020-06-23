# Snapping To Vertex Sample for WinForms

### Description

This project is the first one of a series that will be dedicated to snapping using InteractiveOverlay. In this project, we show how to snap the mouse pointer to the closest vertex of an editable polygon if it is within a set tolerance. The tolerance is shown as a circle around the vertices. This technique can also be applied to snapping vertices within the same shape, between different shapes or even based on a layer. Those different types of snapping will be the subject of future projects related to snapping using InteractiveOverlay.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/SnappingToVertex/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
class SnappingToVertexInteractiveOverlay : EditInteractiveOverlay
{
        protected override Feature SetSelectedControlPointCore(PointShape targetPointShape, double searchingTolerance)
        {
            //If the mouse pointer is within the tolerance of a vertex, allows the dragging of the vertex.
            //If not, the map will pan normally.
            Collection<Feature> existingControlPoints = ExistingControlPointsLayer.FeatureSource.GetFeaturesNearestTo(targetPointShape, GeographyUnit.Meter, 1, ReturningColumnsType.AllColumns);
            if (existingControlPoints.Count == 1)
            {
                PointShape existingControlPointShape = (PointShape)existingControlPoints[0].GetShape();
                double Distance = existingControlPointShape.GetDistanceTo(targetPointShape, arguments.MapUnit, toleranceUnit);

                if (Distance <= tolerance)
                {
                    ControlPointType = ControlPointType.Vertex;
                    if (arguments != null)
                    {
                        //Snapps the location of the mouse pointer to the location of the nearest vertex.
                        ScreenPointF controlPoint = ExtentHelper.ToScreenCoordinate(arguments.CurrentExtent, existingControlPoints[0], arguments.MapWidth, arguments.MapHeight);
                        ScreenPointF targetPoint = ExtentHelper.ToScreenCoordinate(arguments.CurrentExtent, targetPointShape, arguments.MapWidth, arguments.MapHeight);
                        Cursor.Position = new Point(Cursor.Position.X + (int)(controlPoint.X - targetPoint.X), Cursor.Position.Y + (int)(controlPoint.Y - targetPoint.Y));
                    }
                }

                inControlPointDraggingMode = true;
                return existingControlPoints[0];
            }

            return base.SetSelectedControlPointCore(targetPointShape, searchingTolerance);
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
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.WinForms.ControlPointType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.controlpointtype)
- [ThinkGeo.MapSuite.Shapes.ExtentHelper](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.extenthelper)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
