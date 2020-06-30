using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to add, edit, or remove markers on the map using the MarkerOverlay.
    /// </summary>
    public partial class UsingMarkersSample : UserControl
    {
        private SimpleMarkerOverlay simpleMarkerOverlay;
        private FeatureSourceMarkerOverlay hotelMarkers;

        public UsingMarkersSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10778329.017082, 3909598.36751101, -10776250.8853871, 3907890.47766975);

            AddHotelMarkers();

            AddSimpleMarkers();
        }

        /// <summary>
        /// Add a SimpleMarkerOverlay to the map
        /// </summary>
        private void AddSimpleMarkers()
        {
            simpleMarkerOverlay = new SimpleMarkerOverlay();
            mapView.Overlays.Add(simpleMarkerOverlay);
        }

        /// <summary>
        /// Create and add the hotel marker overlay to the map
        /// </summary>
        private void AddHotelMarkers()
        {
            // Create the FeatureSourceMarkerOverlay and load in Frisco Hotels as the feature source
            hotelMarkers = new FeatureSourceMarkerOverlay()
            {
                FeatureSource = new ShapeFileFeatureSource(@"../../../Data/Shapefile/Hotels.shp")
            };

            // Project the Hotel POI data to match the projection on the map
            hotelMarkers.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Create a style for the hotels
            var pointMarkerStyle = new PointMarkerStyle()
            {
                ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute)),
                Width = 20,
                Height = 34,
                YOffset = -17,
                ToolTip = "[#NAME#]."
            };
            hotelMarkers.ZoomLevelSet.ZoomLevel01.CustomMarkerStyle = pointMarkerStyle;
            hotelMarkers.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the hotelMarkers overlay to the map
            mapView.Overlays.Add(hotelMarkers);
        }

        /// <summary>
        /// Adds a marker to the simpleMarkerOverlay where the map click event occurred.
        /// </summary>
        private void MapView_OnMapClick(object sender, MapClickMapViewEventArgs e)
        {
            // Create a marker at the position the mouse was clicked
            var marker = new Marker(e.WorldLocation)
            {
                ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute)),
                Width = 20,
                Height = 34,
                YOffset = -17
            };

            // Add the marker to the simpleMarkerOverlay and refresh the map
            simpleMarkerOverlay.Markers.Add(marker);
            simpleMarkerOverlay.Refresh();
        }

        /// <summary>
        /// Sets the simpleMarkerOverlay's drag mode to none, meaning that the markers cannot be moved or manipulated.
        /// </summary>
        private void StaticMode_OnClick(object sender, RoutedEventArgs e)
        {
            simpleMarkerOverlay.DragMode = MarkerDragMode.None;
        }

        /// <summary>
        /// Sets the simpleMarkerOverlay's drag mode to drag, which allows the user to click and drag on an icon to move it.
        /// </summary>
        private void DragMode_OnClick(object sender, RoutedEventArgs e)
        {
            simpleMarkerOverlay.DragMode = MarkerDragMode.Drag;
        }

        /// <summary>
        /// Sets the simpleMarkerOverlay's drag mode to copy, which allows the user to copy an existing marker by shift-clicking and dragging it.
        /// </summary>
        private void CopyMode_OnClick(object sender, RoutedEventArgs e)
        {
            simpleMarkerOverlay.DragMode = MarkerDragMode.CopyWithShiftKey;
        }
    }
}
