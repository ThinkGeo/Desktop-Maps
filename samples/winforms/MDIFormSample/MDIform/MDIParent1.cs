using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace MDIform
{
    public partial class MDIParent1 : Form
    {
        public MDIParent1()
        {
            InitializeComponent();

            Form childForm1 = new childForm();
            childForm1.MdiParent = this;
            childForm1.Parent = winformsMap1;

            childForm1.Show();
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = ThinkGeoCloudMapsOverlay.GetZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-13914936.35, 5942074.07, -7458405.88, 2875744.62);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            // Displays the Cloud Maps as a background.
            ThinkGeoCloudMapsOverlay cloudMapsOverlay = new ThinkGeoCloudMapsOverlay();
            cloudMapsOverlay.MapType = ThinkGeoCloudMapsMapType.Light;
            winformsMap1.Overlays.Add(cloudMapsOverlay);

            InMemoryFeatureLayer inMemoryFeatureLayer = new InMemoryFeatureLayer();
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.StandardColors.Red, 12, GeoColor.StandardColors.Black, 2);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("Point", inMemoryFeatureLayer);
            winformsMap1.Overlays.Add("PointOverlay", layerOverlay);

            winformsMap1.Refresh();
        }

        private void winformsMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Displays the X and Y in screen coordinates.
            statusStrip1.Items["toolStripStatusLabelScreen"].Text = "X:" + e.X + " Y:" + e.Y;

            //Gets the PointShape in world coordinates from screen coordinates.
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, new ScreenPointF(e.X, e.Y), winformsMap1.Width, winformsMap1.Height);

            //Displays world coordinates.
            statusStrip1.Items["toolStripStatusLabelWorld"].Text = "(world) X:" + Math.Round(pointShape.X, 4) + " Y:" + Math.Round(pointShape.Y, 4);
        }
    }
}
