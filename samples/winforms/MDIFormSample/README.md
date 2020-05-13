# MDI Form Sample for WinForms

### Description

In today’s project, we show how to have the WinformsMap control in a MDI form. Using an MDI form with a map background can represent a challenge. You will see in this project the technique to display properly a child form on top of the map. You can also notice how the map is accessed from the child form by plotting points on it from the child form.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/MDIFormSample-ForWinForms/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0.0](https://www.nuget.org/packages?q=thinkgeo)

### About the Code

```CSharp
winformsMap1.MapUnit = GeographyUnit.Meter;
winformsMap1.ZoomLevelSet = ThinkGeoCloudMapsOverlay.GetZoomLevelSet();
winformsMap1.CurrentExtent = new RectangleShape(-13914936.35, 5942074.07, -7458405.88, 2875744.62);
winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

// Displays the Cloud Maps as a background.
ThinkGeoCloudMapsOverlay cloudMapsOverlay = new ThinkGeoCloudMapsOverlay();
cloudMapsOverlay.MapType = ThinkGeoCloudMapsMapType.Light;
winformsMap1.Overlays.Add(cloudMapsOverlay);

InMemoryFeatureLayer inMemoryFeatureLayer = new InMemoryFeatureLayer();
inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.StandardColors.Red, 12, GeoColor.StandardColors.Black, 2);
inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

LayerOverlay layerOverlay = new LayerOverlay();
layerOverlay.Layers.Add("Point", inMemoryFeatureLayer);
winformsMap1.Overlays.Add("PointOverlay", layerOverlay);

winformsMap1.Refresh();
```

### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.WinformsMap](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.winformsmap)
- [ThinkGeo.MapSuite.GeographyUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.geographyunit)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
