# Popup Overlay Sample for Wpf

### Description

In this Wpf project, we explore more capabilities of the PopupOverlay and its collection of Popups. With the Content property, we can add a TextBox or any other control to a Popup. For example, here we show how to have the column value of a selected feature appear in a Popup on the user MapClick event. Notice how that column value can be easily changed by typing in the TextBox of the Popup and committing the edit.
              
Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/PopUpOverlaySample/ScreenShot.png)

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
ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay() { MapType = ThinkGeoCloudRasterMapsMapType.Hybrid };
wpfMap1.Overlays.Add(baseOverlay);

//Adds the PopupOverlay to the overlay collection of the wpf map.
ThinkGeo.MapSuite.Wpf.PopupOverlay popupOverlay = new ThinkGeo.MapSuite.Wpf.PopupOverlay();
wpfMap1.Overlays.Add("PopupOverlay", popupOverlay);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.WorldStreetsAndImageryOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.worldstreetsandimageryoverlay)
- [ThinkGeo.MapSuite.Wpf.WorldStreetsAndImageryProjection](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.worldstreetsandimageryprojection)
- [ThinkGeo.MapSuite.Wpf.PopupOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.popupoverlay)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
