using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
using System;
using System.Diagnostics;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a WMTS Layer on the map
    /// </summary>
    public partial class WMTSLayerSample : UserControl, IDisposable
    {
        public WMTSLayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the WMTS layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a WMTS overlay using the WMS parameters below.
            // This is a public service and performance may be slow.
            WmtsLayer wmtsLayer = new WmtsLayer
            {
                DrawingExceptionMode = DrawingExceptionMode.DrawException,
                WmtsSeverEncodingType = WmtsSeverEncodingType.Restful
            };

            wmtsLayer.ServerUris.Add(new Uri("https://wmts.geo.admin.ch/1.0.0"));
            wmtsLayer.CapabilitesCacheTimeout = new TimeSpan(0, 0, 0, 1);
            wmtsLayer.ActiveLayerName = "ch.swisstopo.pixelkarte-farbe-pk25.noscale";
            wmtsLayer.ActiveStyleName = "default";
            wmtsLayer.OutputFormat = "image/png";
            wmtsLayer.TileMatrixSetName = "21781_26";
            wmtsLayer.TileCache = new FileRasterTileCache(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "WmtsTmpTileCache"));
            
            //Add the new WMTS Layer to our LayerOverlay
            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(wmtsLayer);

            //Add the overlay to the mapView's Overlay collection.
            mapView.Overlays.Add(layerOverlay);

            // Set the current extent to the Eiger - a famous peak in Switzerland.
            mapView.CurrentExtent = new RectangleShape(641202.9893498598, 159695.95554381475, 645651.6243713424, 156646.11813217978);

            // Refresh the map.
            await mapView.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
