using System;
using System.Configuration;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace ThinkGeo.MapSuite.Layers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create our new OsmWorldMapKit Layer
            WorldStreetsLayer osmWorldMapKitLayer = GetLayerByDatabaseType();
            osmWorldMapKitLayer.Open();

            // Setup the overlay
            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(osmWorldMapKitLayer);

            // Setup the map
			winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.CurrentExtent = new RectangleShape(-10811578.0703145, 3892111.87754448, -10736211.3489208, 3839905.51779612);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 172, 205, 226));

            // Add the overlay to the map
            winformsMap1.Overlays.Add("worldmapkit", layerOverlay);

			osmWorldMapKitLayer.Open ();
			winformsMap1.CurrentExtent = osmWorldMapKitLayer.Layers [7].GetBoundingBox ();

            winformsMap1.Refresh();
        }

        private static WorldStreetsLayer GetLayerByDatabaseType()
        {
            return new WorldStreetsLayer(ConfigurationManager.AppSettings["SqliteConnectionString"]);
        }

        private void winformsMap1_Click(object sender, EventArgs e)
        {
            var extent = winformsMap1.CurrentExtent;
        }
    }
}