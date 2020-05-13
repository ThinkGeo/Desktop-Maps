# Geocoding Sample for WinForms

### Description

[Sample Data Download](http://wiki.thinkgeo.com/wiki/_media/geocoding_hodoisamples_data.zip)

The Map Suite Geocoder “How Do I?” solution offers a series of useful how-to examples for using the Map Suite Geocoder component. The bundled solution comes with a small set of sample data from Chicago, IL and demonstrates geocoding, reverse geocoding, batch geocoding, use of fuzzy matching logic, getting the closest street number to a point, and much more. Full source code is included in both C# and VB.NET languages; simply select your preferred language to download the associated solution.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/GeocodingSample-ForWinForms/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
UsaGeocoder usaGeoCoder = new UsaGeocoder(dataPath);

LayerOverlay markerOverlay = winformsMap1.Overlays["MarkerOverlay"] as LayerOverlay;
MarkerLayer markerLayer = markerOverlay.Layers["MarkerLayer"] as MarkerLayer;
if (matchItem.NameValuePairs.Count > 0)
{
    Proj4Projection proj4Projection = new Proj4Projection(4326, 3857);
    proj4Projection.Open();

    // Get the centroid point from the WKT string in the MatchPair
    if (!string.IsNullOrEmpty(matchItem.NameValuePairs["CentroidPoint"]))
    {
        var point = new PointShape(matchItem.NameValuePairs["CentroidPoint"]);
        markerLayer.MarkerLocation = (PointShape)proj4Projection.ConvertToExternalProjection(point);
    }

    // Set the extent that is from the WKT string in the MatchPair and refresh the map
    if (!string.IsNullOrEmpty(matchItem.NameValuePairs["BoundingBox"]))
    {
        var rectangle = new RectangleShape(matchItem.NameValuePairs["BoundingBox"]);
        winformsMap1.CurrentExtent = proj4Projection.ConvertToExternalProjection(rectangle);
    }
    winformsMap1.Refresh();
}
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.LayerOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.winforms.layeroverlay)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)
- [ThinkGeo.MapSuite.Shapes.RectangleShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.rectangleshape)

### FAQ
- __Q: How do I make background map work?__  
A: Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require a Client ID and Secret. These were sent to you via email when you signed up with ThinkGeo, or you can register now at https://cloud.thinkgeo.com. Once you get them, please update the code in UserControl partial classes.  

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
