# Arc GIS Server Rest Layer Sample for WPF

### Description

In this wpf-based project, we'll demonstrate the new ArcGISServerRestLayer released with Map Suite 9.0. This layer gives developers a simple and powerful tool to access their maps that reside on ArcGIS Server. This new layer utilizes the [ArcGIS Server REST API](http://resources.arcgis.com/en/help/arcgis-rest-api/). Now, the latest version of Map Suite Core can support that. In order to run this project, you will need the Development Build 9.0.443.0 or later.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ArcGISServerRestLayerSample/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
wpfMap1.MapUnit = GeographyUnit.Meter;
wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
/*===========================================
  Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
  a Client ID and Secret. These were sent to you via email when you signed up
  with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/
ThinkGeoCloudRasterMapsOverlay backgroundOverlay = new ThinkGeoCloudRasterMapsOverlay();
wpfMap1.Overlays.Add(backgroundOverlay);

ThinkGeo.MapSuite.Layers.ArcGISServerRestLayer arcGisLayer = new ThinkGeo.MapSuite.Layers.ArcGISServerRestLayer();
```
### Getting Help

[Map Suite Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_wpf_desktop_edition)

[Map Suite Wpf Product Description](http://thinkgeo.com/map-suite-developer-gis/wpf-edition/)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.GeographyUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.geographyunit)
- [ThinkGeo.MapSuite.Wpf.WorldStreetsAndImageryProjection](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.worldstreetsandimageryprojection)
- [ThinkGeo.MapSuite.Layers.WorldStreetsAndImageryRasterLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.worldstreetsandimageryrasterlayer)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
