using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
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
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private readonly string defaultLayerName = "LINZ";
        private readonly Dictionary<string, RectangleShape> bBoxDict = new Dictionary<string, RectangleShape>();

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
                wmtsAsyncLayer = new Core.WmtsAsyncLayer(new Uri("https://basemaps.linz.govt.nz/v1/tiles/aerial/NZTM2000Quad/WMTSCapabilities.xml?api=c01j20m6pmjhc81bn55sakayftb"));
                wmtsAsyncLayer.DrawingExceptionMode = DrawingExceptionMode.DrawException;
                wmtsAsyncLayer.CapabilitiesCacheTimeout = new TimeSpan(0, 0, 0, 1);
                wmtsAsyncLayer.ActiveLayerName = "aerial";
                wmtsAsyncLayer.ActiveStyleName = "default";
                wmtsAsyncLayer.OutputFormat = "image/png";
                wmtsAsyncLayer.TileMatrixSetName = "NZTM2000Quad";

                string layerName = "LINZ";
                layerOverlay.TileType = TileType.SingleTile;
                layerOverlay.Layers.Add(layerName, wmtsAsyncLayer);

                string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wmtsAsyncLayerCache");

                if (!System.IO.Directory.Exists(cachePath))
                {
                    System.IO.Directory.CreateDirectory(cachePath);
                }

                ThinkGeoDebugger.DisplayTileId = true;

                wmtsAsyncLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
                wmtsAsyncLayer.ProjectedTileCache = new FileRasterTileCache(cachePath, "projected");

                wmtsAsyncLayer.TileCache.GottenTile += TileCache_GottenCacheTile;
                wmtsAsyncLayer.ProjectedTileCache.GottenTile += ProjectedTileCache_GottenCacheTile;

                UpdateCancellationToken();
                //MapView.CurrentExtent = new RectangleShape(14303497.448365476, -7610740.842272079, 16022006.68392926, -9080257.632067444);
                //await MapView.RefreshAsync(OverlayRefreshType.Redraw, cancellationTokenSource.Token);

                //var fullExtent = wmtsAsyncLayer.GetBoundingBox();
                await wmtsAsyncLayer.CloseAsync();
                await wmtsAsyncLayer.OpenAsync();
                MapView.CurrentExtent = wmtsAsyncLayer.GetBoundingBox();
                await MapView.ZoomInAsync();
                await MapView.CenterAtAsync(MapView.CurrentExtent.GetCenterPoint());
                await MapView.RefreshAsync(OverlayRefreshType.Redraw, cancellationTokenSource.Token);
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private void UpdateCancellationToken()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = new CancellationTokenSource();
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
                    case "3857":
                        MapView.MapUnit = GeographyUnit.Meter;
                        wmtsAsyncLayer.ProjectionConverter = null;
                        break;

                    case "2193":
                        MapView.MapUnit = GeographyUnit.Meter;
                        wmtsAsyncLayer.ProjectionConverter = new GdalProjectionConverter(3857, 2193);
                        break;

                    default:
                        return;
                }

                await wmtsAsyncLayer.CloseAsync();
                await wmtsAsyncLayer.OpenAsync();
                MapView.CurrentExtent = wmtsAsyncLayer.GetBoundingBox();
                await MapView.RefreshAsync(OverlayRefreshType.Redraw, cancellationTokenSource.Token);
                //await MapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private async void RenderBeyondMaxZoomCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(sender is CheckBox checkBox))
                    return;

                if (checkBox.IsChecked.HasValue)
                    wmtsAsyncLayer.RenderBeyondMaxZoom = checkBox.IsChecked.Value;

                await MapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private async void DisplayTileIdCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(sender is CheckBox checkBox))
                    return;

                if (!checkBox.IsChecked.HasValue)
                    return;

                if (ThinkGeoDebugger.DisplayTileId != checkBox.IsChecked.Value)
                {
                    ThinkGeoDebugger.DisplayTileId = checkBox.IsChecked.Value;
                    await MapView.RefreshAsync();
                }
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
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