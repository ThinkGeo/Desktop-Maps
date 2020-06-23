# Elevation Grade Of Line Sample for Wpf

### Description
In this sample, we show how you can use Map Suite [Elevation SDK](https://thinkgeo.com/gisserver#feature) to get the elevation values of a specific line for your .NET application. It allows you to customize your query to get data as detailed as you need it to be. Draw elevation profiles for your  hiking or biking trip, contral the granularity of the response, sample elevation values at controllable intervals along a route should be shown up.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/ElevationGradeOfLineSample/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
>**Get sample elevation values at controllable intervals along a line**
```cs
        private Collection<Feature> GetElevationByLine(LineShape line, int pointNumber)
        {
            Elevation.Elevation elevation = new Elevation.Elevation();
            elevation.ElevationFeatureSourcesInDecimalDegrees.Add(new SrtmElevationFeatureSource(sourceDir));
            elevation.Open();
            Collection<Feature> features =elevation.GetElevationByLine(line, 3857, DistanceUnit.Meter, pointNumber);

            elevation.Close();
            return features;
        }
```
>**Get elevation value of a point**
```cs
        private Feature GetElevationByPoint(PointShape point)
        {
            Elevation.Elevation elevation = new Elevation.Elevation();
            elevation.ElevationFeatureSourcesInDecimalDegrees.Add(new SrtmElevationFeatureSource(sourceDir));
            elevation.Open();
            var features = elevation.GetElevationByPoint(point, 3857, DistanceUnit.Meter);
            elevation.Close();
            return features;
        }
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)


### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
