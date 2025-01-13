using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for RasterMBTiles.xaml
    /// </summary>
    public partial class RasterMBTiles : IDisposable
    {
        private ThinkGeoRasterMapsAsyncLayer rasterMBTilesOnlineLayer;
        public RasterMBTiles()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var layerOverlay = new LayerOverlay();
                MapView.Overlays.Add(layerOverlay);

                // Add Cloud Maps as a background overlay
                rasterMBTilesOnlineLayer = new ThinkGeoRasterMapsAsyncLayer
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudRasterMapsMapType.Light_V1_X1,
                };

                layerOverlay.TileType = TileType.SingleTile;
                layerOverlay.Layers.Add(rasterMBTilesOnlineLayer);

                string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rasterMbTilesOnlineLayerCache");

                if (!System.IO.Directory.Exists(cachePath))
                {
                    System.IO.Directory.CreateDirectory(cachePath);
                }                

                rasterMBTilesOnlineLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
                rasterMBTilesOnlineLayer.ProjectedTileCache = new FileRasterTileCache(cachePath, "projected")
                { EnableDebugInfo = true };

                rasterMBTilesOnlineLayer.TileCache.GottenCacheTile += TileCache_GottenCacheTile;
                rasterMBTilesOnlineLayer.ProjectedTileCache.GottenCacheTile += ProjectedTileCache_GottenCacheTile;

                layerOverlay.Drawn += LayerOverlayOnDrawn;
                await MapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
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
            try
            {
                if (rasterMBTilesOnlineLayer == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton?.Tag == null) return;

                switch (radioButton.Tag.ToString())
                {
                    case "3857":
                        MapView.MapUnit = GeographyUnit.Meter;
                        rasterMBTilesOnlineLayer.ProjectionConverter = null;
                        break;

                    case "4326":
                        MapView.MapUnit = GeographyUnit.DecimalDegree;
                        rasterMBTilesOnlineLayer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);
                        break;

                    default:
                        return;
                }

                await rasterMBTilesOnlineLayer.CloseAsync();
                await rasterMBTilesOnlineLayer.OpenAsync();
                MapView.CurrentExtent = rasterMBTilesOnlineLayer.GetBoundingBox();
                await MapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private async void RenderBeyondMaxZoomCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(sender is CheckBox checkBox))
                    return;

                if (checkBox.IsChecked.HasValue)
                    rasterMBTilesOnlineLayer.RenderBeyondMaxZoom = checkBox.IsChecked.Value;

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
            ThinkGeoDebugger.DisplayTileId = false;
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
