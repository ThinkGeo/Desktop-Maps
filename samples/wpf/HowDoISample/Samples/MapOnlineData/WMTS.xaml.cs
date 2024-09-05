using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Async;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a WMTS Layer on the map
    /// </summary>
    public partial class WMTS : IDisposable
    {
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        
        private readonly string defaultLayerName = "SwissTopo";
        private readonly LayerOverlay layerOverlay = new LayerOverlay();
        private readonly Dictionary<string, RectangleShape> bBoxDict = new Dictionary<string, RectangleShape>();

        public WMTS()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the WMTS layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.Meter;
            MapView.Overlays.Add(layerOverlay);

            LoadWmtsServer1();
            LoadWmtsServer2();
            LoadWmtsServer3();
            LoadWmtsServer4();

            await SwitchToLayer(defaultLayerName);
        }

        private void UpdateCancellationToken()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = new CancellationTokenSource();
        }

        private async void WMTS_Checked(object sender, RoutedEventArgs e)
        {
            var buttonContent = ((RadioButton)sender).Content;
            if (buttonContent == null) return;

            var activeLayerName = buttonContent.ToString();
            await SwitchToLayer(activeLayerName);
        }

        private async Task SwitchToLayer(string activeLayerName)
        {
            foreach (var layer in layerOverlay.Layers)
            {
                layer.IsVisible = false;
            }

            layerOverlay.Layers[activeLayerName].IsVisible = true;
            var activeBBox = bBoxDict[activeLayerName];

            UpdateCancellationToken();
            MapView.CurrentExtent = activeBBox;
            await MapView.RefreshAsync(OverlayRefreshType.Redraw, cancellationTokenSource.Token);
        }

        private void LoadWmtsServer1()
        {
            var wmtsLayer = new WmtsLayer(new Uri("https://wmts.geo.admin.ch/1.0.0"));
            wmtsLayer.DrawingExceptionMode = DrawingExceptionMode.DrawException;
            wmtsLayer.CapabilitesCacheTimeout = new TimeSpan(0, 0, 0, 1);
            wmtsLayer.ActiveLayerName = "ch.swisstopo.pixelkarte-farbe-pk25.noscale";
            wmtsLayer.ActiveStyleName = "default";
            wmtsLayer.OutputFormat = "image/png";
            wmtsLayer.TileMatrixSetName = "21781_26";
            wmtsLayer.TileCache = new FileRasterTileCache(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "WmtsTmpTileCache_1"));

            layerOverlay.Layers.Add(defaultLayerName, wmtsLayer);
            bBoxDict.Add(defaultLayerName, new RectangleShape(641202.9893498598, 159695.95554381475, 645651.6243713424, 156646.11813217978));
        }

        private void LoadWmtsServer2()
        {
            var wmtsLayer = new WmtsLayer(new Uri("https://geo.vliz.be/geoserver/Dataportal/gwc/service/wmts"));
            wmtsLayer.DrawingExceptionMode = DrawingExceptionMode.DrawException;
            wmtsLayer.CapabilitesCacheTimeout = new TimeSpan(0, 0, 0, 1);
            wmtsLayer.ActiveLayerName = "eurobis_grid_15m-obisenv";
            wmtsLayer.ActiveStyleName = "generic";
            wmtsLayer.OutputFormat = "image/png";
            wmtsLayer.TileMatrixSetName = "EPSG:900913";
            wmtsLayer.TileCache = new FileRasterTileCache(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "WmtsTmpTileCache_2"));

            string layerName = "VLIZ";
            layerOverlay.Layers.Add(layerName, wmtsLayer);
            bBoxDict.Add(layerName, new RectangleShape(-14871588.140544016, 17407590.17351944, 18002448.801711123, -10574476.985662017));
        }

        private void LoadWmtsServer3()
        {
            var wmtsLayer = new WmtsLayer(new Uri("https://ows.emodnet-seabedhabitats.eu/geoserver/emodnet_view/gwc/service/wmts"));
            wmtsLayer.DrawingExceptionMode = DrawingExceptionMode.DrawException;
            wmtsLayer.CapabilitesCacheTimeout = new TimeSpan(0, 0, 0, 1);
            wmtsLayer.ActiveLayerName = "bboxes_eunis_2019";
            wmtsLayer.ActiveStyleName = "";
            wmtsLayer.TileMatrixSetName = "EPSG:900913";
            wmtsLayer.TileCache = new FileRasterTileCache(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "WmtsTmpTileCache_4"));
            wmtsLayer.OutputFormat = "image/png8";

            string layerName = "EMODNET";
            layerOverlay.Layers.Add(layerName, wmtsLayer);
            bBoxDict.Add(layerName, new RectangleShape(-2714010.470843626, 8601816.214356067, 1395244.1469382662, 5104057.819458384));
        }

        private void LoadWmtsServer4()
        {
            var wmtsLayer = new WmtsLayer(new Uri("https://basemaps.linz.govt.nz/v1/tiles/aerial/NZTM2000Quad/WMTSCapabilities.xml?api=c01j20m6pmjhc81bn55sakayftb"));
            wmtsLayer.DrawingExceptionMode = DrawingExceptionMode.DrawException;
            wmtsLayer.CapabilitesCacheTimeout = new TimeSpan(0, 0, 0, 1);
            wmtsLayer.ActiveLayerName = "aerial";
            wmtsLayer.ActiveStyleName = "default";
            wmtsLayer.OutputFormat = "image/png";
            wmtsLayer.TileMatrixSetName = "NZTM2000Quad";
            wmtsLayer.TileCache = new FileRasterTileCache(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "WmtsTmpTileCache_5"));

            string layerName = "LINZ";
            layerOverlay.Layers.Add(layerName, wmtsLayer);
            bBoxDict.Add(layerName, new RectangleShape(11103497.448365476, -5354740.842272079, 19322006.68392926, -12350257.632067444));
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}