# Wmts Tiled Overlay Sample for Wpf

### Description

This project shows how to consume data from a WMTS Server using WmtsOverlay. You would find the code pretty straightforward, while behind the scenes we request tiles from the server asynchronously and efficiently, and stitch them into a proper map.  This class is introduced from version 6.0.187.0, besides this WmtsOverlay, we also have WmtsLayer available.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/WmtsTileOverlaySample/Screenshot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
Map1.CurrentExtent = new RectangleShape(33707536, 18538567, 52962281, 7013115);
WmtsTiledOverlay wmtsTiledOverlay = new WmtsTiledOverlay(new Collection<Uri> { new Uri("http://opencache.statkart.no/gatekeeper/gk/gk.open_wmts/") });
wmtsTiledOverlay.WmtsServerEncodingType = WmtsSeverEncodingType.Kvp;
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Shapes.RectangleShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.rectangleshape)
- [ThinkGeo.MapSuite.Wpf.WmtsTiledOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.wmtstiledoverlay)
- [ThinkGeo.MapSuite.Wpf.WmtsSeverEncodingType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.wmtsseverencodingtype)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
