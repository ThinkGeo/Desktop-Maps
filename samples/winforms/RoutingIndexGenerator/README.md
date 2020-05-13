# Routing Sample for WinForms

### Description

The Map Suite Routing Index Generator is a utility that will allow you to generate routing index files (“.rtg” and “.rtx”) from World Streets .sqlite database. These routing index files will be used by the Map Suite Routing Extension in order to calculate routes and driving directions. This utility allows you to specify things that one-way road information, as well as configuring the road speed and type of routes you would like to calculate. It is easily extendable to allow you to add code to deal with other routing situations.

![Screenshot](https://github.com/ThinkGeo/RoutingIndexGeneratorSample-ForWinForms/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages/ThinkGeo.MapSuite)

[MapSuite Routing](https://www.nuget.org/packages/ThinkGeo.MapSuite.Routing)

[MapSuite Wkbfile Layer](https://www.nuget.org/packages/ThinkGeo.MapSuite.Layers.WkbFile)

[MapSuite SQLite Layer](https://www.nuget.org/packages/ThinkGeo.MapSuite.Layers.Sqlite)

[MapSuite Shapefile Layer](https://www.nuget.org/packages/ThinkGeo.MapSuite.Layers.ShapeFile)


### About the Code

public void StartBuildingRoutableFile()
{
    if (buildRtgParameter.OverrideRoutableFile)
    {
        // reset the process status.
        this.processedRecordCount = 0;
        FeatureSource source;
        if (buildRtgParameter.RoutableFileType == RoutableFileType.ShapeFile)
        {
            source = new ShapeFileFeatureSource(buildRtgParameter.SourceSegmentsFilePath);
            source.Open();
            this.totalRecordCount = source.GetCount();
            RtgRoutingSource.GenerateRoutableShapeFile(buildRtgParameter.SourceSegmentsFilePath, buildRtgParameter.RoutableFilePathName, buildRtgParameter.OverrideRoutableFile ? OverwriteMode.Overwrite : OverwriteMode.DoNotOverwrite, buildRtgParameter.GeographyUnit, 2);
        }
        else
        {
            string sourceSQLiteConnectionString = string.Format("Data Source={0};Version=3;", buildRtgParameter.SourceSegmentsFilePath);
            source = new SqliteFeatureSource(sourceSQLiteConnectionString, "RouteSegments");
            source.Open();
            this.totalRecordCount = source.GetCount();
            RtgRoutingSource.GenerateRoutableSQLiteFile(buildRtgParameter.SourceSegmentsFilePath, buildRtgParameter.RoutableFilePathName, buildRtgParameter.OverrideRoutableFile ? OverwriteMode.Overwrite : OverwriteMode.DoNotOverwrite, buildRtgParameter.GeographyUnit, 10);
        }
        source.Close();
    }
}

### Getting Help

[Map Suite Routing Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_server_routing)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Routing](http://wiki.thinkgeo.com/wiki/map_suite_routing_api)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
