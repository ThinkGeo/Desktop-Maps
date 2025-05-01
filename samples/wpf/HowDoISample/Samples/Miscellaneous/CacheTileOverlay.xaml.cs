using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for RasterMBTiles.xaml
    /// </summary>
    public partial class CacheTileOverlay : IDisposable
    {
        private LayerOverlay _layerOverlay;
        private RectangleShape _bbox;
        private int _finishedTileCount = 0;

        public CacheTileOverlay()
        {
            InitializeComponent();
            ThinkGeoDebugger.DisplayTileId = true;
            DataContext = this;
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;

            var streetsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Streets.shp");
            streetsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Black, 2, true);
            streetsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _layerOverlay = new LayerOverlay();
            _layerOverlay.Layers.Add(streetsLayer);
            MapView.Overlays.Add(_layerOverlay);

            string cachePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache");
            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);

            _layerOverlay.TileCache = new FileRasterTileCache(cachePath, "overlayCacheTest");
            _layerOverlay.IsCacheOnly = true; // so it will not render but only get the tiles from the cache.

            streetsLayer.Open();
            _bbox = streetsLayer.GetBoundingBox();
            MapView.CurrentExtent = _bbox;

            _layerOverlay.TileMatrixSet = TileMatrixSet.CreateTileMatrixSet(512, _bbox, GeographyUnit.Meter);
            MapView.ZoomScales = _layerOverlay.TileMatrixSet.GetScales();
            _layerOverlay.TileCacheGenerated += _layerOverlay_TileCacheGenerated;

            _ = MapView.RefreshAsync();
        }

        private void _layerOverlay_TileCacheGenerated(object sender, TileCacheGeneratedLayerOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                _finishedTileCount++;
                MyProgressBar.Maximum = e.TotalTileCount;
                MyProgressBar.Value = _finishedTileCount;
                LblStatus.Content = $"{_finishedTileCount} / {e.TotalTileCount}";
            });
        }


        private void ckbCacheOnly_OnChecked(object sender, RoutedEventArgs e)
        {
            if (_layerOverlay == null)
                return;

            var checkBox = (CheckBox)sender;
            if (checkBox.IsChecked != null)
                _layerOverlay.IsCacheOnly = checkBox.IsChecked.Value;

            _ = MapView.RefreshAsync();
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

                // get the ScaleFactor
                var dpiInfo = VisualTreeHelper.GetDpi(this);
                var scaleFactor = (float)dpiInfo.DpiScaleX;

                // generate the cache for the current and next 2 zooms. 
                var zoom = _layerOverlay.TileMatrixSet.GetSnappedZoomIndex(MapView.CurrentScale);
                await _layerOverlay.GenerateTileCacheAsync(_bbox, zoom, zoom + 2, scaleFactor);

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
            _layerOverlay.TileCache.ClearCache();
            _ = MapView.RefreshAsync();
        }

        private void btnOpenCacheFolder_OnClick(object sender, RoutedEventArgs e)
        {
            var fileRasterTileCache = _layerOverlay.TileCache as FileRasterTileCache;
            if (fileRasterTileCache == null)
                return;

            var cacheFolder = Path.Combine(fileRasterTileCache.CacheDirectory, fileRasterTileCache.CacheId);
            if (Directory.Exists(cacheFolder))
                Process.Start("explorer.exe", cacheFolder);
            else
                MessageBox.Show("Cache Tiles have not been generated");
        }

        private void btnToggleMatrix_OnClick(object sender, RoutedEventArgs e)
        {
            _layerOverlay.TileCache.ClearCache();

            var button = sender as Button;
            if (button == null)
                return;

            if (button.Content == "Switch to Local Tile Matrix")
            {
                button.Content = "Switch to Global Tile Matrix";
                _layerOverlay.TileMatrixSet = TileMatrixSet.CreateTileMatrixSet(512, _bbox, GeographyUnit.Meter);
                MapView.ZoomScales = _layerOverlay.TileMatrixSet.GetScales();

            }
            else
            {
                button.Content = "Switch to Local Tile Matrix";
                _layerOverlay.TileMatrixSet = TileMatrixSet.CreateTileMatrixSet(512, MaxExtents.SphericalMercator, GeographyUnit.Meter);
                MapView.ZoomScales = _layerOverlay.TileMatrixSet.GetScales();
            }
            _ = MapView.RefreshAsync();
        }

        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            MapView.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
