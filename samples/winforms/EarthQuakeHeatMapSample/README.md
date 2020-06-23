# Earthquakes Heat Map Sample for WinForms

### Description
After the project on displaying swine flu data using the heat map technique, here you learn how to apply parameters other than strictly spatial distribution to affect the coloring of the map.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/EarthQuakeHeatMapSample/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(@"../../data/quksigx020.shp");
HeatLayer heatLayer = new HeatLayer(featureSource);
HeatStyle heatStyle = new HeatStyle();
heatStyle.IntensityColumnName = "other_mag1";
heatStyle.IntensityRangeStart = 0;
heatStyle.IntensityRangeEnd = 12;
heatStyle.Alpha = 180;
heatStyle.PointRadius = 60; 
heatStyle.PointRadiusUnit = DistanceUnit.Kilometer;
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:
- [ThinkGeo.MapSuite.Layers.HeatLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.heatlayer)
- [ThinkGeo.MapSuite.Layers.HeatStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.heatstyle)
- [ThinkGeo.MapSuite.Layers.ShapeFileFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefilefeaturelayer)
- [ThinkGeo.MapSuite.Shapes.DistanceUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.distanceunit)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
