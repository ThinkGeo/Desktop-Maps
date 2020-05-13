# Building 3D Sample for Wpf

### Description
This project shows to create simulated 3D buildings on WPF map control and Shapefile.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/Building3DSample-forWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
OsmBuildingOverlay buildingOverlay = new OsmBuildingOverlay();
ShapeFileFeatureLayer buildingShapeLayer = new ShapeFileFeatureLayer(buildingFilePath);
buildingShapeLayer.Open();
buildingOverlay.FeatureLayer = buildingShapeLayer;
Map1.Overlays.Add(buildingOverlay);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.OsmBuildingOverlay](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Wpf.OsmBuildingOverlay)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.

