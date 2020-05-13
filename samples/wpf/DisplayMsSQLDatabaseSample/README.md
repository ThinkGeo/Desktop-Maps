# Display MsSql GeoDatabase Sample for Wpf

### Description

This sample demonstrates how you can read data from an MsSql database, and you will find the code as straightforward as consuming any other data source in Map Suite.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/DisplayMsSqlDatabaseSample-ForWpf/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
LayerOverlay layerOverlay = new LayerOverlay();
string connectString = "Data Source=MsSqlServer;Initial Catalog=DatabaseName;Persist Security Info=True;User ID=username;Password=password";
MsSqlFeatureLayer sqlLayer = new MsSqlFeatureLayer(connectString, "tableName", "featureId");
sqlLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(new GeoImage(@"..\..\AppData\marker.png"));
sqlLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
layerOverlay.Layers.Add(sqlLayer);
map.Overlays.Add(layerOverlay);
```

### Getting Help

- [Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)
- [Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.MsSqlFeatureLayer](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.ThinkGeo.MapSuite.Layers.MsSqlFeatureLayer)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
