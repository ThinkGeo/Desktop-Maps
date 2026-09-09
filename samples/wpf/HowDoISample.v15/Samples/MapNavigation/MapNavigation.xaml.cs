using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to zoom, pan, rotate and tilt the map control.
    /// </summary>
    public partial class MapNavigation
    {
        private readonly ThinkGeoVectorTileSource _cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);
        private GpuBasemap _backgroundOverlay;
        private bool _darkTheme;
        private bool _isSwitchingTheme;
        private PointShape _empireStateBuildingPosition;
        private bool _initialized;

        public MapNavigation()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.ZoomStep = 0.15;
            Map.CurrentExtentChanged += Map_CurrentExtentChanged;

            Map.MapUnit = GeographyUnit.Meter;
            // The street style carries its own building extrusions, so the 3D
            // Buildings checkbox is a renderer switch, not a style rebuild.
            Map.TiltInteractionEnabled = true;
            _backgroundOverlay = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, _cloud));
            _backgroundOverlay.DrawExtrusionsEnabled = BuildingCheckBox.IsChecked != false;
            Map.Basemap = _backgroundOverlay;

            var markerOverlay = new SimpleMarkerOverlay();
            Map.Overlays.Add(markerOverlay);

            _empireStateBuildingPosition = ProjectionConverter.Convert(4326, 3857, new PointShape(-73.9856654, 40.74843661));

            Map.RotationAngle = -30;
            Map.CurrentScale = 100000;
            Map.CenterPoint = _empireStateBuildingPosition;

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

        public static readonly DependencyProperty TxtCoordinatesProperty =
            DependencyProperty.Register(
                nameof(TxtCoordinates),
                typeof(string),
                typeof(MapNavigation),
                null);

        /// <summary>The center of the view, in latitude and longitude, for the readout.</summary>
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

        private void BuildingCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!_initialized || _backgroundOverlay == null) return;

            _backgroundOverlay.DrawExtrusionsEnabled = BuildingCheckBox.IsChecked == true;
            _ = Map.RefreshAsync();
        }

        private void PitchSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var pitch = Math.Max(0, Math.Min(80, e.NewValue));
            if (PitchValueText != null)
            {
                PitchValueText.Text = $"{pitch:0}°";
            }

            if (_initialized)
            {
                Map.TiltAngle = pitch;
                _ = Map.RefreshAsync();
            }
        }

        private void DarkThemeButton_OnClick(object sender, RoutedEventArgs e)
        {
            _ = ToggleThemeAsync();
        }

        // Light and dark read the SAME vector tiles - a theme is a restyle, not a new
        // basemap, so the camera (pitch included) and the fetched tiles carry over.
        private async Task ToggleThemeAsync()
        {
            if (!_initialized || _isSwitchingTheme) return;

            _isSwitchingTheme = true;
            try
            {
                _darkTheme = !_darkTheme;
                DarkThemeButton.Content = _darkTheme ? "Light Theme" : "Dark Theme";
                await _backgroundOverlay.SetStyleAsync(new MapStyle(_darkTheme ? ThinkGeoVectorStyles.Dark : ThinkGeoVectorStyles.Light, _cloud));
            }
            finally
            {
                _isSwitchingTheme = false;
            }
        }

        private void CompassButton_Click(object sender, RoutedEventArgs e)
        {
            _ = Map.ZoomToAsync(Map.CenterPoint, Map.CurrentScale, 0);
        }

        private void DefaultExtentButton_Click(object sender, RoutedEventArgs e)
        {
            _ = Map.ZoomToAsync(_empireStateBuildingPosition, 100000, -30);
        }
    }
}
