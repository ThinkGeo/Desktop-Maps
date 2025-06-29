using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for RasterMBTiles.xaml
    /// </summary>
    public partial class RasterXyzServer : IDisposable
    {
        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        private ThinkGeoRasterMapsAsyncLayer _thinkGeoRasterMapsAsyncLayer;
        private int _logIndex;
        private bool _initialized;

        public RasterXyzServer()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            ThinkGeoDebugger.DisplayTileId = true;

            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            MapView.Overlays.Add(layerOverlay);

            // Add Cloud Maps as a background overlay
            _thinkGeoRasterMapsAsyncLayer = new ThinkGeoRasterMapsAsyncLayer
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1,
            };

            layerOverlay.Layers.Add(_thinkGeoRasterMapsAsyncLayer);

            string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "thinkgeo_raster_maps_online_layer");

            if (!System.IO.Directory.Exists(cachePath))
            {
                System.IO.Directory.CreateDirectory(cachePath);
            }

            _thinkGeoRasterMapsAsyncLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
            _thinkGeoRasterMapsAsyncLayer.ProjectedTileCache = new FileRasterTileCache(cachePath, "projected");

            _thinkGeoRasterMapsAsyncLayer.TileCache.GottenTile += TileCache_GottenCacheTile;
            _thinkGeoRasterMapsAsyncLayer.ProjectedTileCache.GottenTile += ProjectedTileCache_GottenCacheTile;

            MapView.CenterPoint = MaxExtents.ThinkGeoMaps.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, MaxExtents.ThinkGeoMaps, MapView.MapWidth, MapView.MapHeight);

            _initialized = true;
            _ = MapView.RefreshAsync();
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
                if (_thinkGeoRasterMapsAsyncLayer == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton?.Tag == null) return;

                switch (radioButton.Tag.ToString())
                {
                    case "3857":
                        MapView.MapUnit = GeographyUnit.Meter;
                        _thinkGeoRasterMapsAsyncLayer.ProjectionConverter = null;
                        break;

                    case "4326":
                        MapView.MapUnit = GeographyUnit.DecimalDegree;
                        _thinkGeoRasterMapsAsyncLayer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);
                        break;

                    default:
                        return;
                }

                await _thinkGeoRasterMapsAsyncLayer.CloseAsync();
                await _thinkGeoRasterMapsAsyncLayer.OpenAsync();
                var thinkGeoRasterMapsAsyncLayerBBox = _thinkGeoRasterMapsAsyncLayer.GetBoundingBox();
                MapView.CenterPoint = thinkGeoRasterMapsAsyncLayerBBox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, thinkGeoRasterMapsAsyncLayerBBox, MapView.MapWidth, MapView.MapHeight);
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
                _thinkGeoRasterMapsAsyncLayer.RenderBeyondMaxZoom = checkBox.IsChecked.Value;

            _ = MapView.RefreshAsync();
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
