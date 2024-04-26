using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render a Web Map Service using the WMSOverlay.
    /// </summary>
    public partial class DisplayWmsOverlaySample : IDisposable
    {
        public DisplayWmsOverlaySample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with a background overlay and set the map's extent to Frisco, Tx.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add a simple background overlay
            MapView.BackgroundOverlay.BackgroundBrush = GeoBrushes.AliceBlue;

            // Set the map extent
            MapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

            // Create a WmsOverlay and add it to the map.
            var wmsOverlay = new WmsOverlay();
            wmsOverlay.AxisOrder = WmsAxisOrder.XY;
            wmsOverlay.Uri = new Uri("http://ows.mundialis.de/services/service");
            wmsOverlay.Parameters.Add("VERSION", "1.3.0");
            wmsOverlay.Parameters.Add("LAYERS", "OSM-WMS");
            wmsOverlay.Parameters.Add("STYLES", "default");
            wmsOverlay.Parameters.Add("CRS", "EPSG:3857");  // Make sure to match the WMS CRS to the Map's projection
            MapView.Overlays.Add(wmsOverlay);

            await MapView.RefreshAsync();
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