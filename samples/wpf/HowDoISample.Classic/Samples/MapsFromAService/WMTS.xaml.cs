using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show a Web Map Tile Service with its own tile matrix set, as an overlay or as a layer.
    /// </summary>
    public partial class WMTS : IDisposable
    {
        private const string ServiceUrl = "https://wmts.geo.admin.ch/1.0.0";
        private const string LayerName = "ch.swisstopo.pixelkarte-farbe-pk25.noscale";
        private const string MatrixSetName = "21781_26";

        private bool _initialized;
        private int _request;

        public WMTS()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            await ShowChosenAsync();
        }

        private async void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (_initialized) await ShowChosenAsync();
        }

        private async Task ShowChosenAsync()
        {
            var request = ++_request;
            Map.Overlays.Clear();
            Status.Text = "Reading the service capabilities...";

            try
            {
                if (Overlay.IsChecked == true)
                    await ShowWmtsOverlayAsync(request);
                else
                    await ShowWmtsAsyncLayerAsync(request);
            }
            catch (Exception exception)
            {
                if (request == _request) Status.Text = "Unavailable: " + exception.Message;
            }

            _ = Map.RefreshAsync();
        }

        private async Task ShowWmtsOverlayAsync(int request)
        {
            var wmtsOverlay = new WmtsOverlay(new Uri(ServiceUrl))
            {
                ActiveLayerName = LayerName,
                ActiveStyleName = "default",
                OutputFormat = "image/png",
                TileMatrixSetName = MatrixSetName
            };
            await wmtsOverlay.OpenAsync();
            if (request != _request) return;

            Map.ZoomScales = wmtsOverlay.TileMatrixSet.GetScales();
            Map.Overlays.Add(wmtsOverlay);
            Map.CurrentExtent = wmtsOverlay.GetBoundingBox();
            Status.Text = nameof(WmtsOverlay);
        }

        private async Task ShowWmtsAsyncLayerAsync(int request)
        {
            var wmtsLayer = new WmtsAsyncLayer(new Uri(ServiceUrl))
            {
                DrawingExceptionMode = DrawingExceptionMode.DrawException,
                ActiveLayerName = LayerName,
                ActiveStyleName = "default",
                OutputFormat = "image/png",
                TileMatrixSetName = MatrixSetName
            };
            await wmtsLayer.OpenAsync();
            if (request != _request) return;

            var scales = new Collection<double>();
            foreach (var matrix in wmtsLayer.GetTileMatrixSets()[MatrixSetName].TileMatrices)
            {
                scales.Add(matrix.Scale);
            }
            Map.ZoomScales = scales;

            var layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(wmtsLayer);
            Map.Overlays.Add(layerOverlay);
            Map.CurrentExtent = wmtsLayer.GetBoundingBox();
            Status.Text = nameof(WmtsAsyncLayer);
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
