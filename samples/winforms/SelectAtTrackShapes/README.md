# Select At Track Shape Sample for WinForms

### Description

In today’s Desktop project, we combine the skills we learned in the samples “Spatial Query A Feature Layer” and “Track And Edit Shapes”. You can see how we use the event TrackEnded to get the RectangleShape from the tracked shape of TrackOverlay to do the spatial query. In this example, we use a Rectangle but you could also very easily use another shape such as Polygon, Circle, etc.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/SelectAtTrackShapes/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
RectangleShape rectangleShape = (RectangleShape)e.TrackShape;

ShapeFileFeatureLayer worldLayer = (ShapeFileFeatureLayer)winformsMap1.FindFeatureLayer("WorldLayer");
InMemoryFeatureLayer spatialQueryResultLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("SpatialQueryResultLayer");

Collection<Feature> spatialQueryResults;
worldLayer.Open();
spatialQueryResults = worldLayer.QueryTools.GetFeaturesIntersecting(rectangleShape, ReturningColumnsType.NoColumns);
worldLayer.Close();

spatialQueryResultLayer.Open();

winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Shapes.RectangleShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.rectangleshape)
- [ThinkGeo.MapSuite.Layers.ShapeFileFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.shapefilefeaturelayer)
- [ThinkGeo.MapSuite.Layers.InMemoryFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.inmemoryfeaturelayer)
- [ThinkGeo.MapSuite.Layers.QueryTools](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.querytools)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
