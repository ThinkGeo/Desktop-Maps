using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Generate a tile cache ahead of time, from your own layers or from a tile service, and draw the map only from that cache.
    /// </summary>
    public partial class PreGenerateTileCache : IDisposable
    {
        private const int MinZoom = 10;
        private const int MaxZoom = 14;

        // The user's own folder, not the application's: an installed application's folder is usually read-only.
        private static readonly string CacheFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ThinkGeo", "HowDoI", "TileCache");

        private bool _initialized;
        private RectangleShape _extent;
        private LayerOverlay _streetsOverlay;
        private LayerOverlay _cloudOverlay;
        private ThinkGeoRasterMapsAsyncLayer _cloudLayer;
        private CancellationTokenSource _cancel;
        private int _cloudTilesDone;

        public PreGenerateTileCache()
        {
            InitializeComponent();
            CacheFolderText.Text = CacheFolder;
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            var cityLimits = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "FriscoCityLimits.shp"));
            cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            cityLimits.Open();
            _extent = cityLimits.GetBoundingBox();
            cityLimits.Close();

            var streetsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Streets.shp"));
            streetsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            streetsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Black, 2, true);
            streetsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // IsCacheOnly: the overlay never draws its layers, it only reads the tiles in its cache.
            _streetsOverlay = new LayerOverlay
            {
                TileMatrixSet = TileMatrixSet.CreateTileMatrixSet(256, MaxExtents.SphericalMercator, GeographyUnit.Meter),
                TileCache = new FileRasterTileCache(CacheFolder, "FriscoStreets"),
                IsCacheOnly = true
            };
            _streetsOverlay.Layers.Add(streetsLayer);

            _cloudLayer = new ThinkGeoRasterMapsAsyncLayer
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1,
                TileCache = new FileRasterTileCache(CacheFolder, "ThinkGeoCloudLight"),
                IsCacheOnly = true
            };
            _cloudLayer.TileCacheGenerated += CloudLayer_TileCacheGenerated;
            _cloudOverlay = new LayerOverlay();
            _cloudOverlay.Layers.Add(_cloudLayer);

            // Zoom only to the levels a cache can hold.
            Map.ZoomScales = _streetsOverlay.TileMatrixSet.GetScales();
            Map.CurrentExtent = _extent;
            ShowChosen();
        }

        private void Source_Checked(object sender, RoutedEventArgs e)
        {
            if (_initialized) ShowChosen();
        }

        private void ShowChosen()
        {
            var cache = (FileRasterTileCache)(LocalLayers.IsChecked == true ? _streetsOverlay.TileCache : _cloudLayer.TileCache);
            Map.Overlays.Clear();
            Map.Overlays.Add(LocalLayers.IsChecked == true ? _streetsOverlay : _cloudOverlay);
            Status.Text = Directory.Exists(Path.Combine(cache.CacheDirectory, cache.CacheId))
                ? "Showing the tiles generated last time."
                : "No tiles yet - generate them.";
            _ = Map.RefreshAsync();
        }

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateButton.IsEnabled = false;
            CancelButton.IsEnabled = true;
            GenerateProgress.Value = 0;
            Status.Text = "Generating...";
            _cancel = new CancellationTokenSource();

            try
            {
                if (LocalLayers.IsChecked == true)
                {
                    // Draw the tiles at the screen's DPI, the way the overlay draws them.
                    var scaleFactor = (float)VisualTreeHelper.GetDpi(this).DpiScaleX;
                    await Layer.GenerateTileCacheAsync(_streetsOverlay.Layers, (FileRasterTileCache)_streetsOverlay.TileCache, _streetsOverlay.TileMatrixSet,
                        _extent, GeographyUnit.Meter, MinZoom, MaxZoom,
                        progress => Dispatcher.BeginInvoke(new Action(() => ShowProgress(progress.TilesCompleted, progress.TotalTileCount))),
                        scaleFactor, OverwriteMode.Overwrite, cancellationToken: _cancel.Token);
                }
                else
                {
                    _cloudTilesDone = 0;
                    await _cloudLayer.GenerateTileCacheAsync(_extent, MinZoom, MaxZoom, overwriteMode: OverwriteMode.Overwrite, cancellationToken: _cancel.Token);
                }

                Status.Text = "Done. The map now draws from the cache.";
            }
            catch (OperationCanceledException)
            {
                Status.Text = "Cancelled. The map draws the tiles generated so far.";
            }
            catch (Exception exception)
            {
                Status.Text = "Stopped: " + exception.Message;
            }

            GenerateButton.IsEnabled = true;
            CancelButton.IsEnabled = false;
            _ = Map.RefreshAsync();
        }

        private void CloudLayer_TileCacheGenerated(object sender, TileCacheGeneratedXyzTileAsyncLayerEventArgs e)
        {
            var done = Interlocked.Increment(ref _cloudTilesDone);
            Dispatcher.BeginInvoke(new Action(() => ShowProgress(done, e.TotalTileCount)));
        }

        private void ShowProgress(int done, int total)
        {
            GenerateProgress.Maximum = total;
            GenerateProgress.Value = done;
            Status.Text = done + " of " + total + " tiles generated";
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _cancel?.Cancel();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            var cache = LocalLayers.IsChecked == true ? _streetsOverlay.TileCache : _cloudLayer.TileCache;
            cache.ClearCache();
            Status.Text = "Cache cleared.";
            _ = Map.RefreshAsync();
        }

        private void OpenFolderButton_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(CacheFolder))
                Process.Start("explorer.exe", CacheFolder);
            else
                Status.Text = "Nothing generated yet.";
        }

        public void Dispose()
        {
            _cancel?.Cancel();
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
