# Edit Attribute Of Shapefile Sample for Wpf

### Description

The purpose of this Wpf sample is to show how to edit the attributes of a feature of a shapefile. This sample is useful for anyone wanting to actualize the attributes part of its data by simply clicking on the desired feature on the map and updating its attributes in a textbox. You will find the editing part of the code in the **KeyDown** event of the textbox.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/EditAttributeOfShapeFile/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ScreenPointF bufferPointF = new ScreenPointF(clickedPointF.X + screenBuffer, clickedPointF.Y);

double distanceBuffer = ExtentHelper.GetWorldDistanceBetweenTwoScreenPoints(wpfMap1.CurrentExtent, clickedPointF, bufferPointF,
                                                    (float)wpfMap1.Width, (float)wpfMap1.Height, wpfMap1.MapUnit, DistanceUnit.Meter);

ShapeFileFeatureLayer layerShapefileFeatureLayer = (ShapeFileFeatureLayer)wpfMap1.FindFeatureLayer("LabelCountriesLayer");
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Shapes.ScreenPointF](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.screenpointf)
- [ThinkGeo.MapSuite.Shapes.ExtentHelper](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.extenthelper)
- [ThinkGeo.MapSuite.Layers.ShapeFileFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefilefeaturelayer)
- [ThinkGeo.MapSuite.Shapes.DistanceUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.distanceunit)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
