# Drag Point With Label Sample for WinForms

### Description
In this project, dedicated to **EditInteractiveOverlay**, you will see how easy it is to add some labeling to your dragged control point, showing dynamic information. Here we show how to display the distance from the dragged control point to the closest point of a reference shape. Also, to augment the user experience, the closest point of the reference shape is also shown varying as the control point is dragged around.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/DragPointWithLabelSample-ForWinForms/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
Collection<SimpleCandidate> labelsInAllLayers = new Collection<SimpleCandidate>();
EditShapesLayer.Open();
EditShapesLayer.Draw(canvas, labelsInAllLayers);
canvas.Flush();

ExistingControlPointsLayer.Open();
Collection<Feature> controlPoints = ExistingControlPointsLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);

foreach (Feature feature in controlPoints)
{
    //Looks at the value of "state" to draw the control point as dragged or not.
    Feature[] features = new Feature[1] { feature };
    draggedControlPointStyle.Draw(features, canvas, labelsInAllLayers, labelsInAllLayers);

    PointShape pointShape = feature.GetShape() as PointShape;
    PointShape closestPointShape = referenceShape.GetClosestPointTo(pointShape, GeographyUnit.Meter);
    //Draws the closest point on the reference shape and the distance to it from the dragged control point.
    if (closestPointShape != null)
    {
        double Dist = System.Math.Round(closestPointShape.GetDistanceTo(pointShape, GeographyUnit.Meter, DistanceUnit.Meter));
        ScreenPointF ScreenPointF = ExtentHelper.ToScreenCoordinate(canvas.CurrentWorldExtent, pointShape, canvas.Width, canvas.Height);
        canvas.DrawTextWithScreenCoordinate(System.Convert.ToString(Dist) + " m", new GeoFont("Arial", 12, DrawingFontStyles.Bold), new GeoSolidBrush(GeoColor.StandardColors.Black),
                                          ScreenPointF.X + 35, ScreenPointF.Y, DrawingLevel.LabelLevel);
        canvas.DrawEllipse(closestPointShape, 12, 12, new GeoSolidBrush(GeoColor.StandardColors.Purple), DrawingLevel.LevelFour);
    }
}
foreach (Feature feature in SelectedControlPointLayer.InternalFeatures)
{
    Feature[] features = new Feature[1] { feature };
    controlPointStyle.Draw(features, canvas, labelsInAllLayers, labelsInAllLayers);
}
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Drawing.GeoCanvas](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocanvas)
- [ThinkGeo.MapSuite.Shapes.Feature](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.feature)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.GeographyUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.geographyunit)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
