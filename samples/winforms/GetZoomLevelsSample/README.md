# Get Zoom Level Sample for WinForms

### Description
This example demonstrates how to get the zoom level of the map each time we change its extent. Using custom zoom levels, you will see how to get the zoom level with its characteristics such as the upper and lower scale defining it. You can read the comments inside the project to better understand the relationship of scales with zoom levels.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/GetZoomLevelsSample/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ZoomLevelSet partitionedZoomLevelSet = new ZoomLevelSet();
partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(200000));
partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(100000));
partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(50000));
partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(25000));
partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(10000));
partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(5000));
partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(2500));
partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(1000));
partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(500));
partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(250));
winformsMap1.ZoomLevelSet = partitionedZoomLevelSet;

Collection<ZoomLevel> zoomLevels = winformsMap1.ZoomLevelSet.GetZoomLevels();
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.ZoomLevelSet](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.zoomlevelset)
- [ThinkGeo.MapSuite.Layers.ZoomLevel](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.zoomlevel)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
