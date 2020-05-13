using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace LatLongGraticule
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            // Set the full extent and the background color
            winformsMap1.CurrentExtent = new RectangleShape(-120, 25, 20, -30);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            // Add the static layers to the MapEngine
            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer("../../Data/Countries02.shp", GeoFileReadWriteMode.Read);
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 1);
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay overLayer = new LayerOverlay();
            overLayer.Layers.Add(worldLayer);
            winformsMap1.Overlays.Add("Overlay", overLayer);

            //Add the GraticuleAdormentLayer to the Adornment Layers of the MapEngine
            GraticuleAdornmentLayer graticuleAdornmentLayer = new GraticuleAdornmentLayer();
            winformsMap1.AdornmentOverlay.Layers.Add("graticule", graticuleAdornmentLayer);

            winformsMap1.Refresh();
        }

        //Displays the world coordinates for the mouse pointer in decimal degrees and Degrees Minutes Seconds form.
        private void winformsMap1_MouseMove(object sender, MouseEventArgs e)
        {
            PointShape worldPointShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, new ScreenPointF(e.X, e.Y), winformsMap1.Width, winformsMap1.Height);
            try
            {
                lblLongitudeDMS.Text = DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(worldPointShape.X);
                lblLatitudeDMS.Text = DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(worldPointShape.Y);

                lblLongitude.Text = System.Convert.ToString(worldPointShape.X);
                lblLatitude.Text = System.Convert.ToString(worldPointShape.Y);
            }
            catch
            {
                lblLongitudeDMS.Text = "N/A";
                lblLatitudeDMS.Text = "N/A";

                lblLongitude.Text = "N/A";
                lblLatitude.Text = "N/A";
            }
            finally { }
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
