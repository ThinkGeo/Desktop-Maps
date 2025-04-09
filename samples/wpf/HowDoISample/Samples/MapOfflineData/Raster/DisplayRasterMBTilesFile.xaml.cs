using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for DisplayRasterMbTilesFile.xaml
    /// </summary>
    public partial class DisplayRasterMbTilesFile : IDisposable
    {
        // Observable collection to hold log messages.
        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        private RasterMbTilesAsyncLayer rasterMbTilesLayer;
        private int _logIndex = 0;

        public DisplayRasterMbTilesFile()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            var layerOverlay = new LayerOverlay();
            MapView.Overlays.Add(layerOverlay);
            rasterMbTilesLayer = new RasterMbTilesAsyncLayer(@".\Data\Mbtiles\test.mbtiles");
            layerOverlay.TileType = TileType.SingleTile;
            layerOverlay.Layers.Add(rasterMbTilesLayer);

            string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "raster_mb_tiles_layer");

            if (!System.IO.Directory.Exists(cachePath))
            {
                System.IO.Directory.CreateDirectory(cachePath);
            }

            ThinkGeoDebugger.DisplayTileId = true;

            rasterMbTilesLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
            rasterMbTilesLayer.ProjectedTileCache = new FileRasterTileCache(cachePath, "projected");

            rasterMbTilesLayer.TileCache.GottenTile += TileCache_GottenCacheTile;
            rasterMbTilesLayer.ProjectedTileCache.GottenTile += ProjectedTileCache_GottenCacheTile;

            //layerOverlay.Drawn += LayerOverlayOnDrawn;
            MapView.CenterPoint = MaxExtents.ThinkGeoMaps.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, MaxExtents.ThinkGeoMaps, MapView.MapWidth, MapView.MapHeight);
            _ = MapView.RefreshAsync();
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
                var rasterMbTilesLayerBBox = rasterMbTilesLayer.GetBoundingBox();
                MapView.CenterPoint = rasterMbTilesLayerBBox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, rasterMbTilesLayerBBox, MapView.MapWidth, MapView.MapHeight);
                await MapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private void RenderBeyondMaxZoomCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is CheckBox checkBox))
                return;

            if (checkBox.IsChecked.HasValue)
                rasterMbTilesLayer.RenderBeyondMaxZoom = checkBox.IsChecked.Value;

            _ = MapView.RefreshAsync();
        }

        private void DisplayTileIdCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is CheckBox checkBox))
                return;

            if (!checkBox.IsChecked.HasValue)
                return;

            if (ThinkGeoDebugger.DisplayTileId != checkBox.IsChecked.Value)
            {
                ThinkGeoDebugger.DisplayTileId = checkBox.IsChecked.Value;
                _ = MapView.RefreshAsync();
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
            MapView.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
