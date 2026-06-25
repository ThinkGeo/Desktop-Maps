using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to use the ReverseGeocodingCloudClient to access the ReverseGeocoding APIs available from the ThinkGeo Cloud
    /// </summary>
    public partial class ReverseGeocoding
    {

        private bool _initialized;
        private ReverseGeocodingCloudClient _reverseGeocodingCloudClient;

        public ReverseGeocoding()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay, as well as several feature layers to display the reverse geocoding search area and locations
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            Map.MapUnit = GeographyUnit.Meter;

            // Create a new feature layer to display the search radius of the reverse geocode and create a style for it
            var searchRadiusFeatureLayer = new InMemoryFeatureLayer();
            searchRadiusFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(new GeoColor(100, GeoColors.Blue)), new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));
            searchRadiusFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Cross, 20, GeoBrushes.Red);
            searchRadiusFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create a new feature layer to display selected locations returned from the reverse geocode and create styles for it
            var selectedResultItemFeatureLayer = new InMemoryFeatureLayer();
            // Add a point, line, and polygon style to the layer. These styles control how the shapes will be drawn
            selectedResultItemFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);
            selectedResultItemFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.MediumPurple, 6, false);
            selectedResultItemFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(80, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);
            selectedResultItemFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create an overlay and add the feature layers to it
            var searchFeaturesOverlay = new LayerOverlay();
            searchFeaturesOverlay.Layers.Add("Search Radius", searchRadiusFeatureLayer);
            searchFeaturesOverlay.Layers.Add("Result Feature Geometry", selectedResultItemFeatureLayer);

            // Create a popup overlay to display the best match
            var bestMatchPopupOverlay = new PopupOverlay();

            // Add the overlays to the map
            Map.Overlays.Add("Search Features Overlay", searchFeaturesOverlay);
            Map.Overlays.Add("Best Match Popup Overlay", bestMatchPopupOverlay);

            // Set the map extent to Frisco, TX
            Map.CenterPoint = new PointShape(-10778720, 3915154);
            Map.CurrentScale = 202090;

            // Initialize the ReverseGeocodingCloudClient with our ThinkGeo Cloud credentials
            _reverseGeocodingCloudClient = new ReverseGeocodingCloudClient
            {
                ClientId = SampleKeys.ClientId2,
                ClientSecret = SampleKeys.ClientSecret2,
            };

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

                if (CbDataOsm.IsChecked == true) options.DataSources.Add("osm");
                if (CbDataOa.IsChecked == true) options.DataSources.Add("oa");
                if (CbDataWof.IsChecked == true) options.DataSources.Add("wof");
                if (CbDataGn.IsChecked == true) options.DataSources.Add("gn");
                if (CbDataOt.IsChecked == true) options.DataSources.Add("ot");

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
            // Get the 'Search Radius' layer from the Map
            var searchRadiusFeatureLayer = (InMemoryFeatureLayer)Map.FindFeatureLayer("Search Radius");

            // Clear the existing features and add new features showing the area that was searched by the reverse geocode
            searchRadiusFeatureLayer.Clear();
            searchRadiusFeatureLayer.InternalFeatures.Add(new Feature(new EllipseShape(searchPoint, searchRadius)));
            searchRadiusFeatureLayer.InternalFeatures.Add(new Feature(searchPoint));

            // Get the 'Result Feature' layer and clear it
            var selectedResultItemFeatureLayer = (InMemoryFeatureLayer)Map.FindFeatureLayer("Result Feature Geometry");
            selectedResultItemFeatureLayer.Clear();

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
                    var category = (foundLocation.LocationCategory ?? string.Empty).ToLowerInvariant();

                    // Addresses: legacy AddressPoint, Pelias address layer
                    if (category.Contains("address"))
                    {
                        nearbyAddresses.Add(foundLocation);
                    }
                    // Roads: legacy Aeroway/Road/Rail/Waterway, Pelias street layer
                    else if (category == "aeroway"
                        || category == "road"
                        || category == "rail"
                        || category == "waterway"
                        || category == "street")
                    {
                        nearbyRoads.Add(foundLocation);
                    }
                    // Everything else is a Place (skip intersections)
                    else if (category != "intersection")
                    {
                        // Overture and Pelias venue/admin results often lack a separate Address; fall back to the name.
                        if (string.IsNullOrWhiteSpace(foundLocation.Address) && !string.IsNullOrWhiteSpace(foundLocation.LocationName))
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

            // Set the map extent to show the results of the search
            var searchRadiusFeatureLayerBBox = searchRadiusFeatureLayer.GetBoundingBox();
            Map.CenterPoint = searchRadiusFeatureLayerBBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, searchRadiusFeatureLayerBBox, Map.MapWidth, Map.MapHeight);
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

                    // Get the 'Result Feature' layer from the Map
                    var selectedResultItemFeatureLayer = (InMemoryFeatureLayer)Map.FindFeatureLayer("Result Feature Geometry");

                    // Clear the existing features and add the geometry of the selected location
                    selectedResultItemFeatureLayer.Clear();
                    selectedResultItemFeatureLayer.InternalFeatures.Add(new Feature(locationFeature.GetShape()));

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


    }
}
