# Labeling Based On Size Sample for WinForms

### Description

This project shows some advanced uses of the **ClassBreakStyle** to show how to label countries based on the area. You will notice that we also take advantage of the various zoom level sets for labeling purposes. The result is an eye pleasing labeling of the countries,  with the size proportionate to the countries’ area, with more countries' labels appearing as you zoom in.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/LabelingBasedOnSizeSample-ForWinForms/blob/master/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ClassBreakStyle classBreakStyle4 = new ClassBreakStyle("SQKM");
TextStyle textStyle4a = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 9, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 2, 0, 0);
TextStyle textStyle4b = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 12, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 2, 0, 0);
TextStyle textStyle4c = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 14, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 2, 0, 0);
TextStyle textStyle4d = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 18, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 2, 0, 0);
TextStyle textStyle4e = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 22, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 3, 0, 0);
classBreakStyle4.ClassBreaks.Add(new ClassBreak(0, textStyle4a));
classBreakStyle4.ClassBreaks.Add(new ClassBreak(20000, textStyle4b));
classBreakStyle4.ClassBreaks.Add(new ClassBreak(100000, textStyle4c));
classBreakStyle4.ClassBreaks.Add(new ClassBreak(500000, textStyle4d));
classBreakStyle4.ClassBreaks.Add(new ClassBreak(3000000, textStyle4e));
worldLabelLayer.ZoomLevelSet.ZoomLevel08.CustomStyles.Add(classBreakStyle4);
worldLabelLayer.ZoomLevelSet.ZoomLevel08.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Styles.ClassBreakStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.classbreakstyle)
- [ThinkGeo.MapSuite.Styles.TextStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.textstyle)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
