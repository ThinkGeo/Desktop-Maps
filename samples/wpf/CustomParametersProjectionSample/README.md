# Custom Parameters Projection Sample for Wpf

### Description

In today’s Wpf project, we learn more about projection and how to handle a special case where the projection information in the PRJ file of a shapefile is not found as an EPSG or ESRI code in the spatial reference web site (www.spatial-reference.org). The strategy in this case is to build a proj4 string based on the parameters found in the PRJ file. Note that you must pay special attention to the false easting or northing parameters as they are always expressed in meters even if the projection is in another unit such as feet. For this example, we show how to convert from the local projection in Lambert Conformal Conic with regional specific parameters to Geodetic (WGS84) to match the World Map Kit.


Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CustomParametersProjectionSample/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
/*===========================================
  Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
  a Client ID and Secret. These were sent to you via email when you signed up
  with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/
ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay();
wpfMap1.Overlays.Add(baseOverlay);

ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(@"../../data/2000house_districts.shp");
shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(
    GeoColor.FromArgb(100, GeoColor.StandardColors.Pink), GeoColor.StandardColors.Black);
shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

Proj4Projection proj4 = new Proj4Projection();
string internalProjectionString = "+proj=lcc +lat_1=43 +lat_2=45.5 +lat_0=41.75 +lon_0=-120.5 +x_0=400000 +y_0=0 +ellps=GRS80 +datum=NAD83 +to_meter=0.3048 +no_defs";
proj4.InternalProjectionParametersString = internalProjectionString;
proj4.ExternalProjectionParametersString = Proj4Projection.GetEpsgParametersString(3857);

proj4.Open();
Vertex projVertex = proj4.ConvertToExternalProjection(747032, 1297787);
proj4.Close();
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.WorldMapKitWmsWpfOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.worldmapkitwmswpfoverlay)
- [ThinkGeo.MapSuite.Layers.ShapeFileFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefilefeaturelayer)
- [ThinkGeo.MapSuite.Styles.AreaStyles](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.areastyles)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)
- [ThinkGeo.MapSuite.Layers.ApplyUntilZoomLevel](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.applyuntilzoomlevel)
- [ThinkGeo.MapSuite.Shapes.Proj4Projection](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.proj4projection)
- [ThinkGeo.MapSuite.Shapes.Vertex](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.vertex)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
