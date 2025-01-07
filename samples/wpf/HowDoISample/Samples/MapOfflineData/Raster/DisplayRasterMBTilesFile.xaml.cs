using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for DisplayRasterMBTilesFile.xaml
    /// </summary>
    public partial class DisplayRasterMBTilesFile : IDisposable
    {
        private RasterMbTilesAsyncLayer rasterMbTilesLayer;

        public DisplayRasterMBTilesFile()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            var layerOverlay = new LayerOverlay();
            MapView.Overlays.Add(layerOverlay);
            rasterMbTilesLayer = new RasterMbTilesAsyncLayer(@".\Data\Mbtiles\test.mbtiles");
            layerOverlay.TileType = TileType.SingleTile;
            layerOverlay.Layers.Add(rasterMbTilesLayer);

            string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rasterMbTilesLayerCache");

            if (!System.IO.Directory.Exists(cachePath))
            {
                System.IO.Directory.CreateDirectory(cachePath);
            }

            rasterMbTilesLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
            rasterMbTilesLayer.ProjectedTileCache = new FileRasterTileCache(cachePath, "projected")
                { EnableDebugInfo = true};

            rasterMbTilesLayer.TileCache.GottenCacheTile += TileCache_GottenCacheTile;
            rasterMbTilesLayer.ProjectedTileCache.GottenCacheTile += ProjectedTileCache_GottenCacheTile;

            layerOverlay.Drawn += LayerOverlayOnDrawn;
            await MapView.RefreshAsync();

        }

        private void LayerOverlayOnDrawn(object sender, DrawnOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                LogTextBox.ScrollToEnd();
            });
        }

        private void ProjectedTileCache_GottenCacheTile(object sender, GottenCacheImageBitmapTileCacheEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = e.Tile.Content == null ? "Projection Cache Not Hit:" : "Projection Cache Hit:";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.Column}-{e.Tile.Row}";

                LogTextBox.Text += message + Environment.NewLine;
            });
        }

        private void TileCache_GottenCacheTile(object sender, GottenCacheImageBitmapTileCacheEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = e.Tile.Content == null ? "Cache Not Hit:" : "Cache Hit:";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.Column}-{e.Tile.Row}";

                LogTextBox.Text += message + Environment.NewLine;
            });
        }


        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            if (rasterMbTilesLayer == null) return;

            var radioButton = sender as RadioButton;
            if (radioButton?.Tag == null) return;

            switch (radioButton.Tag.ToString())
            {
                case "3857":
                    MapView.MapUnit = GeographyUnit.Meter;
                    rasterMbTilesLayer.ProjectionConverter = null;
                    break;

                case "4326":
                    MapView.MapUnit = GeographyUnit.DecimalDegree;
                    rasterMbTilesLayer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);
                    break;

                default:
                    return;
            }

            await rasterMbTilesLayer.CloseAsync();
            await rasterMbTilesLayer.OpenAsync();
            MapView.CurrentExtent = rasterMbTilesLayer.GetBoundingBox();
            await MapView.RefreshAsync();
        }

        private async void RenderBeyondMaxZoomCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is CheckBox checkBox))
                return;

            if (checkBox.IsChecked.HasValue)
                rasterMbTilesLayer.RenderBeyondMaxZoom = checkBox.IsChecked.Value;

            await MapView.RefreshAsync();
        }

        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
