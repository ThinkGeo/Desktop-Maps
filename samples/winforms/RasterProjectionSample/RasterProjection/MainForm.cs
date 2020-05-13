using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.WinForms;
using System.IO;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite;

namespace RasterProjection
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

            //To resolve issue that we cannot run the executable by double click it on linux, we need to find out the absolute path by reflection.
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string ecwLayerFileName = Path.GetFullPath(Path.Combine(baseDirectory, "../../App_Data/World.ecw"));

            EcwRasterLayer ecwRasterLayer = new EcwRasterLayer(ecwLayerFileName);
            overlay.Layers.Add(ecwRasterLayer);

            UnmanagedProj4Projection proj4Projection = new UnmanagedProj4Projection();
            proj4Projection.InternalProjectionParametersString = Proj4Projection.GetDecimalDegreesParametersString();
            proj4Projection.ExternalProjectionParametersString = Proj4Projection.GetGoogleMapParametersString();

            ecwRasterLayer.ImageSource.Projection = proj4Projection;
            proj4Projection.Open();

            winformsMap1.Overlays.Add(overlay);

            ecwRasterLayer.Open();
            winformsMap1.CurrentExtent = ecwRasterLayer.GetBoundingBox();

            winformsMap1.Refresh();
        }
    }
}