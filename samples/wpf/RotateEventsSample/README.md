# Rotate Events Sample for Wpf

### Description

This sample shows how to take the advantage of a touchable screen, to play with the map using fingers. You would see we can pan the map with one finger, zoom in/out or rotate a map using 2 fingers. Not only that, we can add a marker, and track/edit a shape (point, line or polygons) by tapping on the screen. And marker/popup/label won't rotate with the map. It is straightforward to use and checking out the code, you would see it is very simple to implement with Map Suite! It is available in 8.0.48.0 or later. 

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/RotateEventsSample/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
SimpleMarkerOverlay markerOverlay = new SimpleMarkerOverlay();
Map1.Overlays.Add("MarkerOverlay", markerOverlay);

PopupOverlay popupOverlay = new PopupOverlay();
Map1.Overlays.Add("PopupOverlay", popupOverlay);
Popup popup = new Popup(new PointShape(-10777662.2854073, 3912165.79621789));
popup.Content = new TextBlock() { Text = "ThinkGeo", FontSize = 20 };
popupOverlay.Popups.Add(popup);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.SimpleMarkerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.simplemarkeroverlay)
- [ThinkGeo.MapSuite.Wpf.PopupOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.popupoverlay)
- [ThinkGeo.MapSuite.Wpf.Popup](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.popup)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense. 
