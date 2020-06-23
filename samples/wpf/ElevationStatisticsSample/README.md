# Elevation Statistics Sample for WPF

### Description
This **Sample**   shows the elevation data of a road in the form of a line [chart][1].

- **Elevation SDK** - support query elevation data by points, line and polygon based on [SRTM][2] and Ned13 elevation source data.
 - **For point** - Create a buffer and aggregate all points within buffer to create average for the elevation of the point. 
 - **For line** - There are two ways to get the elevation data of the line. First, get the points on the line by setting the interval distance. The other, is to take the points by setting the number of points to be fetched. Then query the elevation data of the point.
 - **For polygon** - By setting the interval distance, clip the polygon to the grids and get all the center of the grids where the polygon is located. Now, determine whether the center points are within the surface or inside the surface (use improved arc-length method).
 
![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ElevationStatisticsSample/Screenshot.png)


### Requirements
This sample makes use of the following NuGet Packages

- [MapSuite 10.0.0][4]
- [AWSSDK.Core][5]
- [AWSSDK.S3][6]

### About the Code
>**Get elevation by points**
```cs
// If you have Elevation Data on the local machine, pass in the folder where the data exists. 
private Collection<Feature> GetElevationByPoints(Collection<PointShape> points)
{
    Elevation.Elevation elevation = new Elevation.Elevation();
    elevation.ElevationFeatureSources.Add(new SrtmElevationFeatureSource(sourceDir));
    elevation.Open();
    var features = elevation.GetElevationByPoints(points, 3857, DistanceUnit.Meter);
    elevation.Close();
    return features;
}
// After purchasing the Elevation data, you can consume the data online by passing in the Amazon ID and Key, like following.   
private Collection<Feature> GetElevationByPoints(Collection<PointShape> points)
{
    Elevation.Elevation elevation = new Elevation.Elevation();
    elevation.ElevationFeatureSources.Add(new S3CompressedSrtmElevationFeatureSource("accessKeyId", "secretAccessKey", "cacheFolder"));
    elevation.Open();
    var features = elevation.GetElevationByPoints(points, 4326, DistanceUnit.Meter);
    elevation.Close();
    return features;
}

```
> **Get elevation value and point**
```cs
foreach (var feature in features)
{
	PointShape point = new PointShape(feature.ColumnValues["point"]);
	double value = double.Parse(feature.ColumnValues["elevation"]);
}
```

### Getting Help
- [Map Suite Desktop for WPF Wiki Resources][7]
- [Map Suite Desktop for WPF Product Description][8]
- [ThinkGeo Community Site][9]
- [ThinkGeo Web Site][10]

### FAQ
- __Q: How do I make background map work?__  
A: Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require a Client ID and Secret. These were sent to you via email when you signed up with ThinkGeo, or you can register now at https://cloud.thinkgeo.com. Once you get them, please update the code in method Window_Loaded() in MainWindow.xaml.cs.  

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.


  [1]: https://lvcharts.net/App/examples/v1/wpf/Basic%20Line%20Chart
  [2]: https://dds.cr.usgs.gov/srtm/version2_1/Documentation/SRTM_Topo.pdf
  [4]: https://www.nuget.org/packages?q=ThinkGeo
  [5]: https://www.nuget.org/packages?q=AWSSDK
  [6]: https://www.nuget.org/packages?q=AWSSDK
  [7]: http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf
  [8]: https://thinkgeo.com/desktop
  [9]: http://community.thinkgeo.com/
  [10]: https://www.thinkgeo.com/
