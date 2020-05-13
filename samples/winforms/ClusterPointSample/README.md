# Cluster Point Sample for WinForms

### Description

ClusterPointStyle is integrated into Map Suite's styles. In this project you will see how to use the ClusterPointStyle for clustering various features into one. Sometimes, the map may have too many features which are stacked on top of each other making the map illegible at higher zoom levels. Clustering is a useful technique as it allows you to group together various features into one labeled symbol with the count of all the features. 

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://github.com/ThinkGeo/ClusterPointSample-ForWinForms/blob/master/Screenshot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
ClusterPointStyle clusterPointStyle = new ClusterPointStyle();
clusterPointStyle.MinimumFeaturesPerCellToCluster = 1;
clusterPointStyle.TextStyle = TextStyles.CreateSimpleTextStyle("FeatureCount", "Arail", 10, DrawingFontStyles.Regular, GeoColor.SimpleColors.Black);
clusterPointStyle.TextStyle.PointPlacement = PointPlacement.Center;
clusterPointStyle.TextStyle.OverlappingRule = LabelOverlappingRule.AllowOverlapping;
clusterPointStyle.DrawingClusteredFeature += (s,arg)=> {
    classBreakStyle = classBreakStyle ?? GetClassBreakStyle("FeatureCount");
    arg.Styles.Add(classBreakStyle);
};
```

### Getting Help

- [Map Suite Desktop for WinForms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)
- [Map Suite Desktop for WinForms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs

This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Styles.ClusterPointStyle](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Styles.ClusterPointStyle)

### About Map Suite

Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo

ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.

