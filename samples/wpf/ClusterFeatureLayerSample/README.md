# Cluster Feature Layer Sample for Wpf

### Description
The sample shows how to use ClusterFeatureLayer to render the specified column data as a pie chart. Allows users to compare data visually.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ClusterFeatureLayerSample/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(@"..\..\AppData\cntry02.shp");
shapeFileFeatureLayer.Open();

var allFeatures = shapeFileFeatureLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);
var clusterColumns = new FeatureSourceColumn[] { new FeatureSourceColumn (){ColumnName="POP_CNTRY",TypeName="Number" },
new FeatureSourceColumn() { ColumnName = "SQKM", TypeName = "Number" },
new FeatureSourceColumn() { ColumnName = "LANDLOCKED", TypeName = "Bool" } };
ClusterFeatureLayer clusterFeatureLayer = new ClusterFeatureLayer(clusterColumns, allFeatures);
layerOverlay.Layers.Add(clusterFeatureLayer);
map.Overlays.Add(layerOverlay);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.ClusterFeatureLayer](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.ClusterFeatureLayer)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
