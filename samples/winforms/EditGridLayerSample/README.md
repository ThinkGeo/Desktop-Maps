# Edit Grid Layer Sample for WinForms

### Description

This WinForms project demonstrates how you can update a grid shape file using a spatial query. The elements in the file are rendered using ClassBreakStyles, and change when the values of the Features are incremented.

We have dramatically improved the performance for GridFeatureLayer on MapSuite10.0. The performance compare is as below:

| **Feature Count**                             | **Version**   | **Times(ms)**     |
| --------------------------------------------- | ------------- | ----------------- |
| 1164800                                       | Old           | 13105             | 
| 1164800                                       | New           | 3735              |
| 11680                                         | Old           | 188               |
| 11680                                         | New           | 175               |

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/EditGridLayerSample-ForWinFoms/blob/master/Screenshot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
GridFeatureLayer gridFeatureLayer = (GridFeatureLayer)staticOverlay.Layers["Grid View"];
Collection<Feature> featuresToUpdate = gridFeatureLayer.QueryTools.GetFeaturesWithin(selectionFeature, new string[0]);
gridFeatureLayer.Open();
gridFeatureLayer.EditTools.BeginTransaction();
foreach (Feature featureToUpdate in featuresToUpdate)
{
    featureToUpdate.ColumnValues["CellValue"] = (int.Parse(featureToUpdate.ColumnValues["CellValue"]) + 1).ToString();
    gridFeatureLayer.EditTools.Update(featureToUpdate);
}
gridFeatureLayer.EditTools.CommitTransaction();
gridFeatureLayer.Close();
```

### Getting Help

- [Map Suite Desktop for WinForms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)
- [Map Suite Desktop for WinForms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs

This example makes use of the following APIs:

- [ThinkGeo.MapSuite.WinForms.WinformsMap](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.WinForms.WinformsMap)
- [ThinkGeo.MapSuite.Layers.GridFeatureLayer](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.GridFeatureLayer)

### About Map Suite

Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo

ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
