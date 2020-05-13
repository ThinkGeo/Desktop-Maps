# Detect Gps Sample for WinForms

### Description
Upon request of our users, today we publish a project that is the Desktop version of “Detect GPS” for Web. Notice how we use ValueStyle and change the column value of the feature based on the Spatial Query feature at each new position. We chose this structure so that you can have more flexibility for adding more than one moving vehicle features to the InMemoryFeatureLayer. For that, you can pretty much keep the same code and just add an outer loop for looping thru all the moving features.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/DetectGPSSample-ForWinForms/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code

InMemoryFeatureLayer vehicleLayer = new InMemoryFeatureLayer();
vehicleLayer.FeatureSource.Projection = new Proj4Projection(4326, 3857);
vehicleLayer.Open();
vehicleLayer.Columns.Add(new FeatureSourceColumn("DETECT", "string", 3));
vehicleLayer.Close();

// Draw features based on values
ValueStyle valueStyle = new ValueStyle();
valueStyle.ColumnName = "DETECT";
valueStyle.ValueItems.Add(new ValueItem("No", new PointStyle(new GeoImage("../../data/Top.png"))));
valueStyle.ValueItems.Add(new ValueItem("yes", new PointStyle(new GeoImage("../../data/Top2.png"))));
vehicleLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);
vehicleLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

Feature GPSfeature = new Feature(GPSlocations[0]);
GPSfeature.ColumnValues.Add("DETECT", "No");
vehicleLayer.InternalFeatures.Add("GPSFeature1", GPSfeature);

LayerOverlay vehicleOverlay = new LayerOverlay();
vehicleOverlay.Layers.Add("VehicleLayer", vehicleLayer);
winformsMap1.Overlays.Add("VehicleOverlay", vehicleOverlay);

### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.InMemoryFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.InMemoryFeatureLayer)
- [ThinkGeo.MapSuite.Styles.ValueStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.ValueStyle)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
