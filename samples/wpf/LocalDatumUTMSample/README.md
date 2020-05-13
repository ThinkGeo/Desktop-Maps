# Local Datum UTM Sample for Wpf

### Description

The purpose of this WPF sample is to get familiar with the concept of datum and datum shift in UTM and Geographic coordinates. Using local data, you might be required to apply datum transformation to your data to go from an old datum to a more recent datum. Here we are taking the example of Australia, which went to change its datum (from AGD84 to GDA94) for its mapping purposes. We show how to apply the datum transformation in both the longitude/latitude coordinate system and in the local UTM systems (AMG based on AGD84 and MGA based on GD94). 

See the code and note the comments in the **MouseMove** event where all the projection logic is taken place. Notice that the correction is about 200 meters. Once you understand for the case for Australia, you will be able to apply the same principles for your own datums in UTM. 
              
Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/LocalDatumUTMSample-ForWpf/blob/master/ScreenShot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.Width, (float)wpfMap1.Height);

Proj4Projection WGS84toAGD84proj4 = new Proj4Projection();
WGS84toAGD84proj4.InternalProjectionParametersString = Proj4Projection.GetEpsgParametersString(4326); //WGS84
WGS84toAGD84proj4.ExternalProjectionParametersString = "+proj=longlat +ellps=aust_SA +towgs84=-117.763,-51.51,139.061,0.292,-0.443,-0.277,-0.03939657799319541 +no_defs";

WGS84toAGD84proj4.Open();
Vertex AGD84vertex = WGS84toAGD84proj4.ConvertToExternalProjection(pointShape.X, pointShape.Y);
WGS84toAGD84proj4.Close();
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.Shapes.ExtentHelper](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.extenthelper)
- [ThinkGeo.MapSuite.Shapes.Proj4Projection](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.proj4projection)
- [ThinkGeo.MapSuite.Shapes.Vertex](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.vertex)

### FAQ
- __Q: How do I make background map work?__  
A: Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require a Client ID and Secret. These were sent to you via email when you signed up with ThinkGeo, or you can register now at https://cloud.thinkgeo.com. Once you get them, please update the code in TestWindow.xaml.cs.  

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
