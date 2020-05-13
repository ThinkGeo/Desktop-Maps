# GPS Exchange Format Feature Layer Sample for Wpf

### Description
This sample demonstrates how to read GPS EXchange Format file(*.gpx) with Map Suite. GPX (GPS Exchange Format) is a light-weight XML data format for the interchange of GPS data (waypoints, routes, and tracks) between applications and Web services on the Internet, which you can find more information:here. Now Map Suite supports the GPX 1.0 and 1.1 schema. This sample works with Map Suite development branch daily build 7.0.275.0 or later.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/GPSExchangeFormatFeatureLayerSample-ForWpf/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
GpxFeatureLayer shapeLayer = new GpxFeatureLayer(@"..\..\Data\" + sampleFileListBox.SelectedItem.ToString());

ValueStyle pointStyle = new ValueStyle();
pointStyle.ColumnName = "IsWayPoint";
pointStyle.ValueItems.Add(new ValueItem("0", PointStyles.CreateSimplePointStyle(PointSymbolType.Circle, GeoColor.SimpleColors.Red, 4)));
pointStyle.ValueItems.Add(new ValueItem("1", PointStyles.CreateSimplePointStyle(PointSymbolType.Circle, GeoColor.SimpleColors.Green, 8)));
LineStyle roadstyle = LineStyles.CreateSimpleLineStyle(GeoColor.SimpleColors.Black, 1, true);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.GpxFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.gpxfeaturelayer)
- [ThinkGeo.MapSuite.Styles.ValueStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.valuestyle)
- [ThinkGeo.MapSuite.Styles.ValueItem](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.valueitem)
- [ThinkGeo.MapSuite.Styles.LineStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.linestyle)
- [ThinkGeo.MapSuite.Styles.PointStyles](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointstyles)
- [ThinkGeo.MapSuite.Styles.PointSymbolType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointsymboltype)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)
- [ThinkGeo.MapSuite.Styles.LineStyles](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.linestyles)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
