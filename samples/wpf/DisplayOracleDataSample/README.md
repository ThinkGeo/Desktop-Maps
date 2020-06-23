# Display Oracle Data Sample for Wpf

### Description

Discover how to use OracleFeatureLayer to build up your map. Use oracle data to render map.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/DisplayOracleDataSample/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
var oracleFeatureLayer = new OracleFeatureLayer("User ID=system;Password=tg2017;Data Source=192.168.0.101/orcl;", "AUSTINSTREETS1", "ID");
oracleFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(new GeoSolidBrush(GeoColor.SimpleColors.Copper), 2.0f));
oracleFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
```

### Getting Help

- [Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)
- [Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.OracleFeatureLayer](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.OracleFeatureLayer)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
