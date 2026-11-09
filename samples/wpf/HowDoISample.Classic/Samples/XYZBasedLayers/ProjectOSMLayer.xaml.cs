using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for ProjectOSMLayer.xaml
    /// </summary>
    public partial class ProjectOSMLayer : UserControl
    {
        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        private OpenStreetMapAsyncLayer _osmLayer;
        private int _logIndex;
        private bool _initialized;

        public ProjectOSMLayer()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            _osmLayer = new OpenStreetMapAsyncLayer();
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            layerOverlay.Layers.Add(_osmLayer);
            Map.Overlays.Add(layerOverlay);

            string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "projected_osm_layer");

            if (!System.IO.Directory.Exists(cachePath))
            {
                System.IO.Directory.CreateDirectory(cachePath);
            }

            _osmLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
            _osmLayer.ProjectedTileCache = new FileRasterTileCache(cachePath, "projected");

            _osmLayer.TileCache.GottenTile += TileCache_GottenCacheTile;
            _osmLayer.ProjectedTileCache.GottenTile += ProjectedTileCache_GottenCacheTile;

            Map.MapUnit = GeographyUnit.Meter;
            _osmLayer.ProjectionConverter = null;

            await _osmLayer.CloseAsync();
            await _osmLayer.OpenAsync();

            var extentIn25832 = new RectangleShape(166021.4, 9328006, 833978, 0);
            Map.CenterPoint = extentIn25832.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, extentIn25832, Map.MapWidth, Map.MapHeight);

            _initialized = true;
            await Map.RefreshAsync();
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
            if (!_initialized)
                return;

            try
            {
                if (_osmLayer == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton?.Tag == null) return;

                switch (radioButton.Tag.ToString())
                {
                    case "3857":
                        Map.MapUnit = GeographyUnit.Meter;
                        _osmLayer.ProjectionConverter = null;
                        break;

                    case "25832":
                        Map.MapUnit = GeographyUnit.Meter;
                        _osmLayer.ProjectionConverter = new GdalProjectionConverter(3857, 25832);
                        break;

                    default:
                        return;
                }

                await _osmLayer.CloseAsync();
                await _osmLayer.OpenAsync();

                var extentIn25832 = new RectangleShape(166021.4, 9328006, 833978, 0);
                Map.CenterPoint = extentIn25832.GetCenterPoint();
                Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, extentIn25832, Map.MapWidth, Map.MapHeight);
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
            
            _osmLayer.RenderBeyondMaxZoom = checkBox.IsChecked ?? false;
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
           
            ThinkGeoDebugger.DisplayTileId = checkBox.IsChecked ?? false;
            _ = Map.RefreshAsync();
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
