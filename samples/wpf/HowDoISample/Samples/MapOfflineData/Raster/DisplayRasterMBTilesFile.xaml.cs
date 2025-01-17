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
        
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            { 
                var layerOverlay = new LayerOverlay();
                MapView.Overlays.Add(layerOverlay);
                rasterMbTilesLayer = new RasterMbTilesAsyncLayer(@".\Data\Mbtiles\test.mbtiles");
                layerOverlay.TileType = TileType.SingleTile;
                layerOverlay.Layers.Add(rasterMbTilesLayer);

                string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rasterMbTilesLayerCache");

                if (!System.IO.Directory.Exists(cachePath))
                {
                    System.IO.Directory.CreateDirectory(cachePath);
                }

                rasterMbTilesLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
                rasterMbTilesLayer.ProjectedTileCache = new FileRasterTileCache(cachePath, "projected")
                    { EnableDebugInfo = true};

                rasterMbTilesLayer.TileCache.GottenCacheTile += TileCache_GottenCacheTile;
                rasterMbTilesLayer.ProjectedTileCache.GottenCacheTile += ProjectedTileCache_GottenCacheTile;

                //layerOverlay.Drawn += LayerOverlayOnDrawn;
                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private void ProjectedTileCache_GottenCacheTile(object sender, GottenCacheImageBitmapTileCacheEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = e.Tile.Content == null ? "Projection Cache Not Hit: " : "Projection Cache Hit: ";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.Column}-{e.Tile.Row}";

                AppendLog(message);
            });
        }

        private void TileCache_GottenCacheTile(object sender, GottenCacheImageBitmapTileCacheEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = e.Tile.Content == null ? "Cache Not Hit: " : "Cache Hit: ";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.Column}-{e.Tile.Row}";

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
                MapView.CurrentExtent = rasterMbTilesLayer.GetBoundingBox();
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
                    rasterMbTilesLayer.RenderBeyondMaxZoom = checkBox.IsChecked.Value;

                await MapView.RefreshAsync();
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
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
