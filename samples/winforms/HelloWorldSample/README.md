# Hello World Sample for WinForms

### Description
This sample shows you how to get started building your first application with the Map Suite Desktop for WinForms 10.0.0.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/HelloWorldSample/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
winformsMap1.MapUnit = GeographyUnit.Meter;
winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
winformsMap1.CurrentExtent = new RectangleShape(-13949117, 6589486, -8078754, 3042807);

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

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.WinformsMap](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.winformsmap)
- [ThinkGeo.MapSuite.GeographyUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.geographyunit)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
