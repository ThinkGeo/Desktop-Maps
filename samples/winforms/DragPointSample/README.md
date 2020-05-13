# Drag Point Sample for WinForms

### Description
In this project, we focus our attention on how to control the style of the control points. You will see how to override the **DrawCore** function of **EditInteractiveOverlay**. 

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/DragPointSample-ForWinForms/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
class DragInteractiveOverlay : EditInteractiveOverlay
{
    protected override void DrawCore(GeoCanvas canvas)
    {
        //Draws the Edit Shapes as default.
        Collection<SimpleCandidate> labelsInAllLayers = new Collection<SimpleCandidate>();
        EditShapesLayer.Open();
        EditShapesLayer.Draw(canvas, labelsInAllLayers);
        canvas.Flush();

        //Draws the control points. 
        ExistingControlPointsLayer.Open();
        Collection<Feature> controlPoints = ExistingControlPointsLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);

        //Loops thru the control points.
        foreach (Feature feature in controlPoints)
        {
            Feature[] features = new Feature[1] { feature };
            draggedControlPointStyle.Draw(features, canvas, labelsInAllLayers, labelsInAllLayers);
        }

        foreach (Feature feature in SelectedControlPointLayer.InternalFeatures)
        {
            Feature[] features = new Feature[1] { feature };
            controlPointStyle.Draw(features, canvas, labelsInAllLayers, labelsInAllLayers);
        }
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
- [ThinkGeo.MapSuite.Styles.SimpleCandidate](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.simplecandidate)
- [ThinkGeo.MapSuite.Shapes.Feature](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.feature)
- [ThinkGeo.MapSuite.Drawing.GeoCanvas](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocanvas)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
