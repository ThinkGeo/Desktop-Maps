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

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.MultiTile;
            MapView.Overlays.Add(layerOverlay);

            var openstackMbtiles = new MbTilesLayer(@".\Data\Mbtiles\maplibre.mbtiles", @".\Data\Mbtiles\style.json");
            layerOverlay.Layers.Add(openstackMbtiles);

            MapView.CurrentExtent = new RectangleShape(-11305077.39954415, 11301934.55158609, 6893050.193489946, -2669531.148872344);
            await openstackMbtiles.OpenAsync();
            MapView.Background = new SolidColorBrush(Color.FromRgb(216, 242, 255));

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
            if (MapView.Overlays.Count > 0 && MapView.Overlays[0] is LayerOverlay layerOverlay)
            {
                var tileSize = int.Parse(((RadioButton)sender).Tag.ToString());
                MapView.ZoomLevelSet = new SphericalMercatorZoomLevelSet(tileSize);

                layerOverlay.TileWidth = tileSize;
                layerOverlay.TileHeight = tileSize;

                if (layerOverlay.Layers[0] is MbTilesLayer mbTilesLayer)
                {
                    mbTilesLayer.ZoomLevelSet = new SphericalMercatorZoomLevelSet(tileSize, MaxExtents.SphericalMercator);
                }
                await MapView.RefreshAsync();
            }
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