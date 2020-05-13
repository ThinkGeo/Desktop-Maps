# Get Features Clicked On Sample for WinForms

### Description
The purpose of this project is to show the technique for finding the feature the user clicked on. To give the user the expected behavior, a buffer in screen coordinates needs to be set so that the feature gets selected within a constant distance in screen coordinates to where the user clicked, regardless of the zoom level. 

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/GetFeaturesClickedOnSample-ForWinForms/blob/master/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
int screenBuffer = 15;

ScreenPointF clickedPointF = new ScreenPointF(e.ScreenX, e.ScreenY);
ScreenPointF bufferPointF = new ScreenPointF(clickedPointF.X + screenBuffer, clickedPointF.Y);

double distanceBuffer = ExtentHelper.GetWorldDistanceBetweenTwoScreenPoints(winformsMap1.CurrentExtent, clickedPointF, bufferPointF,
                                                    winformsMap1.Width, winformsMap1.Height, winformsMap1.MapUnit, DistanceUnit.Meter);
ShapeFileFeatureLayer streetLayer = (ShapeFileFeatureLayer)winformsMap1.FindFeatureLayer("StreetLayer");
Collection<string> columnNames = new Collection<string>();
columnNames.Add("FENAME");

Collection<Feature> features = streetLayer.FeatureSource.GetFeaturesNearestTo(new PointShape(e.WorldX, e.WorldY), winformsMap1.MapUnit, 1, columnNames, distanceBuffer, DistanceUnit.Meter);

InMemoryFeatureLayer selectLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("SelectLayer");

selectLayer.InternalFeatures.Clear();

if (features.Count > 0)
{
    selectLayer.InternalFeatures.Add(features[0]);
}

winformsMap1.Refresh(winformsMap1.Overlays["SelectOverlay"]);
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:
- [ThinkGeo.MapSuite.Layers.ShapeFileFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefilefeaturelayer)
- [ThinkGeo.MapSuite.Layers.InMemoryFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.inmemoryfeaturelayer)
- [ThinkGeo.MapSuite.Shapes.Feature](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.feature)
- [ThinkGeo.MapSuite.Shapes.ExtentHelper](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.extenthelper)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
