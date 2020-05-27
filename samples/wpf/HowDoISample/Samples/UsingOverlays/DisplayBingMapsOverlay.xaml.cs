﻿using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render Bing Maps using the BingMapsOverlay.
    /// A valid Bing Maps ApplicationID is required.
    /// </summary>
    public partial class DisplayBingMapsOverlay : UserControl
    {
        public DisplayBingMapsOverlay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with a background overlay and set the map's extent to Frisco, Tx.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add a simple background overlay
            mapView.BackgroundOverlay.BackgroundBrush = GeoBrushes.AliceBlue;

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);
        }

        /// <summary>
        /// Create a Bing Maps overlay and add it to the map view.
        /// </summary>
        private void DisplayBingMaps_Click(object sender, RoutedEventArgs e)
        {
            BingMapsOverlay bingMapsOverlay = new BingMapsOverlay(bingApplicationId.Text, BingMapsMapType.Road);
            mapView.Overlays.Add(bingMapsOverlay);
            mapView.Refresh();
        }
    }
}
