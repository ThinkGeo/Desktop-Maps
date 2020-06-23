# Nldas Ascii Grid Layer Sample for WinFoms

### Description

This WinForms project demonstrates how you can create a North American Land Data Assimilation System (NLDAS) grid layer. In this sample, you can find what NLDAS grid cells are fully contained in a given Shape file and what cells are partially contained. For each partially contained cell we can calculate what percent of the cell area is contained within the ShapeFile. Please refer to [NLDAS grid](https://ldas.gsfc.nasa.gov/nldas/NLDASspecs.php) for details.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/NLDASAnalysisSample/Screenshot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
public class NldasAsciiGridFeatureSource : FeatureSource
{
	protected override Collection<Feature> GetAllFeaturesCore(IEnumerable<string> returningColumnNames)
        {
            ValidatorHelper.CheckFeatureSourceIsOpen(IsOpen);
            var allFeatures = new Collection<Feature>();
        
            for (int i = 0; i < rowsCount - 1; i++)
            {
                for (int j = 0; j < columnsCount - 1; j++)
                {
                    PointShape upperLeftPoint = new PointShape(gridExtent.UpperLeftPoint.X + j * cellSize, gridExtent.UpperLeftPoint.Y - i * cellSize);
                    PointShape lowerRightPoint = new PointShape(upperLeftPoint.X + cellSize, upperLeftPoint.Y - cellSize);
                    var rectangleShape = new RectangleShape(upperLeftPoint, lowerRightPoint);
                    var feature = new Feature(rectangleShape, new Collection<string>() { $"GridNumber:{i + 1},{j + 1}" });
                    feature.Id = $"{i}_{j}";
                    allFeatures.Add(feature);
                }
            }
        
            return allFeatures;
        }
}
```

### Getting Help

- [Map Suite Desktop for WinForms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)
- [Map Suite Desktop for WinForms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs

This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.FeatureSource](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.featuresource)
- [ThinkGeo.MapSuite.Shapes.Feature](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.feature)
- [ThinkGeo.MapSuite.Shapes.PointShape](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.pointshape)

### About Map Suite

Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo

ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
