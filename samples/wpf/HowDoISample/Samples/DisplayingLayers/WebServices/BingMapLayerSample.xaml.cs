using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
using System.Diagnostics;
using System;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a Bing Maps Layer on the map
    /// </summary>
    public partial class BingMapLayerSample : UserControl, IDisposable
    {
        public BingMapLayerSample()
        {
            InitializeComponent();
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Set the current extent to the whole world.
            mapView.CurrentExtent = new RectangleShape(-10000000, 10000000, 10000000, -10000000);
        }

        /// <summary>
        /// Add the Bing Maps layer to the map
        /// </summary>
        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            if (txtApplicationID.Text != null && !mapView.Overlays.Contains("Bing Map"))
            {
                btnActivate.IsEnabled = false;
                // Set the map zoom level set to the bing map zoom level set so all the zoom levels line up.
                mapView.ZoomLevelSet = new BingMapsZoomLevelSet(256);

                // Create the layer overlay with some additional settings and add to the map.
                LayerOverlay layerOverlay = new LayerOverlay() { TileHeight = 256, TileWidth = 256 };
                layerOverlay.TileSizeMode = TileSizeMode.Small;
                layerOverlay.MaxExtent = MaxExtents.BingMaps;
                mapView.Overlays.Add("Bing Map", layerOverlay);

                // Create the bing map layer and add it to the map.
                BingMapsLayer bingMapsLayer = new BingMapsLayer(txtApplicationID.Text, BingMapsMapType.Road, "C:\\temp");
                layerOverlay.Layers.Add(bingMapsLayer);

                // Refresh the map.
                mapView.Refresh();
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
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
