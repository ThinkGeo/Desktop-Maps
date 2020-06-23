# Building 3D Style Sample for WinForms

### Description

This project shows to create simulated 3D buildings with WinForms Map control and shapefile.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/Building3DSample/Screenshot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ShapeFileFeatureLayer buildingShapeLayer = new ShapeFileFeatureLayer(buildingFilePath);
buildingShapeLayer.ZoomLevelSet.ZoomLevel16.CustomStyles.Add(new OsmBuildingAreaStyle());
buildingShapeLayer.ZoomLevelSet.ZoomLevel16.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
buildingShapeLayer.Open();

LayerOverlay buildingOverlay = new LayerOverlay();
buildingOverlay.Layers.Add(buildingShapeLayer);
winformsMap1.Overlays.Add(buildingOverlay);
```

### Getting Help

- [Map Suite Desktop for WinForms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)
- [Map Suite Desktop for WinForms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs

This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Styles.OsmBuildingAreaStyle](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Styles.OsmBuildingAreaStyle)

### About Map Suite

Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo

ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.

