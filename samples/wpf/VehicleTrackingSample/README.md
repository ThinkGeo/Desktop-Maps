# Vehicle Tracking Sample for Wpf

### Description
The Vehicle Tracking sample template gives you a head start on your next tracking project. With a working code example to draw from, you can spend more of your time implementing the features you care about and less time thinking about how to accomplish the basic functionality of a tracking system.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/VehicleTrackingSample/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
if (!traceOverlay.Layers.Contains("VehicleTrail"))
{
    vehicleTrailLayer = new InMemoryFeatureLayer();
    vehicleTrailLayer.Open();
    vehicleTrailLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = GetVehicleTrailStyle();
    vehicleTrailLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

    vehicleTrailLayer.FeatureSource.Open();
    vehicleTrailLayer.Columns.Add(new FeatureSourceColumn("Speed"));
    vehicleTrailLayer.Columns.Add(new FeatureSourceColumn("DateTime"));
    vehicleTrailLayer.Columns.Add(new FeatureSourceColumn("Longitude"));
    vehicleTrailLayer.Columns.Add(new FeatureSourceColumn("Latitude"));
    vehicleTrailLayer.Columns.Add(new FeatureSourceColumn("VehicleName"));
    vehicleTrailLayer.Columns.Add(new FeatureSourceColumn("Duration"));
    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
    {
        lock (traceOverlay.Layers)
        {
            traceOverlay.Layers.Add("VehicleTrail", vehicleTrailLayer);
        }
    }));
}
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.InMemoryFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.inmemoryfeaturelayer)
- [ThinkGeo.MapSuite.Layers.FeatureSourceColumn](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.featuresourcecolumn)
- [ThinkGeo.MapSuite.Layers.ApplyUntilZoomLevel](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.applyuntilzoomlevel)

### FAQ
- __Q: How do I make background map work?__
A: Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require a Client ID and Secret. These were sent to you via email when you signed up with ThinkGeo, or you can register now at https://cloud.thinkgeo.com. Once you get them, please update the code in method InitializeOverlays() in Model/MapModel.cs.

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
