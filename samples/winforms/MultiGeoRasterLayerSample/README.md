# Multi Geo Raster Layer Sample for WinForms

### Description

MapSuite API has RasterLayer from which inherits MrSIDRasterLayer and ECWRasterLayer etc. If we have many raster files, we would need to add all the raster files as separate layer. However this has a performance issue. In this project, we show how to create a class MultiGeoRasterLayer that treats all the raster file as one layer.
              
This class show how to do that using JPEG images with its associating JGW world file. It speeds up the loading of a large number of Raster layers by loading and drawing on demand only the files in the current extent. It loads a reference file that contains the bounding box, path and file information for all of the Raster files. We load this information into an in-memory spatial index. When the map requests to draw the layer, we find the Rasters that are in the current extent, create a layer on-the-fly, call their Draw method and then close them. In this way, we load on demand only the files that are in the current extent.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/MultiGeoRasterLayerSample-ForWinForms/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
public class MultiGeoRasterLayer : Layer
{
    protected override void OpenCore()
    {
        if (File.Exists(rasterRefrencePathFileName))
        {
            string[] rasterFiles = File.ReadAllLines(rasterRefrencePathFileName);
            spatialIndex = new STRtree<string>(rasterFiles.Length);

            Collection<BaseShape> boundingBoxes = new Collection<BaseShape>();

            foreach (string rasterLine in rasterFiles)
            {
                string[] parts = rasterLine.Split(new string[] { "," }, StringSplitOptions.None);
                RectangleShape rasterBoundingBox = new RectangleShape(new PointShape(double.Parse(parts[upperLeftXPosition]), double.Parse(parts[upperLeftYPosition])), new PointShape(double.Parse(parts[lowerRightXPosition]), double.Parse(parts[lowerRightYPosition])));
                boundingBoxes.Add(rasterBoundingBox);

                Envelope envelope = new Envelope(double.Parse(parts[upperLeftXPosition]), double.Parse(parts[lowerRightXPosition]), double.Parse(parts[upperLeftYPosition]), double.Parse(parts[lowerRightYPosition]));
                spatialIndex.Insert(envelope, parts[pathFileNamePosition]);
            }
            spatialIndex.Build();
            boundingBox = ExtentHelper.GetBoundingBoxOfItems(boundingBoxes);
        }
        else
        {
            throw new FileNotFoundException("The Raster reference file could not be found.", rasterRefrencePathFileName);
        }
    }
}
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:
- [ThinkGeo.MapSuite.Layers.Layer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.layer)
- [ThinkGeo.MapSuite.Shapes.BaseShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.baseshape)
- [ThinkGeo.MapSuite.Shapes.RectangleShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.rectangleshape)
- [ThinkGeo.MapSuite.Shapes.ExtentHelper](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.extenthelper)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
