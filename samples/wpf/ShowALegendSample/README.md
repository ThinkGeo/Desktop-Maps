# Show A Legend Sample for Wpf

### Description

In today’s project we learn how to display a simple legend using the new and improved LegendAdornmentLayer. The improved LegendAdornmentLayer was added to Map Suite 5.0 and provides an easy to use API for creating legend adornments. The LegendAdornmentLayer is part of Map Suite Core which allows you to access this powerful feature across all Map Suite products. 
              
Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/ShowALegendSample-ForWpf/blob/master/ScreenShot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
LegendItem legendItem3 = new LegendItem();
legendItem3.ImageStyle = new PointStyle(new GeoImage(@"..\..\Resources\airport_small_size3.png"));
legendItem3.TextStyle = new TextStyle("Airports", new GeoFont("Arial", 8), new GeoSolidBrush(GeoColor.SimpleColors.Black));

// Create the LegendAdornmentLayer and add the LegendItems.
LegendAdornmentLayer legendLayer = new LegendAdornmentLayer();
legendLayer.BackgroundMask = AreaStyles.CreateLinearGradientStyle(new GeoColor(255, 255, 255, 255), new GeoColor(255, 230, 230, 230), 90, GeoColor.SimpleColors.Black);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.LegendItem](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.legenditem)
- [ThinkGeo.MapSuite.Styles.PointStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.pointstyle)
- [ThinkGeo.MapSuite.Styles.TextStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.textstyle)
- [ThinkGeo.MapSuite.Layers.LegendAdornmentLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.legendadornmentlayer)
- [ThinkGeo.MapSuite.Styles.AreaStyles](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.areastyles)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)

### FAQ
- __Q: How do I make background map work?__  
A: Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require a Client ID and Secret. These were sent to you via email when you signed up with ThinkGeo, or you can register now at https://cloud.thinkgeo.com. Once you get them, please update the code in method LayoutRoot_Loaded().  

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
