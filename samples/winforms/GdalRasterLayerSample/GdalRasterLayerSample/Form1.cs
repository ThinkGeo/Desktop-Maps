using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.WinForms;

namespace GdalRasterLayerSample
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
            GdalRasterLayer gdalRasterLayer = new GdalRasterLayer(@"AppData/FdoGdal.tif");
            gdalRasterLayer.Open();
            layerOverlay.Layers.Add(gdalRasterLayer);
            winformsMap1.Overlays.Add(layerOverlay);

            winformsMap1.CurrentExtent = gdalRasterLayer.GetBoundingBox();
            winformsMap1.Refresh();
        }
    }
}
