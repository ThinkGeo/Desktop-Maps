using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show the Standard MBTiles File (v1.3)
    /// </summary>
    public partial class DisplayMBTilesFile : IDisposable
    {
        public DisplayMBTilesFile()
        {
            InitializeComponent();
        }

        LayerOverlay _layerOverlay;


        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;
            _layerOverlay = new LayerOverlay();
            _layerOverlay.TileType = TileType.SingleTile;
            MapView.Overlays.Add(_layerOverlay);

            var openstackMbtiles = new VectorMbTilesAsyncLayer(@".\Data\Mbtiles\maplibre.mbtiles", @".\Data\Mbtiles\style.json");
            _layerOverlay.Layers.Add(openstackMbtiles);

            MapView.CurrentExtent = MaxExtents.SphericalMercator;
            await openstackMbtiles.OpenAsync();
            //MapView.Background = new SolidColorBrush(Color.FromRgb(216, 242, 255));

            await MapView.RefreshAsync();
        }

        private async void ShowTileID_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            ThinkGeoDebugger.DisplayTileId = (sender as CheckBox).IsChecked == true;
            if (MapView != null)
                await MapView.RefreshAsync();
        }

        private async void SwitchTileSize_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (MapView.Overlays.Count <= 0) return;

            if (!(_layerOverlay.Layers[0] is VectorMbTilesAsyncLayer mbTilesLayer))
                return;

            var content = ((RadioButton)sender).Content.ToString();
            {
                if (content == "256 * 256")
                {
                    MapView.ZoomLevelSet = new SphericalMercatorZoomLevelSet(256);
                    _layerOverlay.TileType = TileType.MultiTile;
                    _layerOverlay.TileWidth = 256;
                    _layerOverlay.TileHeight = 256;
                    await mbTilesLayer.CloseAsync();
                    mbTilesLayer.TileWidth = 256;
                    mbTilesLayer.TileHeight = 256;
                    await mbTilesLayer.OpenAsync();

                }
                else if (content == "512 * 512")
                {
                    MapView.ZoomLevelSet = new SphericalMercatorZoomLevelSet(512);
                    _layerOverlay.TileType = TileType.MultiTile;
                    _layerOverlay.TileWidth = 512;
                    _layerOverlay.TileHeight = 512;
                    await mbTilesLayer.CloseAsync();
                    mbTilesLayer.TileWidth = 512;
                    mbTilesLayer.TileHeight = 512;
                    await mbTilesLayer.OpenAsync();
                }
                else
                {
                    _layerOverlay.TileType = TileType.SingleTile;

                }
            }
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