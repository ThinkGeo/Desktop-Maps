# Wmts Layer Sample for Wpf

### Description

This project shows how to consume data from a WMTS Server using WmtsLayer. You would find the code pretty straightforward, just like displaying a shapefile, while behind the scenes we request tiles from the server asynchronously and efficiently, and stitch them into a proper map. 

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/WmtsLayerSample/Screenshot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
map.MapUnit = GeographyUnit.Meter;
map.CurrentExtent = new RectangleShape(-13450952.9269994, 8588337.56133263, 5764694.11157119, -3680716.09771399);

ThinkGeo.MapSuite.Layers.WmtsLayer wmtsLayer = new ThinkGeo.MapSuite.Layers.WmtsLayer();
wmtsLayer.DrawingExceptionMode = DrawingExceptionMode.DrawException;
wmtsLayer.WmtsSeverEncodingType = ThinkGeo.MapSuite.Layers.WmtsSeverEncodingType.Restful;
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.GeographyUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.geographyunit)
- [ThinkGeo.MapSuite.Shapes.RectangleShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.rectangleshape)
- [ThinkGeo.MapSuite.Layers.WmtsLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.wmtslayer)
- [ThinkGeo.MapSuite.Drawing.DrawingExceptionMode](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.drawingexceptionmode)
- [ThinkGeo.MapSuite.Layers.WmtsSeverEncodingType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.wmtsseverencodingtype)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
