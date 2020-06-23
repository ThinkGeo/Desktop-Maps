# Place search world reverse geocoding sample for Wpf

### Description
In this sample, we show how you can use Map Suite [World Reverse Geocoding SDK](https://thinkgeo.com/gisserver#feature) to turn a geographic location into meaningful addresses. It ships with an optimized set of worldwide coverage of cities and towns, but any customized data can be supported as well.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/PlaceSearchWordReverseGeocoding/Screenshot.gif)


### What areas does the World Reverse Geocoding cover?

![World Reverse Geocoding Data Coverage](https://github.com/ThinkGeo/PlaceSearchWorldReverseGeocodingSample-ForWpf/blob/master/Reverse%20geocoding%20data%20coverage.png)


### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)
### About the Code
>**Search the specific point and return best-matching places with nearbys**
```cs
        private void SearchPlaceAndNearbys()
        {
            // Reset UI and clear existing before search again.
            ClearupPreviousSearchResult();

            searchPoint = GetSearchPoint();

            // Create the searchPreference by UI controls.
            SearchPreference searchPreference = GetSearchPreferenceFromUI();
            searchResult = osmReverseGeocoder.Search(searchPoint, searchPreference);
            if (searchResult != null)
            {
                // Update search result to map markers.
                DisplaySearchResult(searchResult);

                // Update search result to left panel list.
                tabSearchResult.SelectedIndex = 0;
                TabSearchResult_SelectionChanged(tabSearchResult, null);
            }
        }
```
>**Display the searched results on map**
```cs
        private void DisplaySearchResult(PlaceReverseGeocoderResult searchResult)
        {
            if (searchResult != null && searchResult.BestMatchingPlace != null)
            {
                // Display address of the BestMatchingPlace in the left panel and add a marker.
                txtBestMatchingPlace.Text = searchResult.BestMatchingPlace.Address;
                Marker marker = CreateMarkerByCategory("BestMatchingPlace", searchPoint, searchResult.BestMatchingPlace.Address);
                bestmatchingMarkerOverlay.Markers.Add(marker);

                // Add index,distance and direction for addresses, places and intersections.
                for (int i = 0; i < searchResult.Intersections.Count; i++)
                {
                    AddPropertiesForPlace(searchResult.Intersections[i], i);
                }
                for (int i = 0; i < searchResult.Addresses.Count; i++)
                {
                    AddPropertiesForPlace(searchResult.Addresses[i], i);
                }

                int placeIndex = 0;
                foreach (Place place in searchResult.Places)
                {
                    if (place.PlaceCategory != PlaceCategory.Highway && place.PlaceCategory != PlaceCategory.Road && place.PlaceCategory != PlaceCategory.Path && place.PlaceCategory != PlaceCategory.LinkRoad)
                    {
                        serachedPlaces.Add(place);
                        AddPropertiesForPlace(place, placeIndex);
                        placeIndex++;
                    }
                }

                // Bind addresses,intersections,places to listbox.
                lsbAddress.ItemsSource = searchResult.Addresses;
                lsbPlaces.ItemsSource = serachedPlaces;
                lsbIntersection.ItemsSource = searchResult.Intersections;
            }
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
