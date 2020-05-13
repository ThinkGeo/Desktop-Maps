# Four Color Map Sample for Wpf

### Description

Any map can be colored using four colors in such a way that adjacent regions receive different colors. 
The sample discover how to use ShapeFileFeatureLayer to get four color features, then use ValueStyle to render the four color map.

At present, the four color map only supports polygon, and doesn't support point and line. 

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/FourColorMapSample-ForWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
Collection<Feature> fourColorFeatures = shapeFileLayer.FeatureSource.GetFeaturesForFourColor();
```

### Getting Help

- [Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)
- [Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.FeatureSource](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.FeatureSource)
- [ThinkGeo.MapSuite.Styles.ValueStyle](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Styles.ValueStyle)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
