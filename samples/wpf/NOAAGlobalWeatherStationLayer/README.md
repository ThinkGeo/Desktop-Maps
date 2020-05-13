# NOAA Globel Weather Station Layer Sample for Wpf

### Description

This WPF project, demonstrates how to query and display real time NOAA weather station data directly as a **Layer**, which allows you to display up-to-date weather station data from around the world on top of your maps. The weather station data is sourced from NOAA and is refreshed every 15 minutes in the background. As these classes are inherited from the **FeatureSource** and **FeatureLayer**, many properties are querable and can be used to create a variety of maps or analysis of current weather patterns. This feature can be applied in all Map Suite products.             

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/NOAAGlobelWeatherStationLayerSample-ForWpf/blob/master/ScreenShot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
NoaaWeatherStationFeatureLayer noaaWeatherStationFeatureLayer = new NoaaWeatherStationFeatureLayer();
noaaWeatherStationFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new NoaaWeatherStationStyle());
noaaWeatherStationFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.NoaaWeatherStationFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.noaaweatherstationfeaturelayer)
- [ThinkGeo.MapSuite.Layers.NoaaWeatherStationStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.noaaweatherstationstyle)
- [ThinkGeo.MapSuite.Layers.ApplyUntilZoomLevel](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.applyuntilzoomlevel)

### FAQ
- __Q: How do I make background map work?__  
A: Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require a Client ID and Secret. These were sent to you via email when you signed up with ThinkGeo, or you can register now at https://cloud.thinkgeo.com. Once you get them, please update the code in MainWindow.xaml.cs.  

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
