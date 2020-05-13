# Earthquake Statistics Sample for Wpf

### Description
The Earthquake Statistics sample template is a statistical report system for earthquakes that have occurred in the past few years across the United States. It can help you generate infographics and analyze the severely afflicted areas, or used as supporting evidence when recommending measures to minimize the damage in future quakes.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/EarthquakeStatisticsSample-ForWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
GridInterpolationModel interpolationModel = new InverseDistanceWeightedGridInterpolationModel(3, double.MaxValue);
isoLineLayer = new DynamicIsoLineLayer(dataPoints, IsoLineLayer.GetIsoLineLevels(dataPoints.Values, 12), interpolationModel, IsoLineType.ClosedLinesAsPolygons);

IsoLineLayer.GetIsoLineLevels(dataPoints, 12);

levelClassBreakStyle = new ClassBreakStyle(isoLineLayer.DataValueColumnName);
levelClassBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new AreaStyle(new GeoPen(GeoColor.FromHtml("#FE6B06"), 1), new GeoSolidBrush(new GeoColor(100, levelAreaColors[0])))));
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.GridInterpolationModel](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.gridinterpolationmodel)
- [ThinkGeo.MapSuite.Layers.InverseDistanceWeightedGridInterpolationModel](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.inversedistanceweightedgridinterpolationmodel)
- [ThinkGeo.MapSuite.Layers.IsoLineType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.isolinetype)
- [ThinkGeo.MapSuite.Layers.IsoLineLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.isolinelayer)
- [ThinkGeo.MapSuite.Styles.ClassBreakStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.classbreakstyle)
- [ThinkGeo.MapSuite.Styles.ClassBreak](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.classbreak)
- [ThinkGeo.MapSuite.Styles.AreaStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.areastyle)
- [ThinkGeo.MapSuite.Drawing.GeoPen](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geopen)
- [ThinkGeo.MapSuite.Drawing.GeoSolidBrush](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geosolidbrush)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
