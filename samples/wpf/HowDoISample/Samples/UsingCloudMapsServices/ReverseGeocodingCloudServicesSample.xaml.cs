using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingCloudMapsServices
{
    /// <summary>
    /// Learn how to use the ReverseGeocodingCloudClient to access the ReverseGeocoding APIs available from the ThinkGeo Cloud
    /// </summary>
    public partial class ReverseGeocodingCloudServicesSample : IDisposable
    {
        private ReverseGeocodingCloudClient _reverseGeocodingCloudClient;

        public ReverseGeocodingCloudServicesSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay, as well as several feature layers to display the reverse geocoding search area and locations
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

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
            MapView.Overlays.Add("Search Features Overlay", searchFeaturesOverlay);
            MapView.Overlays.Add("Best Match Popup Overlay", bestMatchPopupOverlay);

            // Set the map extent to Frisco, TX
            MapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Initialize the ReverseGeocodingCloudClient with our ThinkGeo Cloud credentials
            _reverseGeocodingCloudClient = new ReverseGeocodingCloudClient
            {
                ClientId = SampleKeys.ClientId2,
                ClientSecret = SampleKeys.ClientSecret2,
            };

            cboLocationCategories.SelectedIndex = 0;

            await MapView.RefreshAsync();
        }

        /// <summary>
        /// Perform the reverse geocode when the user clicks on the map
        /// </summary>
        private void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (e.MouseButton == MapMouseButton.Left)
            {
                // Set the coordinates in the UI
                txtCoordinates.Text = $"{e.WorldY:0.000000},{e.WorldX:0.000000}";

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
            // Perform some simple validation on the input text boxes
            if (!ValidateSearchParameters()) return;
            var options = new CloudReverseGeocodingOptions();

            // Set up the CloudReverseGeocodingOptions object based on the parameters set in the UI
            var coordinates = txtCoordinates.Text.Split(',');
            var lat = double.Parse(coordinates[0].Trim());
            var lon = double.Parse(coordinates[1].Trim());
            var searchRadius = int.Parse(txtSearchRadius.Text);
            const DistanceUnit searchRadiusDistanceUnit = DistanceUnit.Meter;
            const int pointProjectionInSrid = 3857;
            var searchPoint = new PointShape(lon, lat);
            options.MaxResults = int.Parse(txtMaxResults.Text);

            switch (((ComboBoxItem)cboLocationCategories.SelectedItem).Content.ToString())
            {
                case "All":
                    options.LocationCategories = CloudLocationCategories.All;
                    break;
                case "Common":
                    options.LocationCategories = CloudLocationCategories.Common;
                    break;
                case "None":
                    options.LocationCategories = CloudLocationCategories.None;
                    break;
                default:
                    options.LocationCategories = CloudLocationCategories.All;
                    break;
            }

            // Show a loading graphic to let users know the request is running
            loadingImage.Visibility = Visibility.Visible;

            // Run the reverse geocode
            var searchResult = await _reverseGeocodingCloudClient.SearchPointAsync(lon, lat, pointProjectionInSrid, searchRadius, searchRadiusDistanceUnit, options);

            // Hide the loading graphic
            loadingImage.Visibility = Visibility.Hidden;

            // Handle an exception returned from the service
            if (searchResult.Exception != null)
            {
                MessageBox.Show(searchResult.Exception.Message, "Error");
                return;
            }

            // Update the UI
            await DisplaySearchResultsAsync(searchPoint, searchRadius, searchResult);
        }

        /// <summary>
        /// Update the UI based on the search results from the reverse geocode
        /// </summary>
        private async Task DisplaySearchResultsAsync(PointShape searchPoint, int searchRadius, CloudReverseGeocodingResult searchResult)
        {
            // Get the 'Search Radius' layer from the MapView
            var searchRadiusFeatureLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("Search Radius");

            // Clear the existing features and add new features showing the area that was searched by the reverse geocode
            searchRadiusFeatureLayer.Clear();
            searchRadiusFeatureLayer.InternalFeatures.Add(new Feature(new EllipseShape(searchPoint, searchRadius)));
            searchRadiusFeatureLayer.InternalFeatures.Add(new Feature(searchPoint));

            // Get the 'Result Feature' layer and clear it
            var selectedResultItemFeatureLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("Result Feature Geometry");
            selectedResultItemFeatureLayer.Clear();

            // If a match was found for the geocode, update the UI
            if (searchResult?.BestMatchLocation != null)
            {
                // Get the 'Best Match' PopupOverlay from the MapView and clear it
                var bestMatchPopupOverlay = (PopupOverlay)MapView.Overlays["Best Match Popup Overlay"];
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
                        nearbyPlaces.Add(foundLocation);
                    }
                }

                // Set the data sources for the addresses, roads, and places list boxes
                lsbAddresses.ItemsSource = nearbyAddresses;
                lsbRoads.ItemsSource = nearbyRoads;
                lsbPlaces.ItemsSource = nearbyPlaces;

                txtSearchResultsBestMatch.Text = "Best Match: " + searchResult.BestMatchLocation.Address;
            }
            else
            {
                txtSearchResultsBestMatch.Text = "No address or place matches found for this location";
            }

            // Set the map extent to show the results of the search
            MapView.CurrentExtent = searchRadiusFeatureLayer.GetBoundingBox();
            var standardZoomLevelSet = new ZoomLevelSet();
            if (MapView.CurrentScale < standardZoomLevelSet.ZoomLevel18.Scale)
            {
                await MapView.ZoomToScaleAsync(standardZoomLevelSet.ZoomLevel18.Scale);
            }
            await MapView.RefreshAsync();
        }

        /// <summary>
        /// When a location is selected in the UI, draw the matching feature found and center the map on it
        /// </summary>
        private async void lsbSearchResults_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedResultList = (ListBox)sender;
            if (selectedResultList.SelectedItem != null)
            {
                // Get the selected location
                var locationFeature = ((CloudReverseGeocodingLocation)selectedResultList.SelectedItem).LocationFeature;

                // Get the 'Result Feature' layer from the MapView
                var selectedResultItemFeatureLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("Result Feature Geometry");

                // Clear the existing features and add the geometry of the selected location
                selectedResultItemFeatureLayer.Clear();
                selectedResultItemFeatureLayer.InternalFeatures.Add(new Feature(locationFeature.GetShape()));

                // Center the map on the chosen location
                MapView.CurrentExtent = locationFeature.GetBoundingBox();
                var standardZoomLevelSet = new ZoomLevelSet();
                if (MapView.CurrentScale < standardZoomLevelSet.ZoomLevel18.Scale)
                {
                    await MapView.ZoomToScaleAsync(standardZoomLevelSet.ZoomLevel18.Scale);
                }
                await MapView.RefreshAsync();
            }
        }

        /// <summary>
        /// Helper function to change the tip shown for different CloudLocationCategories
        /// </summary>
        private void cboLocationType_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var comboBoxContent = (cboLocationCategories.SelectedItem as ComboBoxItem)?.Content;

            if (comboBoxContent == null) return;
            switch (comboBoxContent.ToString())
            {
                case "All":
                    txtLocationCategoriesDescription.Text = "(Includes all available location types in the search)";
                    break;
                case "Common":
                    txtLocationCategoriesDescription.Text = "(Includes only commonly-used 'Place' types in the search)";
                    break;
                case "None":
                    txtLocationCategoriesDescription.Text = "(Only the best matching result will be returned)";
                    break;
            }
        }

        /// <summary>
        /// Helper function to perform simple validation on the input text boxes
        /// </summary>
        private bool ValidateSearchParameters()
        {
            // Check if the 'Location' text box has a valid value
            if (!string.IsNullOrWhiteSpace(txtCoordinates.Text))
            {
                var coordinates = txtCoordinates.Text.Split(',');

                if (coordinates.Count() != 2)
                {
                    txtCoordinates.Focus();
                    MessageBox.Show("Please enter a valid set of coordinates to search", "Error");
                    return false;
                }

                if (!(double.TryParse(coordinates[0].Trim(), out double lat) && double.TryParse(coordinates[1].Trim(), out double lon)))
                {
                    txtCoordinates.Focus();
                    MessageBox.Show("Please enter a valid set of coordinates to search", "Error");
                    return false;
                }
            }
            else
            {
                txtCoordinates.Focus();
                MessageBox.Show("Please enter a valid set of coordinates to search", "Error");
                return false;
            }

            // Check if the 'Search Radius' text box has a valid value
            if (string.IsNullOrWhiteSpace(txtSearchRadius.Text) || !(int.TryParse(txtSearchRadius.Text, out int searchRadiusInt) && searchRadiusInt > 0))
            {
                txtSearchRadius.Focus();
                MessageBox.Show("Please enter an integer greater than 0", "Error");
                return false;
            }

            // Check if the 'Max Results' text box has a valid value
            if (string.IsNullOrWhiteSpace(txtMaxResults.Text) || !(int.TryParse(txtMaxResults.Text, out int maxResultsInt) && maxResultsInt > 0))
            {
                txtMaxResults.Focus();
                MessageBox.Show("Please enter an integer greater than 0", "Error");
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}