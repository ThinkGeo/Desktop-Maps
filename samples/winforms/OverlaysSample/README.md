# Overlays Sample for WinForms

### Description

Discover how to use Overlays to build up your map, or to add existing basemaps to your application. 
The sample can show the following four basemaps:
  1. Google Maps
  2. Bing Maps
  3. World Kit Maps
  4. Open street Maps

It can display different styles of maps by setting the map type.


Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/OverlaysSample/Screenshot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
GoogleMapsOverlay googleMapOverlay = new GoogleMapsOverlay();
googleMapOverlay.IsVisible = false;
winformsMap1.Overlays.Add("Google Maps", googleMapOverlay);
cboBaseMap.Items.Add("Google Maps");

BingMapsOverlay bingMapOverlay = new BingMapsOverlay();
bingMapOverlay.ApplicationId = ""; //Please set your application id.
bingMapOverlay.IsVisible = false;
winformsMap1.Overlays.Add("Bing Maps", bingMapOverlay);
cboBaseMap.Items.Add("Bing Maps");

/*===========================================
  Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
  a Client ID and Secret. These were sent to you via email when you signed up
  with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/
ThinkGeoCloudRasterMapsOverlay worldOverlay = new ThinkGeoCloudRasterMapsOverlay();
worldOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light;
worldOverlay.IsVisible = true;
winformsMap1.Overlays.Add("Cloud Maps", worldOverlay);
cboBaseMap.Items.Add("Cloud Maps");
cboBaseMap.SelectedItem = "Cloud Maps";

OpenStreetMapOverlay osmOverlay = new OpenStreetMapOverlay();
osmOverlay.IsVisible = false;
winformsMap1.Overlays.Add("Open Street Map", osmOverlay);
cboBaseMap.Items.Add("Open Street Map");
```

### Getting Help

- [Map Suite Desktop for WinForms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)
- [Map Suite Desktop for WinForms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs

This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.WinformsMap](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.WinForms.WinformsMap)
- [ThinkGeo.MapSuite.WinForms.GoogleMapsOverlay](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.WinForms.GoogleMapsOverlay)
- [ThinkGeo.MapSuite.WinForms.BingMapsOverlay](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.WinForms.BingMapsOverlay)
- [ThinkGeo.MapSuite.WinForms.OpenStreetMapOverlay](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.WinForms.OpenStreetMapOverlay)
- [ThinkGeo.MapSuite.WinForms.WorldStreetsAndImageryOverlay](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.WinForms.WorldStreetsAndImageryOverlay)

### About Map Suite

Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo

ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
