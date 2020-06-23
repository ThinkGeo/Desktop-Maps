# Combine Overlay Sample for Wpf

### Description

In today’s Wpf project, we show a technique of using a common FeatureSource for two different Overlays. From a physical shapefile representing cities, a regular LayerOverlay is used for the higher zoom levels while a FeatureSourceMarkerOverlay is used for the lower zoom levels. FeatureSourceMarkerOverlay is a Wpf specific overlay offering features for a better user experience such a Tooltips and ImageSource.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CombineOverlaySample/Screenshot.gif)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
LayerOverlay layerOverlay = new LayerOverlay();
layerOverlay.Layers.Add(shapeFileFeatureLayer);
wpfMap1.Overlays.Add(layerOverlay);

FeatureSourceMarkerOverlay markerOverlay = new FeatureSourceMarkerOverlay();
ShapeFileFeatureSource shapeFileFeatureSource = (ShapeFileFeatureSource)shapeFileFeatureLayer.FeatureSource;
markerOverlay.FeatureSource = shapeFileFeatureSource;
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.layeroverlay)
- [ThinkGeo.MapSuite.Wpf.FeatureSourceMarkerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.featuresourcemarkeroverlay)
- [ThinkGeo.MapSuite.Layers.ShapeFileFeatureSource](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefilefeaturesource)

### FAQ
- __Q: How do I make background map work?__
A: Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require a Client ID and Secret. These were sent to you via email when you signed up with ThinkGeo, or you can register now at https://cloud.thinkgeo.com. Once you get them, please update the code in method Window_Loaded() in TestWindow .cs.

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
