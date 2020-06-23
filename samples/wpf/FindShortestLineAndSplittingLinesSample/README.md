# Find Shortest Line And Splitting Lines Sample for Wpf

### Description

This sample will show you how to find the closest line between two features by using the GetShortestLineTo API and how to split a line at a given point using the GetLineOnALine API. These APIs can be very useful when doing spatial analysis and editing of features. This sample also allows you to dynamically alter the test features using the EditOverlay so you can try out different scenarios and see the results quickly. A MapShapes layer is used to display and style the individual results.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/FindShortestLineAndSplittingLinesSample/Screenshot.gif)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
MapShapeLayer results = (MapShapeLayer)((LayerOverlay)wpfMap1.Overlays["layerOverlay"]).Layers["Results"];
results.MapShapes.Clear();

//Get a reference to the test data layer.
InMemoryFeatureLayer testDataLayer = (InMemoryFeatureLayer)((LayerOverlay)wpfMap1.Overlays["layerOverlay"]).Layers["TestData"];
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.MapShapeLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.mapshapelayer)
- [ThinkGeo.MapSuite.Layers.InMemoryFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.inmemoryfeaturelayer)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
