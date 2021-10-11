﻿using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render a Web Map Service using the WMSOverlay.
    /// </summary>
    public partial class DisplayWMSOverlaySample : UserControl, IDisposable
    {
        public DisplayWMSOverlaySample()
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

            // Create a WmsOverlay and add it to the map.
            WmsOverlay wmsOverlay = new WmsOverlay();
            wmsOverlay.ServerUri = new Uri("http://ows.mundialis.de/services/service");
            wmsOverlay.Parameters.Add("LAYERS", "OSM-WMS");
            wmsOverlay.Parameters.Add("STYLES", "default");
            wmsOverlay.Parameters.Add("CRS", "EPSG:3857");  // Make sure to match the WMS CRS to the Map's projection
            wmsOverlay.Parameters.Add("SRS", "EPSG:3857");  // Make sure to match the WMS CRS to the Map's projection
            mapView.Overlays.Add(wmsOverlay);
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
