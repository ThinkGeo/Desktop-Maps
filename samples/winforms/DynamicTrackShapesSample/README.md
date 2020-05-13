# Dynamic Track Shapes Sample for WinForms

### Description
This Desktop project shows how to handle **TrackOverlay** to obtain dynamic information about the shape being tracked.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/DynamicTrackShapesSample-ForWinForms/blob/master/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
EcwRasterLayer ecwImageLayer = new EcwRasterLayer("../../Data/World.ecw");
ecwImageLayer.UpperThreshold = double.MaxValue;
ecwImageLayer.LowerThreshold = 0;
LayerOverlay imageOverlay = new LayerOverlay();
imageOverlay.Layers.Add("EcwImageLayer", ecwImageLayer);
winformsMap1.Overlays.Add(imageOverlay);

winformsMap1.TrackOverlay.TrackStarted += new EventHandler<TrackStartedTrackInteractiveOverlayEventArgs>(TrackStarted);
winformsMap1.TrackOverlay.MouseMoved += new EventHandler<MouseMovedTrackInteractiveOverlayEventArgs>(MouseMoved);
winformsMap1.TrackOverlay.TrackEnded += new EventHandler<TrackEndedTrackInteractiveOverlayEventArgs>(TrackEnded);
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.EcwRasterLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.ecwrasterlayer)
- [ThinkGeo.MapSuite.WinForms.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.layeroverlay)
- [ThinkGeo.MapSuite.WinForms.TrackInteractiveOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.trackinteractiveoverlay)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
