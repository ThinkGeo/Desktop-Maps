# Centering And Rotating Sample for Wpf

### Description

This project is the Wpf version of the services edition sample “Centering and Rotating On Moving Feature”, where we learn how to have the map always centered and rotated based on the location and direction of a moving vehicle. This issues addresses some issues you have to be aware of regarding both ShapeFileFeatureLayer and InMemoryFeatureLayer when applying rotation to the map.
              
Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/CenteringAndRotatingSample-ForWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
LayerOverlay dynamicOverlay = (LayerOverlay)wpfMap1.Overlays["DynamicOverlay"];
InMemoryFeatureLayer inMemoryFeatureLayer = (InMemoryFeatureLayer)dynamicOverlay.Layers["GPSlocations"];

PointShape currentPointShape = (PointShape)inMemoryFeatureLayer.InternalFeatures[count].GetShape();

//Step 1: Center the map on the moving vehicle.
PointShape rotatedPointShape = (PointShape)rotateProjection.ConvertToExternalProjection(currentPointShape);
//Notice that we use Register method instead of CenterAt to avoid refreshing the map when centering.
RectangleShape newExtent = (RectangleShape)wpfMap1.CurrentExtent.Register(wpfMap1.CurrentExtent.GetCenterPoint(), rotatedPointShape, DistanceUnit.Meter, GeographyUnit.Meter);

//Step 2: Rotate the map based on the vehicle direction.
//Gets the angle based on the current GPS position and the previous one to get the direction of the vehicle.
double angle = GetAngleFromTwoVertices(new Vertex(previousLong, previousLat), new Vertex(currentPointShape.X, currentPointShape.Y));

rotateProjection.Angle = angle;
wpfMap1.CurrentExtent = rotateProjection.GetUpdatedExtent(newExtent);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.layeroverlay)
- [ThinkGeo.MapSuite.Layers.InMemoryFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.inmemoryfeaturelayer)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.Shapes.DistanceUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.distanceunit)
- [ThinkGeo.MapSuite.GeographyUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.geographyunit)
- [ThinkGeo.MapSuite.Shapes.Vertex](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.vertex)
- [ThinkGeo.MapSuite.GeographyUnit](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.geographyunit)
- [ThinkGeo.MapSuite.Layers.RotationProjection](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.rotationprojection)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
