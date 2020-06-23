# Site Selection Sample for WinForms

### Description

The Site Selection sample template allows you to view, understand, interpret, and visualize spatial data in many ways that reveal relationships, patterns, and trends. In the example illustrated, the user can apply the features of GIS to analyze spatial data to efficiently choose a suitable site for a new retail outlet.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/SiteSelectionSample/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
Collection<Feature> queryResultFeatures = new Collection<Feature>();
if (mapModel.ServiceAreaLayer.InternalFeatures.Count == 0) return queryResultFeatures;

BaseShape serviceAreaShape = mapModel.ServiceAreaLayer.InternalFeatures[0].GetShape();
Collection<Feature> selectedFeatures = mapModel.QueryingFeatureLayer.QueryTools.GetFeaturesWithin(serviceAreaShape, ReturningColumnsType.AllColumns);

if (CandidatePoiTypesComboBox.SelectedItem.ToString().Equals(Resources.FindAllText, StringComparison.OrdinalIgnoreCase))
{
    foreach (Feature feature in selectedFeatures)
    {
        queryResultFeatures.Add(feature);
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

- [ThinkGeo.MapSuite.Shapes.Feature](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.feature)
- [ThinkGeo.MapSuite.Shapes.BaseShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.baseshape)
- [ThinkGeo.MapSuite.Layers.QueryTools](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.querytools)

### FAQ
- __Q: How do I make background map work?__  
A: Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require a Client ID and Secret. These were sent to you via email when you signed up with ThinkGeo, or you can register now at https://cloud.thinkgeo.com. Once you get them, please update the code in method InitializeOverlays() in MapModel.cs.  

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
