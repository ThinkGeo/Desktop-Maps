# Google Map To Geodetic Sample for Wpf

### Description

In this Wpf project, we are showing a trick for getting the current extent of the map with Google Map as an image and displaying it on a map in decimal degrees unit. You may be in a situation where you want the details and accuracy of Google Map, especially the satellite view but want to have it displayed on your map in decimal degrees. In this sample, you will see how to get the Google Map image and create the accompanying world file for decimal degrees. Notice that we are realizing an affine transformation on the image to go to Geodetic as illustrated in the case 3
              

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/GoogleMapToGeodeticSample-ForWpf/blob/master/Screenshot.gif)

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

NativeImageRasterLayer gdiPlusRasterLayer = new NativeImageRasterLayer(@"../../data/mymap_geo.bmp");
LayerOverlay imageOverlay = new LayerOverlay();
imageOverlay.Layers.Add(gdiPlusRasterLayer);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.WorldMapKitWmsWpfOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.worldmapkitwmswpfoverlay)
- [ThinkGeo.MapSuite.Wpf.WorldMapKitProjection](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.worldmapkitprojection)
- [ThinkGeo.MapSuite.Layers.NativeImageRasterLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.nativeimagerasterlayer)
- [ThinkGeo.MapSuite.Wpf.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.layeroverlay)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
