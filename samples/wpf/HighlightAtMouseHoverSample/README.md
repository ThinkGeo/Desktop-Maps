# Highlight At Mouse Hover Sample for Wpf

### Description

In this WPF project, we show you how to highlight a feature when the user hovers the mouse pointer over it. This effect can be realized in Map Suite by using a timer and doing some spatial querying based on the location of the mouse pointer. This sample works with area-based features but the same could be accomplished with point- or line-based features simply by using different spatial queries.
              
Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/HighlightAtMouseHoverSample/ScreenShot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
LayerOverlay highlightOverlay = (LayerOverlay)wpfMap1.Overlays["HighlightOverlay"];
ShapeFileFeatureLayer featureLayer = (ShapeFileFeatureLayer)((LayerOverlay)wpfMap1.Overlays["LayerFeatureOverlay"]).Layers["StatesLayer"];
InMemoryFeatureLayer highlightLayer = (InMemoryFeatureLayer)highlightOverlay.Layers["HighlightLayer"];

featureLayer.Open();
Collection<Feature> features = featureLayer.QueryTools.GetFeaturesContaining(point, ReturningColumnsType.NoColumns);
featureLayer.Close();
```

### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.layeroverlay)
- [ThinkGeo.MapSuite.Layers.ShapeFileFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefilefeaturelayer)
- [ThinkGeo.MapSuite.Layers.InMemoryFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.inmemoryfeaturelayer)
- [ThinkGeo.MapSuite.Shapes.Feature](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.feature)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
