/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Cloud;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeoCloudGeocoding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const string GisServerUri = "https://gisserver1.thinkgeo.com";

        private string clientId;
        private string clientSecret;
        private GeocodingClient geocodingClient;
        private SimpleMarkerOverlay markerOverlay;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbLocationType.ItemsSource = Enum.GetNames(typeof(GeocodingLocationType)).Where(s => s != "All");
            cmbLocationType.SelectedIndex = 0;

            if (!TryReadClientIdSecretFromConfig())
            {
                ShowClientIdSecretInputer();
            }
            UpdateIdSecretToClient();

            WpfMap.MapUnit = GeographyUnit.Meter;
            WpfMap.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            WpfMap.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            baseOverlay.WrappingMode = WrappingMode.WrapDateline;
            WpfMap.Overlays.Add(baseOverlay);

            // Add marker overlay for showing the chosen location.
            markerOverlay = new SimpleMarkerOverlay();
            WpfMap.Overlays.Add("MarkerOverlay", markerOverlay);
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!TryGetSearchInfo(out var searchText, out var options, out var errMsg))
            {
                MessageBox.Show(errMsg, "Error");
                return;
            }

            await SearchLocation(searchText, options);
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

        private void LsbLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var chosenLocation = lsbLocations.SelectedItem as GeocodingLocation;
            if (chosenLocation != null)
            {
                markerOverlay.Markers.Clear();
                markerOverlay.Markers.Add(new Marker(chosenLocation.LocationPoint));
                WpfMap.CurrentExtent = chosenLocation.BoundingBox;
                WpfMap.Refresh();
            }
        }

        private void UpdateIdSecretToClient()
        {
            geocodingClient?.Dispose();
            geocodingClient = new GeocodingClient(clientId, clientSecret);
            geocodingClient.BaseUris.Add(new Uri(GisServerUri));
        }

        private bool TryReadClientIdSecretFromConfig()
        {
            var id = ConfigurationManager.AppSettings["ClientId"];
            var secret = ConfigurationManager.AppSettings["ClientSecret"];
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(secret))
            {
                return false;
            }
            clientId = id.Trim();
            clientSecret = secret.Trim();
            return true;
        }

        private void ShowClientIdSecretInputer()
        {
            var clientIdSecretInputer = new ClientIdSecretInputer
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            clientIdSecretInputer.BaseUris.Add(new Uri(GisServerUri));
            if (clientIdSecretInputer.ShowDialog() != true)
            {
                Environment.Exit(0);
            }
            clientId = clientIdSecretInputer.ClientId;
            clientSecret = clientIdSecretInputer.ClientSecret;
        }

        private bool TryGetSearchInfo(out string searchText, out GeocodingOptions options, out string errorMsg)
        {
            searchText = null;
            options = null;
            errorMsg = null;

            if (string.IsNullOrWhiteSpace(txtSearchText.Text))
            {
                txtSearchText.Focus();
                errorMsg = "Search text can't be empty.";
                return false;
            }
            searchText = txtSearchText.Text.Trim();

            options = new GeocodingOptions();
            if (!int.TryParse(txtMaxResults.Text, out var maxResults))
            {
                txtMaxResults.Focus();
                errorMsg = "Max Results text is not a number.";
                return false;
            }
            if (maxResults < 1 || maxResults > 100)
            {
                txtMaxResults.Focus();
                errorMsg = "Max Results should be range(1,100).";
                return false;
            }
            options.MaxResults = maxResults;
            options.SearchMode = ((ComboBoxItem)cmbSearchMode.SelectedItem).Content.ToString() == "Fuzzy" ? GeocodingSearchMode.FuzzyMatch : GeocodingSearchMode.ExactMatch;
            options.LocationType = (GeocodingLocationType)Enum.Parse(typeof(GeocodingLocationType), cmbLocationType.SelectedItem.ToString());
            options.ResultProjectionInSrid = 3857;
            return true;
        }


    }
}
