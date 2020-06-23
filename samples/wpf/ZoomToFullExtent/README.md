# Zoom To Full Extent Wpf Sample for Wpf

### Description

This is a simple project that shows how to set the current extent based on a collection of layers. You can use this technique for the common task of having the map set to the full extent. Instead of having to manually set the full extent, you can pass all the layers you want the full extent to be based on.
              
Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ZoomToFullExtent/ScreenShot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
Collection<BaseShape> rectangleShapes = new Collection<BaseShape>();

foreach (Layer layer in Layers)
{
    layer.Open();
    if (layer.HasBoundingBox == true)
        rectangleShapes.Add(layer.GetBoundingBox());
}
return ExtentHelper.GetBoundingBoxOfItems(rectangleShapes);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Shapes.BaseShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.baseshape)
- [ThinkGeo.MapSuite.Shapes.RectangleShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.rectangleshape)
- [ThinkGeo.MapSuite.Shapes.ExtentHelper](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.extenthelper)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
