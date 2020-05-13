# Edit Geometry Of Shapefile Sample for WinForms

### Description
The purpose of this sample is to show how to update the geometry of a feature of a shapefile in one step. This sample is useful for anyone wanting to actualize the geometry part of its data. You can see how only a few lines of code are necessary for this process, and that the spatial index gets automatically updated after calling the committing the change. 

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/EditGeometryOfShapefileSample-ForWinForms/blob/master/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
LayerOverlay layerOverlay = (LayerOverlay)winformsMap1.Overlays["StreetsOverlay"];
ShapeFileFeatureLayer layer1 = (ShapeFileFeatureLayer)layerOverlay.Layers["Streets"];

layer1.Open();
layer1.FeatureSource.BeginTransaction();

Collection<Feature> features = layer1.FeatureSource.GetFeaturesByColumnValue("RECID", "13762", ReturningColumnsType.AllColumns);

MultilineShape newMultiLineShape = (MultilineShape)features[0].GetShape();
newMultiLineShape.Lines[0].Vertices.Add(new Vertex(-97.8015, 30.2880));
newMultiLineShape.Id = features[0].Id;
layer1.FeatureSource.UpdateFeature(newMultiLineShape, features[0].ColumnValues);

layer1.FeatureSource.CommitTransaction();
layer1.Close();

winformsMap1.Refresh(layerOverlay);

btnEdit.Enabled = false;
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.layeroverlay)
- [ThinkGeo.MapSuite.Layers.FeatureSource](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.featuresource)
- [ThinkGeo.MapSuite.Layers.ShapeFileFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefilefeaturelayer)
- [ThinkGeo.MapSuite.Shapes.MultilineShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.multilineshape)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
