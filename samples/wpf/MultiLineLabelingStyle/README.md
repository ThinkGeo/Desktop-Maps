# Multi Line Labeling Sample for Wpf

### Description

For labeling purpose, **TextStyle** has a property called LabelAllPolygonParts that will label all the parts making up a polygon based feature. Unfortunately, we don’t have an equivalent API for labeling all the parts of a line based feature. But thanks to the flexible framework of Map Suite, we show in this WPF sample how easily you can expand the **TextStyle** class to allow this labeling capability. Look at the custom class **MultiLinetextStyle** and how **DrawCore** function is overridden to have the expected labeling behavior.
              
Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/MultiLineLabelingStyle/ScreenShot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
class MultiLineTextStyle : TextStyle
{
    protected override void DrawCore(System.Collections.Generic.IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
    {
        Collection<Feature> featuresForDrawing = new Collection<Feature>();
        foreach (Feature feature in features)
        {
            if (feature.GetWellKnownType() == WellKnownType.Multiline)
            {
                MultilineShape multiline = (MultilineShape)(feature.GetShape());

                foreach (LineShape line in multiline.Lines)
                {
                    Feature newFeature = new Feature(line);
                    CopyColumnValues(newFeature, feature);
                    featuresForDrawing.Add(newFeature);
                }
            }
            else
            {
                featuresForDrawing.Add(feature);
            }
        }

        base.DrawCore(featuresForDrawing, canvas, labelsInThisLayer, labelsInAllLayers);
    }
}
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Styles.TextStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.textstyle)
- [ThinkGeo.MapSuite.Shapes.Feature](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.feature)
- [ThinkGeo.MapSuite.Drawing.GeoCanvas](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocanvas)
- [ThinkGeo.MapSuite.Styles.SimpleCandidate](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.simplecandidate)
- [ThinkGeo.MapSuite.Shapes.WellKnownType](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.wellknowntype)
- [ThinkGeo.MapSuite.Shapes.MultilineShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.multilineshape)
- [ThinkGeo.MapSuite.Shapes.LineShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.lineshape)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
