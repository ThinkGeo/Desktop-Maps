# Display File GeoDatabase Sample for Wpf

### Description

This sample demonstrates how you can read data from an ESRI FileGeodatabase, and you will find the code as straightforward as consuming any other data source in Map Suite.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/DisplayFileGeoDatabaseSample-ForWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
FileGeoDatabaseFeatureLayer fileGeoDatabaseFeatureLayer = new FileGeoDatabaseFeatureLayer("../../AppData/Shapes.gdb", "states");
AreaStyle areaStyle = new AreaStyle();
areaStyle.FillSolidBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214));
areaStyle.OutlinePen = new GeoPen(GeoColor.FromArgb(255, 118, 138, 69), 1);
areaStyle.OutlinePen.DashStyle = LineDashStyle.Solid;
fileGeoDatabaseFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = areaStyle;
fileGeoDatabaseFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
```

### Getting Help

- [Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)
- [Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.FileGeoDatabaseFeatureLayer](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.ThinkGeo.MapSuite.Layers.FileGeoDatabaseFeatureLayer)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
