using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render coordinate info based on the mouse cursor position on the map.
    /// </summary>
    public partial class DisplayMapMouseCoordinatesSample : UserControl, IDisposable
    {
        public DisplayMapMouseCoordinatesSample()
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
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

            mapView.Refresh();
        }
        
        /// <summary>
        /// Sets the visibility of the MouseCoordinates to true
        /// </summary>
        private void DisplayMouseCoordinates_Checked(object sender, RoutedEventArgs e)
        {
            mapView.MapTools.MouseCoordinate.IsEnabled = true;
        }

        /// <summary>
        /// Sets the visibility of the MouseCoordinates to false
        /// </summary>
        private void DisplayMouseCoordinates_Unchecked(object sender, RoutedEventArgs e)
        {
            mapView.MapTools.MouseCoordinate.IsEnabled = false;
        }

        /// <summary>
        /// Changes the display format of the MouseCoordinates based on ComboBox selection
        /// </summary>
        private void CoordinateType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ComboBoxItem)coordinateType.SelectedItem).Content)
            {
                case "(lat), (lon)":
                    // Set to Lat, Lon format
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.LatitudeLongitude;                    
                    break;
                case "(lon), (lat)":
                    // Set to Lon, Lat format
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.LongitudeLatitude;
                    break;
                case "(degrees), (minutes), (seconds)":
                    // Set to Degrees, Minutes, Seconds format
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.DegreesMinutesSeconds;
                    break;
                case "(custom)":
                    // Set to a custom format
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.Custom;
                    // Add an EventHandler to handle what the formatted output should look like
                    mapView.MapTools.MouseCoordinate.CustomFormatted += new System.EventHandler<CustomFormattedMouseCoordinateMapToolEventArgs>(MouseCoordinate_CustomMouseCoordinateFormat);
                    break;
            }
        }

        /// <summary>
        /// Event handler that formats the MouseCoordinates to use WorldCoordinates and changes the Foreground color to red.
        /// Other modifications to the display of the MouseCoordinates can be safely done here.
        /// </summary>
        private void MouseCoordinate_CustomMouseCoordinateFormat(object sender, CustomFormattedMouseCoordinateMapToolEventArgs e)
        {
            ((MouseCoordinateMapTool)sender).Foreground = new SolidColorBrush(Colors.Red);
            e.Result = $"X: {e.WorldCoordinate.X.ToString("N0")}, Y: {e.WorldCoordinate.Y.ToString("N0")}";
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

    }
}
