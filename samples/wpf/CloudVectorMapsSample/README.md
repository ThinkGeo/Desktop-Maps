# ThinkGeo Cloud Vector Maps Sample for Wpf

### Description

This sample demonstrates how you can draw the map with Vector Tiles requested from ThinkGeo Cloud Services in your Map Suite GIS applications, with any style you want from [StyleJSON (Mapping Definition Grammar)](https://wiki.thinkgeo.com/wiki/thinkgeo_stylejson). It will show you how to use the XyzFileBitmapTileCache and XyzFileVectorTileCache to improve the performance of map rendering. It supports have 3 built-in default map styles and more awasome styles from StyleJSON file you passed in, by 'Custom': 
- Light
- Dark
- TransparentBackground
- Custom

ThinkGeo Cloud Vector Maps support would work in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/ThinkGeoCloudVectorMapsSample-ForWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.5.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
 this.wpfMap.MapUnit = GeographyUnit.Meter;
 this.wpfMap.ZoomLevelSet = ThinkGeoCloudVectorMapsOverlay.GetZoomLevelSet();

 // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
 this.thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(cloudServiceClientId, cloudServiceClientSecret);
 this.wpfMap.Overlays.Add(this.thinkGeoCloudVectorMapsOverlay);

 // Create the overlay for satellite and overlap with trasparent_background as hybrid map.
 this.satelliteOverlay = new ThinkGeoCloudMapsOverlay(cloudServiceClientId, cloudServiceClientSecret, ThinkGeoCloudMapsMapType.Aerial);
 this.satelliteOverlay.IsVisible = false;
 this.wpfMap.Overlays.Add(this.satelliteOverlay);

 this.wpfMap.CurrentExtent = new RectangleShape(-10775293.1819701, 3866499.57476108, -10774992.2111729, 3866281.90838096);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](https://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/gis-ui-controls#wpf-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

Working...


### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
