# Vehicle Tracking Sample for WinForms

### Description

The Vehicle Tracking sample template gives you a head start on your next tracking project. With a working code example to draw from, you can spend more of your time implementing the features you care about and less time thinking about how to accomplish the basic functionality of a tracking system.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

Because this sample needs Microsoft.Jet.OLEDB provider, please **run in x86 mode**.
![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/VehicleTrackingSample/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
WorldMapKitWmsDesktopOverlay worldMapKitRoadOverlay = new WorldMapKitWmsDesktopOverlay();
worldMapKitRoadOverlay.Name = Resources.WorldMapKitRoadOverlay;
worldMapKitRoadOverlay.Projection = WorldMapKitProjection.SphericalMercator;
worldMapKitRoadOverlay.IsVisible = true;
worldMapKitRoadOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.WorldMapKitRoadOverlay);
worldMapKitRoadOverlay.MapType = WorldMapKitMapType.Road;
winformsMap.Overlays.Add(Resources.WorldMapKitRoadOverlay, worldMapKitRoadOverlay);
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.WorldMapKitWmsDesktopOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.worldmapkitwmsdesktopoverlay)
- [ThinkGeo.MapSuite.Layers.FileBitmapTileCache](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.filebitmaptilecache)
- [ThinkGeo.MapSuite.WinForms.WorldMapKitProjection](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.worldmapkitprojection)
- [ThinkGeo.MapSuite.Layers.WorldMapKitMapType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.worldmapkitmaptype)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
