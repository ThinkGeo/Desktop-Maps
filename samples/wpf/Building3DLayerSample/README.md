# Building 3D Layer Sample for Wpf

### Description
This project shows to create simulated 3D buildings on WPF map control and OsmBuildingOnlineServiceFeatureLayer.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/Building3DLayerSample/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
BuildingOverlay buildingsOverlay = new BuildingOverlay();
OsmBuildingOnlineServiceFeatureLayer osmBuildingOnlineServiceFeatureLayer = new OsmBuildingOnlineServiceFeatureLayer();
osmBuildingOnlineServiceFeatureLayer.Open();
buildingsOverlay.FeatureLayer = osmBuildingOnlineServiceFeatureLayer;
buildingsOverlay.TileType = TileType.SingleTile;
map.Overlays.Add(buildingsOverlay);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.OsmBuildingOverlay](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Wpf.OsmBuildingOverlay)
- [ThinkGeo.MapSuite.Layers.OsmBuildingOnlineServiceFeatureLayer](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.OsmBuildingOnlineServiceFeatureLayer)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
