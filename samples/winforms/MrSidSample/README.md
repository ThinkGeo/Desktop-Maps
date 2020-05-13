# Mr Sid Sample for WinForms

### Description
MrSID (pronounced Mister Sid) is an acronym that stands for multiresolution seamless image database. In order to run this sample, development build 10.0.0.0 or later is required.

This sample includes a map with MrSID as base overlay, the MrSID shows the image data of the world range.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://raw.githubusercontent.com/ThinkGeo/MrSidSample-ForWinForms/master/ScreenShot.png)

**Known issue:** The screenshot is taken under Linux, but it not well tested, maybe sometimes runs to exception. 

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
LayerOverlay overlay = new LayerOverlay();

MrSidRasterLayer mrSidRasterLayer = new MrSidRasterLayer("../../App_Data/World.sid");
overlay.Layers.Add(mrSidRasterLayer);

winformsMap1.Overlays.Add(overlay);

mrSidRasterLayer.Open();
winformsMap1.CurrentExtent = mrSidRasterLayer.GetBoundingBox();
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.layeroverlay)
- [ThinkGeo.MapSuite.Layers.MrSidRasterLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.mrsidrasterlayer)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
