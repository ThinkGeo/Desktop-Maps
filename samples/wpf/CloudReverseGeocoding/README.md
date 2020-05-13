# ThinkGeo Cloud Reverse Geocoding Sample for Wpf

### Description

This **Sample**  demonstrates how you can use ThinkGeo Cloud Client to get meaningful addresses from ThinkGeo GIS Server by a geographic location. It ships with an optimized set of worldwide coverage of cities and towns, but any customized data can be supported as well.

ThinkGeo Cloud Client support would work in all of the Map Suite controls such as Wpf, Web, MVC, WebApi, Android and iOS.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/ThinkGeoCloudReverseGeocodingSample-ForWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
>**Search the specific point and return best-matching places with nearbys**
```csharp
ReverseGeocodingClient reverseGeocodingClient = new ReverseGeocodingClient(clientId, clientSecret);
reverseGeocodingClient.BaseUris.Add(new Uri(GisServerUri));
ReverseGeocodingResult searchResult = await reverseGeocodingClient.SearchPointAsync(searchPoint.X, searchPoint.Y, 3857, searchRadius, DistanceUnit.Meter, reverseGeocodingOption);
```

>**Display the searched results on map**
```csharp
private void DisplaySearchResult(ReverseGeocodingResult searchResult)
{
    Collection<ReverseGeocodingLocation> serachedAddresses = new Collection<ReverseGeocodingLocation>();
    Collection<ReverseGeocodingLocation> serachedIntersection = new Collection<ReverseGeocodingLocation>();
    if (searchResult?.BestMatchLocation != null)
    {
        // Display address of the BestMatchingPlace in the left panel and add a marker.
        txtBestMatchingPlace.Text = searchResult.BestMatchLocation.Address;
        Marker marker = CreateMarkerByCategory("BestMatchingPlace", searchPoint, searchResult.BestMatchLocation.Address);
        var bestmatchingMarkerOverlay = (SimpleMarkerOverlay)WpfMap.Overlays["BestMatchingMarkerOverlay"];
        bestmatchingMarkerOverlay.Markers.Add(marker);

        var nearbyLocations = new List<ReverseGeocodingLocation>(searchResult.NearbyLocations);
        int intersectionIndex = 0;
        var intersections = nearbyLocations.FindAll(p => p.LocationCategory.ToLower().Contains("intersection"));
        foreach (var intersection in intersections)
        {
            serachedIntersection.Add(intersection);
            AddPropertiesForPlace(intersection, intersectionIndex);
            intersectionIndex++;
            nearbyLocations.Remove(intersection);
        }
        int addressIndex = 0;
        var addressPoints = nearbyLocations.FindAll(p => p.LocationCategory.ToLower().Contains("addresspoint"));
        foreach (var address in addressPoints)
        {
            serachedAddresses.Add(address);
            AddPropertiesForPlace(address, addressIndex);
            addressIndex++;
            nearbyLocations.Remove(address);
        }

        int placeIndex = 0;
        foreach (var place in nearbyLocations)
        {
            if (!nameof(LocationCategories.Aeroway).Equals(place.LocationCategory, StringComparison.InvariantCultureIgnoreCase)
                && !nameof(LocationCategories.Road).Equals(place.LocationCategory, StringComparison.InvariantCultureIgnoreCase)
                && !nameof(LocationCategories.Rail).Equals(place.LocationCategory, StringComparison.InvariantCultureIgnoreCase)
                && !nameof(LocationCategories.Waterway).Equals(place.LocationCategory, StringComparison.InvariantCultureIgnoreCase))
            {
                serachedPlaces.Add(place);
                AddPropertiesForPlace(place, placeIndex);
                placeIndex++;
            }
        }
        // Bind addresses,intersections,places to listbox.
        lsbAddress.ItemsSource = serachedAddresses;
        lsbPlaces.ItemsSource = serachedPlaces;
        lsbIntersection.ItemsSource = serachedIntersection;
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
