using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Pan, zoom, rotate and tilt the map with the mouse or touch, and watch the events the map raises.
    /// </summary>
    public partial class MapNavigation
    {
        private PointShape _empireStateBuildingPosition;
        private int _logIndex;
        private bool _initialized;

        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();

        public MapNavigation()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // Right-drag turns and tilts the map.
            Map.IsTiltEnabled = true;
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) }));

            var markerOverlay = new SimpleMarkerOverlay();
            Map.Overlays.Add(markerOverlay);

            _empireStateBuildingPosition = ProjectionConverter.Convert(4326, 3857, new PointShape(-73.9856654, 40.74843661));

            Map.RotationAngle = -30;
            Map.CurrentScale = 100000;
            Map.CenterPoint = _empireStateBuildingPosition;

            markerOverlay.Markers.Add(new Marker(_empireStateBuildingPosition)
            {
                ImageSource = new BitmapImage(new Uri("/Resources/empire_state_building.png", UriKind.RelativeOrAbsolute)),
                Width = 32,
                Height = 64,
                YOffset = -32
            });

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
            if (!_initialized) return;

            var currentExtent = e.NewExtent ?? Map.CurrentExtent;
            if (currentExtent == null) return;

            var centerInDecimalDegrees = ProjectionConverter.Convert(3857, 4326, currentExtent.GetCenterPoint());
            TxtCoordinates = $"Center Point: (Lat: {centerInDecimalDegrees.Y:N4}, Lon: {centerInDecimalDegrees.X:N4})";
            AppendLog($"CurrentExtentChanged: center {centerInDecimalDegrees.Y:N4}, {centerInDecimalDegrees.X:N4}");
        }

        private void Map_CurrentScaleChanged(object sender, CurrentScaleChangedMapViewEventArgs e)
        {
            if (!_initialized) return;

            AppendLog($"CurrentScaleChanged: 1:{e.OldScale:N0} to 1:{e.NewScale:N0}");
        }

        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            AppendLog($"MapClick: {e.MouseButton} at ({e.WorldX:N0}, {e.WorldY:N0})");
        }

        private void Map_MapDoubleClick(object sender, MapClickMapViewEventArgs e)
        {
            AppendLog($"MapDoubleClick: {e.MouseButton} at ({e.WorldX:N0}, {e.WorldY:N0})");
        }

        private void AppendLog(string message)
        {
            LogMessages.Add($"{_logIndex++}: {message}");
            LogListBox.ScrollIntoView(LogMessages[LogMessages.Count - 1]);
        }

        private void ClearLog_Click(object sender, RoutedEventArgs e)
        {
            LogMessages.Clear();
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
