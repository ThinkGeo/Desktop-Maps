# ThinkGeo Cloud Geocoding Sample for Wpf

### Description

This **Sample**  demonstrates how you can use ThinkGeo Cloud Client to get a geographic location from ThinkGeo GIS Server by a street address.

ThinkGeo Cloud Client support would work in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/CloudGeocodingSample/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
private void UpdateIdSecretToClient()
{
    geocodingClient?.Dispose();
    geocodingClient = new GeocodingClient(clientId, clientSecret);
    geocodingClient.BaseUris.Add(new Uri(GisServerUri));
}

private async Task SearchLocation(string searchText, GeocodingOptions options)
{
    GeocodingResult result = null;
    result = await geocodingClient.SearchAsync(searchText, options);
    if (result.Exception != null)
    {
        MessageBox.Show(result.Exception.Message, "Error");
        return;
    }
    markerOverlay.Markers.Clear();
    lsbLocations.ItemsSource = null;
    WpfMap.Refresh();

    txbSearchResultDescription.Text = $"Find {result.Locations.Count} locations.";
    lsbLocations.ItemsSource = result.Locations;
    if (result.Locations.Count > 0)
    {
        lsbLocations.Visibility = Visibility.Visible;
        lsbLocations.SelectedIndex = 0;
    }
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