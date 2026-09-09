using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to use the ReverseGeocodingCloudClient to access the ReverseGeocoding APIs available from the ThinkGeo Cloud
    /// </summary>
    public partial class ReverseGeocoding
    {

        private bool _initialized;
        private ReverseGeocodingCloudClient _reverseGeocodingCloudClient;
        private readonly InMemoryGeometrySource _searchRadius = new InMemoryGeometrySource();
        private readonly InMemoryGeometrySource _selectedResult = new InMemoryGeometrySource();

        public ReverseGeocoding()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay, as well as several feature layers to display the reverse geocoding search area and locations
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // The circle that was searched and the location picked out of the results go
            // to the GPU with the basemap. Drawn on an overlay they would be raster tiles,
            // so they would stretch through a fractional zoom while the map under them
            // stayed sharp.
            var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));

            // The GPU draws a marker from a named image, so point styles the GPU has no
            // shape of its own for - a cross, a star - are drawn once by the CPU and
            // registered under the names the markers ask for.
            style.Images.Add("center", new PointStyle(PointSymbolType.Cross, 20, GeoBrushes.Red));
            style.Images.Add("match", new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple));
            style.AddGeometry(_searchRadius, new GeometryStyle
            {
                FillColor = new GeoColor(10, GeoColors.Blue),
                OutlineColor = new GeoColor(100, GeoColors.Blue),
                OutlineWidthInPixels = 2,
            });
            style.AddGeometry(_selectedResult, new GeometryStyle
            {
                FillColor = new GeoColor(80, GeoColors.MediumPurple),
                OutlineColor = GeoColors.MediumPurple,
                OutlineWidthInPixels = 2,
                LineColor = GeoColors.MediumPurple,
                LineWidthInPixels = 6,
            });
            Map.Basemap = new GpuBasemap(style);

            // Create a popup overlay to display the best match
            Map.Overlays.Add("Best Match Popup Overlay", new PopupOverlay());

            Map.CenterPoint = new PointShape(-10778720, 3915154);
            Map.CurrentScale = 202090;

            // Initialize the ReverseGeocodingCloudClient with our ThinkGeo Cloud credentials
            _reverseGeocodingCloudClient = new ReverseGeocodingCloudClient
            {
                ClientId = SampleKeys.ClientId2,
                ClientSecret = SampleKeys.ClientSecret2,
            };

            CboLocationCategories.SelectedIndex = 0;

            _ = Map.RefreshAsync();
        }

        /// <summary>
        /// Perform the reverse geocode when the user clicks on the map
        /// </summary>
        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (e.MouseButton == MapMouseButton.Left)
            {
                // Set the coordinates in the UI
                TxtCoordinates.Text = $"{e.WorldY:0.000000},{e.WorldX:0.000000}";

                // Run the reverse geocode
                PerformReverseGeocode();
            }
        }

        /// <summary>
        /// Perform the reverse geocode when the user clicks the 'Search' button
        /// </summary>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            // Run the reverse geocode using the coordinates in the 'Location' text box
            PerformReverseGeocode();
        }

        /// <summary>
        /// Perform the reverse geocode using the ReverseGeocodingCloudClient and update the UI
        /// </summary>
        private async void PerformReverseGeocode()
        {
            try
            { 
                // Perform some simple validation on the input text boxes
                if (!ValidateSearchParameters()) return;
                var options = new CloudReverseGeocodingOptions();

                // Set up the CloudReverseGeocodingOptions object based on the parameters set in the UI
                var coordinates = TxtCoordinates.Text.Split(',');
                var lat = double.Parse(coordinates[0].Trim());
                var lon = double.Parse(coordinates[1].Trim());
                var searchRadius = int.Parse(TxtSearchRadius.Text);
                const DistanceUnit searchRadiusDistanceUnit = DistanceUnit.Meter;
                const int pointProjectionInSrid = 3857;
                var searchPoint = new PointShape(lon, lat);
                options.MaxResults = int.Parse(TxtMaxResults.Text);
                // IncludeOverturePlaces and LocationCategories exist in the released
                // ThinkGeo.Core package but not in this repo's
                // CloudReverseGeocodingOptions, so those two controls are inert here.

                // Show a loading graphic to let users know the request is running
                LoadingImage.Visibility = Visibility.Visible;

                // Run the reverse geocode
                //var searchResult = await _reverseGeocodingCloudClient.SearchPointAsync(lon, lat, pointProjectionInSrid, searchRadius, searchRadiusDistanceUnit, options);
                CloudReverseGeocodingResult searchResult;
                try
                {
                    searchResult = await _reverseGeocodingCloudClient.SearchPointAsync(lon, lat, pointProjectionInSrid, searchRadius, searchRadiusDistanceUnit, options);

                }
                catch (System.ArgumentNullException)
                {
                    MessageBox.Show("Please enter a valid set of coordinates to search", "Error");
                    return;
                }
                // Hide the loading graphic
                LoadingImage.Visibility = Visibility.Hidden;

                // Handle an exception returned from the service
                if (searchResult.Exception != null)
                {
                    MessageBox.Show(searchResult.Exception.Message, "Error");
                    return;
                }

                // Update the UI
                await DisplaySearchResultsAsync(searchPoint, searchRadius, searchResult);
            }
            catch 
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// Update the UI based on the search results from the reverse geocode
        /// </summary>
        private async Task DisplaySearchResultsAsync(PointShape searchPoint, int searchRadius, CloudReverseGeocodingResult searchResult)
        {
            // Show the area that was searched, and the point it was searched around
            var searched = new EllipseShape(searchPoint, searchRadius);
            _searchRadius.UpdateAreas(new[] { searched });
            _searchRadius.UpdatePoints(new[] { searchPoint }, sizeInPixels: 20f, iconName: "center");

            // Nothing is picked out of the results yet
            _selectedResult.UpdateAreas(Array.Empty<BaseShape>());
            _selectedResult.UpdateLines(Array.Empty<BaseShape>());
            _selectedResult.UpdatePoints(Array.Empty<BaseShape>());

            // If a match was found for the geocode, update the UI
            if (searchResult?.BestMatchLocation != null)
            {
                // Get the 'Best Match' PopupOverlay from the Map and clear it
                var bestMatchPopupOverlay = (PopupOverlay)Map.Overlays["Best Match Popup Overlay"];
                bestMatchPopupOverlay.Popups.Clear();

                // Get the location of the 'Best Match' found within the search radius
                var bestMatchLocation = searchResult.BestMatchLocation.LocationFeature.GetShape().GetClosestPointTo(searchPoint, GeographyUnit.Meter) ??
                                        searchResult.BestMatchLocation.LocationFeature.GetShape().GetCenterPoint();

                // Create a popup to display the best match, and add it to the PopupOverlay
                var bestMatchPopup = new Popup(bestMatchLocation)
                {
                    Content = "Best Match: " + searchResult.BestMatchLocation.Address,
                    FontSize = 10d,
                    FontFamily = new System.Windows.Media.FontFamily("Verdana")
                };
                bestMatchPopupOverlay.Popups.Add(bestMatchPopup);

                // Sort the locations found into three groups (Addresses, Places, Roads) based on their LocationCategory
                var nearbyLocations = new Collection<CloudReverseGeocodingLocation>(searchResult.NearbyLocations);
                var nearbyAddresses = new Collection<CloudReverseGeocodingLocation>();
                var nearbyPlaces = new Collection<CloudReverseGeocodingLocation>();
                var nearbyRoads = new Collection<CloudReverseGeocodingLocation>();
                foreach (var foundLocation in nearbyLocations)
                {
                    if (foundLocation.LocationCategory.ToLower().Contains("addresspoint"))
                    {
                        nearbyAddresses.Add(foundLocation);
                    }
                    else if (nameof(CloudLocationCategories.Aeroway).Equals(foundLocation.LocationCategory)
                        || nameof(CloudLocationCategories.Road).Equals(foundLocation.LocationCategory)
                        || nameof(CloudLocationCategories.Rail).Equals(foundLocation.LocationCategory)
                        || nameof(CloudLocationCategories.Waterway).Equals(foundLocation.LocationCategory))
                    {
                        nearbyRoads.Add(foundLocation);
                    }
                    else if (!nameof(CloudLocationCategories.Intersection).Equals(foundLocation.LocationCategory))
                    {
                        // Note:  Overture Place data does not have 'address' info, so it's null and we just use lat/lon.
                        if (foundLocation.LocationCategory == "Overture_Places")
                        {
                            foundLocation.Address = foundLocation.LocationName;
                        }
                        nearbyPlaces.Add(foundLocation);
                    }
                }

                // Set the data sources for the addresses, roads, and places list boxes
                LsbAddresses.ItemsSource = nearbyAddresses;
                LsbRoads.ItemsSource = nearbyRoads;
                LsbPlaces.ItemsSource = nearbyPlaces;

                TxtSearchResultsBestMatch.Text = "Best Match: " + searchResult.BestMatchLocation.Address;
            }
            else
            {
                TxtSearchResultsBestMatch.Text = "No address or place matches found for this location";
            }

            var searchRadiusBoundingBox = searched.GetBoundingBox();
            Map.CenterPoint = searchRadiusBoundingBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, searchRadiusBoundingBox, Map.MapWidth, Map.MapHeight);
            var standardZoomLevelSet = new ZoomLevelSet();
            if (Map.CurrentScale < standardZoomLevelSet.ZoomLevel18.Scale)
            {
                await Map.ZoomToAsync(standardZoomLevelSet.ZoomLevel18.Scale);
            }
            await Map.RefreshAsync();
        }

        /// <summary>
        /// When a location is selected in the UI, draw the matching feature found and center the map on it
        /// </summary>
        private async void LsbSearchResults_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedResultList = (ListBox)sender;
                if (selectedResultList.SelectedItem != null)
                {
                    // Get the selected location
                    var locationFeature = ((CloudReverseGeocodingLocation)selectedResultList.SelectedItem).LocationFeature;

                    // A found location can be an address point, a road or a building
                    // footprint, so it goes to all three calls and each takes its own.
                    var found = new[] { locationFeature.GetShape() };
                    _selectedResult.UpdateAreas(found);
                    _selectedResult.UpdateLines(found);
                    _selectedResult.UpdatePoints(found, sizeInPixels: 24f, iconName: "match");

                    // Center the map on the chosen location
                    var locationFeatureBBox = locationFeature.GetBoundingBox();
                    Map.CenterPoint = locationFeatureBBox.GetCenterPoint();
                    Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, locationFeatureBBox, Map.MapWidth, Map.MapHeight);
                    var standardZoomLevelSet = new ZoomLevelSet();
                    if (Map.CurrentScale < standardZoomLevelSet.ZoomLevel18.Scale)
                    {
                        await Map.ZoomToAsync(standardZoomLevelSet.ZoomLevel18.Scale);
                    }
                    await Map.RefreshAsync();
                }
            }
            catch 
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// Helper function to perform simple validation on the input text boxes
        /// </summary>
        private bool ValidateSearchParameters()
        {
            // Check if the 'Location' text box has a valid value
            if (!string.IsNullOrWhiteSpace(TxtCoordinates.Text))
            {
                var coordinates = TxtCoordinates.Text.Split(',');

                if (coordinates.Length != 2)
                {
                    TxtCoordinates.Focus();
                    MessageBox.Show("Please enter a valid set of coordinates to search", "Error");
                    return false;
                }

                if (!(double.TryParse(coordinates[0].Trim(), out _) && double.TryParse(coordinates[1].Trim(), out _)))
                {
                    TxtCoordinates.Focus();
                    MessageBox.Show("Please enter a valid set of coordinates to search", "Error");
                    return false;
                }
            }
            else
            {
                TxtCoordinates.Focus();
                MessageBox.Show("Please enter a valid set of coordinates to search", "Error");
                return false;
            }

            // Check if the 'Search Radius' text box has a valid value
            if (string.IsNullOrWhiteSpace(TxtSearchRadius.Text) || !(int.TryParse(TxtSearchRadius.Text, out var searchRadiusInt) && searchRadiusInt > 0))
            {
                TxtSearchRadius.Focus();
                MessageBox.Show("Please enter an integer greater than 0", "Error");
                return false;
            }

            // Check if the 'Max Results' text box has a valid value
            if (!string.IsNullOrWhiteSpace(TxtMaxResults.Text) &&
                (int.TryParse(TxtMaxResults.Text, out var maxResultsInt) && maxResultsInt > 0)) return true;
            TxtMaxResults.Focus();
            MessageBox.Show("Please enter an integer greater than 0", "Error");
            return false;

        }


        /// <summary>
        /// Helper function to change the tip shown for different CloudLocationCategories
        /// </summary>
        private void CboLocationType_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var comboBoxContent = (CboLocationCategories.SelectedItem as ComboBoxItem)?.Content;
        }
    }
}
