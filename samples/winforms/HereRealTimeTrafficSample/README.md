# Here Real Time Traffic Sample for WinForms

### Description

This sample demonstrates how you use HERE Real-time Traffic to render map in your Map Suite GIS applications.
Before running this sample, you need to config "AppId" and "AppCode" in App.Config. If you have ESRI or Here developer account, you can generate them on [Here's official Web Site](https://developer.here.com/).

HERE Real-time Traffic provides the closest thing to a live depiction of the road. It identifies where, when and why traffic congestion occurs, and delivers up-to-the-minute information on the road conditions and incidents that could set a driver back.

This HereRealTimeTrafficLayer is supported in all of the Map Suite controls such as WPF, Web, MVC and WebAPI.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/HereRealTimeTrafficSample/Screenshot.gif)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ClassBreakStyle classBreakStyle = new ClassBreakStyle();
classBreakStyle.ColumnName = "jf";
classBreakStyle.ClassBreaks.Add(new ClassBreak(0, new LineStyle(new GeoPen(GeoColor.SimpleColors.Green, 2.0f))));
classBreakStyle.ClassBreaks.Add(new ClassBreak(4, new LineStyle(new GeoPen(GeoColor.SimpleColors.Yellow, 2.0f))));
classBreakStyle.ClassBreaks.Add(new ClassBreak(8, new LineStyle(new GeoPen(GeoColor.SimpleColors.Red, 2.0f))));
classBreakStyle.ClassBreaks.Add(new ClassBreak(10, new LineStyle(new GeoPen(GeoColor.SimpleColors.Black, 2.0f))));

var drawingFeatures = FeatureSource.GetFeaturesForDrawing(canvas.CurrentWorldExtent, canvas.Width, canvas.Height, ReturningColumnsType.AllColumns);
classBreakStyle.Draw(drawingFeatures, canvas, new Collection<SimpleCandidate>(), labelsInAllLayers);
```

### Getting Help

- [Map Suite Desktop for WinForms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)
- [Map Suite Desktop for WinForms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs

This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.WinformsMap](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.WinForms.WinformsMap)
- [ThinkGeo.MapSuite.Layers.FeatureLayer](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.FeatureLayer)
- [ThinkGeo.MapSuite.Styles.ClassBreakStyle](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Styles.ClassBreakStyle)

### About Map Suite

Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo

ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
