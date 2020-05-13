# Drag Icon Sample for WinForms

### Description

In today’s sample, we show how to drag icons representing vehicle on the map. This is a handy feature if you want to give your users the ability to drag and drop some non stationary features such as vehicles. You can see that to accomplish this functionality, you can use EditInteractiveOverlay as it already has all the necessary logic for dragging purposes. Look at the code to see how to set up that overlay to have the expected behavior.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/DragIconSample-ForWinForms/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ValueStyle valueStyle = new ValueStyle();
valueStyle.ColumnName = "Type";

PointStyle carPointStyle = new PointStyle(new GeoImage("../../data/car_normal.png"));
carPointStyle.PointType = PointType.Bitmap;
PointStyle busPointStyle = new PointStyle(new GeoImage("../../data/bus_normal.png"));
busPointStyle.PointType = PointType.Bitmap;
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Styles.ValueStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.valuestyle)
- [ThinkGeo.MapSuite.Styles.PointStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointstyle)
- [ThinkGeo.MapSuite.Styles.PointType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointtype)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
