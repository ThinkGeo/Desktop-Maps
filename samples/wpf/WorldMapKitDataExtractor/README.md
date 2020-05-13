# World Map Kit Data Extractor Sample for Wpf

### Description

The World Map Kit Data Extractor allows you to create new smaller subsets from the World Map Kit SQLite master database. You simply specify the bounding box (or a shape file) for the new area then it will create a new SQLite database for that regions. There are options to preserve high level data allowing you to keep higher extent features, about 700 meg of data, such as the world, countries, high level roads etc. as a backdrop to the cut out area. If you choose not to preserve the high level data the tool crosscuts every layer leaving you with the smallest dataset possible. The tool works with multiple projections but does not re-project. Specifying the srid allows you to configure your bounding box in decimal degrees regardless of the source database projection. We will consider enhancing the tool to support projection based on feedback.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/WorldMapKitDataExtractorSample-ForWpf/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
Collection<string> targetTables = SqliteFeatureSource.GetTableNames($@"Data Source={outputDataPath};Version=3;");

if (!targetTables.Contains(table.TableName))
   SqliteFeatureSource.CreateTable($@"Data Source={outputDataPath};Version=3;", table.TableName, table.Columns, geographyUnit);

SqliteFeatureSource tempSource = new SqliteFeatureSource($@"Data Source={outputDataPath};Version=3;", table.TableName);
tempSource.Open();
tempSource.BeginTransaction();

int counter = 0;
UpdateStatus($"Adding <1000 new features.");
foreach (var feature in intersectFeatures.Distinct(new FeatureComparer()))
{
   tempSource.AddFeature(feature);
   counter++;
   if (counter % 1000 == 0)
   {
      UpdateStatus($"Adding {counter} new features.");
   }
}
UpdateStatus("Commiting sqlite database transaction.");
tempSource.CommitTransaction();
```

### Getting Help

- [Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)
- [Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.WorldStreetsLayer](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.WorldStreetsLayer)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
