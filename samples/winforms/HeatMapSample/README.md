# Heat Map Sample for WinForms

### Description

Heat maps is a technique increasingly used in various fields such in biology and other fields. See http://en.wikipedia.org/wiki/Heat_map. They are also used for displaying areas of webs page most frequently scanned by users. http://csscreme.com/heat-maps/.

At ThinkGeo, we are taking this concept to GIS and applying it to geographic maps. Heat maps are a great way to give the users a visually compelling representation of the distribution and intensity of geographic phenomenon. 

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/HeatMapSample/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource("../../data/swineflu.shp"); 

HeatLayer heatLayer = new HeatLayer(featureSource);
//Creates the HeatStyle to set the properties determining the display of the heat map with earth quake data.
//Notice that each point is treated with an intensity depending on the value of the column "other_magn1".
//So, in addition to the density of points location, the value for each point is also going to be counted into account
//for the coloring of the map.
HeatStyle heatStyle = new HeatStyle();
heatStyle.Alpha = 255;
heatStyle.PointRadius = 100; 
heatStyle.PointRadiusUnit = DistanceUnit.Kilometer;
heatStyle.Alpha = 180;
heatStyle.PointIntensity = 10; 
heatStyle.IntensityColumnName = "CONFIRMED";
heatStyle.IntensityRangeStart = 0;
heatStyle.IntensityRangeEnd = 638;

heatLayer.HeatStyle = heatStyle;
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Drawing.GeoCanvas](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocanvas)
- [ThinkGeo.MapSuite.Drawing.GeoImage](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geoimage)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.Shapes.Feature](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.feature)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
