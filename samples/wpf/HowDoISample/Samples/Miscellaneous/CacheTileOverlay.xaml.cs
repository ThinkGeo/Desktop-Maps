using System;
using System.Diagnostics;
using System.IO;
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
            CacheFolderTextBox.Text = cachePath;

            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);

            _layerOverlay.TileCache = new FileRasterTileCache(cachePath, "overlayCacheTest");
            _layerOverlay.IsCacheOnly = true; // so it will not render but only get the tiles from the cache.

            streetsLayer.Open();
            _bbox = streetsLayer.GetBoundingBox();

            MinXTextBox.Text = _bbox.UpperLeftPoint.X.ToString();  // minX
            MaxYTextBox.Text = _bbox.UpperLeftPoint.Y.ToString();  // maxY
            MaxXTextBox.Text = _bbox.LowerRightPoint.X.ToString(); // maxX
            MinYTextBox.Text = _bbox.LowerRightPoint.Y.ToString(); // minY

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

        private void SelectAreaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Enable rectangle drawing mode
            MapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            MapView.TrackOverlay.TrackMode = TrackMode.Rectangle;
            MapView.TrackOverlay.TrackEnded += TrackOverlay_TrackEnded;
        }

        private void SelectAreaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Disable drawing mode and clear any drawn shapes
            MapView.TrackOverlay.TrackMode = TrackMode.None;
            MapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            MapView.TrackOverlay.TrackEnded -= TrackOverlay_TrackEnded;
        }

        private void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            // Retrieve the drawn rectangle
            var rectangle = e.TrackShape as RectangleShape;

            if (rectangle != null)
                _bbox = rectangle;

            // Reset the TrackMode to None to stop further drawing
            MapView.TrackOverlay.TrackMode = TrackMode.None;

            MinXTextBox.Text = _bbox.UpperLeftPoint.X.ToString();  // minX
            MaxYTextBox.Text = _bbox.UpperLeftPoint.Y.ToString();  // maxY
            MaxXTextBox.Text = _bbox.LowerRightPoint.X.ToString(); // maxX
            MinYTextBox.Text = _bbox.LowerRightPoint.Y.ToString(); // minY
        }

        private void BrowseCacheFolder_OnClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.Description = "Select Cache Folder";
                dialog.SelectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache");

                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    CacheFolderTextBox.Text = dialog.SelectedPath;
                }
            }
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

                // Override cache folder before generating
                string cacheFolder = CacheFolderTextBox.Text.Trim();
                if (!Directory.Exists(cacheFolder))
                {
                    Directory.CreateDirectory(cacheFolder);
                }

                // Reassign TileCache with custom path
                _layerOverlay.TileCache = new FileRasterTileCache(cacheFolder, "overlayCacheTest");

                // Optional: clear old cache files before regenerating
                _layerOverlay.TileCache.ClearCache();

                // get the ScaleFactor
                var dpiInfo = VisualTreeHelper.GetDpi(this);
                var scaleFactor = (float)dpiInfo.DpiScaleX;

                // generate the cache from minZoomLevel to maxZoomLevel. 
                int minZoomLevel = int.Parse(MinZoomTextBox.Text.Trim());
                int maxZoomLevel = int.Parse(MaxZoomTextBox.Text.Trim());

                double minX = double.Parse(MinXTextBox.Text);
                double maxY = double.Parse(MaxYTextBox.Text);
                double maxX = double.Parse(MaxXTextBox.Text);
                double minY = double.Parse(MinYTextBox.Text);
                
                _bbox = new RectangleShape(minX, maxY, maxX, minY);

                await _layerOverlay.GenerateTileCacheAsync(_bbox, minZoomLevel, maxZoomLevel, scaleFactor);

                MyProgressBar.Visibility = Visibility.Hidden;
                LblStatus.Visibility = Visibility.Hidden;

                int targetZoomLevel = minZoomLevel;
                double newScale = _layerOverlay.TileMatrixSet.GetScales()[targetZoomLevel];

                MapView.CurrentScale = newScale;
                MapView.CenterPoint = _bbox.GetCenterPoint();

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

            if ((string)button.Content == "Switch to Local Tile Matrix")
            {
                button.Content = "Switch to Global Tile Matrix";
                _layerOverlay.TileMatrixSet = TileMatrixSet.CreateTileMatrixSet(512, _bbox, GeographyUnit.Meter, 10);
                MapView.ZoomScales = _layerOverlay.TileMatrixSet.GetScales();

                var zoom = _layerOverlay.TileMatrixSet.GetSnappedZoomIndex(MapView.CurrentScale);
                MinZoomTextBox.Text = zoom.ToString();
                MaxZoomTextBox.Text = (int.Parse(zoom.ToString()) + 3).ToString();
                NoteTextBlock.Text = "Note: Min Zoom should be < Max Zoom. Valid range: 0–9.";
            }
            else
            {
                button.Content = "Switch to Local Tile Matrix";
                _layerOverlay.TileMatrixSet = TileMatrixSet.CreateTileMatrixSet(512, MaxExtents.SphericalMercator, GeographyUnit.Meter);
                MapView.ZoomScales = _layerOverlay.TileMatrixSet.GetScales();

                var zoom = _layerOverlay.TileMatrixSet.GetSnappedZoomIndex(MapView.CurrentScale);
                MinZoomTextBox.Text = zoom.ToString();
                MaxZoomTextBox.Text = (int.Parse(zoom.ToString())+3).ToString();
                NoteTextBlock.Text = $"Note: Current Zoom Level is {zoom}, Min Zoom should be < Max Zoom. Valid range: 0–19.";
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
