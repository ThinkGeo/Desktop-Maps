# Jpeg2000 Sample for WinForms

### Description
The Jpeg2000 Sample template represents a .JP2 (JPEG2000) image type to be drawn on the map.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://raw.githubusercontent.com/ThinkGeo/Jpeg2000Sample-ForWinForms/master/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
winformsMap1.MapUnit = GeographyUnit.Meter;

LayerOverlay overlay = new LayerOverlay();

Jpeg2000RasterLayer jpeg2000RasterLayer = new Jpeg2000RasterLayer("../../App_Data/World.jp2");
overlay.Layers.Add(jpeg2000RasterLayer);

winformsMap1.Overlays.Add(overlay);

jpeg2000RasterLayer.Open();
winformsMap1.CurrentExtent = jpeg2000RasterLayer.GetBoundingBox();

winformsMap1.Refresh();
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.WinformsMap](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.winformsmap)
- [ThinkGeo.MapSuite.WinForms.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.layeroverlay)
- [ThinkGeo.MapSuite.Layers.Jpeg2000RasterLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.jpeg2000rasterlayer)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
