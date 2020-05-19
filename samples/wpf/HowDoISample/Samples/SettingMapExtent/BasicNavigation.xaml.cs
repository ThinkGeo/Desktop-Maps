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
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);
            mapView.ZoomToScale(mapView.ZoomLevelSet.ZoomLevel03.Scale);
        }

        private void Pan_Click(object sender, RoutedEventArgs e)
        {
            var percentage = (int)panPercentage.Value;
            switch (((Button)sender).Name)
            {
                case "panNorth":
                    mapView.Pan(PanDirection.Up, percentage);
                    break;
                case "panEast":
                    mapView.Pan(PanDirection.Right, percentage);
                    break;
                case "panWest":
                    mapView.Pan(PanDirection.Left, percentage);
                    break;
                case "panSouth":
                    mapView.Pan(PanDirection.Down, percentage);
                    break;
            }
        }
    }
}
