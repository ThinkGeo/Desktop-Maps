# Quickstart Sample for WinForms

### Description
The Winforms QuickStart Guide will guide you through the process of creating a sample application and will help you become familiar with Map Suite. This QuickStart Guide supports Map Suite 10.0.0.0 and higher and will show you how to create a Winforms application.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/QuickstartSample-ForWinForms/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ShapeFileFeatureLayer capitalLabelLayer = new ShapeFileFeatureLayer(@"../../Data/WorldCapitals.shp");
// We can customize our own TextStyle. Here we passed in the font, the size, the style and the color.
GeoFont font = new GeoFont("Arial", 9, DrawingFontStyles.Bold);
GeoSolidBrush txtBrush = new GeoSolidBrush(GeoColor.StandardColors.Maroon);
TextStyle textStyle = new TextStyle("CITY_NAME", font, txtBrush);
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:
- [ThinkGeo.MapSuite.Layers.ShapeFileFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefilefeaturelayer)
- [ThinkGeo.MapSuite.Drawing.GeoFont](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geofont)
- [ThinkGeo.MapSuite.Drawing.DrawingFontStyles](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.drawingfontstyles)
- [ThinkGeo.MapSuite.Drawing.GeoSolidBrush](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geosolidbrush)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)
- [ThinkGeo.MapSuite.Styles.TextStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.textstyle)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
