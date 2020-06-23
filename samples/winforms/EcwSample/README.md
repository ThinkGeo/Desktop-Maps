# Ecw Sample for WinForms

### Description
The Ecw Sample template represents an ECW file for drawing on the map.

ECW: ECW is a wavelet image compression system developed by ER Mapper.It allows you to combine and compress large sets of satellite images into a single file. 
The images can be accessed very quickly at a variety of scales. It is very popular in the GIS community.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/EcwSample/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
LayerOverlay overlay = new LayerOverlay();

//To resolve issue that we cannot run the executable by double click it on linux, we need to find out the absolute path by reflection.
string baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
string ecwLayerFileName = Path.GetFullPath (Path.Combine (baseDirectory, "../../App_Data/World.ecw"));

EcwRasterLayer ecwRasterLayer = new EcwRasterLayer(ecwLayerFileName);
overlay.Layers.Add(ecwRasterLayer);
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.layeroverlay)
- [ThinkGeo.MapSuite.Layers.EcwRasterLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.ecwrasterlayer)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
