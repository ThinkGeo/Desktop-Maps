# Color Replacement Sample for Wpf

### Description
In today’s WPF project, we show you how to replace a specific color in a raster image, with the advantage of new added API Color Mapping. For the example, in this project, the lake in Green can be replaced with blue.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ColorReplacementSample/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
/*===========================================
  Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
  a Client ID and Secret. These were sent to you via email when you signed up
  with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/
ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay() { MapType = ThinkGeoCloudRasterMapsMapType.Aerial };
wpfMap1.Overlays.Add(baseOverlay);

LayerOverlay layerOverlay = new LayerOverlay();
layerOverlay.TileType = TileType.SingleTile;
wpfMap1.Overlays.Add("layerOverlay", layerOverlay);

RectangleShape targetExtent = new RectangleShape(-11191114.7309915, 4674772.10425472, -11189708.4809915, 4673647.10425472);
NativeImageRasterLayer rasterLayer = new NativeImageRasterLayer(@"..\..\App_Data\Lake.png", targetExtent);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.WorldStreetsAndImageryOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.worldstreetsandimageryoverlay)
- [ThinkGeo.MapSuite.Wpf.WorldStreetsAndImageryProjection](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.worldstreetsandimageryprojection)
- [ThinkGeo.MapSuite.Layers.WorldStreetsAndImageryMapType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.worldstreetsandimagerymaptype)
- [ThinkGeo.MapSuite.Wpf.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.layeroverlay)
- [ThinkGeo.MapSuite.Wpf.TileType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.tiletype)
- [ThinkGeo.MapSuite.Shapes.RectangleShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.rectangleshape)
- [ThinkGeo.MapSuite.Layers.NativeImageRasterLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.nativeimagerasterlayer)

### FAQ
- __Q: How do I make background map work?__
A: Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require a Client ID and Secret. These were sent to you via email when you signed up with ThinkGeo, or you can register now at https://cloud.thinkgeo.com. Once you get them, please update the code in method Window_Loaded() in MainWindow.xaml.cs.

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
