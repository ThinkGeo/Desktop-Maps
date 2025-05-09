using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show the Standard MBTiles File (v1.3)
    /// </summary>
    public partial class DisplayMbTilesFile : IDisposable
    {
        public DisplayMbTilesFile()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;
            var mvtLayer = new VectorMbTilesAsyncLayer(@".\Data\Mbtiles\maplibre.mbtiles");
            mvtLayer.StyleJsonUri = @".\Data\Mbtiles\style.json";

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(mvtLayer);

            MapView.Overlays.Clear();
            MapView.Overlays.Add(layerOverlay);

            await mvtLayer.OpenAsync();
            MapView.CurrentExtent = mvtLayer.GetBoundingBox();

            await MapView.RefreshAsync();
        }

        private void SwitchTileSize_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            var selectedType = ((RadioButton)sender).Content?.ToString();

            if (selectedType == null)
                return;

            var mvtLayer = new VectorMbTilesAsyncLayer(@".\Data\Mbtiles\maplibre.mbtiles");
            if (selectedType == "Apply Style")
                mvtLayer.StyleJsonUri = @".\Data\Mbtiles\style.json";

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(mvtLayer);

            MapView.Overlays.Clear();
            MapView.Overlays.Add(layerOverlay);

            _ = MapView.RefreshAsync();
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