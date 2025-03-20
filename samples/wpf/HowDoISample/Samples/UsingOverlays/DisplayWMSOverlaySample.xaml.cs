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
            try
            {
                // Set the map's unit of measurement to meters(Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

                // Add a simple background overlay
                MapView.BackgroundOverlay.BackgroundBrush = GeoBrushes.AliceBlue;

                // Set the map extent
                MapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

                // Create a WmsOverlay and add it to the map.
                var wmsOverlay = new WmsOverlay(new Uri("http://ows.mundialis.de/services/service"));
                wmsOverlay.AxisOrder = WmsAxisOrder.XY;
                wmsOverlay.Crs = "EPSG:3857"; // Make sure to match the WMS CRS to the Map's projection
                wmsOverlay.ActiveLayerNames.Add("OSM-WMS");
                wmsOverlay.ActiveStyleNames.Add("default");
                MapView.Overlays.Add(wmsOverlay);

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
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