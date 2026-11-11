using System;
using System.Globalization;
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
    /// Find addresses and places by name, or click the map to find the addresses and places around a point.
    /// </summary>
    public partial class Geocoding
    {
        private bool _initialized;
        private bool _reverse;
        private GeocodingCloudClient _geocodingCloudClient;
        private ReverseGeocodingCloudClient _reverseGeocodingCloudClient;
        private readonly DynamicSource _searchedArea = new DynamicSource();
        private readonly DynamicSource _selectedResult = new DynamicSource();

        // The map is in Spherical Mercator (3857); people read and type latitude and longitude (4326).
        private readonly ProjectionConverter _latLon = new ProjectionConverter(3857, 4326);

        public Geocoding()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;

            var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
            style.Images.Add("center", new PointStyle(PointSymbolType.Cross, 20, GeoBrushes.Red));
            style.Images.Add("match", new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple));
            style.AddDynamic(_searchedArea);
            style.AddDynamic(_selectedResult);
            Map.Basemap = new GpuBasemap(style);

            Map.CenterPoint = new PointShape(-10778720, 3915154);
            Map.CurrentScale = 202090;

            _geocodingCloudClient = new GeocodingCloudClient
            {
                ClientId = SampleShared.ClientId,
                ClientSecret = SampleShared.ClientSecret,
            };
            _reverseGeocodingCloudClient = new ReverseGeocodingCloudClient
            {
                ClientId = SampleShared.ClientId,
                ClientSecret = SampleShared.ClientSecret,
            };
            _latLon.Open();

            _ = Map.RefreshAsync();
        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _reverse = (string)((RadioButton)sender).Tag == "reverse";
            SearchOptions.Visibility = _reverse ? Visibility.Collapsed : Visibility.Visible;
            ReverseOptions.Visibility = _reverse ? Visibility.Visible : Visibility.Collapsed;
            Status.Text = _reverse ? "Click the map, or type a location and search." : string.Empty;

            Results.ItemsSource = null;
            _searchedArea.Clear();
            _selectedResult.Clear();
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await SearchAsync();
            }
            catch (Exception ex)
            {
                LoadingImage.Visibility = Visibility.Hidden;
                Status.Text = ex.Message;
            }
        }

        private async void SearchHere_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var coordinates = TxtCoordinates.Text.Split(',');
                if (coordinates.Length != 2
                    || !double.TryParse(coordinates[0], NumberStyles.Float, CultureInfo.InvariantCulture, out var latitude)
                    || !double.TryParse(coordinates[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var longitude))
                {
                    Status.Text = "Type the location as latitude, longitude - for example 33.150076, -96.834530.";
                    return;
                }

                var point = _latLon.ConvertToInternalProjection(longitude, latitude);
                await ReverseGeocodeAsync(new PointShape(point.X, point.Y));
            }
            catch (Exception ex)
            {
                LoadingImage.Visibility = Visibility.Hidden;
                Status.Text = ex.Message;
            }
        }

        private async void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (!_reverse || e.MouseButton != MapMouseButton.Left) return;

            try
            {
                var latLon = _latLon.ConvertToExternalProjection(e.WorldLocation.X, e.WorldLocation.Y);
                TxtCoordinates.Text = FormattableString.Invariant($"{latLon.Y:0.000000}, {latLon.X:0.000000}");
                await ReverseGeocodeAsync(e.WorldLocation);
            }
            catch (Exception ex)
            {
                LoadingImage.Visibility = Visibility.Hidden;
                Status.Text = ex.Message;
            }
        }

        private async Task SearchAsync()
        {
            if (string.IsNullOrWhiteSpace(TxtSearchString.Text))
            {
                Status.Text = "Type an address or a place to search for.";
                return;
            }
            if (!int.TryParse(TxtMaxResults.Text, out var maxResults) || maxResults < 1 || maxResults > 100)
            {
                Status.Text = "Maximum results is a number from 1 to 100.";
                return;
            }

            var options = new CloudGeocodingOptions
            {
                MaxResults = maxResults,
                Autocomplete = ChkAutocomplete.IsChecked == true,
                ResultProjectionInSrid = 3857,
            };
            if (ChkRestrictToExtent.IsChecked == true)
            {
                options.BBox = Map.CurrentExtent;
            }

            var countries = TxtCountryCodes.Text.Split(',').Select(s => s.Trim()).Where(s => s.Length > 0).ToArray();
            if (countries.Length > 0)
            {
                options.Countries = countries;
            }
            var languages = TxtLanguage.Text.Split(',').Select(s => s.Trim()).Where(s => s.Length > 0).ToArray();
            if (languages.Length > 0)
            {
                options.Language = languages;
            }

            LoadingImage.Visibility = Visibility.Visible;
            var result = await _geocodingCloudClient.SearchAsync(TxtSearchString.Text.Trim(), options);
            LoadingImage.Visibility = Visibility.Hidden;

            _selectedResult.Clear();
            if (result.Exception != null)
            {
                Status.Text = result.Exception.Message;
                Results.ItemsSource = null;
                return;
            }

            Status.Text = $"Found {result.Locations.Count} matching locations.";
            Results.ItemsSource = result.Locations;
            if (result.Locations.Count > 0)
            {
                Results.SelectedIndex = 0;
            }
        }

        private async Task ReverseGeocodeAsync(PointShape point)
        {
            if (!int.TryParse(TxtSearchRadius.Text, out var searchRadius) || searchRadius <= 0)
            {
                Status.Text = "The search radius is a whole number of meters, greater than 0.";
                return;
            }
            if (!int.TryParse(TxtReverseMaxResults.Text, out var maxResults) || maxResults <= 0)
            {
                Status.Text = "Maximum results is a whole number greater than 0.";
                return;
            }

            var options = new CloudReverseGeocodingOptions { MaxResults = maxResults };
            if (CbDataOsm.IsChecked == true) options.DataSources.Add("osm");
            if (CbDataOa.IsChecked == true) options.DataSources.Add("oa");
            if (CbDataWof.IsChecked == true) options.DataSources.Add("wof");
            if (CbDataGn.IsChecked == true) options.DataSources.Add("gn");
            if (CbDataOt.IsChecked == true) options.DataSources.Add("ot");

            var searched = new EllipseShape(point, searchRadius);
            _searchedArea.Clear();
            _searchedArea.SetArea("searched", new Feature(searched), new GeoColor(10, GeoColors.Blue), new GeoColor(100, GeoColors.Blue), 2f);
            _searchedArea.SetPoint("center", new Feature(point), sizeInPixel: 20f, iconName: "center");
            _selectedResult.Clear();

            LoadingImage.Visibility = Visibility.Visible;
            var result = await _reverseGeocodingCloudClient.SearchPointAsync(point.X, point.Y, 3857, searchRadius, DistanceUnit.Meter, options);
            LoadingImage.Visibility = Visibility.Hidden;

            if (result.Exception != null)
            {
                Status.Text = result.Exception.Message;
                Results.ItemsSource = null;
                return;
            }

            // Addresses first, then the other places; intersections are left out. A place
            // with no address of its own is listed by its name.
            var nearby = result.NearbyLocations
                .Where(location => !string.Equals(location.LocationCategory, "intersection", StringComparison.OrdinalIgnoreCase))
                .OrderBy(location => (location.LocationCategory ?? string.Empty).IndexOf("address", StringComparison.OrdinalIgnoreCase) >= 0 ? 0 : 1)
                .ToList();
            foreach (var location in nearby.Where(location => string.IsNullOrWhiteSpace(location.Address)))
            {
                location.Address = location.LocationName;
            }

            Status.Text = result.BestMatchLocation == null
                ? "No address or place was found within the search radius."
                : "Best match: " + result.BestMatchLocation.Address;
            Results.ItemsSource = nearby;

            await ZoomToAsync(searched.GetBoundingBox());
        }

        private async void Results_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var feature = Results.SelectedItem switch
                {
                    CloudGeocodingLocation found => new Feature(found.Shape),
                    CloudReverseGeocodingLocation nearby => nearby.LocationFeature,
                    _ => null,
                };
                if (feature == null) return;

                // A location can be a point, a street or a boundary; each is drawn the way its kind is.
                _selectedResult.Clear();
                switch (feature.GetShape())
                {
                    case AreaBaseShape _:
                        _selectedResult.SetArea("found", feature, new GeoColor(80, GeoColors.MediumPurple), GeoColors.MediumPurple, 2f);
                        break;
                    case LineBaseShape _:
                        _selectedResult.SetLine("found", feature, GeoColors.MediumPurple, 6f);
                        break;
                    case PointBaseShape _:
                        _selectedResult.SetPoint("found", feature, sizeInPixel: 24f, iconName: "match");
                        break;
                }

                await ZoomToAsync(feature.GetBoundingBox());
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        // Fits the extent with a margin, but never closer than street level: a point has no size.
        private Task ZoomToAsync(RectangleShape extent)
        {
            var fit = MapUtil.GetScale(Map.MapUnit, extent, Map.MapWidth, Map.MapHeight) * 1.5;
            var streetLevel = new ZoomLevelSet().ZoomLevel17.Scale;
            return Map.ZoomToAsync(extent.GetCenterPoint(), Math.Max(fit, streetLevel));
        }
    }
}
