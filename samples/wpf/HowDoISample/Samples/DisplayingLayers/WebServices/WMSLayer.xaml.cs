using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
using System;
using System.Diagnostics;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class WMSLayer : UserControl
    {
        public WMSLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            UseOverlay();
            mapView.CurrentExtent = new RectangleShape(-96.8538765269409, 33.1618647290098, -96.7987487018851, 33.1054126590461);

            mapView.Refresh();
        }

        private void rbLayerOrOverlay_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (button.Content != null)
            {
                switch (button.Content.ToString())
                {
                    case "Use WmsOverlay":
                        UseOverlay();
                        break;
                    case "Use WmsRasterLayer":
                        UseLayer();
                        break;
                    default:
                        break;
                }
                mapView.Refresh();
            }
        }
        private void UseOverlay()
        {
            mapView.Overlays.Clear();

            WmsOverlay wmsOverlay = new WmsOverlay();
            wmsOverlay.ServerUri = new Uri("http://ows.mundialis.de/services/service");
            wmsOverlay.Parameters.Add("layers", "OSM-WMS");
            wmsOverlay.Parameters.Add("STYLES", "default");
            wmsOverlay.Parameters.Add("CRS", "3857");
            mapView.Overlays.Add(wmsOverlay);
        }

        private void UseLayer()
        {
            mapView.Overlays.Clear();

            WmsRasterLayer wmsImageLayer = new WmsRasterLayer(new Uri("http://ows.mundialis.de/services/service"));
            wmsImageLayer.UpperThreshold = double.MaxValue;
            wmsImageLayer.LowerThreshold = 0;

            wmsImageLayer.Open();

            wmsImageLayer.ActiveLayerNames.Add("OSM-WMS");
            wmsImageLayer.ActiveStyleNames.Add("default");

            // this parameter is just optional.
            wmsImageLayer.Exceptions = "application/vnd.ogc.se_xml";

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("wmsImageLayer", wmsImageLayer);
            mapView.Overlays.Add(staticOverlay);
            wmsImageLayer.Close();
        }
    }
}
