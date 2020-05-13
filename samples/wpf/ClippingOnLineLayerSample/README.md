# Clipping On Line Layer Sample for Wpf

### Description
This Wpf project completes the series of projects dedicated to the clipping geoprocessing. We already saw how to perform clipping on a polygon based layer in “Clipping” and on a point layer in “Clipping On Point Layer”. Here we show how to perform the clipping geopressing on a line based layer. As for the same operation on a polygon based layer, the key geometric function is GetIntersection. We will also appreciate the operation of creating a layer from scratch as in addition to the geometric operation itself, geoprocessing also involves creating a result layer from the original layers, the clipping layer and the clipped layer.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/ClippingOnLineLayerSample-ForWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
private ShapeFileFeatureLayer ClipLineBasedShapeFile(ShapeFileFeatureLayer shapeFileFeatureLayer, string outputShapeFileFeatureLayerPath, AreaBaseShape ClippingShape)
{
    //Gets the columns from the line based shapefile.
    shapeFileFeatureLayer.Open();
    Collection<DbfColumn> dbfColumns = ((ShapeFileFeatureSource)shapeFileFeatureLayer.FeatureSource).GetDbfColumns();

    ShapeFileFeatureLayer.CreateShapeFile(ShapeFileType.Polyline, outputShapeFileFeatureLayerPath, dbfColumns);

    ShapeFileFeatureSource clippedShapeFileFeatureSource = new ShapeFileFeatureSource(outputShapeFileFeatureLayerPath, GeoFileReadWriteMode.ReadWrite);

    //Gets the features with Spatial Query that will be used for the clipping.
    Collection<Feature> features = shapeFileFeatureLayer.QueryTools.GetFeaturesIntersecting(ClippingShape, ReturningColumnsType.AllColumns);
    shapeFileFeatureLayer.Close();

    //Loops thru all the features from the Spatial Query.
    clippedShapeFileFeatureSource.Open();
    clippedShapeFileFeatureSource.BeginTransaction();
    foreach (Feature feature in features)
    {
        //Gets the MultilineShape and clip it using GetIntersection geometric function.
        MultilineShape currentMultilineShape = (MultilineShape)(feature.GetShape());
        MultilineShape clippedMultilineShape = currentMultilineShape.GetIntersection(ClippingShape);

        //Adds the feature to the clipped layer with the clipped MultilineShape with its ColumnValues.
        Dictionary<string, string> Columns = feature.ColumnValues;
        Feature clippedFeature = new Feature(clippedMultilineShape, Columns);
        clippedShapeFileFeatureSource.AddFeature(clippedFeature);
    }

    clippedShapeFileFeatureSource.CommitTransaction();
    clippedShapeFileFeatureSource.Close();

    //Returns the clipped ShapeFileFeatureLayer.
    ShapeFileFeatureLayer clippedShapeFileFeatureLayer = new ShapeFileFeatureLayer(outputShapeFileFeatureLayerPath);
    return clippedShapeFileFeatureLayer;
}
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.ShapeFileFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefilefeaturelayer)
- [ThinkGeo.MapSuite.Shapes.AreaBaseShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.areabaseshape)
- [ThinkGeo.MapSuite.Layers.DbfColumn](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.dbfcolumn)
- [ThinkGeo.MapSuite.Layers.ShapeFileType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefiletype)
- [ThinkGeo.MapSuite.Layers.ShapeFileFeatureSource](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefilefeaturesource)
- [ThinkGeo.MapSuite.Layers.GeoFileReadWriteMode](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.geofilereadwritemode)
- [ThinkGeo.MapSuite.Shapes.ReturningColumnsType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.returningcolumnstype)
- [ThinkGeo.MapSuite.Shapes.MultilineShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.multilineshape)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
