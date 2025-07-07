using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to use the GeocodingCloudClient to access the Geocoding APIs available from the ThinkGeo Cloud
    /// </summary>
    public partial class Geocoding : IDisposable
    {
        private GeocodingCloudClient _geocodingCloudClient;

        public Geocoding()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
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

            // Create a marker overlay to display the geocoded locations that will be generated, and add it to the map
            MarkerOverlay geocodedLocationsOverlay = new SimpleMarkerOverlay();
            MapView.Overlays.Add("Geocoded Locations Overlay", geocodedLocationsOverlay);

            // Set the map extent to Frisco, TX
            MapView.CenterPoint = new PointShape(-10778720, 3915154);
            MapView.CurrentScale = 202090;

            // Initialize the GeocodingCloudClient using our ThinkGeo Cloud credentials
            _geocodingCloudClient = new GeocodingCloudClient
            {
                ClientId = SampleKeys.ClientId2,
                ClientSecret = SampleKeys.ClientSecret2,
            };

            CboSearchType.SelectedIndex = 0;
            CboLocationType.SelectedIndex = 0;

            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Search for an address using the GeocodingCloudClient
        /// </summary>
        private async Task<CloudGeocodingResult> PerformGeocodingQuery()
        {
            // Overture Queries require a BBox - check to make sure the extent is not too large.
            bool includeOvertureBool = (bool)((ComboBoxItem)CboIncludeOverturePlaces.SelectedValue).Tag;
            if (includeOvertureBool && MapView.CurrentExtent.GetArea(GeographyUnit.Meter, AreaUnit.SquareMiles) > 100000)
            {
                MessageBox.Show("Please zoom in before including Overture Place Data in Request.");
                return await Task.FromResult<CloudGeocodingResult>(new CloudGeocodingResult(null, null));
            }

            // Show a loading graphic to let users know the request is running
            LoadingImage.Visibility = Visibility.Visible;

            var options = new CloudGeocodingOptions
            {
                // Set up the CloudGeocodingOptions object based on the parameters set in the UI
                MaxResults = int.Parse(TxtMaxResults.Text),
                SearchMode = ((ComboBoxItem)CboSearchType.SelectedItem).Content.ToString() == "Fuzzy" ? CloudGeocodingSearchMode.FuzzyMatch : CloudGeocodingSearchMode.ExactMatch,
                LocationType = (CloudGeocodingLocationType)Enum.Parse(typeof(CloudGeocodingLocationType), ((ComboBoxItem)CboLocationType.SelectedItem).Content.ToString() ?? string.Empty),
                ResultProjectionInSrid = 3857,
                BBox = MapView.CurrentExtent,
                IncludeOverturePlaces = includeOvertureBool
            };

            // Run the geocode
            var searchString = TxtSearchString.Text.Trim();
            var searchResult = await _geocodingCloudClient.SearchAsync(searchString, options);

            // Hide the loading graphic
            LoadingImage.Visibility = Visibility.Hidden;

            return searchResult;
        }

        /// <summary>
        /// Update the UI based on the results of a Cloud Geocoding Query
        /// </summary>
        private async Task UpdateSearchResultsOnUIAsync(CloudGeocodingResult searchResult)
        {
            // Clear the locations list and existing location markers on the map
            var geocodedLocationOverlay = (SimpleMarkerOverlay)MapView.Overlays["Geocoded Locations Overlay"];
            geocodedLocationOverlay.Markers.Clear();
            LsbLocations.ItemsSource = null;
            await geocodedLocationOverlay.RefreshAsync();

            if (searchResult.Locations != null)
            {
                // Update the UI with the number of results found and the list of locations found
                TxtSearchResultsDescription.Text = $"Found {searchResult.Locations.Count} matching locations.";
                LsbLocations.ItemsSource = searchResult.Locations;
                if (searchResult.Locations.Count > 0)
                {
                    LsbLocations.Visibility = Visibility.Visible;
                    LsbLocations.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Search for an address using the GeocodingCloudClient and update the UI
        /// </summary>
        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Perform some simple validation on the input text boxes
                if (ValidateSearchParameters())
                {
                    // Run the Cloud Geocoding query
                    var searchResult = await PerformGeocodingQuery();

                    // Handle an error returned from the geocoding service
                    if (searchResult.Exception != null)
                    {
                        MessageBox.Show(searchResult.Exception.Message, "Error");
                        return;
                    }

                    // Update the UI based on the results
                    await UpdateSearchResultsOnUIAsync(searchResult);
                }
            }
            catch 
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// When a location is selected in the UI, add a marker at that location and center the map on it
        /// </summary>
        private async void lsbLocations_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            { 
                // Get the selected location
                var chosenLocation = LsbLocations.SelectedItem as CloudGeocodingLocation;
                if (chosenLocation == null) return;
                // Get the MarkerOverlay from the MapView
                var geocodedLocationOverlay = (SimpleMarkerOverlay)MapView.Overlays["Geocoded Locations Overlay"];

                // Clear the existing markers and add a new marker at the chosen location
                geocodedLocationOverlay.Markers.Clear();
                geocodedLocationOverlay.Markers.Add(CreateNewMarker(chosenLocation.LocationPoint));

                // Center the map on the chosen location
                var chosenLocationBBox = chosenLocation.BoundingBox;
                MapView.CenterPoint = chosenLocationBBox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, chosenLocationBBox, MapView.MapWidth, MapView.MapHeight);
                var standardZoomLevelSet = new ZoomLevelSet();
                await MapView.ZoomToAsync(standardZoomLevelSet.ZoomLevel18.Scale);
                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// Helper function to change the tip shown for different Search Types
        /// </summary>
        private void cboSearchType_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var comboBoxContent = (CboSearchType.SelectedItem as ComboBoxItem)?.Content;

            if (comboBoxContent == null) return;
            switch (comboBoxContent.ToString())
            {
                case "Fuzzy":
                    TxtSearchTypeDescription.Text = "(Returns both exact and approximate matches for the search address)";
                    break;
                case "Exact":
                    TxtSearchTypeDescription.Text = "(Only returns exact matches for the search address)";
                    break;
            }
        }

        /// <summary>
        /// Helper function to change the tip shown for different Location Types
        /// </summary>
        private void cboLocationType_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var comboBoxContent = (CboLocationType.SelectedItem as ComboBoxItem)?.Content;
        }

        /// <summary>
        /// Helper function to perform simple validation on the input text boxes
        /// </summary>
        private bool ValidateSearchParameters()
        {
            // Check if the address text box is empty
            if (string.IsNullOrWhiteSpace(TxtSearchString.Text))
            {
                TxtSearchString.Focus();
                MessageBox.Show("Please enter an address to search", "Error");
                return false;
            }

            // Check if the 'Max Results' text box has a valid value
            if (!string.IsNullOrWhiteSpace(TxtMaxResults.Text) &&
                (int.TryParse(TxtMaxResults.Text, out var result) && result > 0 && result < 101)) return true;
            TxtMaxResults.Focus();
            MessageBox.Show("Please enter a number between 1 - 100", "Error");
            return false;

        }

        /// <summary>
        /// Create a new map marker using preloaded image assets
        /// </summary>
        private static Marker CreateNewMarker(PointShape point)
        {
            return new Marker(point)
            {
                ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute)),
                Width = 20,
                Height = 34,
                YOffset = -17
            };
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