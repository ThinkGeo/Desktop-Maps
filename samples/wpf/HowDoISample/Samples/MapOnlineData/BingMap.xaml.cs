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
            MapView.CurrentExtent = new RectangleShape(-10000000, 10000000, 10000000, -10000000);
        }

        /// <summary>
        /// Add the Bing Maps layer to the map
        /// </summary>
        private async void BtnActivate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtApplicationId.Text) || MapView.Overlays.Contains("Bing Map")) return;
                BtnActivate.IsEnabled = false;
                // Set the map zoom level set to the bing map zoom level set so all the zoom levels line up.
                MapView.ZoomLevelSet = new BingMapsZoomLevelSet();

                // Create the layer overlay with some additional settings and add to the map.
                var layerOverlay = new LayerOverlay
                {
                    TileHeight = 256,
                    TileWidth = 256,
                    TileSizeMode = TileSizeMode.Small
                };
                MapView.Overlays.Add("Bing Map", layerOverlay);

                // Create the bing map layer and add it to the map.                
                var bingMapsLayer = new Core.BingMapsAsyncLayer(TxtApplicationId.Text, BingMapsMapType.Road)
                {
                    TileCache = new FileRasterTileCache("C:\\temp", "bingMapsRoad")
                };
                layerOverlay.Layers.Add(bingMapsLayer);

                // Refresh the map.
                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
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