using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for DisplayRasterPmTilesFile.xaml
    /// </summary>
    public partial class DisplayRasterPmTilesFile : IDisposable
    {
        // Observable collection to hold log messages.
        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        private RasterPmTilesAsyncLayer rasterPmTilesLayer;
        private int _logIndex = 0;
        private bool _initialized;

        public DisplayRasterPmTilesFile()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            var layerOverlay = new LayerOverlay();
            Map.Overlays.Add(layerOverlay);
            rasterPmTilesLayer = new RasterPmTilesAsyncLayer(@".\Data\Pmtiles\stamen_toner.pmtiles");
            layerOverlay.TileType = TileType.SingleTile;
            layerOverlay.Layers.Add(rasterPmTilesLayer);

            string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "raster_pm_tiles_layer");

            if (!System.IO.Directory.Exists(cachePath))
            {
                System.IO.Directory.CreateDirectory(cachePath);
            }

            rasterPmTilesLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
            rasterPmTilesLayer.ProjectedTileCache = new FileRasterTileCache(cachePath, "projected");

            rasterPmTilesLayer.TileCache.GottenTile += TileCache_GottenCacheTile;
            rasterPmTilesLayer.ProjectedTileCache.GottenTile += ProjectedTileCache_GottenCacheTile;

            //layerOverlay.Drawn += LayerOverlayOnDrawn;
            Map.CenterPoint = MaxExtents.ThinkGeoMaps.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, MaxExtents.ThinkGeoMaps, Map.MapWidth, Map.MapHeight);

            _initialized = true;
            _ = Map.RefreshAsync();
        }

        private void ProjectedTileCache_GottenCacheTile(object sender, GottenTileTileCacheEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = e.Tile.Content == null ? "Projected Tle Not Exist: " : "Projected Tile From Cache: ";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.X}-{e.Tile.Y}";

                AppendLog(message);
            });
        }

        private void TileCache_GottenCacheTile(object sender, GottenTileTileCacheEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = e.Tile.Content == null ? "Tile From Source: " : "Tile From Cache: ";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.X}-{e.Tile.Y}";

                AppendLog(message);
            });
        }


        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rasterPmTilesLayer == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton?.Tag == null) return;

                switch (radioButton.Tag.ToString())
                {
                    case "3857":
                        Map.MapUnit = GeographyUnit.Meter;
                        rasterPmTilesLayer.ProjectionConverter = null;
                        break;

                    case "4326":
                        Map.MapUnit = GeographyUnit.DecimalDegree;
                        rasterPmTilesLayer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);
                        break;

                    default:
                        return;
                }

                await rasterPmTilesLayer.CloseAsync();
                await rasterPmTilesLayer.OpenAsync();
                var rasterPmTilesLayerBBox = rasterPmTilesLayer.GetBoundingBox();
                Map.CenterPoint = rasterPmTilesLayerBBox.GetCenterPoint();
                Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, rasterPmTilesLayerBBox, Map.MapWidth, Map.MapHeight);
                await Map.RefreshAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        private void RenderBeyondMaxZoomCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is CheckBox checkBox))
                return;

            if (checkBox.IsChecked.HasValue)
                rasterPmTilesLayer.RenderBeyondMaxZoom = checkBox.IsChecked.Value;

            _ = Map.RefreshAsync();
        }

        private void DisplayTileIdCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            if (!(sender is CheckBox checkBox))
                return;

            if (!checkBox.IsChecked.HasValue)
                return;

            if (ThinkGeoDebugger.DisplayTileId != checkBox.IsChecked.Value)
            {
                ThinkGeoDebugger.DisplayTileId = checkBox.IsChecked.Value;
                _ = Map.RefreshAsync();
            }
        }

        public void AppendLog(string message)
        {
            // Add log message to the observable collection
            LogMessages.Add($"{_logIndex++}: {message}");
            LogListBox.ScrollIntoView(LogMessages[LogMessages.Count - 1]);
        }

        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
