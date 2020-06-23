# Intersect Line Sample for WinForms

### Description
In today’s project, we show how to split a line based on an intersecting line. To accomplish this task, basically two steps are needed. First, you need to find the crossing point using the GetCrossing function and then you split the line based on the crossing point using the GetLineOnLine function. If you are in the utilities industry working with electric network, gas pipes etc, you will find this project useful.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/IntersectLineSample/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
LayerOverlay layerOverLay = (LayerOverlay)winformsMap1.Overlays[1];
MapShapeLayer mapShapeLayer = (MapShapeLayer)layerOverLay.Layers[0];

LineShape lineShape = (LineShape)mapShapeLayer.MapShapes["Line1"].Feature.GetShape();
LineShape lineShape2 = (LineShape)mapShapeLayer.MapShapes["Line2"].Feature.GetShape();

MultipointShape multipointShape = lineShape.GetCrossing(lineShape2);

LineShape splitLineShape1 = (LineShape)lineShape.GetLineOnALine(StartingPoint.FirstPoint, multipointShape.Points[0]);
LineShape splitLineShape2 = (LineShape)lineShape.GetLineOnALine(StartingPoint.LastPoint, multipointShape.Points[0]);
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.layeroverlay)
- [ThinkGeo.MapSuite.Layers.MapShapeLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.mapshapelayer)
- [ThinkGeo.MapSuite.Shapes.LineShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.lineshape)
- [ThinkGeo.MapSuite.Shapes.MultipointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.multipointshape)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
