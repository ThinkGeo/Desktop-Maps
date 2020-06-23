# Multiple Labels Sample for WinForms

### Description

This sample shows how you can display multiple labels for a given point or feature. You can do this by setting a single **TextStyle** or multiple **TextStyles**. If you use a single **TextStyle**, you can simply use a pattern like "[ColumnName1][ColumnName2]..." and when Map Suite displays the text it will combine the values of the columns in your pattern. If you use a different styling method, you will need to manually control the offset of each piece of text to avoid overlapping.


Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/MultiplelabelsSample/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ShapeFileFeatureLayer austinStreetsLayer = ((LayerOverlay)winformsMap1.Overlays["AustinStreetsOverlay"]).Layers["AustinStreetsLayer"] as ShapeFileFeatureLayer;
austinStreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
austinStreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.White, 9.2F, GeoColor.StandardColors.DarkGray, 12.2F, true));

if (rbnSingleStyle.Checked)
{
    austinStreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(WorldStreetsTextStyles.Poi("[FENAME] [FETYPE]  [FRADDL]-[TOADDL]", 8, -12));
}
else
{
    TextStyle primaryTextStyle = WorldStreetsTextStyles.Poi("[FENAME] [FETYPE]", 8, -12);
    primaryTextStyle.XOffsetInPixel = 0;

    TextStyle secondaryTextStyle = WorldStreetsTextStyles.Poi("[FRADDL]-[TOADDL]", 8, -12);
    secondaryTextStyle.YOffsetInPixel =  15;
    secondaryTextStyle.Font = new GeoFont("Arial", 7);
    secondaryTextStyle.Mask = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 1);
    secondaryTextStyle.OverlappingRule = LabelOverlappingRule.AllowOverlapping;
    secondaryTextStyle.DuplicateRule = LabelDuplicateRule.UnlimitedDuplicateLabels;

    austinStreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(primaryTextStyle);
    austinStreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(secondaryTextStyle);
}
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.ShapeFileFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefilefeaturelayer)
- [ThinkGeo.MapSuite.Styles.TextStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.textstyle)
- [ThinkGeo.MapSuite.Styles.WorldStreetsTextStyles](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.worldstreetstextstyles)
- [ThinkGeo.MapSuite.Styles.LabelOverlappingRule](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.labeloverlappingrule)
- [ThinkGeo.MapSuite.Styles.LabelDuplicateRule](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.labelduplicaterule)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
