# ThinkGeo Cloud Maps Sample for Wpf

### Description

This sample demonstrates how you can display ThinkGeo Cloud Maps in your Map Suite GIS applications. It will show you how to use the XYZFileBitmapTileCache to improve the performance of map rendering. ThinkGeoCloudMapsOverlay uses the ThinkGeo Cloud XYZ Tile Server as raster map tile server. It supports 5 different map styles: 
- Light
- Dark
- Aerial
- Hybrid
- TransparentBackground

ThinkGeo Cloud Maps support would work in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/ThinkGeoCloudMapsSample-ForWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
map.MapUnit = GeographyUnit.Meter;
map.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

/*===========================================
  Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
  a Client ID and Secret. These were sent to you via email when you signed up
  with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/
thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay();
thinkGeoCloudMapsOverlay.WrappingMode = WrappingMode.WrapDateline;
// Tiles will be cached in the TEMP folder (%USERPROFILE%\AppData\Local\Temp\MapSuite\PersistentCaches) by default if the TileCache property is not set.
thinkGeoCloudMapsOverlay.TileCache = new XyzFileBitmapTileCache("ThinkGeoCloudMapsTileCache");
map.Overlays.Add(thinkGeoCloudMapsOverlay);

map.CurrentExtent = new ThinkGeo.MapSuite.Shapes.RectangleShape(-20037508.2314698, 20037508.2314698, 20037508.2314698, -20037508.2314698);
map.Refresh();
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_Wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#wpf-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

Working...


### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
