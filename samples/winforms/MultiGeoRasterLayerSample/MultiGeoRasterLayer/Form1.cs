using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace MultiGeoRasterLayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set the full extent and the background color
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = ExtentHelper.GetDrawingExtent(new RectangleShape(-180.0, 83.0, 180.0, -90.0), winformsMap1.Width, winformsMap1.Height);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            LayerOverlay layerOverlay = new LayerOverlay();
            //Add MultiGeoRasterLayer to the MapEngine
            ThinkGeo.MapSuite.Layers.MultiGeoRasterLayer.BuildReferenceFile(@"..\..\Data\referenceFile.txt", @"..\..\Data");
            ThinkGeo.MapSuite.Layers.MultiGeoRasterLayer multiGeoRasterLayer = new ThinkGeo.MapSuite.Layers.MultiGeoRasterLayer(@"..\..\Data\referenceFile.txt");
            layerOverlay.Layers.Add(multiGeoRasterLayer);
            winformsMap1.Overlays.Add(layerOverlay);
            winformsMap1.Refresh();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
