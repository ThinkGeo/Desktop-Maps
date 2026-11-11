using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Find addresses and places by name, or click the map to find the addresses and places around a point.
    /// </summary>
    public partial class Geocoding : IDisposable
    {
        private bool _initialized;
        private bool _reverse;
        private GeocodingCloudClient _geocodingCloudClient;
        private ReverseGeocodingCloudClient _reverseGeocodingCloudClient;
        private InMemoryFeatureLayer _searchedAreaLayer;
        private InMemoryFeatureLayer _resultLayer;
        private LayerOverlay _resultOverlay;
        private PopupOverlay _bestMatchOverlay;

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
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            _searchedAreaLayer = new InMemoryFeatureLayer();
            _searchedAreaLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(new GeoColor(100, GeoColors.Blue), 2), new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));
            _searchedAreaLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Cross, 20, GeoBrushes.Red);
            _searchedAreaLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _resultLayer = new InMemoryFeatureLayer();
            _resultLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);
            _resultLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.MediumPurple, 6, false);
            _resultLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(80, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);
            _resultLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _resultOverlay = new LayerOverlay();
            _resultOverlay.Layers.Add(_searchedAreaLayer);
            _resultOverlay.Layers.Add(_resultLayer);
            Map.Overlays.Add(_resultOverlay);

            _bestMatchOverlay = new PopupOverlay();
            Map.Overlays.Add(_bestMatchOverlay);

            Map.CenterPoint = new PointShape(-10778720, 3915154);
            Map.CurrentScale = 202090;

            _geocodingCloudClient = new GeocodingCloudClient
            {
                ClientId = SampleKeys.ClientId2,
                ClientSecret = SampleKeys.ClientSecret2,
            };
            _reverseGeocodingCloudClient = new ReverseGeocodingCloudClient
            {
                ClientId = SampleKeys.ClientId2,
                ClientSecret = SampleKeys.ClientSecret2,
            };
            _latLon.Open();

            _ = Map.RefreshAsync();
        }

        private async void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _reverse = (string)((RadioButton)sender).Tag == "reverse";
            SearchOptions.Visibility = _reverse ? Visibility.Collapsed : Visibility.Visible;
            ReverseOptions.Visibility = _reverse ? Visibility.Visible : Visibility.Collapsed;
            Status.Text = _reverse ? "Click the map, or type a location and search." : string.Empty;

            Results.ItemsSource = null;
            _searchedAreaLayer.InternalFeatures.Clear();
            _resultLayer.InternalFeatures.Clear();
            _bestMatchOverlay.Popups.Clear();

            try
            {
                await Map.RefreshAsync();
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await SearchAsync();
            }
            catch (OperationCanceledException)
            {
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
            catch (OperationCanceledException)
            {
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
            catch (OperationCanceledException)
            {
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

            var options = new CloudGeocodingOptions { ResultProjectionInSrid = 3857 };

            LoadingImage.Visibility = Visibility.Visible;
            var result = await _geocodingCloudClient.SearchAsync(TxtSearchString.Text.Trim(), options);
            LoadingImage.Visibility = Visibility.Hidden;

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
            if (!int.TryParse(TxtSearchRadius.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var searchRadius) || searchRadius <= 0)
            {
                Status.Text = "The search radius is a whole number of meters, greater than 0.";
                return;
            }

            var searched = new EllipseShape(point, searchRadius);
            _searchedAreaLayer.InternalFeatures.Clear();
            _searchedAreaLayer.InternalFeatures.Add(new Feature(searched));
            _searchedAreaLayer.InternalFeatures.Add(new Feature(point));
            _resultLayer.InternalFeatures.Clear();
            _bestMatchOverlay.Popups.Clear();

            LoadingImage.Visibility = Visibility.Visible;
            var result = await _reverseGeocodingCloudClient.SearchPointAsync(point.X, point.Y, 3857, searchRadius, DistanceUnit.Meter, new CloudReverseGeocodingOptions());
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

            if (result.BestMatchLocation == null)
            {
                Status.Text = "No address or place was found within the search radius.";
            }
            else
            {
                Status.Text = "Best match: " + result.BestMatchLocation.Address;
                var bestMatchShape = result.BestMatchLocation.LocationFeature.GetShape();
                var popupPosition = bestMatchShape.GetClosestPointTo(point, GeographyUnit.Meter) ?? bestMatchShape.GetCenterPoint();
                _bestMatchOverlay.Popups.Add(new Popup(popupPosition) { Content = Status.Text, FontSize = 10d });
            }
            Results.ItemsSource = nearby;

            await ZoomToAsync(searched.GetBoundingBox());
        }

        private async void Results_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Feature feature = null;
                if (Results.SelectedItem is CloudGeocodingLocation found)
                {
                    feature = new Feature(found.Shape);
                }
                else if (Results.SelectedItem is CloudReverseGeocodingLocation nearby)
                {
                    feature = nearby.LocationFeature;
                }
                if (feature == null) return;

                _resultLayer.InternalFeatures.Clear();
                _resultLayer.InternalFeatures.Add(feature);

                await ZoomToAsync(feature.GetBoundingBox());
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        // Fits the extent with a margin, but never closer than street level: a point has no size.
        private async Task ZoomToAsync(RectangleShape extent)
        {
            var fit = MapUtil.GetScale(Map.MapUnit, extent, Map.MapWidth, Map.MapHeight) * 1.5;
            var streetLevel = new ZoomLevelSet().ZoomLevel17.Scale;
            await Map.ZoomToAsync(extent.GetCenterPoint(), Math.Max(fit, streetLevel));
            await _resultOverlay.RefreshAsync();
            await _bestMatchOverlay.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
