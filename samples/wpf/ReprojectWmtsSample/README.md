# Reproject Wmts Sample for Wpf

### Description
In today’s project, we show how to create your own projection class that allows projecting a WMTS layer from any internal projection to any external. 

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/ReprojectWmtsSample-ForWpf/blob/master/Screenshot.png)

### About the Code
```csharp
wmtsLayer = new WmtsLayer();
wmtsLayer.WmtsSeverEncodingType = ThinkGeo.MapSuite.Layers.WmtsSeverEncodingType.Kvp;
wmtsLayer.ServerUris.Add(new Uri("https://basemap.nationalmap.gov/arcgis/rest/services/USGSImageryOnly/MapServer/WMTS"));
wmtsLayer.ActiveLayerName = "USGSImageryOnly";
wmtsLayer.ActiveStyleName = "default";
wmtsLayer.TileMatrixSetName = "GoogleMapsCompatible";
wmtsLayer.OutputFormat = "image/png";
wmtsLayer.Projection = new Proj4Projection(3857, 4326);
wmtsLayer.ProjectedTileCache = new FileBitmapTileCache("WmtsProjectedTileCache", "USGSImageryOnly-4326");
wmtsLayer.TileCache = new FileBitmapTileCache("WmtsTileCache", "USGSImageryOnly-3857");
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
