# Tracked Shapes To File Sample for Wpf

### Description

In this Wpf sample, we show how to persist the tracked shapes by saving the WKT (Well Known Text) to a file using the TrackEnded event. Also, you can see how to retrieve the WKT from files to create the features for the TrackShapeLayer of the TrackOverlay when loading the map. You will need the MapSuiteCore.dll and WpfDesktopEdition.dll references for this sample.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/TrackedShapeFile/Screenshot.gif)

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
ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay();
wpfMap1.Overlays.Add(baseOverlay);

wpfMap1.TrackOverlay.TrackEnded += new EventHandler<TrackEndedTrackInteractiveOverlayEventArgs>(trackOverlay_TrackEnded);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.WorldMapKitWmsWpfOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.worldmapkitwmswpfoverlay)
- [ThinkGeo.MapSuite.Wpf.TrackInteractiveOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.trackinteractiveoverlay)
- [ThinkGeo.MapSuite.Wpf.TrackEndedTrackInteractiveOverlayEventArgs](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.trackendedtrackinteractiveoverlayeventargs)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
