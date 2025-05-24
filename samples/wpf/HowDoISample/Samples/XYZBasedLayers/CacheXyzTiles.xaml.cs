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
        private RectangleShape _selectedRectangle;
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
            CacheFolderTextBox.Text = cachePath;

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

        private void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            // Retrieve the drawn rectangle
            var rectangle = e.TrackShape as RectangleShape;

            if (rectangle != null)
                _selectedRectangle = rectangle;

            // Reset the TrackMode to None to stop further drawing
            MapView.TrackOverlay.TrackMode = TrackMode.None;

            MinXTextBox.Text = _selectedRectangle.UpperLeftPoint.X.ToString();  // minX
            MaxYTextBox.Text = _selectedRectangle.UpperLeftPoint.Y.ToString();  // maxY
            MaxXTextBox.Text = _selectedRectangle.LowerRightPoint.X.ToString(); // maxX
            MinYTextBox.Text = _selectedRectangle.LowerRightPoint.Y.ToString(); // minY
        }

        private async void BtnGenerateCache_OnClick(object sender, RoutedEventArgs e)
        {
            _openStreetMapAsyncLayer.TileCache.ClearCache();
            MapView.TrackOverlay.TrackMode = TrackMode.None;
            MapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            MapView.TrackOverlay.TrackEnded -= TrackOverlay_TrackEnded;

            try
            {
                MyProgressBar.Visibility = Visibility.Visible;
                MyProgressBar.Value = 0;
                LblStatus.Visibility = Visibility.Visible;
                LblStatus.Content = "";
                _finishedTileCount = 0;

                int minZoomLevel = int.Parse(MinZoomTextBox.Text);
                int maxZoomLevel = int.Parse(MaxZoomTextBox.Text);

                double minX = double.Parse(MinXTextBox.Text);
                double maxY = double.Parse(MaxYTextBox.Text);
                double maxX = double.Parse(MaxXTextBox.Text);
                double minY = double.Parse(MinYTextBox.Text);

                _selectedRectangle = new RectangleShape(minX,maxY,maxX,minY);

                // Override cache folder before generating
                string cacheFolder = CacheFolderTextBox.Text.Trim();
                if (!Directory.Exists(cacheFolder))
                {
                    Directory.CreateDirectory(cacheFolder);
                }

                // Reassign TileCache with custom path
                _openStreetMapAsyncLayer.TileCache = new FileRasterTileCache(cacheFolder, "xyzLayerCacheTest");

                // Optional: clear old cache files before regenerating
                _openStreetMapAsyncLayer.TileCache.ClearCache();

                await _openStreetMapAsyncLayer.GenerateTileCacheAsync(_selectedRectangle, minZoomLevel, maxZoomLevel);

                PointShape center = _selectedRectangle.GetCenterPoint();
                double zoomLevel0Scale = MapView.ZoomScales[0]; 

                MapView.CenterPoint = center;
                MapView.CurrentScale = zoomLevel0Scale;

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
