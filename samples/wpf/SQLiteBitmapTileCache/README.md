# SQLite Bitmap Tile Cache Sample for Wpf

### Description

This sample shows how you can cache the tile images in SQLite database.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/SQLiteBitmapTileCacheSample-ForWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages/ThinkGeo.MapSuite/)

[MapSuiteDesktopForWpf-BareBone 10.0.0](https://www.nuget.org/packages/MapSuiteDesktopForWpf-BareBone)

[MapSuiteDependency-SQLiteCore 10.0.0](https://www.nuget.org/packages/MapSuiteDependency-SQLiteCore/)

### About the Code
```csharp
/*===========================================
  Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
  a Client ID and Secret. These were sent to you via email when you signed up
  with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/
ThinkGeoCloudRasterMapsOverlay layerOverlay = new ThinkGeoCloudRasterMapsOverlay();
layerOverlay.TileCache = new SQLiteBitmapTileCache();
```
### Getting Help
[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.BitmapTileCache](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.bitmaptilecache)
- [ThinkGeo.MapSuite.Wpf.WorldStreetsAndImageryOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.worldstreetsandimageryoverlay)

### FAQ
- __Q: How do I make background map work?__  
A: Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require a Client ID and Secret. These were sent to you via email when you signed up with ThinkGeo, or you can register now at https://cloud.thinkgeo.com. Once you get them, please update the code in MainWindow.xaml.cs.  

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
