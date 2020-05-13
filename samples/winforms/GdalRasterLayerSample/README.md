# Gdal Raster Layer Sample for WinForms

### Description
This sample demonstrates how you can load raster format data supported by Gdal.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/GdalRasterLayerSample-ForWinForms/blob/master/Screenshot.png)

### About the Code
```csharp
LayerOverlay layerOverlay = new LayerOverlay();
GdalRasterLayer gdalRasterLayer = new GdalRasterLayer(@"AppData/FdoGdal.tif");
gdalRasterLayer.Open();
layerOverlay.Layers.Add(gdalRasterLayer);
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
