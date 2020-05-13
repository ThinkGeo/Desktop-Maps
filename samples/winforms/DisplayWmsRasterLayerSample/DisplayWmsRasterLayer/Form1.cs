using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.WinForms;

namespace DisplayWmsRasterLayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;

            LayerOverlay layerOverlay = new LayerOverlay();
            WmsRasterLayer wmsLayer = new WmsRasterLayer(new Uri("http://howdoiwms.thinkgeo.com/WmsServer.aspx"));
            wmsLayer.ActiveLayerNames.Add("COUNTRIES02");
            wmsLayer.ActiveStyleNames.Add("Simple");
            wmsLayer.Crs = "EPSG:4326";
            wmsLayer.OutputFormat = "image/png";
            layerOverlay.Layers.Add(wmsLayer);
            winformsMap1.Overlays.Add(layerOverlay);

            wmsLayer.Open();
            winformsMap1.CurrentExtent = wmsLayer.GetBoundingBox();
            winformsMap1.Refresh();
        }
    }
}
