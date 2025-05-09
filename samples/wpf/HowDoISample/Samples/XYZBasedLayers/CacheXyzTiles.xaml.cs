using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for RasterMBTiles.xaml
    /// </summary>
    public partial class CacheXyzTiles : IDisposable
    {
        private OpenStreetMapAsyncLayer _openStreetMapAsyncLayer;
        private int _finishedTileCount;

        public CacheXyzTiles()
        {
            InitializeComponent();
            ThinkGeoDebugger.DisplayTileId = true;
            DataContext = this;
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            MapView.Overlays.Add(layerOverlay);

            // Add Cloud Maps as a background overlay
            _openStreetMapAsyncLayer = new OpenStreetMapAsyncLayer();
            _openStreetMapAsyncLayer.IsCacheOnly = true;
            layerOverlay.Layers.Add(_openStreetMapAsyncLayer);

            string cachePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache");

            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);

            _openStreetMapAsyncLayer.TileCache = new FileRasterTileCache(cachePath, "xyzLayerCacheTest");
            _openStreetMapAsyncLayer.TileCacheGenerated += OpenStreetMapAsyncLayerOnTileCacheGenerated;

            MapView.CurrentExtent = MaxExtents.OsmMaps;
            _ = MapView.RefreshAsync();
        }

        private void OpenStreetMapAsyncLayerOnTileCacheGenerated(object sender,
            TileCacheGeneratedXyzTileAsyncLayerEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                _finishedTileCount++;
                MyProgressBar.Maximum = e.TotalTileCount;
                MyProgressBar.Value = _finishedTileCount;
                LblStatus.Content = $"{_finishedTileCount} / {e.TotalTileCount}";
            });
        }

        private async void BtnGenerateCache_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MyProgressBar.Visibility = Visibility.Visible;
                MyProgressBar.Value = 0;
                LblStatus.Visibility = Visibility.Visible;
                LblStatus.Content = "";
                _finishedTileCount = 0;

                var northAmericaExtent = new RectangleShape(-20030000, 20030000, 0, 0);
                await _openStreetMapAsyncLayer.GenerateTileCacheAsync(northAmericaExtent, 0, 4);

                MyProgressBar.Visibility = Visibility.Hidden;
                LblStatus.Visibility = Visibility.Hidden;

                await MapView.RefreshAsync();
            }
            catch
            {
                // deal with the exception if needed.
            }
        }

        private void BtnClearCache_OnClick(object sender, RoutedEventArgs e)
        {
            _openStreetMapAsyncLayer.TileCache.ClearCache();
            _ = MapView.RefreshAsync();
        }

        private void ckbCacheOnly_OnChecked(object sender, RoutedEventArgs e)
        {
            if (_openStreetMapAsyncLayer == null)
                return;
            var checkBox = (CheckBox)sender;
            if (checkBox.IsChecked != null)
                _openStreetMapAsyncLayer.IsCacheOnly = checkBox.IsChecked.Value;
            _ = MapView.RefreshAsync();
        }

        private void btnOpenCacheFolder_OnClick(object sender, RoutedEventArgs e)
        {
            var fileRasterTileCache = _openStreetMapAsyncLayer.TileCache as FileRasterTileCache;

            if (fileRasterTileCache == null)
                return;

            var cacheFolder = Path.Combine(fileRasterTileCache.CacheDirectory, fileRasterTileCache.CacheId);
            if (Directory.Exists(cacheFolder))
                Process.Start("explorer.exe", cacheFolder);
        }
        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            MapView.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
