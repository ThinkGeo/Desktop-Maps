# Get Feature Clicked On With Projection Sample for Wpf

### Description

This WPF project shows a technique for finding the feature of a point, line or polygon-based layer that the user clicked on.  As with the earlier sample entitled "Get Feature Clicked On", in order to give the user the expected behavior, a buffer in screen coordinates needs to be set so that the selected feature is chosen within a constant distance in screen coordinates from where the user clicked on, regardless of the zoom level.  In addition, this sample also addresses a limitation of the earlier sample by showing the contrast between using the functions GetFeaturesNearestTo and GetFeaturesWithinDistanceOf.

While the function GetFeaturesNearestTo as used in "Get Feature Clicked On" works quickly and is adequate in most cases, it is not guaranteed to get the closest feature.  In contrast, with the new function GetFeaturesWithinDistanceOf, you can loop through the features within a specified tolerance and get their exact distance.  While this approach requires some more code, it is guaranteed to get the closest feature in any case.

This sample also demonstrates another aspect of distance queries. We have the layers projected to the Google Map projection from WGS84, and you'll notice that the aforementioned distance functions (GetFeaturesWithinDistanceOf and GetFeaturesWithinDistanceOf) work seamlessly without requiring the developer to worry whether the layers are projected or not.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/GetFeatureClickedOnWithProjectionSample/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
Proj4Projection proj4 = new Proj4Projection();
proj4.InternalProjectionParametersString = Proj4Projection.GetWgs84ParametersString();
proj4.ExternalProjectionParametersString = Proj4Projection.GetGoogleMapParametersString();

proj4.Open();
RectangleShape currentExtentGoogleMap = proj4.ConvertToExternalProjection(currentExtentWGS84);
proj4.Close();

GoogleMapsOverlay googleMapsOverlay = new GoogleMapsOverlay();
googleMapsOverlay.MapType = GoogleMapsMapType.Satellite;
wpfMap1.Overlays.Add(googleMapsOverlay);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Shapes.Proj4Projection](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.proj4projection)
- [ThinkGeo.MapSuite.Shapes.RectangleShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.rectangleshape)
- [ThinkGeo.MapSuite.Wpf.GoogleMapsOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.googlemapsoverlay)
- [ThinkGeo.MapSuite.Layers.GoogleMapsMapType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.googlemapsmaptype)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
