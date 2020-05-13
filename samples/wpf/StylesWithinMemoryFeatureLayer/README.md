# Styles With Inmemory Feature Layer Sample for Wpf

### Description
In this WPF project, we show how to build an InMemoryFeatureLayer from a text file. You'll notice how the columns are set up so that styles can be used as if the InMemoryFeatureLayer were a static layer such as a Shapefile. Here, we apply a Class Breask Style and a Text Style to our InMemoryFeatureLayer. What we learn in this sample can be applied to all the different editions of Map Suite.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/StylesWithInmemoryFeatureLayerSample-ForWpf/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ClassBreakStyle cbs = new ClassBreakStyle(roomsColumn);
cbs.BreakValueInclusion = BreakValueInclusion.IncludeValue;
cbs.ClassBreaks.Add(new ClassBreak(double.MinValue, PointStyles.CreateSimplePointStyle(PointSymbolType.Circle,
                                GeoColor.FromArgb(150, GeoColor.StandardColors.Blue), GeoColor.StandardColors.Black, 8)));

cbs.ClassBreaks.Add(new ClassBreak(100, PointStyles.CreateSimplePointStyle(PointSymbolType.Circle,
                                GeoColor.FromArgb(150, GeoColor.StandardColors.Blue), GeoColor.StandardColors.Black, 12)));
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Styles.ClassBreakStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.classbreakstyle)
- [ThinkGeo.MapSuite.Styles.BreakValueInclusion](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.breakvalueinclusion)
- [ThinkGeo.MapSuite.Styles.ClassBreak](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.classbreak)
- [ThinkGeo.MapSuite.Styles.PointStyles](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointstyles)
- [ThinkGeo.MapSuite.Styles.PointSymbolType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointsymboltype)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
