# Find Nearest Cross Streets Sample for Wpf

### Description

This sample demonstrates how to geocode an address using Map Suite Geocoder. It then returns and highlights the low and high cross streets.          

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/FindNearestCrossSreetsSample/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
LineShape roadSegment = GetLineShape(feature);
bool isLeftSide = geocoderMath.NameValuePairs["SideOfStreet"].Equals("Left", StringComparison.InvariantCultureIgnoreCase);
bool isFollowRoadDirection = IsDirectionFollowRoad(geocoderMath);

FeatureLayer roadFeatureLayer = ((LayerOverlay)Map1.Overlays[roadOverlayName]).Layers[0] as FeatureLayer;

RectangleShape startVertextBbox = BufferVertex(roadSegment.Vertices[0]);
Collection<Feature> intersectedStartFeatures = roadFeatureLayer.FeatureSource.GetFeaturesInsideBoundingBox(startVertextBbox, new[] { "ROAD_NAME" });
Feature startCrossFeature = GetCrossRoad(feature, isLeftSide, intersectedStartFeatures);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Shapes.LineShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.lineshape)
- [ThinkGeo.MapSuite.Layers.FeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.featurelayer)
- [ThinkGeo.MapSuite.Wpf.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.layeroverlay)
- [ThinkGeo.MapSuite.Shapes.RectangleShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.rectangleshape)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
