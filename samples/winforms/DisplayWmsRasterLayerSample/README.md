# Display Wms Raster Layer Sample for WinForms

### Description

This sample demonstrates how you use WmsRasterLayer to render wms server in your Map Suite GIS applications. This WmsRasterLayer support would work in all of the Map Suite controls such as Wpf, Web, MVC and WebApi. 

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/DisplayWmsRasterLayerSample-ForWinForms/blob/master/Screenshot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
WmsRasterLayer wmsLayer = new WmsRasterLayer(new Uri("http://howdoiwms.thinkgeo.com/WmsServer.aspx"));
wmsLayer.ActiveLayerNames.Add("COUNTRIES02");
wmsLayer.ActiveStyleNames.Add("Simple");
wmsLayer.Crs = "EPSG:4326";
wmsLayer.OutputFormat = "image/png";
```

### Getting Help

- [Map Suite Desktop for WinForms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)
- [Map Suite Desktop for WinForms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs

This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.WinformsMap](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.WinForms.WinformsMap)
- [ThinkGeo.MapSuite.Layers.WmsRasterLayer](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.WmsRasterLayer)

### About Map Suite

Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo

ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
