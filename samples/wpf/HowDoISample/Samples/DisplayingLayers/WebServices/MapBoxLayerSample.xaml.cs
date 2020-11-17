using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
using System.Diagnostics;
using System;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a MapBox StaticTiles Layer on the map
    /// </summary>
    public partial class MapBoxLayerSample : UserControl, IDisposable
    {
        public MapBoxLayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the MapBox StaticTiles layer to the map
        /// </summary>
        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the layer overlay with some additional settings and add to the map.
            LayerOverlay layerOverlay = new LayerOverlay() { TileHeight = 256, TileWidth = 256 };
            layerOverlay.TileSizeMode = TileSizeMode.Small;
            mapView.Overlays.Add("Bing Map", layerOverlay);

            // Create the MapBox StaticTiles layer and add it to the map.
            MapBoxStaticTilesLayer mapBoxStaticTilesLayer = new MapBoxStaticTilesLayer(txtAccessToken.Text, MapBoxStyleId.Streets);
            layerOverlay.Layers.Add(mapBoxStaticTilesLayer);

            // Set the current extent to the whole world.
            mapView.CurrentExtent = new RectangleShape(-10000000, 10000000, 10000000, -10000000);

            // Refresh the map.
            mapView.Refresh();
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
