using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class LoadAWmsImage : UserControl
    {
        public LoadAWmsImage()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = new RectangleShape(-143.4, 109.3, 116.7, -76.3);

            UseOverlay();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                RadioButton rbt = (RadioButton)sender;
                switch (rbt.Content.ToString())
                {
                    case "Use WmsRasterLayer":
                        UseLayer();
                        break;

                    case "Use WmsOverlay":
                        UseOverlay();
                        break;
                }
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

            mapView.Refresh();
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

            mapView.Refresh();
        }
    }
}