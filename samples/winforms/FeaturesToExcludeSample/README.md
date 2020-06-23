# Feature Ids To Exclude Sample for WinForms

### Description
The purpose of this project is to show how to use the **FeatureIdsToExclude** collection of **FeatureLayer**. In the project, you will see how you can exclude some features from being part of the **GetFeaturesNearestTo** function. Using that collection is a handy method for not taking into account some features in doing spatial queries, searching and even drawing without having to change the structure of the layer or create another layer.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/FeaturesToExcludeSample/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
LayerOverlay layerOverlay = (LayerOverlay)winformsMap1.Overlays[1];
InMemoryFeatureLayer inMemoryFeatureLayer = (InMemoryFeatureLayer)layerOverlay.Layers[0];

inMemoryFeatureLayer.FeatureIdsToExclude.Add("Dam 24");
inMemoryFeatureLayer.FeatureIdsToExclude.Add("Dam 31");

InMemoryFeatureLayer inMemoryFeatureLayer2 = (InMemoryFeatureLayer)layerOverlay.Layers[1];
PointShape pointShape = (PointShape)inMemoryFeatureLayer2.InternalFeatures[0].GetShape();

inMemoryFeatureLayer.Open();
int numberToFind = 5;
Collection<Feature> nearestFeatures = inMemoryFeatureLayer.QueryTools.GetFeaturesNearestTo(pointShape, GeographyUnit.DecimalDegree, numberToFind, ReturningColumnsType.NoColumns);

if (nearestFeatures.Count < numberToFind)
{
    numberToFind = numberToFind + (numberToFind - nearestFeatures.Count);
    nearestFeatures = inMemoryFeatureLayer.QueryTools.GetFeaturesNearestTo(pointShape, GeographyUnit.DecimalDegree, numberToFind, ReturningColumnsType.NoColumns);
}
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.layeroverlay)
- [ThinkGeo.MapSuite.Layers.InMemoryFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.inmemoryfeaturelayer)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.Layers.QueryTools](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.querytools)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
