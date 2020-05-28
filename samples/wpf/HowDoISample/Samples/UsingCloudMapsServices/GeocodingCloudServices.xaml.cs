using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingCloudMapsServices
{
    /// <summary>
    /// Interaction logic for GeocodingCloudServices.xaml
    /// </summary>
    public partial class GeocodingCloudServices : UserControl
    {
        private GeocodingCloudClient geocodingCloudClient;

        public GeocodingCloudServices()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            MarkerOverlay geocodedLocationsOverlay = new SimpleMarkerOverlay();
            mapView.Overlays.Add("Geocoded Locations Overlay", geocodedLocationsOverlay);

            mapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);
            mapView.Refresh();

            geocodingCloudClient = new GeocodingCloudClient("FSDgWMuqGhZCmZnbnxh-Yl1HOaDQcQ6mMaZZ1VkQNYw~", "IoOZkBJie0K9pz10jTRmrUclX6UYssZBeed401oAfbxb9ufF1WVUvg~~");
        }

        private void cboSearchType_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var comboBoxContent = (cboSearchType.SelectedItem as ComboBoxItem).Content;

            if (comboBoxContent != null)
            {
                switch (comboBoxContent.ToString())
                {
                    case "Fuzzy":
                        txtSearchTypeDescription.Text = "Returns both exact and approximate matches for the search address";
                        break;
                    case "Exact":
                        txtSearchTypeDescription.Text = "Only returns exact matches for the search address";
                        break;
                    default:
                        break;
                }
            }
        }

        private void cboLocationType_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var comboBoxContent = (cboLocationType.SelectedItem as ComboBoxItem).Content;

            if (comboBoxContent != null)
            {
                switch (comboBoxContent.ToString())
                {
                    case "Default":
                        txtLocationTypeDescription.Text = "Searches for any matches to the search string";
                        break;
                    case "Address":
                        txtLocationTypeDescription.Text = "Searches for addresses matching the search string";
                        break;
                    case "Street":
                        txtLocationTypeDescription.Text = "Searches for streets matching the search string";
                        break;
                    case "City":
                        txtLocationTypeDescription.Text = "Searches for cities matching the search string";
                        break;
                    case "County":
                        txtLocationTypeDescription.Text = "Searches for counties matching the search string";
                        break;
                    case "ZipCode":
                        txtLocationTypeDescription.Text = "Searches for zip codes matching the search string";
                        break;
                    case "State":
                        txtLocationTypeDescription.Text = "Searches for states matching the search string";
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSearchParameters())
            {
                CloudGeocodingOptions options = new CloudGeocodingOptions();

                options.MaxResults = int.Parse(txtMaxResults.Text);
                options.SearchMode = ((ComboBoxItem)cboSearchType.SelectedItem).Content.ToString() == "Fuzzy" ? CloudGeocodingSearchMode.FuzzyMatch : CloudGeocodingSearchMode.ExactMatch;
                options.LocationType = (CloudGeocodingLocationType)Enum.Parse(typeof(CloudGeocodingLocationType), ((ComboBoxItem)cboLocationType.SelectedItem).Content.ToString());
                options.ResultProjectionInSrid = 3857;

                string searchString = txtSearchString.Text.Trim();
                CloudGeocodingResult searchResult = geocodingCloudClient.Search(searchString, options);

                if (searchResult.Exception != null)
                {
                    MessageBox.Show(searchResult.Exception.Message, "Error");
                    return;
                }

                SimpleMarkerOverlay geocodedLocationOverlay = (SimpleMarkerOverlay)mapView.Overlays["Geocoded Locations Overlay"];

                geocodedLocationOverlay.Markers.Clear();
                lsbLocations.ItemsSource = null;
                geocodedLocationOverlay.Refresh();

                txtSearchResultsDescription.Text = $"Found {searchResult.Locations.Count} matching locations.";
                lsbLocations.ItemsSource = searchResult.Locations;
                if (searchResult.Locations.Count > 0)
                {
                    lsbLocations.Visibility = Visibility.Visible;
                    lsbLocations.SelectedIndex = 0;
                }
            }
        }

        private void lsbLocations_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var chosenLocation = lsbLocations.SelectedItem as CloudGeocodingLocation;
            if (chosenLocation != null)
            {
                SimpleMarkerOverlay geocodedLocationOverlay = (SimpleMarkerOverlay)mapView.Overlays["Geocoded Locations Overlay"];

                geocodedLocationOverlay.Markers.Clear();
                geocodedLocationOverlay.Markers.Add(new Marker(chosenLocation.LocationPoint));
                mapView.CurrentExtent = chosenLocation.BoundingBox;
                mapView.Refresh();
            }
        }

        private bool ValidateSearchParameters()
        {
            if (string.IsNullOrWhiteSpace(txtSearchString.Text))
            {
                txtSearchString.Focus();
                MessageBox.Show("Please enter an address to search", "Error");
                return false;
            }

            if (!(int.TryParse(txtMaxResults.Text, out int result) && result > 0 && result < 101))
            {
                txtMaxResults.Focus();
                MessageBox.Show("Please enter a number between 1 - 100", "Error");
                return false;
            }

            return true;
        }
    }
}
