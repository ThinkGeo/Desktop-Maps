using System;
using System.Diagnostics;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a Bing Maps Layer on the map
    /// </summary>
    public partial class BingMap : IDisposable
    {
        public BingMap()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.Meter;

            // Set the current extent to the whole world.
            MapView.CenterPoint = new PointShape(0, 0);
            MapView.CurrentScale = 105721100;
        }

        /// <summary>
        /// Add the Bing Maps layer to the map
        /// </summary>
        private void BtnActivate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtApplicationId.Text) || MapView.Overlays.Contains("Bing Map")) return;
            BtnActivate.IsEnabled = false;

            // Create the layer overlay with some additional settings and add to the map.
            var layerOverlay = new LayerOverlay
            {
                TileHeight = 256,
                TileWidth = 256,
            };
            MapView.Overlays.Add("Bing Map", layerOverlay);

            // Create the bing map layer and add it to the map.                
            var bingMapsLayer = new Core.BingMapsAsyncLayer(TxtApplicationId.Text, BingMapsMapType.Road)
            {
                TileCache = new FileRasterTileCache("C:\\temp", "bingMapsRoad")
            };
            layerOverlay.Layers.Add(bingMapsLayer);

            // Refresh the map.
            _ = MapView.RefreshAsync();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
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