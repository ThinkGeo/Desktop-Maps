# Save Load State Sample for Wpf

### Description

The purpose of this Wpf project is to show how to use the new SaveState and LoadState of the SimpleMarkerOverlay. We show how you can simply drag the icons to change their location, save their state to a file and then reload the state from that file. For this sample to work, you will need to use the latest
              
Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/SaveLoadStateSample-ForWpf/blob/master/ScreenShot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
wpfMap1.MapUnit = GeographyUnit.Meter;
wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

/*===========================================
  Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
  a Client ID and Secret. These were sent to you via email when you signed up
  with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/
WorldMapKitWmsWpfOverlay worldMapKitDesktopOverlay = new WorldMapKitWmsWpfOverlay();
wpfMap1.Overlays.Add(worldMapKitDesktopOverlay);

SimpleMarkerOverlay simpleMarkerOverlay = new SimpleMarkerOverlay();
simpleMarkerOverlay.DragMode = MarkerDragMode.Drag;
wpfMap1.Overlays.Add("SimpleMarkerOverlay", simpleMarkerOverlay);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.WorldMapKitWmsWpfOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.worldmapkitwmswpfoverlay)
- [ThinkGeo.MapSuite.Wpf.SimpleMarkerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.simplemarkeroverlay)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
