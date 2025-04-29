using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a WMTS Layer on the map
    /// </summary>
    public partial class WMTSAsXYZLayer : IDisposable
    {
        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        private WmtsAsyncLayer wmtsAsyncLayer;
        private int _logIndex = 0;

        public WMTSAsXYZLayer()
        {
            InitializeComponent();

            DataContext = this;
        }

        /// <summary>
        /// Add the WMTS layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var layerOverlay = new LayerOverlay();
                MapView.Overlays.Add(layerOverlay);
                wmtsAsyncLayer = new WmtsAsyncLayer(new Uri("https://wmts.geo.admin.ch/1.0.0"));
                wmtsAsyncLayer.DrawingExceptionMode = DrawingExceptionMode.DrawException;
                wmtsAsyncLayer.ActiveLayerName = "ch.swisstopo.pixelkarte-farbe-pk25.noscale";
                wmtsAsyncLayer.ActiveStyleName = "default";
                wmtsAsyncLayer.OutputFormat = "image/png";
                wmtsAsyncLayer.TileMatrixSetName = "21781_26";

                layerOverlay.TileType = TileType.SingleTile;
                layerOverlay.Layers.Add(wmtsAsyncLayer);

                string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "wmts_async_layer");

                if (!System.IO.Directory.Exists(cachePath))
                {
                    System.IO.Directory.CreateDirectory(cachePath);
                }

                ThinkGeoDebugger.DisplayTileId = true;

                wmtsAsyncLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
                wmtsAsyncLayer.ProjectedTileCache = new FileRasterTileCache(cachePath, "projected");

                wmtsAsyncLayer.TileCache.GottenTile += TileCache_GottenCacheTile;
                wmtsAsyncLayer.ProjectedTileCache.GottenTile += ProjectedTileCache_GottenCacheTile;

                await wmtsAsyncLayer.OpenAsync();
                // Create a zoomlevelSet from the WMTS server
                MapView.ZoomScales = GetZoomScalesFromWmtsServer();

                var wmtsAsyncLayerBBox = wmtsAsyncLayer.GetBoundingBox();
                MapView.CenterPoint = wmtsAsyncLayerBBox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, wmtsAsyncLayerBBox, MapView.MapWidth, MapView.MapHeight);
                await MapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private Collection<double> GetZoomScalesFromWmtsServer()
        {
            var matrices = wmtsAsyncLayer.GetTileMatrixSets()[wmtsAsyncLayer.TileMatrixSetName].TileMatrices;

            var scales = new Collection<double>();
            foreach (var matrix in matrices)
            {
                scales.Add(matrix.Scale);
            }

            return scales;
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
                if (wmtsAsyncLayer == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton?.Tag == null) return;

                switch (radioButton.Tag.ToString())
                {
                    case "21781":
                        MapView.MapUnit = GeographyUnit.Meter;
                        wmtsAsyncLayer.ProjectionConverter = null;
                        break;

                    case "4326":
                        MapView.MapUnit = GeographyUnit.DecimalDegree;
                        var currentCrs = wmtsAsyncLayer.GetTileMatrixSets()[wmtsAsyncLayer.TileMatrixSetName].SupportedCrs;
                        wmtsAsyncLayer.ProjectionConverter = new GdalProjectionConverter(currentCrs, 4326);
                        break;

                    default:
                        return;
                }

                await wmtsAsyncLayer.CloseAsync();
                await wmtsAsyncLayer.OpenAsync();
                var wmtsAsyncLayerBBox = wmtsAsyncLayer.GetBoundingBox();
                MapView.CenterPoint = wmtsAsyncLayerBBox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, wmtsAsyncLayerBBox, MapView.MapWidth, MapView.MapHeight);
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
                wmtsAsyncLayer.RenderBeyondMaxZoom = checkBox.IsChecked.Value;

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