using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
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

            _openStreetMapAsyncLayer.TileCache = new FileRasterTileCache(cachePath, "cacheTest");

            MapView.CurrentExtent = MaxExtents.OsmMaps;
            MapView.ZoomScales = new OpenStreetMapsZoomLevelSet().GetScales();
            _ = MapView.RefreshAsync();
        }

        private async void BtnGenerateCache_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var northAmericaExtent = new RectangleShape(-20030000, 20030000, 0, 0);
                await _openStreetMapAsyncLayer.GenerateTileCache(northAmericaExtent, 0, 4);

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

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (_openStreetMapAsyncLayer == null)
                return;
            var checkBox = (CheckBox)sender;
            if (checkBox.IsChecked != null) 
                _openStreetMapAsyncLayer.IsCacheOnly = checkBox.IsChecked.Value;
            _ = MapView.RefreshAsync();
        }

        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            MapView.Dispose();
            GC.SuppressFinalize(this);
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
    }
}
