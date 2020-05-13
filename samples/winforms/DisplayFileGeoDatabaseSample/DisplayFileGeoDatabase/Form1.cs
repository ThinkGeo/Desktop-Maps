using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace DisplayFileGeoDatabase
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

            FileGeoDatabaseFeatureLayer fileGeoDatabaseFeatureLayer = new FileGeoDatabaseFeatureLayer("../../AppData/Shapes.gdb", "states");
            fileGeoDatabaseFeatureLayer.Open();
            AreaStyle areaStyle = new AreaStyle();
            areaStyle.FillSolidBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214));
            areaStyle.OutlinePen = new GeoPen(GeoColor.FromArgb(255, 118, 138, 69), 1);
            areaStyle.OutlinePen.DashStyle = LineDashStyle.Solid;
            fileGeoDatabaseFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = areaStyle;
            fileGeoDatabaseFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(fileGeoDatabaseFeatureLayer);
            winformsMap1.Overlays.Add(layerOverlay);

            winformsMap1.CurrentExtent = fileGeoDatabaseFeatureLayer.GetBoundingBox();
            winformsMap1.Refresh();
        }
    }
}
