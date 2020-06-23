# Multiple Jpeg2000 Raster Layer Sample for Wpf

### Description

This sample shows how to create a customized raster layer for loading multiple files. In this project, we create the MultipleJpeg2000RasterLayer to inherit from Layer to load multiple .jp2 files. You can use the world files or a RectangleShape which is the extent of the image in world coordinate.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/MultipleJpeg2000RasterLayerSample/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
public class MultipleJpeg2000RasterLayer : Layer
{
    private Collection<Jpeg2000RasterLayer> featureLayers;
    protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
    {
        foreach (var featureLayer in featureLayers)
        {
            featureLayer.Draw(canvas, labelsInAllLayers);
        }
    }
}
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.Layer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.layer)
- [ThinkGeo.MapSuite.Layers.Jpeg2000RasterLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.jpeg2000rasterlayer)
- [ThinkGeo.MapSuite.Drawing.GeoCanvas](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocanvas)
- [ThinkGeo.MapSuite.Styles.SimpleCandidate](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.simplecandidate)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense. 
