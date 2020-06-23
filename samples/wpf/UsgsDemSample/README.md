# Usgs Dem Sample for Wpf

### Description

This sample demonstrates how you can read data from an DEM file in your Map Suite GIS applications, and how to render it with DEM embedded value style as well as a customized style. This DEM File support would work in all of the Map Suite controls such as Wpf, Web, Android and iOS.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/UsgsDemSample/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
UsgsDemFeatureLayer layer = new UsgsDemFeatureLayer("../../AppData/mobile-e");
layer.DrawingQuality = DrawingQuality.HighSpeed;
layer.Open();
ValueStyle valueStyle = new ValueStyle();
valueStyle.ColumnName = layer.DataValueColumnName;
var features = layer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);
foreach (var feature in features)
{
    double height = Convert.ToDouble(feature.ColumnValues[layer.DataValueColumnName]) * ((UsgsDemFeatureSource)layer.FeatureSource).ResolutionZ;
    int ratio = (int)(((height - ((UsgsDemFeatureSource)layer.FeatureSource).MinElevation) / (((UsgsDemFeatureSource)layer.FeatureSource).MaxElevation - ((UsgsDemFeatureSource)layer.FeatureSource).MinElevation)) * 255f);

    if (valueStyle.ValueItems.Any(i => i.Value == feature.ColumnValues[layer.DataValueColumnName]))
        continue;

    valueStyle.ValueItems.Add(new ValueItem(feature.ColumnValues[layer.DataValueColumnName], new AreaStyle(new GeoPen(new GeoSolidBrush(), 0), new GeoSolidBrush(new GeoColor(128, ratio, 128)))));
}

layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);
layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
```

### Getting Help

- [Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)
- [Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.UsgsDemFeatureLayer](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.UsgsDemFeatureLayer)
- [ThinkGeo.MapSuite.Styles.ValueStyle](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Styles.ValueStyle)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
