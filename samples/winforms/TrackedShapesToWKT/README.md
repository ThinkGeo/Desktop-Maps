# Tracked Shapes To WKT Sample for WinForms

### Description

In today’s project, we show how to save a tracked shape to WKT (Well Known Text). You will notice that we make the distinction between the shape being tracked and the finished tracked shape thanks to two different events of TrackOverlay, TrackEnding and TrackEnded.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/TrackedShapesToWKTSample-ForWinForms/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0.0](https://www.nuget.org/packages?q=thinkgeo)

### About the Code
```csharp
winformsMap1.MapUnit = GeographyUnit.Meter;
winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
winformsMap1.CurrentExtent = new RectangleShape(-10453078, 3636477, -9665471, 3094267);
winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

//Event handlers for the TrackOverlay.
winformsMap1.TrackOverlay.TrackEnded += new EventHandler<TrackEndedTrackInteractiveOverlayEventArgs>(trackOverlay_TrackEnded);
winformsMap1.TrackOverlay.TrackEnding += new EventHandler<TrackEndingTrackInteractiveOverlayEventArgs>(trackOverlay_TrackEnding);
winformsMap1.TrackOverlay.TrackStarting += new EventHandler<TrackStartingTrackInteractiveOverlayEventArgs>(trackOverlay_TrackStarting);

winformsMap1.TrackOverlay.TrackMode = TrackMode.Polygon;

/*===========================================
  Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
  a Client ID and Secret. These were sent to you via email when you signed up
  with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/
ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay();
winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

winformsMap1.Refresh();
```
### Getting Help

[Map Suite Desktop for WinForms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for WinForms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.WinformsMap](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.winformsmap)
- [ThinkGeo.MapSuite.GeographyUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.geographyunit)
- [ThinkGeo.MapSuite.Shapes.RectangleShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.rectangleshape)
- [ThinkGeo.MapSuite.WinForms.BackgroundOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.backgroundoverlay)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
