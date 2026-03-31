using System;
using System.Windows;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to zoom, pan, and rotate the map control.
    /// </summary>
    public partial class NavigationMap : IDisposable
    {
        private ThinkGeoCloudRasterMapsOverlay _backgroundOverlay;
        private PointShape _empireStateBuildingPosition;
        private bool _initialized;

        public NavigationMap()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.CurrentExtentChanged += Map_CurrentExtentChanged;

            // Set the map's unit of measurement to meters(Spherical Mercator)
            Map.MapUnit = GeographyUnit.Meter;
            // Add ThinkGeo Cloud Maps as the background 
            _backgroundOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X2,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(_backgroundOverlay);

            // Create the marker overlay to hold UI elements like labels
            var markerOverlay = new SimpleMarkerOverlay();
            Map.Overlays.Add(markerOverlay);

            // Convert Lat/Lon (EPSG:4326) to Spherical Mercator (EPSG:3857)
            _empireStateBuildingPosition = ProjectionConverter.Convert(4326, 3857, new PointShape(-73.9856654, 40.74843661));

            // set up the map extent and refresh
            Map.RotationAngle = -30;
            Map.CurrentScale = 100000;
            Map.CenterPoint = _empireStateBuildingPosition;

            // Create a marker with both label and image content
            var marker = new Marker(_empireStateBuildingPosition)
            {
                ImageSource = new BitmapImage(new Uri("/Resources/empire_state_building.png", UriKind.RelativeOrAbsolute)),
                Width = 32,
                Height = 64,
                YOffset = -32
            };
            markerOverlay.Markers.Add(marker);

            _ = Map.RefreshAsync();
        }

        // Register the dependency property.
        public static readonly DependencyProperty TxtCoordinatesProperty =
            DependencyProperty.Register(
                nameof(TxtCoordinates),
                typeof(string),
                typeof(NavigationMap),
                null);

        /// <summary>
        /// Gets or sets the text that represents the coordinates.
        /// This is a bindable property.
        /// </summary>
        public string TxtCoordinates
        {
            get => (string)GetValue(TxtCoordinatesProperty);
            set => SetValue(TxtCoordinatesProperty, value);
        }

        private void Map_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            var currentExtent = e.NewExtent ?? Map.CurrentExtent;
            if (currentExtent == null) return;

            var center = currentExtent.GetCenterPoint();
            var centerInDecimalDegrees = ProjectionConverter.Convert(3857, 4326, center);
            TxtCoordinates = $"Center Point: (Lat: {centerInDecimalDegrees.Y:N4}, Lon: {centerInDecimalDegrees.X:N4})";
        }

        private void ThemeCheckBox_Click(object sender, RoutedEventArgs e)
        {
            _backgroundOverlay.MapType = ThemeCheckBox.IsChecked == true
                ? ThinkGeoCloudRasterMapsMapType.Dark_V2_X2
                : ThinkGeoCloudRasterMapsMapType.Light_V2_X2;

            // You may need to reset the tile cache ID to avoid mixing dark/light tiles:
            _backgroundOverlay.TileCache = new FileRasterTileCache(@".\cache",
                ThemeCheckBox.IsChecked == true ? "thinkgeo_vector_dark" : "thinkgeo_vector_light");

            Map.CancellationTokenSource.Cancel(); // Cancel the ongoing rendering
            _ = _backgroundOverlay.RefreshAsync();
        }

        private void CompassButton_Click(object sender, RoutedEventArgs e)
        {
            _ = Map.ZoomToAsync(Map.CenterPoint, Map.CurrentScale, 0);
        }

        private void DefaultExtentButton_Click(object sender, RoutedEventArgs e)
        {
            _ = Map.ZoomToAsync(_empireStateBuildingPosition, 100000, -30);
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}

