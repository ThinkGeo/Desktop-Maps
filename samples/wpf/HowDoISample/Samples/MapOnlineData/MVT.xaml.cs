using System;
using System.Threading.Tasks;
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
        }

        private string _selectedType = string.Empty;
        private string _selectedWvtServer = string.Empty;
        private bool _mapLoaded = false;

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;
            _mapLoaded = true;

            _selectedWvtServer = "https://demotiles.maplibre.org/style.json";
            _selectedType = "512 * 512";
            ThinkGeoDebugger.DisplayTileId = true;

            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.MultiTile;

            var mvtLayer = new MvtTilesAsyncLayer(_selectedWvtServer);
            layerOverlay.Layers.Add(mvtLayer);

            await mvtLayer.OpenAsync();
            MapView.CurrentExtent = mvtLayer.GetBoundingBox();
            MapView.Overlays.Add(layerOverlay);

            await MapView.RefreshAsync();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_mapLoaded)
                return;

            _selectedWvtServer = ((ComboBoxItem)e.AddedItems[0])?.Content.ToString();
            _ = RefreshMvtAsync();
        }

        private void ShowTileID_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!_mapLoaded)
                return;

            ThinkGeoDebugger.DisplayTileId = (sender as CheckBox).IsChecked == true;
            _ = MapView.RefreshAsync();
        }

        private void SwitchTileSize_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!_mapLoaded)
                return;

            _selectedType = ((RadioButton)sender).Content?.ToString();
            _ = RefreshMvtAsync();
        }

        private async Task RefreshMvtAsync()
        {
            if (_selectedType == "256 * 256")
                await RefreshMvtAsync(_selectedWvtServer, 256);
            else if (_selectedType == "512 * 512")
                await RefreshMvtAsync(_selectedWvtServer, 512);
            else
                await RefreshMvtAsync(_selectedWvtServer, 512, true);
        }

        private async Task RefreshMvtAsync(string mvtServer, int tileSize, bool singleTile = false)
        {
            MapView.Overlays.Clear();
            MapView.ZoomScales = new SphericalMercatorZoomLevelSet(tileSize).GetScales();

            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = singleTile ? TileType.SingleTile : TileType.MultiTile;
            layerOverlay.TileWidth = tileSize;
            layerOverlay.TileHeight = tileSize;

            var mvtLayer = new MvtTilesAsyncLayer(mvtServer);
            mvtLayer.TileWidth = tileSize;
            mvtLayer.TileHeight = tileSize;
            layerOverlay.Layers.Add(mvtLayer);

            MapView.Overlays.Add(layerOverlay);

            await MapView.RefreshAsync();
        }
        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}