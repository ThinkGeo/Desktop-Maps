﻿using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render a PanZoomBar on the map.
    /// </summary>
    public partial class DisplayMapPanZoomBarSample : UserControl
    {
        public DisplayMapPanZoomBarSample()
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
        /// Enable the PanZoomBar and remove it from the MapView
        /// </summary>
        private void DisplayPanZoomBar_Checked(object sender, RoutedEventArgs e)
        {
            mapView.MapTools.PanZoomBar.IsEnabled = true;
        }

        /// <summary>
        /// Disable the PanZoomBar and remove it from the MapView
        /// </summary>
        private void DisplayPanZoomBar_Unchecked(object sender, RoutedEventArgs e)
        {
            mapView.MapTools.PanZoomBar.IsEnabled = false;
        }
    }
}