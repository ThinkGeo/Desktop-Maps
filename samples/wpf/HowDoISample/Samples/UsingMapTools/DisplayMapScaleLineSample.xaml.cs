using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render a ScaleLine on the map in a variety of different of styles.
    /// </summary>
    public partial class DisplayMapScaleLineSample : UserControl
    {
        public DisplayMapScaleLineSample()
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
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);
        }

        /// <summary>
        /// Enable the ScaleLine and add it to the MapView (default: bottom left)
        /// </summary>
        private void DisplayScaleLine_Checked(object sender, RoutedEventArgs e)
        {
            mapView.MapTools.ScaleLine.IsEnabled = true;
        }

        /// <summary>
        /// Disable the ScaleLine and remove it from the MapView
        /// </summary>
        private void DisplayScaleLine_Unchecked(object sender, RoutedEventArgs e)
        {
            mapView.MapTools.ScaleLine.IsEnabled = false;
        }
    }
}
