using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.WinForms;

namespace ThinkGeo.MapSuite.Samples
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;

            LayerOverlay overlay = new LayerOverlay();

            Jpeg2000RasterLayer jpeg2000RasterLayer = new Jpeg2000RasterLayer("../../App_Data/World.jp2");
            overlay.Layers.Add(jpeg2000RasterLayer);

            winformsMap1.Overlays.Add(overlay);

            jpeg2000RasterLayer.Open();
            winformsMap1.CurrentExtent = jpeg2000RasterLayer.GetBoundingBox();

            winformsMap1.Refresh();
        }
    }
}