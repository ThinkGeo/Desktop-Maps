using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingCloudMapsServices
{
    /// <summary>
    /// Learn how to use the GeocodingCloudClient to access the Geocoding APIs available from the ThinkGeo Cloud
    /// </summary>
    public partial class GeocodingCloudServicesSample : UserControl, IDisposable
    {
        private GeocodingCloudClient geocodingCloudClient;

        public GeocodingCloudServicesSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a marker overlay to display the geocoded locations that will be generated, and add it to the map
            MarkerOverlay geocodedLocationsOverlay = new SimpleMarkerOverlay();
            mapView.Overlays.Add("Geocoded Locations Overlay", geocodedLocationsOverlay);

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Initialize the GeocodingCloudClient using our ThinkGeo Cloud credentials
            geocodingCloudClient = new GeocodingCloudClient("FSDgWMuqGhZCmZnbnxh-Yl1HOaDQcQ6mMaZZ1VkQNYw~", "IoOZkBJie0K9pz10jTRmrUclX6UYssZBeed401oAfbxb9ufF1WVUvg~~");

            cboSearchType.SelectedIndex = 0;
            cboLocationType.SelectedIndex = 0;
        }

        /// <summary>
        /// Search for an address using the GeocodingCloudClient
        /// </summary>
        private async Task<CloudGeocodingResult> PerformGeocodingQuery()
        {
            // Show a loading graphic to let users know the request is running
            loadingImage.Visibility = Visibility.Visible;

            CloudGeocodingOptions options = new CloudGeocodingOptions();

            // Set up the CloudGeocodingOptions object based on the parameters set in the UI
            options.MaxResults = int.Parse(txtMaxResults.Text);
            options.SearchMode = ((ComboBoxItem)cboSearchType.SelectedItem).Content.ToString() == "Fuzzy" ? CloudGeocodingSearchMode.FuzzyMatch : CloudGeocodingSearchMode.ExactMatch;
            options.LocationType = (CloudGeocodingLocationType)Enum.Parse(typeof(CloudGeocodingLocationType), ((ComboBoxItem)cboLocationType.SelectedItem).Content.ToString());
            options.ResultProjectionInSrid = 3857;

            // Run the geocode
            string searchString = txtSearchString.Text.Trim();
            CloudGeocodingResult searchResult = await geocodingCloudClient.SearchAsync(searchString, options);

            // Hide the loading graphic
            loadingImage.Visibility = Visibility.Hidden;

            return searchResult;
        }

        /// <summary>
        /// Update the UI based on the results of a Cloud Geocoding Query
        /// </summary>
        private void UpdateSearchResultsOnUI(CloudGeocodingResult searchResult)
        {
            // Clear the locations list and existing location markers on the map
            SimpleMarkerOverlay geocodedLocationOverlay = (SimpleMarkerOverlay)mapView.Overlays["Geocoded Locations Overlay"];
            geocodedLocationOverlay.Markers.Clear();
            lsbLocations.ItemsSource = null;
            geocodedLocationOverlay.Refresh();

            // Update the UI with the number of results found and the list of locations found
            txtSearchResultsDescription.Text = $"Found {searchResult.Locations.Count} matching locations.";
            lsbLocations.ItemsSource = searchResult.Locations;
            if (searchResult.Locations.Count > 0)
            {
                lsbLocations.Visibility = Visibility.Visible;
                lsbLocations.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Search for an address using the GeocodingCloudClient and update the UI
        /// </summary>
        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            // Perform some simple validation on the input text boxes
            if (ValidateSearchParameters())
            {
                // Run the Cloud Geocoding query
                CloudGeocodingResult searchResult = await PerformGeocodingQuery();

                // Handle an error returned from the geocoding service
                if (searchResult.Exception != null)
                {
                    MessageBox.Show(searchResult.Exception.Message, "Error");
                    return;
                }

                // Update the UI based on the results
                UpdateSearchResultsOnUI(searchResult);
            }
        }

        /// <summary>
        /// When a location is selected in the UI, add a marker at that location and center the map on it
        /// </summary>
        private void lsbLocations_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Get the selected location
            var chosenLocation = lsbLocations.SelectedItem as CloudGeocodingLocation;
            if (chosenLocation != null)
            {
                // Get the MarkerOverlay from the MapView
                SimpleMarkerOverlay geocodedLocationOverlay = (SimpleMarkerOverlay)mapView.Overlays["Geocoded Locations Overlay"];

                // Clear the existing markers and add a new marker at the chosen location
                geocodedLocationOverlay.Markers.Clear();
                geocodedLocationOverlay.Markers.Add(CreateNewMarker(chosenLocation.LocationPoint));

                // Center the map on the chosen location
                mapView.CurrentExtent = chosenLocation.BoundingBox;
                ZoomLevelSet standardZoomLevelSet = new ZoomLevelSet();
                mapView.ZoomToScale(standardZoomLevelSet.ZoomLevel18.Scale);
                mapView.Refresh();
            }
        }

        /// <summary>
        /// Helper function to change the tip shown for different Search Types
        /// </summary>
        private void cboSearchType_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var comboBoxContent = (cboSearchType.SelectedItem as ComboBoxItem).Content;

            if (comboBoxContent != null)
            {
                switch (comboBoxContent.ToString())
                {
                    case "Fuzzy":
                        txtSearchTypeDescription.Text = "(Returns both exact and approximate matches for the search address)";
                        break;
                    case "Exact":
                        txtSearchTypeDescription.Text = "(Only returns exact matches for the search address)";
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Helper function to change the tip shown for different Location Types
        /// </summary>
        private void cboLocationType_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var comboBoxContent = (cboLocationType.SelectedItem as ComboBoxItem).Content;

            if (comboBoxContent != null)
            {
                switch (comboBoxContent.ToString())
                {
                    case "Default":
                        txtLocationTypeDescription.Text = "(Searches for any matches to the search string)";
                        break;
                    case "Address":
                        txtLocationTypeDescription.Text = "(Searches for addresses matching the search string)";
                        break;
                    case "Street":
                        txtLocationTypeDescription.Text = "(Searches for streets matching the search string)";
                        break;
                    case "City":
                        txtLocationTypeDescription.Text = "(Searches for cities matching the search string)";
                        break;
                    case "County":
                        txtLocationTypeDescription.Text = "(Searches for counties matching the search string)";
                        break;
                    case "ZipCode":
                        txtLocationTypeDescription.Text = "(Searches for zip codes matching the search string)";
                        break;
                    case "State":
                        txtLocationTypeDescription.Text = "(Searches for states matching the search string)";
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Helper function to perform simple validation on the input text boxes
        /// </summary>
        private bool ValidateSearchParameters()
        {
            // Check if the address text box is empty
            if (string.IsNullOrWhiteSpace(txtSearchString.Text))
            {
                txtSearchString.Focus();
                MessageBox.Show("Please enter an address to search", "Error");
                return false;
            }

            // Check if the 'Max Results' text box has a valid value
            if (string.IsNullOrWhiteSpace(txtMaxResults.Text) || !(int.TryParse(txtMaxResults.Text, out int result) && result > 0 && result < 101))
            {
                txtMaxResults.Focus();
                MessageBox.Show("Please enter a number between 1 - 100", "Error");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Create a new map marker using preloaded image assets
        /// </summary>
        private Marker CreateNewMarker(PointShape point)
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
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
