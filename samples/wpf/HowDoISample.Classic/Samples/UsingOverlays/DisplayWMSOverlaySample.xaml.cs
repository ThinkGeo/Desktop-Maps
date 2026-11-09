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

        private bool _initialized;
        public DisplayWmsOverlaySample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with a background overlay and set the map's extent to Frisco, Tx.
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // Set the map's unit of measurement to meters(Spherical Mercator)
            Map.MapUnit = GeographyUnit.Meter;

            // Add a simple background overlay
            Map.BackgroundOverlay.BackgroundBrush = GeoBrushes.AliceBlue;

            // Set the map extent
            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 77000;

            // Create a WmsOverlay and add it to the map.
            var wmsOverlay = new WmsOverlay(new Uri("http://ows.mundialis.de/services/service"));
            wmsOverlay.AxisOrder = WmsAxisOrder.XY;
            wmsOverlay.Crs = "EPSG:3857"; // Make sure to match the WMS CRS to the Map's projection
            wmsOverlay.ActiveLayerNames.Add("OSM-WMS");
            wmsOverlay.ActiveStyleNames.Add("default");
            Map.Overlays.Add(wmsOverlay);

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}