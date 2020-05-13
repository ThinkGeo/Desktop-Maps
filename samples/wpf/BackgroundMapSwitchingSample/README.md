# BackgroundMapSwitchingSample-ForWpf

### Description
This sample shows you how to use these four different types of maps as background in Map Suite Desktop (WPF) Edition. 
 - **ThinkGeo Cloud Maps**
 - **Open Street Map**
 - **Bing Maps**
 - **Google Maps**

![Screenshot](https://github.com/ThinkGeo/BackgroundMapSwitchingSample-ForWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Package.

[MapSuiteDesktopForWpf-BareBone 10.2.4](https://www.nuget.org/packages/MapSuiteDesktopForWpf-BareBone/10.2.4)

### About the Code
>**Create a WorldStreetsAndImageryOverlay and add it to wpfMap1**
```csharp
/*===========================================
  Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
  a Client ID and Secret. These were sent to you via email when you signed up
  with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/
ThinkGeoCloudRasterMapsOverlay thinkgeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay();
thinkgeoCloudMapsOverlay.Name = "ThinkGeo Cloud Maps";
wpfMap1.Overlays.Add(thinkgeoCloudMapsOverlay);
```

>**Create a OpenStreetMapOverlay and add it to wpfMap1**
```csharp
OpenStreetMapOverlay openStreetMapOverlay = new OpenStreetMapOverlay();
wpfMap1.Overlays.Add(openStreetMapOverlay);
```

>**Create a BingMapsTileOverlay and add it to wpfMap1**
```csharp
BingMapsTileOverlay bingMapsOverlay = new BingMapsTileOverlay();
bingMapsOverlay.MapStyle = BingMapsMapType.AerialWithLabels;
bingMapsOverlay.ApplicationId = "Place Bing Maps Key Here";
wpfMap1.Overlays.Add(bingMapsOverlay);
```

>**Create a GoogleMapsOverlay and add it to wpfMap1**
```csharp
GoogleMapsOverlay googleMapsOverlay = new GoogleMapsOverlay();
googleMapsOverlay.MapType = GoogleMapsMapType.Satellite;
googleMapsOverlay.ClientId = "Place Google Maps ClientId Here";
googleMapsOverlay.PrivateKey = "Place Google Maps PrivateKey Here";
wpfMap1.Overlays.Add(googleMapsOverlay); 
```

### Getting Help

- [Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)
- [Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)
- [How to Use Bing Maps in Map Suite Desktop (WPF) Edition](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf_how_to_use_bingmaps)
- [How to Use Google Maps in Map Suite Desktop (WPF) Edition](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf_how_to_use_googlemaps)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
