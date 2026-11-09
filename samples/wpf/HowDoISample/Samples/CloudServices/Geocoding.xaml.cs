using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to use the GeocodingCloudClient to access the Geocoding APIs available from the ThinkGeo Cloud
    /// </summary>
    public partial class Geocoding
    {
        private bool _initialized;
        private GeocodingCloudClient _geocodingCloudClient;
        private readonly DynamicSource _selectedResult = new DynamicSource();

        public Geocoding()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps basemap
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;

            // The location picked out of the results is drawn with the basemap: a star for a
            // point, and a street or a boundary as the line or area it is.
            var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
            style.Images.Add("match", new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple));
            style.AddDynamic(_selectedResult);
            Map.Basemap = new GpuBasemap(style);

            // Set the map extent to Frisco, TX
            Map.CenterPoint = new PointShape(-10778720, 3915154);
            Map.CurrentScale = 202090;

            // Initialize the GeocodingCloudClient using our ThinkGeo Cloud credentials
            _geocodingCloudClient = new GeocodingCloudClient
            {
                ClientId = SampleShared.ClientId,
                ClientSecret = SampleShared.ClientSecret,
            };

            _ = Map.RefreshAsync();
        }

        /// <summary>
        /// Search for an address using the GeocodingCloudClient
        /// </summary>
        private async Task<CloudGeocodingResult> PerformGeocodingQuery()
        {
            // Show a loading graphic to let users know the request is running
            LoadingImage.Visibility = Visibility.Visible;

            var options = new CloudGeocodingOptions
            {
                MaxResults = int.Parse(TxtMaxResults.Text),
                Autocomplete = ChkAutocomplete.IsChecked == true,
                ResultProjectionInSrid = 3857,
            };
            if (ChkRestrictToExtent.IsChecked == true)
            {
                options.BBox = Map.CurrentExtent;
            }

            var countries = (TxtCountryCodes.Text ?? string.Empty)
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToArray();
            if (countries.Length > 0)
            {
                options.Countries = countries;
            }

            var languages = (TxtLanguage.Text ?? string.Empty)
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToArray();
            if (languages.Length > 0)
            {
                options.Language = languages;
            }

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
        private void UpdateSearchResultsOnUI(CloudGeocodingResult searchResult)
        {
            // Clear the locations list and the location drawn on the map
            _selectedResult.Clear();
            LsbLocations.ItemsSource = null;

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
                    UpdateSearchResultsOnUI(searchResult);
                }
            }
            catch
            {
                LoadingImage.Visibility = Visibility.Hidden;
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// When a location is selected in the UI, draw it on the map and center the map on it
        /// </summary>
        private async void lsbLocations_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the selected location
                var chosenLocation = LsbLocations.SelectedItem as CloudGeocodingLocation;
                if (chosenLocation == null) return;

                // A found location can be a point, a street or a boundary, so it is drawn
                // the way its kind is drawn.
                _selectedResult.Clear();
                var locationFeature = new Feature(chosenLocation.Shape);
                switch (chosenLocation.Shape)
                {
                    case AreaBaseShape _:
                        _selectedResult.SetArea("found", locationFeature, new GeoColor(80, GeoColors.MediumPurple), GeoColors.MediumPurple, 2f);
                        break;
                    case LineBaseShape _:
                        _selectedResult.SetLine("found", locationFeature, GeoColors.MediumPurple, 6f);
                        break;
                    case PointBaseShape _:
                        _selectedResult.SetPoint("found", locationFeature, sizeInPixel: 24f, iconName: "match");
                        break;
                }

                // Center the map on the chosen location
                var chosenLocationBBox = chosenLocation.BoundingBox;
                Map.CenterPoint = chosenLocationBBox.GetCenterPoint();
                Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, chosenLocationBBox, Map.MapWidth, Map.MapHeight);
                var standardZoomLevelSet = new ZoomLevelSet();
                await Map.ZoomToAsync(standardZoomLevelSet.ZoomLevel18.Scale);
                await Map.RefreshAsync();
            }
            catch
            {
                LoadingImage.Visibility = Visibility.Hidden;
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
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
    }
}
