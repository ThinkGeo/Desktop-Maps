using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show a Web Map Service, as a layer over a basemap or as an overlay of its own.
    /// </summary>
    public partial class WMS : IDisposable
    {
        private bool _initialized;

        public WMS()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            ShowChosen();
        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (_initialized) ShowChosen();
        }

        private void ShowChosen()
        {
            Map.Overlays.Clear();
            if (AsyncLayer.IsChecked == true)
                ShowWmsAsyncLayer();
            else
                ShowWmsOverlay();

            _ = Map.RefreshAsync();
        }

        private void ShowWmsAsyncLayer()
        {
            var basemapOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            basemapOverlay.Layers.Add(new ThinkGeoRasterMapsAsyncLayer
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1
            });
            Map.Overlays.Add(basemapOverlay);

            var wmsLayer = new WmsAsyncLayer(new Uri("https://geo.vliz.be/geoserver/Dataportal/ows?service=WMS&"))
            {
                DrawingExceptionMode = DrawingExceptionMode.DrawException,
                OutputFormat = "image/png",
                Crs = "EPSG:3857"
            };
            wmsLayer.Parameters.Add("LAYERS", "eurobis_grid_15m-obisenv");
            wmsLayer.Parameters.Add("STYLES", "generic");

            var wmsOverlay = new LayerOverlay { TileType = TileType.SingleTile, Opacity = 0.5 };
            wmsOverlay.Layers.Add(wmsLayer);
            Map.Overlays.Add(wmsOverlay);

            Map.CenterPoint = new PointShape(15253160, 4318830);
            Map.CurrentScale = 9266220;
        }

        private void ShowWmsOverlay()
        {
            // The overlay must ask the server for the map's own projection.
            var wmsOverlay = new WmsOverlay(new Uri("https://ows.mundialis.de/osm/service"))
            {
                AxisOrder = WmsAxisOrder.XY,
                Crs = "EPSG:3857"
            };
            wmsOverlay.ActiveLayerNames.Add("OSM-WMS");
            wmsOverlay.ActiveStyleNames.Add("default");
            Map.Overlays.Add(wmsOverlay);

            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 77000;
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
