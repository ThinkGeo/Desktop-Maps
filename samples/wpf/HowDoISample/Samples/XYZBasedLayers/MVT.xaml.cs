using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for MVT.xaml
    /// </summary>
    public partial class MVT : IDisposable
    {
        public MVT()
        {
            InitializeComponent();
            DataContext = this;
        }

        private string _selectedType = string.Empty;
        private int _selectedProjection = 3857;
        bool _initialized;
        private MvtTilesAsyncLayer _mvtLayer;
        private LayerOverlay _layerOverlay;

        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        private int _logIndex = 0;


        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;

            _selectedType = "512 * 512";
            ThinkGeoDebugger.DisplayTileId = true;

            _layerOverlay = new LayerOverlay();
            _layerOverlay.TileType = TileType.MultiTile;

            _mvtLayer = new MvtTilesAsyncLayer("https://demotiles.maplibre.org/style.json");
            _mvtLayer.VectorTileCache = new FileTileCache(".\\TileCache\\VectorTileCache");
            _mvtLayer.VectorTileCache.GottenTile += VectorTileCacheOnGottenTile;
            _mvtLayer.VectorTileCache.SavedTile += VectorTileCache_SavedTile;
            _layerOverlay.Layers.Add(_mvtLayer);

            await _mvtLayer.OpenAsync();
            MapView.CurrentExtent = _mvtLayer.GetBoundingBox();
            MapView.Overlays.Add(_layerOverlay);

            _initialized = true;
            await MapView.RefreshAsync();
        }

        private void VectorTileCache_SavedTile(object sender, SavedTileTileCacheEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = $"{e.Tile.ZoomIndex}-{e.Tile.X}-{e.Tile.Y} saved to cache";
                AppendLog(message);
            });
        }

        private void VectorTileCacheOnGottenTile(object sender, GottenTileTileCacheEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = $"{e.Tile.ZoomIndex}-{e.Tile.X}-{e.Tile.Y} loaded from cache";
                AppendLog(message);
            });
        }

        private async void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_initialized)
                return;

            _mvtLayer.StyleJsonUri = ((ComboBoxItem)e.AddedItems[0])?.Content.ToString();

            await _mvtLayer.CloseAsync();
            await _mvtLayer.OpenAsync();
            await MapView.RefreshAsync();
        }

        private void ShowTileID_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            ThinkGeoDebugger.DisplayTileId = (sender as CheckBox).IsChecked == true;
            _ = MapView.RefreshAsync();
        }

        private void SwitchTileSize_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            _selectedType = ((RadioButton)sender).Content?.ToString();

            var tileSize = _selectedType == "256 * 256" ? 256 : 512;
            _layerOverlay.TileWidth = tileSize;
            _layerOverlay.TileHeight = tileSize;

            if (_selectedType == "256 * 256" || _selectedType == "512 * 512")
                _layerOverlay.TileType = TileType.MultiTile;
            else
                _layerOverlay.TileType = TileType.SingleTile;

            if (_selectedProjection == 4326)
                _layerOverlay.TileMatrixSet = TileMatrixSet.CreateTileMatrixSet(tileSize, MaxExtents.DecimalDegree, GeographyUnit.DecimalDegree);
            else
                _layerOverlay.TileMatrixSet = TileMatrixSet.CreateTileMatrixSet(tileSize, MaxExtents.SphericalMercator, GeographyUnit.Meter);

            _ = _layerOverlay.RefreshAsync();
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
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", ".\\TileCache\\VectorTileCache");
        }

        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            _selectedProjection = int.Parse(((RadioButton)sender).Tag.ToString());

            if (!_initialized)
                return;

            try
            {
                if (_mvtLayer == null) return;

                var centerPoint = MapView.CenterPoint;

                var tileSize = 512;
                if (_selectedType == "256 * 256")
                    tileSize = 256;

                switch (_selectedProjection)
                {
                    case 3857:
                        MapView.MapUnit = GeographyUnit.Meter;
                        _mvtLayer.ProjectionConverter = null;
                        centerPoint = ProjectionConverter.Convert(4326, 3857, centerPoint);
                        _layerOverlay.TileMatrixSet = TileMatrixSet.CreateTileMatrixSet(tileSize, MaxExtents.SphericalMercator, GeographyUnit.Meter);
                        break;

                    case 4326:
                        MapView.MapUnit = GeographyUnit.DecimalDegree;
                        _mvtLayer.ProjectionConverter = new ProjectionConverter(3857, 4326);
                        centerPoint = ProjectionConverter.Convert(3857, 4326, centerPoint);
                        _layerOverlay.TileMatrixSet = TileMatrixSet.CreateTileMatrixSet(tileSize, MaxExtents.DecimalDegree, GeographyUnit.DecimalDegree);

                        break;

                    default:
                        return;
                }

                await _mvtLayer.CloseAsync();
                await _mvtLayer.OpenAsync();

                MapView.CenterPoint = centerPoint;
                await _layerOverlay.RefreshAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }
    }
}