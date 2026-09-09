using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn to add, edit, or remove markers on the map using the MarkerOverlay.
    /// </summary>
    public partial class Markers
    {

        private bool _initialized;
        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        private int _logIndex = 0;

        // A dot drawn AT the center point, for contrast with the marker: the marker
        // is a screen element anchored by its image's tip, the dot is geometry.
        private readonly InMemoryGeometrySource _centerDot = new InMemoryGeometrySource();

        public Markers()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // Tilt with the right mouse button or the bar on the right: a marker is a
            // screen element, and it stays on its ground point under perspective.
            Map.TiltInteractionEnabled = true;
            Map.MapTools.TiltBar.IsEnabled = true;

            var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));
            style.Images.Add("dot", PointStyle.CreateSimpleCircleStyle(GeoColors.Red, 10));
            style.AddGeometry(_centerDot);
            Map.Basemap = new GpuBasemap(style);

            Map.CenterPoint = new PointShape(-10777290, 3908740);
            Map.CurrentScale = 9030;

            var simpleMarkerOverlay = new SimpleMarkerOverlay();
            simpleMarkerOverlay.MarkerDragged += SimpleMarkerOverlayOnMarkerDragged;
            simpleMarkerOverlay.MarkerDragging += SimpleMarkerOverlay_MarkerDragging;
            Map.Overlays.Add("simpleMarkerOverlay", simpleMarkerOverlay);

            // The dot marks the exact point; the marker's image tip points at it.
            _centerDot.UpdatePoints(new[] { Map.CenterPoint }, sizeInPixels: 10f, iconName: "dot");

            // Create a marker at the center point
            var marker = new Marker(Map.CenterPoint)
            {
                ImageSource = new BitmapImage(new Uri("/Resources/marker.png", UriKind.RelativeOrAbsolute)),
                Width = 20,
                Height = 34,
                YOffset = -17
            };

            marker.PositionChanged += Marker_PositionChanged;
            // Add the marker to the simpleMarkerOverlay and refresh the map
            simpleMarkerOverlay.Markers.Add(marker);

            _ = Map.RefreshAsync();
        }

        /// <summary>
        /// Adds a marker to the simpleMarkerOverlay where the map click event occurred.
        /// </summary>
        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            var simpleMarkerOverlay = (SimpleMarkerOverlay)Map.Overlays["simpleMarkerOverlay"];

            // Create a marker at the position the mouse was clicked
            var marker = new Marker(e.WorldLocation)
            {
                ImageSource = new BitmapImage(new Uri("/Resources/marker.png", UriKind.RelativeOrAbsolute)),
                Width = 20,
                Height = 34,
                YOffset = -17
            };

            marker.PositionChanged += Marker_PositionChanged;

            // Add the marker to the simpleMarkerOverlay and refresh the map
            simpleMarkerOverlay.Markers.Add(marker);
            _ = simpleMarkerOverlay.RefreshAsync();
        }

        private void Marker_PositionChanged(object sender, PositionChangedMarkerEventArgs e)
        {
            AppendLog($"PositionChanged: {e.NewPosition.Y:N0}");
        }

        private void SimpleMarkerOverlay_MarkerDragging(object sender, MarkerDraggingSimpleMarkerOverlayEventArgs e)
        {
            AppendLog($"MarkerDragging: ");
        }

        private void SimpleMarkerOverlayOnMarkerDragged(object sender, MarkerDraggedSimpleMarkerOverlayEventArgs e)
        {
            AppendLog($"MarkerDragged: ");
        }

        /// <summary>
        /// Sets the simpleMarkerOverlay's drag mode to none, meaning that the markers cannot be moved or manipulated.
        /// </summary>
        private void StaticMode_OnClick(object sender, RoutedEventArgs e)
        {
            var simpleMarkerOverlay = (SimpleMarkerOverlay)Map.Overlays["simpleMarkerOverlay"];
            simpleMarkerOverlay.DragMode = MarkerDragMode.None;
        }

        /// <summary>
        /// Sets the simpleMarkerOverlay's drag mode to drag, which allows the user to click and drag on an icon to move it.
        /// </summary>
        private void DragMode_OnClick(object sender, RoutedEventArgs e)
        {
            var simpleMarkerOverlay = (SimpleMarkerOverlay)Map.Overlays["simpleMarkerOverlay"];
            simpleMarkerOverlay.DragMode = MarkerDragMode.Drag;
        }

    
        private void AppendLog(string message)
        {
            // Add log message to the observable collection
            LogMessages.Add($"{_logIndex++}: {message}");
            LogListBox.ScrollIntoView(LogMessages[LogMessages.Count - 1]);
        }
    }
}
