# Us Earthquake Statistics Sample for WinForms

### Description

The Earthquake Statistics sample template is a statistical report system for earthquakes that have occurred in the past few years across the United States. It can help you generate infographics and analyze the severely afflicted areas, or used as supporting evidence when recommending measures to minimize the damage in future quakes.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/UsEarthquakeStatisticsSample-ForWinForms/blob/master/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
IEnumerable<double> isoLineLevels = GetClassBreakValues(dataPoints.Select(n => n.Value), 12);
GridInterpolationModel gridInterpolationModel = new InverseDistanceWeightedGridInterpolationModel(3, double.MaxValue);

isoLineLayer = new DynamicIsoLineLayer(dataPoints, isoLineLevels, gridInterpolationModel, IsoLineType.ClosedLinesAsPolygons);

levelClassBreakStyle = new ClassBreakStyle(isoLineLayer.DataValueColumnName);
levelClassBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new AreaStyle(new GeoPen(GeoColor.FromHtml("#FE6B06"), 1), new GeoSolidBrush(new GeoColor(100, levelAreaColors[0])))));
for (int i = 0; i < IsoLineLevels.Count - 1; i++)
{
    levelClassBreakStyle.ClassBreaks.Add(new ClassBreak(IsoLineLevels[i + 1], new AreaStyle(new GeoPen(GeoColor.FromHtml("#FE6B06"), 1), new GeoSolidBrush(new GeoColor(100, levelAreaColors[i + 1])))));
}
isoLineLayer.CustomStyles.Add(levelClassBreakStyle);
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.GridInterpolationModel](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.gridinterpolationmodel)
- [ThinkGeo.MapSuite.Layers.DynamicIsoLineLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.dynamicisolinelayer)
- [ThinkGeo.MapSuite.Styles.ClassBreakStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.classbreakstyle)
- [ThinkGeo.MapSuite.Styles.ClassBreak](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.classbreak)
- [ThinkGeo.MapSuite.Drawing.GeoPen](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geopen)
- [ThinkGeo.MapSuite.Drawing.GeoSolidBrush](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geosolidbrush)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
