# ThinkGeo MBTiles Maps Sample for WPF

### Description

This sample demonstrates how you can draw the map with Vector Tiles saved in *.MBTiles in your Map Suite GIS applications, with any style you want from [StyleJSON (Mapping Defination Grammar)](https://wiki.thinkgeo.com/wiki/thinkgeo_stylejson). It will show you how to use the XyzFileBitmapTileCache to improve the performance of map rendering. It supports have 3 built-in default map styles and more awasome styles from StyleJSON file you passed in, by 'Custom': 
- Light
- Dark
- TransparentBackground
- Custom

If you want the *.mbtile file of any area in the world, or you have any requirement of building *.mbtile file based on your own data, such as shape file, Oracle, MsSql and more, please contact support@thinkgeo.com.


*.MBTile format can be supported in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

Please refer to [Wiki](https://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/ThinkGeoMBTilesMapsSample-ForWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.5.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
this.wpfMap.MapUnit = GeographyUnit.Meter;
this.wpfMap.ZoomLevelSet = ThinkGeoMBTilesFeatureLayer.GetZoomLevelSet();

// Create background world map with vector tile requested from loacl Database. 
thinkGeoMBTilesFeatureLayer = new ThinkGeoMBTilesFeatureLayer("Data/tiles_Frisco.mbtiles", new Uri("Data/thinkgeo-world-streets-light.json", UriKind.Relative));

layerOverlay = new LayerOverlay();
layerOverlay.SetTileMatrix(thinkGeoMBTilesFeatureLayer.GetTileMatrixBoundingBox(), thinkGeoMBTilesFeatureLayer.GetGeographyUnit());
layerOverlay.TileWidth = 512;
layerOverlay.TileHeight = 512;

layerOverlay.Layers.Add(thinkGeoMBTilesFeatureLayer);

this.wpfMap.Overlays.Add(layerOverlay);
this.wpfMap.CurrentExtent = new RectangleShape(-10780508.5162109, 3916643.16078401, -10775922.2945393, 3914213.89649231);
this.wpfMap.Refresh();
```
### Getting Help

[Map Suite UI Control for WPF Wiki Resources](https://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite UI Control for WPF Product Description](https://thinkgeo.com/gis-ui-desktop#platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

Working...


### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
