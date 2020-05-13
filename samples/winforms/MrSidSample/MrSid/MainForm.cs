using System;
using System.IO;
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
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;

            LayerOverlay overlay = new LayerOverlay();

            MrSidRasterLayer mrSidRasterLayer = new MrSidRasterLayer("../../App_Data/World.sid");
            overlay.Layers.Add(mrSidRasterLayer);

            winformsMap1.Overlays.Add(overlay);

            mrSidRasterLayer.Open();
            winformsMap1.CurrentExtent = mrSidRasterLayer.GetBoundingBox();

            winformsMap1.Refresh();
        }
    }
}