using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a WMS Layer on the map
    /// </summary>
    public partial class WMS : IDisposable
    {
        public WMS()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the WMS layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            UseLayerWithReProjection();
            await MapView.RefreshAsync();
        }

        private async void RbLayerOrOverlay_Checked(object sender, RoutedEventArgs e)
        {
            // Based on the radio buttons we switch between using the overlay and layer.
            var button = (RadioButton)sender;
            if (button.Content == null) return;
            switch (button.Content.ToString())
            {
                case "Use WmsOverlay":
                    UseOverlay();
                    break;
                case "Use WmsRasterLayer":
                    UseLayer();
                    break;
                case "Use WmsLayer with ReProjection":
                    UseLayerWithReProjection();
                    break;
            }
            await MapView.RefreshAsync();
        }

        private void UseOverlay()
        {
            MapView.MapUnit = GeographyUnit.DecimalDegree;

            // Clear out the overlays so we start fresh
            MapView.Overlays.Clear();

            // Create a WMS overlay using the WMS parameters below.
            // This is a public service and is very slow most of the time.
            var wmsOverlay = new WmsOverlay
            {
                Uri = new Uri("http://ows.mundialis.de/services/service")
            };
            wmsOverlay.Parameters.Add("layers", "OSM-WMS");
            wmsOverlay.Parameters.Add("STYLES", "default");

            // Add the overlay to the map.
            MapView.Overlays.Add(wmsOverlay);

            // Set the current extent to a local area.
            MapView.CurrentExtent = new RectangleShape(-96.8538765269409, 33.1618647290098, -96.7987487018851, 33.1054126590461);
        }

        private void UseLayer()
        {
            MapView.MapUnit = GeographyUnit.DecimalDegree;

            // Clear out the overlays so we start fresh
            MapView.Overlays.Clear();

            // Create an overlay that we will add the layer to.
            var staticOverlay = new LayerOverlay();
            MapView.Overlays.Add(staticOverlay);

            // Create the WMS layer using the parameters below.
            // This is a public service and is very slow most of the time.
            var wmsImageLayer = new Core.WmsAsyncLayer(new Uri("http://ows.mundialis.de/services/service"));
            wmsImageLayer.ActiveLayerNames.Add("OSM-WMS");
            wmsImageLayer.ActiveStyleNames.Add("default");
            wmsImageLayer.Exceptions = "application/vnd.ogc.se_xml";

            // Add the layer to the overlay.
            staticOverlay.Layers.Add("wmsImageLayer", wmsImageLayer);

            // Set the current extent to a local area.
            MapView.CurrentExtent = new RectangleShape(-96.8538765269409, 33.1618647290098, -96.7987487018851, 33.1054126590461);
        }

        private void UseLayerWithReProjection()
        {
            MapView.MapUnit = GeographyUnit.Meter;
            // Clear out the overlays so we start fresh
            MapView.Overlays.Clear();

            // Create an overlay that we will add the layer to.
            var staticOverlay = new LayerOverlay();
            MapView.Overlays.Add(staticOverlay);

            // Create a ProjectionConverter to convert from EPSG:4326 (WGS 84) to EPSG:3857 (Spherical Mercator).
            var gdalProjectionConverter = new GdalProjectionConverter(4326, 3857);
            // Open the projection converter before using it
            if (!gdalProjectionConverter.IsOpen)
            {
                gdalProjectionConverter.Open();
            }

            // Create the first WMS layer using the parameters below.
            var wmsImageLayer = new Core.WmsAsyncLayer(new Uri("http://ows.mundialis.de/services/service"));
            wmsImageLayer.ActiveLayerNames.Add("OSM-WMS");
            wmsImageLayer.ActiveStyleNames.Add("default");
            wmsImageLayer.Exceptions = "application/vnd.ogc.se_xml";
            wmsImageLayer.Transparency = 100;

            // Apply the projection conversion to WMS layer (convert from EPSG:4326 to EPSG:3857)
            wmsImageLayer.ProjectionConverter = gdalProjectionConverter;
            // Add the layer to the overlay.
            staticOverlay.Layers.Add("wmsImageLayer", wmsImageLayer);

            // Create the second WMS layer using the parameters below.
            var wmsLayer = new WmsAsyncLayer(new Uri("http://geo.vliz.be/geoserver/Dataportal/ows?service=WMS&"));
            wmsLayer.DrawingExceptionMode = DrawingExceptionMode.DrawException;
            wmsLayer.Parameters.Add("LAYERS", "eurobis_grid_15m-obisenv");  
            wmsLayer.Parameters.Add("STYLES", "generic");  
            wmsLayer.OutputFormat = "image/png";  
            wmsLayer.Crs = "EPSG:3857";  // Coordinate system, typically EPSG:3857 for WMS with Spherical Mercator
            wmsLayer.Transparency = 100;

            // Set the map's current extent
            MapView.CurrentExtent = new RectangleShape(14702448.140544016, -1074476.17351944, 15302448.801711123, -5574476.985662017);
            staticOverlay.Layers.Add(wmsLayer);
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