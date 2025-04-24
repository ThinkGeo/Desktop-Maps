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

        public NavigationMap()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.CurrentExtentChanged += MapView_CurrentExtentChanged;
            
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;
            // Add ThinkGeo Cloud Maps as the background 
            _backgroundOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X2,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(_backgroundOverlay);

            // Create the marker overlay to hold UI elements like labels
            var markerOverlay = new SimpleMarkerOverlay();
            MapView.Overlays.Add(markerOverlay);

            // Convert Lat/Lon (EPSG:4326) to Spherical Mercator (EPSG:3857)
            _empireStateBuildingPosition = ProjectionConverter.Convert(4326, 3857, new PointShape(-73.9856654, 40.74843661));

            // Create a marker with both label and image content
            var marker = new Marker(_empireStateBuildingPosition)
            {
                //Content = markerContent,
                ImageSource = new BitmapImage(new Uri("/Resources/empire_state_building.png", UriKind.RelativeOrAbsolute)),
                Width = 32,
                Height = 64,
                YOffset = -32
            };

            // Add the marker to the overlay
            markerOverlay.Markers.Add(marker);

            // set up the map extent and refresh
            MapView.RotationAngle = -30;
            MapView.CurrentScale = 100000;
            MapView.CenterPoint = _empireStateBuildingPosition;

            _ = MapView.RefreshAsync();
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

        private void MapView_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            var center = MapView.CurrentExtent.GetCenterPoint();
            var centerInDecimalDegrees= ProjectionConverter.Convert(3857, 4326, center);
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

            MapView.CancellationTokenSource.Cancel(); // Cancel the ongoing rendering
            _ = _backgroundOverlay.RefreshAsync();
        }

        private void CompassButton_Click(object sender, RoutedEventArgs e)
        {
            _ = MapView.ZoomToAsync(MapView.CenterPoint, MapView.CurrentScale, 0);
        }

        private void DefaultExtentButton_Click(object sender, RoutedEventArgs e)
        {
            _ = MapView.ZoomToAsync(_empireStateBuildingPosition, 100000, -30);
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