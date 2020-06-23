# Select And Drag Feature Sample for Wpf

### Description

In this Wpf project, we show how to select a feature from a shapefile based on a column value using the GetFeaturesByColumnValue. We also show to setup the EditOverlay to give the user the ability to drag the selected feature. In this project, you can also see how to get the world coordinates at the mouse move event. Notice that the code for doing this is quite different in Wpf compared to the winforms edition.
              
Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/SelectAndDragFeatureSample/ScreenShot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ShapeFileFeatureLayer Layer1 = new ShapeFileFeatureLayer(@"..\..\data\counties.shp");
Layer1.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.LightGreen, GeoColor.StandardColors.Black);
Layer1.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

LayerOverlay layerOverlay = new LayerOverlay();
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.ShapeFileFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefilefeaturelayer)
- [ThinkGeo.MapSuite.Styles.AreaStyles](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.areastyles)
- [ThinkGeo.MapSuite.Drawing.GeoSolidBrush](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geosolidbrush)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)
- [ThinkGeo.MapSuite.Layers.ApplyUntilZoomLevel](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.applyuntilzoomlevel)
- [ThinkGeo.MapSuite.Wpf.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.layeroverlay)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
