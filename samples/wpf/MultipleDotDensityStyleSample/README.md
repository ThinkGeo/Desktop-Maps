# Multiple Dot Density Styles Sample for Wpf

### Description

This sample takes a detailed look at the **DotDensityStyle**. You can combine different **DotDensityStyles** on one FeatureLayer using different columns. Demographic data are usually well fit to be used with that type of style. Here each dot of a certain color represents 100,000 persons of the same age group by state. Using multiple **DotDensityStyles**, the dimension of age group demographics is added to the representation of population density. 

Note that the **PointToValueRatio** is used to set how many people a dot is going to represent. For example, if you want a dot to represent 100,000 persons, you set it to 0.00001 (1 / 100000). If you want it to be 500,000 persons, you set it to 0.000002 (1 / 500000).
              
Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/MultipleDotDensityStylesSample-ForWpf/blob/master/ScreenShot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
DotDensityStyle dotDensityStyle = new DotDensityStyle();
dotDensityStyle.ColumnName = "AGE_UNDER5";
dotDensityStyle.PointToValueRatio = 0.00001;  
dotDensityStyle.CustomPointStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(alpha, GeoColor.StandardColors.Green)), 6);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Styles.DotDensityStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.dotdensitystyle)
- [ThinkGeo.MapSuite.Styles.PointStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointstyle)
- [ThinkGeo.MapSuite.Styles.PointSymbolType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointsymboltype)
- [ThinkGeo.MapSuite.Drawing.GeoSolidBrush](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geosolidbrush)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
