# Get Features From Arc GIS Server Sample for WPF

### Description

In this wpf-based project, it illustrates how to get the features from the ArcGIS Restful Server. On the left side of the screenshot shows the raster data from ArcGIS Server and on the other side shows the features from ArcGIS Restful Server. In order to run this project, you will need the Development Build 9.0.482.0 or later.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/GetFeatureFromArcGISServer/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
WorldStreetsAndImageryRasterLayer worldMapKitLayer = new WorldStreetsAndImageryRasterLayer();
worldMapKitLayer.Projection = ThinkGeo.MapSuite.Layers.WorldStreetsAndImageryProjection.DecimalDegrees;

LayerOverlay layerOverlay = new LayerOverlay();
layerOverlay.Layers.Add(worldMapKitLayer);

InMemoryFeatureLayer inMemoryArcGISServerRestLayer = new InMemoryFeatureLayer();
```
### Getting Help

[Map Suite Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_wpf_desktop_edition)

[Map Suite Wpf Product Description](http://thinkgeo.com/map-suite-developer-gis/wpf-edition/)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.WorldStreetsAndImageryRasterLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.worldstreetsandimageryrasterlayer)
- [ThinkGeo.MapSuite.Wpf.WorldStreetsAndImageryProjection](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.worldstreetsandimageryprojection)
- [ThinkGeo.MapSuite.Wpf.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.layeroverlay)
- [ThinkGeo.MapSuite.Wpf.LayerOverlayThinkGeo.MapSuite.Layers.InMemoryFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.inmemoryfeaturelayer)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
