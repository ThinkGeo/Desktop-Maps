using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for DisplayRasterMbTilesFile.xaml
    /// </summary>
    public partial class DisplayRasterFileTiles : IDisposable
    {
        // Observable collection to hold log messages.
        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        private XyzFileTilesAsyncLayer fileTilesAsyncLayer;
        private int _logIndex = 0;

        public DisplayRasterFileTiles()
        {
            InitializeComponent();

            DataContext = this;
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Directory.Exists(@".\Data\OSM_Tiles_z0-z5_Created_By_QGIS"))
                    ZipFile.ExtractToDirectory(@".\Data\OSM_Tiles_z0-z5_Created_By_QGIS.zip", @".\Data\OSM_Tiles_z0-z5_Created_By_QGIS");

                var layerOverlay = new LayerOverlay();
                MapView.Overlays.Add(layerOverlay);
                fileTilesAsyncLayer = new XyzFileTilesAsyncLayer(@".\Data\OSM_Tiles_z0-z5_Created_By_QGIS");
                fileTilesAsyncLayer.MaxZoomOfTheData = 5; // The MaxZoom with data

                layerOverlay.TileType = TileType.SingleTile;
                layerOverlay.Layers.Add(fileTilesAsyncLayer);

                string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "raster_file_tiles_layer");

                if (!System.IO.Directory.Exists(cachePath))
                {
                    System.IO.Directory.CreateDirectory(cachePath);
                }

                fileTilesAsyncLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
                fileTilesAsyncLayer.ProjectedTileCache = new FileRasterTileCache(cachePath, "projected");

                fileTilesAsyncLayer.TileCache.GottenTile += TileCache_GottenCacheTile;
                fileTilesAsyncLayer.ProjectedTileCache.GottenTile += ProjectedTileCache_GottenCacheTile;

                //layerOverlay.Drawn += LayerOverlayOnDrawn;
                MapView.CurrentExtent = MaxExtents.ThinkGeoMaps;
                await MapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private void ProjectedTileCache_GottenCacheTile(object sender, GottenTileTileCacheEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = e.Tile.Content == null ? "Projection Cache Not Hit: " : "Projection Cache Hit: ";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.X}-{e.Tile.Y}";

                AppendLog(message);
            });
        }
        private void TileCache_GottenCacheTile(object sender, GottenTileTileCacheEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = e.Tile.Content == null ? "Cache Not Hit: " : "Cache Hit: ";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.X}-{e.Tile.Y}";

                AppendLog(message);
            });
        }


        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (fileTilesAsyncLayer == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton?.Tag == null) return;

                switch (radioButton.Tag.ToString())
                {
                    case "3857":
                        MapView.MapUnit = GeographyUnit.Meter;
                        fileTilesAsyncLayer.ProjectionConverter = null;
                        break;

                    case "4326":
                        MapView.MapUnit = GeographyUnit.DecimalDegree;
                        fileTilesAsyncLayer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);
                        break;

                    default:
                        return;
                }

                await fileTilesAsyncLayer.CloseAsync();
                await fileTilesAsyncLayer.OpenAsync();
                MapView.CurrentExtent = fileTilesAsyncLayer.GetBoundingBox();
                await MapView.RefreshAsync();
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
                    fileTilesAsyncLayer.RenderBeyondMaxZoom = checkBox.IsChecked.Value;

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


    class XyzFileTilesAsyncLayer : RasterXyzTileAsyncLayer
    {
        private string _root;

        public XyzFileTilesAsyncLayer(string tilesFolder)
        {
            _root = tilesFolder;
        }

        protected override Task<RasterTile> GetTileAsyncCore(int zoomLevel, long x, long y, float resolutionFactor, CancellationToken cancellationToken)
        {
            var path = $"{_root}\\{zoomLevel}\\{x}\\{y}.jpg";
            if (!File.Exists(path))
                return Task.FromResult(new RasterTile(null, zoomLevel, x, y));

            var bytes = File.ReadAllBytes(path);
            return Task.FromResult(new RasterTile(bytes, zoomLevel, x, y));
        }
    }
}
