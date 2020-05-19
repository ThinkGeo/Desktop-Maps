using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// This sample demonstrates how to programmatically pan and zoom the map control. 
    /// </summary>
    public partial class BasicNavigation : UserControl
    {
        public BasicNavigation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);
        }

        private void Pan_Click(object sender, RoutedEventArgs e)
        {
            Enum.TryParse(((Button)sender).Name, out PanDirection direction);
            int.TryParse(Strength.Text, out int strength);
            mapView.Pan(direction, strength);
        }
    }
}
