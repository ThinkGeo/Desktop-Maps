# World Streets Layer SDK On Windows for WinForms

### Description

The World Streets Vector Layer Explorer is a tool that enables you to view the SQLite World Streets data using Map Suite WinForms and provides complete performance metrics.

This project requires a full or evaluation version of Map Suite WinForms Edition.

This sample passed on Linux with Mono Runtime. On Windows platform, it is required to replace the ThinkGeo.MapSuite.Layers.SqliteForLinux with ThinkGeo.MapSuite.Layers.Sqlite package.

**To run the sample, please unzip the database file at WorldStreetsLayerSample/App_Data/DallasCounty-3857-20170218.zip, and change the connection string in WorldStreetsLayerSample/App.config to connect database that you extracted to.**

Working...

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/WorldStreetsLayerSDKOnWindows-ForWinForms/blob/master/screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
public class ImageryOptimizedWorldStreetsVectorLayer : WorldStreetsLayer
{
    protected override PointStyle GetImagePointStyle(string filename)
    {
        PointStyle background = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColors.White), new GeoPen(GeoColors.SlateGray, 1), 20);
        background.CustomPointStyles.Add(new PointStyle()
        {
            PointType = PointType.Bitmap,
            Image = new GeoImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", filename)),
            DrawingLevel = DrawingLevel.LabelLevel
        });

        return background;
    }
}
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Styles.PointStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointstyle)
- [ThinkGeo.MapSuite.Styles.PointSymbolType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointsymboltype)
- [ThinkGeo.MapSuite.Drawing.GeoSolidBrush](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geosolidbrush)
- [ThinkGeo.MapSuite.Drawing.GeoPen](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geopen)
- [ThinkGeo.MapSuite.Styles.PointType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointtype)
- [ThinkGeo.MapSuite.Drawing.GeoImage](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geoimage)
- [ThinkGeo.MapSuite.Drawing.DrawingLevel](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.drawinglevel)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense. 
