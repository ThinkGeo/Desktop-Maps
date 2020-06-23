# World Streets Layer SDK On Linux for WinForms

### Description

The World Streets Vector Layer Explorer is a tool that enables you to view the SQLite World Streets data using Map Suite WinForms and provides complete performance metrics.

This project requires a full or evaluation version of Map Suite WinForms Edition.

This sample passed on Linux with Mono Runtime. On Windows platform, it is required to replace the ThinkGeo.MapSuite.Layers.SqliteForLinux with ThinkGeo.MapSuite.Layers.Sqlite package.

**To run the sample, please unzip the database file at WorldStreetsLayerSample/App_Data/DallasCounty-3857-20170218.zip, and change the connection string in WorldStreetsLayerSample/App.config to connect database that you extracted to.**

Working...

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/WorldStreetLayersSDKLinux/screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
WorldStreetsLayer osmWorldMapKitLayer = GetLayerByDatabaseType();
osmWorldMapKitLayer.Open();

LayerOverlay layerOverlay = new LayerOverlay();
layerOverlay.Layers.Add(osmWorldMapKitLayer);

winformsMap1.MapUnit = GeographyUnit.Meter;
winformsMap1.CurrentExtent = new RectangleShape(-10811578.0703145, 3892111.87754448, -10736211.3489208, 3839905.51779612);
winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 172, 205, 226));
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.layeroverlay)
- [ThinkGeo.MapSuite.Shapes.RectangleShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.rectangleshape)
- [ThinkGeo.MapSuite.GeographyUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.geographyunit)
- [ThinkGeo.MapSuite.Drawing.GeoSolidBrush](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geosolidbrush)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense. 
