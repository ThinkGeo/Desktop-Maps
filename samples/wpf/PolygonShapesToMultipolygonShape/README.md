# Polygon Shapes To Multipolygon Shape Sample for Wpf

### Description

In this Wpf project, we show how to create a MultipolygonShape from various PolygonShapes. Since a collection of PolygonShapes cannot be directly cast to a MultipolygonShape, we show the technique to build a MultipolygonShape passing an IEnumerable of PolygonShape. This is a Wpf sample and you will need the references for MapSuiteCore.dll and WpfDesktopEdition.dll.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/PolygonShapesToMultipolygonShape/Screenshot.gif)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
LayerOverlay dynamicOverlay = (LayerOverlay)wpfMap1.Overlays["DynamicOverlay"];
InMemoryFeatureLayer inMemoryFeatureLayer = (InMemoryFeatureLayer)dynamicOverlay.Layers["Polygons"];

inMemoryFeatureLayer.Open();

Collection<Feature> features = inMemoryFeatureLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns);
Collection<PolygonShape> polygonShapes = new Collection<PolygonShape>();

foreach (Feature feature in features)
{
    polygonShapes.Add((PolygonShape)feature.GetShape());
}

MultipolygonShape multipolygonShape = new MultipolygonShape(polygonShapes);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.layeroverlay)
- [ThinkGeo.MapSuite.Layers.InMemoryFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.inmemoryfeaturelayer)
- [ThinkGeo.MapSuite.Shapes.Feature](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.feature)
- [ThinkGeo.MapSuite.Layers.FeatureSource](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.featuresource)
- [ThinkGeo.MapSuite.Shapes.PolygonShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.polygonshape)
- [ThinkGeo.MapSuite.Shapes.MultipolygonShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.multipolygonshape)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
