using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to add, edit, or remove markers on the map using the MarkerOverlay.
    /// </summary>
    public partial class Markers : IDisposable
    {
        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        private int _logIndex = 0;

        public Markers()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10777290, 3908740);
            MapView.CurrentScale = 9030;

            var simpleMarkerOverlay = new SimpleMarkerOverlay();
            simpleMarkerOverlay.MarkerDragged += SimpleMarkerOverlayOnMarkerDragged;
            simpleMarkerOverlay.MarkerDragging += SimpleMarkerOverlay_MarkerDragging;
            MapView.Overlays.Add("simpleMarkerOverlay", simpleMarkerOverlay);

            // create a point at the center point
            InMemoryFeatureLayer layer = new InMemoryFeatureLayer();
            layer.InternalFeatures.Add(new Feature(MapView.CenterPoint)); 
            layer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Red, 10);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var overlay = new LayerOverlay();
            overlay.Layers.Add(layer);
            MapView.Overlays.Add(overlay);

            // Create a marker at the center point
            var marker = new Marker(MapView.CenterPoint)
            {
                ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute)),
                Width = 20,
                Height = 34,
                YOffset = -17
            };

            marker.PositionChanged += Marker_PositionChanged;
            // Add the marker to the simpleMarkerOverlay and refresh the map
            simpleMarkerOverlay.Markers.Add(marker);

            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Adds a marker to the simpleMarkerOverlay where the map click event occurred.
        /// </summary>
        private void MapView_OnMapClick(object sender, MapClickMapViewEventArgs e)
        {
            var simpleMarkerOverlay = (SimpleMarkerOverlay)MapView.Overlays["simpleMarkerOverlay"];

            // Create a marker at the position the mouse was clicked
            var marker = new Marker(e.WorldLocation)
            {
                ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute)),
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
            var simpleMarkerOverlay = (SimpleMarkerOverlay)MapView.Overlays["simpleMarkerOverlay"];
            simpleMarkerOverlay.DragMode = MarkerDragMode.None;
        }

        /// <summary>
        /// Sets the simpleMarkerOverlay's drag mode to drag, which allows the user to click and drag on an icon to move it.
        /// </summary>
        private void DragMode_OnClick(object sender, RoutedEventArgs e)
        {
            var simpleMarkerOverlay = (SimpleMarkerOverlay)MapView.Overlays["simpleMarkerOverlay"];
            simpleMarkerOverlay.DragMode = MarkerDragMode.Drag;
        }

    
        private void AppendLog(string message)
        {
            // Add log message to the observable collection
            LogMessages.Add($"{_logIndex++}: {message}");
            LogListBox.ScrollIntoView(LogMessages[LogMessages.Count - 1]);
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