# ThinkGeo Cloud Elevation Sample for Wpf

### Description

This **Sample**  demonstrates how you can use ThinkGeo Cloud Client to get elevation data from ThinkGeo GIS Server, and shows the elevation data of a road in the form of a line chart.

There are two ways to get the elevation data of the line. First, get the points on the line by setting the interval distance. The other, is to take the points by setting the number of points to be fetched. Then query the elevation data of the point.

ThinkGeo Cloud Client support would work in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/ThinkGeoCloudElevationSample-ForWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
private void UpdateIdSecretToClient()
{
    elevationClient?.Dispose();
    elevationClient = new ElevationClient(clientId, clientSecret);
    elevationClient.BaseUris.Add(new Uri(GisServerUri));
}

private async Task<ElevationResult> GetElevationByLineAsync(LineShape line, int pointNumber, int distance)
{
    if (comboType.SelectedIndex == 0)
    {
        var response = await elevationClient.GetElevationOfLineAsync(line, 3857, numberOfSegments: pointNumber, elevationUnit: DistanceUnit.Meter);
        return response;
    }
    else if (comboType.SelectedIndex == 1)
    {
        var response = await elevationClient.GetElevationOfLineAsync(line, 3857, intervalDistance: distance, elevationUnit: DistanceUnit.Meter, intervalDistanceUnit: DistanceUnit.Meter);
        return response;
    }
    throw new NotSupportedException("Unknown Line model");
}

```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_Wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#wpf-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

Working...


### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
