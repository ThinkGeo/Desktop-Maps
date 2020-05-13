# Quickstart Sample for Wpf

### Description
The Map Suite WPF QuickStart Guide will guide you through the process of creating a sample application and will help you become familiar with Map Suite. This QuickStart Guide supports Map Suite 10.0.0.0 and higher and will show you how to create a WPF application using Map Suite WPF components.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/QuickstartSample-ForWpf/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ShapeFileFeatureLayer capitalLabelLayer = new ShapeFileFeatureLayer(@"../../Data/WorldCapitals.shp");
GeoFont font = new GeoFont("Arial", 9, DrawingFontStyles.Bold);
GeoSolidBrush txtBrush = new GeoSolidBrush(GeoColor.StandardColors.Maroon);
TextStyle textStyle = new TextStyle("CITY_NAME", font, txtBrush);
textStyle.XOffsetInPixel = 0;
textStyle.YOffsetInPixel = -6;
capitalLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.CreateSimpleTextStyle("CITY_NAME", "Arial", 8, DrawingFontStyles.Italic, GeoColor.StandardColors.Black, 3, 3);
capitalLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level05;

capitalLabelLayer.ZoomLevelSet.ZoomLevel06.DefaultTextStyle = textStyle;
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.ShapeFileFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefilefeaturelayer)
- [ThinkGeo.MapSuite.Drawing.GeoFont](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geofont)
- [ThinkGeo.MapSuite.Drawing.GeoSolidBrush](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geosolidbrush)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)
- [ThinkGeo.MapSuite.Styles.TextStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.textstyle)
- [ThinkGeo.MapSuite.Styles.TextStyles](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.textstyles)
- [ThinkGeo.MapSuite.Drawing.DrawingFontStyles](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.drawingfontstyles)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
