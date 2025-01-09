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
            try
            { 
                UseLayerWithReProjection();
                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private async void RbLayerOrOverlay_Checked(object sender, RoutedEventArgs e)
        {
            try
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
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
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
            var wmsImageLayer = new WmsAsyncLayer(new Uri("http://ows.mundialis.de/services/service"));
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

            // Create the first WMS layer using the parameters below.
            var wmsLayer1 = new WmsAsyncLayer(new Uri("http://ows.mundialis.de/services/service"));
            wmsLayer1.ActiveLayerNames.Add("OSM-WMS");
            wmsLayer1.ActiveStyleNames.Add("default");
            wmsLayer1.Exceptions = "application/vnd.ogc.se_xml";
            wmsLayer1.Transparency = 100;

            // Apply the projection conversion to WMS layer (convert from EPSG:4326 to EPSG:3857)
            wmsLayer1.ProjectionConverter = new GdalProjectionConverter(4326, 3857); 
            // Add the layer to the overlay.
            staticOverlay.Layers.Add("wmsImageLayer", wmsLayer1);

            // Create the second WMS layer using the parameters below.
            var wmsLayer2 = new WmsAsyncLayer(new Uri("http://geo.vliz.be/geoserver/Dataportal/ows?service=WMS&"));
            wmsLayer2.DrawingExceptionMode = DrawingExceptionMode.DrawException;
            wmsLayer2.Parameters.Add("LAYERS", "eurobis_grid_15m-obisenv");
            wmsLayer2.Parameters.Add("STYLES", "generic");
            wmsLayer2.OutputFormat = "image/png";
            wmsLayer2.Crs = "EPSG:3857";  // Coordinate system, typically EPSG:3857 for WMS with Spherical Mercator
            wmsLayer2.Transparency = 100;

            // Set the map's current extent
            MapView.CurrentExtent = new RectangleShape(14702448, -1074476, 15302448, -5574476);
            staticOverlay.Layers.Add(wmsLayer2);
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