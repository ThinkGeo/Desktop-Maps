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

        private bool _initialized;
        private OpenStreetMapAsyncLayer _openStreetMapAsyncLayer;
        private int _finishedTileCount;
        private string _cachePath;

        public CacheXyzTiles()
        {
            InitializeComponent();
            ThinkGeoDebugger.DisplayTileId = true;
            DataContext = this;
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            Map.Overlays.Add(layerOverlay);

            // Add Cloud Maps as a background overlay
            _openStreetMapAsyncLayer = new OpenStreetMapAsyncLayer();
            _openStreetMapAsyncLayer.IsCacheOnly = true;
            layerOverlay.Layers.Add(_openStreetMapAsyncLayer);

            _cachePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache");

            if (!Directory.Exists(_cachePath))
                Directory.CreateDirectory(_cachePath);

            _openStreetMapAsyncLayer.TileCache = new FileRasterTileCache(_cachePath, "xyzLayerCacheTest");
            _openStreetMapAsyncLayer.TileCacheGenerated += OpenStreetMapAsyncLayerOnTileCacheGenerated;

            Map.CurrentExtent = MaxExtents.OsmMaps;
            _ = Map.RefreshAsync();
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
            _openStreetMapAsyncLayer.TileCache.ClearCache();

            try
            {
                MyProgressBar.Visibility = Visibility.Visible;
                MyProgressBar.Value = 0;
                LblStatus.Visibility = Visibility.Visible;
                LblStatus.Content = "";
                _finishedTileCount = 0;

                int minZoom = int.Parse(MinZoomTextBox.Text);
                int maxZoom = int.Parse(MaxZoomTextBox.Text);

                var northAmericaExtent = new RectangleShape(-20030000, 20030000, 0, 0);
                await _openStreetMapAsyncLayer.GenerateTileCacheAsync(northAmericaExtent, minZoom, maxZoom);

                MyProgressBar.Visibility = Visibility.Hidden;
                LblStatus.Visibility = Visibility.Hidden;

                await Map.RefreshAsync();
            }
            catch
            {
                // deal with the exception if needed.
            }
        }

        private void BtnClearCache_OnClick(object sender, RoutedEventArgs e)
        {
            _openStreetMapAsyncLayer.TileCache.ClearCache();
            _ = Map.RefreshAsync();
        }

        private void ckbCacheOnly_OnChecked(object sender, RoutedEventArgs e)
        {
            if (_openStreetMapAsyncLayer == null)
                return;
            var checkBox = (CheckBox)sender;
            if (checkBox.IsChecked != null)
                _openStreetMapAsyncLayer.IsCacheOnly = checkBox.IsChecked.Value;
            _ = Map.RefreshAsync();
        }

        private void btnOpenCacheFolder_OnClick(object sender, RoutedEventArgs e)
        {
            var fileRasterTileCache = _openStreetMapAsyncLayer.TileCache as FileRasterTileCache;
            if (fileRasterTileCache == null)
                return;

            var cacheFolder = Path.Combine(fileRasterTileCache.CacheDirectory, fileRasterTileCache.CacheId);
            if (Directory.Exists(cacheFolder))
                Process.Start("explorer.exe", cacheFolder);
            else
                MessageBox.Show("Cache Tiles have not been generated");
        }
        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
