# Kml Sample for WinForms

### Description
KML is the file format for displaying geographic data in a Google Earth browser such as Google Earth. Now, you can also display such a file on a Map Suite control. Thanks to its flexible architecture to extent to new file formats, in today’s project we wrote the logic for supporting KML. Look at the class KmlfeatureSource inheriting from FeatureSource to see how the logic for reading FML files was implemented. As well, you can see the class KmlStyle inheriting from Style for the drawing logic. And feel free to modify and improve those classes with your own implementations.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/KmlSample/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
KmlFeatureLayer.BuildIndexFile("../../App_Data/KML_Samples.kml", BuildIndexMode.Rebuild);
KmlFeatureLayer layer = new KmlFeatureLayer("../../App_Data/KML_Samples.kml");
layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColor.SimpleColors.Black), new GeoSolidBrush(GeoColor.SimpleColors.Yellow));
layer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColor.SimpleColors.Blue, 5));
layer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.SimpleColors.Green), 10);
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.KmlFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.kmlfeaturelayer)
- [ThinkGeo.MapSuite.Styles.AreaStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.areastyle)
- [ThinkGeo.MapSuite.Styles.LineStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.linestyle)
- [ThinkGeo.MapSuite.Styles.PointStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointstyle)
- [ThinkGeo.MapSuite.Layers.ApplyUntilZoomLevel](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.applyuntilzoomlevel)
- [ThinkGeo.MapSuite.Drawing.GeoPen](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geopen)
- [ThinkGeo.MapSuite.Drawing.GeoSolidBrush](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geosolidbrush)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)
- [ThinkGeo.MapSuite.Styles.PointSymbolType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointsymboltype)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
