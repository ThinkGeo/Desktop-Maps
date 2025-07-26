using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render coordinate info based on the mouse cursor position on the map.
    /// </summary>
    public partial class MouseCoordinates : IDisposable
    {
        private bool _initialized;

        public MouseCoordinates()
        {
            InitializeComponent();
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

            MapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.LatitudeLongitude;
            MapView.MapTools.MouseCoordinate.IsEnabled = true;

            //Add a mouse move event handler to the map so that we can refresh the textboxes (off the map)
            MapView.MouseMove += MapView_MouseMove;

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10778000, 3912000);
            MapView.CurrentScale = 77000;

            _initialized = true;
            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Sets the visibility of the MouseCoordinates to true
        /// </summary>
        private void DisplayMouseCoordinates_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            MapView.MapTools.MouseCoordinate.IsEnabled = true;
        }

        /// <summary>
        /// Sets the visibility of the MouseCoordinates to false
        /// </summary>
        private void DisplayMouseCoordinates_Unchecked(object sender, RoutedEventArgs e)
        {
            MapView.MapTools.MouseCoordinate.IsEnabled = false;
        }

        /// <summary>
        /// Changes the display format of the MouseCoordinates based on ComboBox selection
        /// </summary>
        private void CoordinateType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_initialized)
                return;

            switch (((ComboBoxItem)CoordinateType.SelectedItem).Content)
            {
                case "(lat), (lon)":
                    // Set to Lat, Lon format
                    MapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.LatitudeLongitude;
                    break;
                case "(lon), (lat)":
                    // Set to Lon, Lat format
                    MapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.LongitudeLatitude;
                    break;
                case "(custom)":
                    // Set to a custom format
                    MapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.Custom;
                    // Add an EventHandler to handle what the formatted output should look like
                    MapView.MapTools.MouseCoordinate.CustomFormatted += MouseCoordinate_CustomMouseCoordinateFormat;
                    break;
            }
        }

        /// <summary>
        /// Event handler that formats the MouseCoordinates to use WorldCoordinates and changes the Foreground color to red.
        /// Other modifications to the display of the MouseCoordinates can be safely done here.
        /// </summary>
        private static void MouseCoordinate_CustomMouseCoordinateFormat(object sender, CustomFormattedMouseCoordinateMapToolEventArgs e)
        {
            ((MouseCoordinateMapTool)sender).Foreground = new SolidColorBrush(Colors.Black);
            e.Result = $"X: {e.WorldCoordinate.X:N0}, Y: {e.WorldCoordinate.Y:N0}";
        }

        /// <summary>
        /// Event handler for the MapView's MouseMove event.  
        /// We just get the mouse position's current location 
        /// </summary>
        private void MapView_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (DisplayMouseCoordinatesTextBox.IsChecked == true)
            {
                var currentPoint = e.GetPosition(MapView);
                var worldPoint = MapUtil.ToWorldCoordinate(MapView.CurrentExtent, currentPoint.X, currentPoint.Y, MapView.MapWidth, MapView.MapHeight);

                switch (((ComboBoxItem)CoordinateType.SelectedItem).Content)
                {
                    case "(lat), (lon)":
                        // Set to Lat, Lon format
                        txtCoordinate.Text = $"{worldPoint.Y:F3}, {worldPoint.X:F3}";
                        break;
                    case "(lon), (lat)":
                        // Set to Lon, Lat format
                        txtCoordinate.Text = $"{worldPoint.X:F3}, {worldPoint.Y:F3}";
                        break;
                    case "(custom)":
                        txtCoordinate.Text = $"{worldPoint.X:N0}, {worldPoint.Y:N0}";
                        break;
                }
            }
            else
            {
                txtCoordinate.Text = "N/A";
            }
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