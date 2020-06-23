# Display Iso Lines Sample for Wpf

### Description
In this sample we show how you can use Map Suite to add isolines (commonly known as contour lines) to your .NET application. Isolines are a way to visualize breaks between different groups of data such as elevation levels, soil properties, or just about anything else you can imagine. This sample also shows the various steps in creating isolines, including the gathering of point data, creating a grid using interpolation, and finally, picking your isoline break levels. We also quickly dive into some more advanced options such as generating isolines on the fly.

To bring this all together, check out our [instructional video](https://www.youtube.com/watch?v=eejtCTftpzo) that will walk you through the process of setting up and working with isolines in Map Suite.

Please note that you will need version 5.0.87.0 or newer of Map Suite in order to use isolines. For more information on how to upgrade, see the [Map Suite Daily Builds Guide](http://wiki.thinkgeo.com/wiki/map_suite_daily_builds_guide).

From 6.0.187.0, the sample has been updated that polygons can also be returned as IsoLines results. You need version 6.0.187.0 or newer of Map Suite in order to use this sample.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/DisplayIsoLinesSample/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
public class DynamicGridFeatureLayer : InMemoryGridFeatureLayer
{
    protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
    {
        double resolution = GetResolutionFromScale(canvas.CurrentScale, canvas.MapUnit);

        double cellSize = Math.Min(cellHeightInPixel * resolution, cellWidthInPixel * resolution);
        GridDefinition gridDefinition = new GridDefinition(canvas.CurrentWorldExtent, cellSize, NoDataValue, wellDepthPoints);
        GridCell[,] gridMatrix = GridFeatureSource.GenerateGridMatrix(gridDefinition, InterpolationModel);

        this.GridMatrix = gridMatrix;

        this.FeatureSource.Close();
        this.FeatureSource.Open();

        base.DrawCore(canvas, labelsInAllLayers);
    }
}
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.InMemoryGridFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.inmemorygridfeaturelayer)
- [ThinkGeo.MapSuite.Drawing.GeoCanvas](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocanvas)
- [ThinkGeo.MapSuite.Styles.SimpleCandidate](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.simplecandidate)
- [ThinkGeo.MapSuite.Layers.GridDefinition](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.griddefinition)
- [ThinkGeo.MapSuite.Layers.GridCell](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.gridcell)
- [ThinkGeo.MapSuite.Layers.GridFeatureSource](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.gridfeaturesource)

### FAQ
- __Q: How do I make background map work?__
A: Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require a Client ID and Secret. These were sent to you via email when you signed up with ThinkGeo, or you can register now at https://cloud.thinkgeo.com. Once you get them, please update the code in method LayoutRoot_Loaded in MainViewModel.cs.


### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
