/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite;

namespace EditingRectangles
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            // Set the full extent and the background color
            winformsMap1.CurrentExtent = new RectangleShape(-16143935, 10161889, 9920479, -5766363);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            winformsMap1.EditOverlay = new CustomEditInteractiveOverlay();

            Feature rectangle = new Feature(new RectangleShape(-5343336, 0, 5788614, -4865942));
            // Set the value of column "Edit" to "rectangle", so this shape will be editing by custom way.
            rectangle.ColumnValues.Add("Edit", "rectangle");
            winformsMap1.EditOverlay.EditShapesLayer.InternalFeatures.Add(rectangle);

            Feature polygon = new Feature(new PolygonShape("POLYGON((-13358338.8951928 6274861.39400658,-7347086.39235606 5465442.18332275,-9016878.75425516 2999080.94347064,-10797990.6069475 2753408.10936498,-13247019.4043996 3895303.96339389,-13914936.3491592 4865942.27950318,-13358338.8951928 6274861.39400658))"));
            // Set the value of column "Edit" to "polygon" not "rectangle" so this shape will be editing by original way.
            polygon.ColumnValues.Add("Edit", "polygon");
            winformsMap1.EditOverlay.EditShapesLayer.InternalFeatures.Add(polygon);

            winformsMap1.EditOverlay.EditShapesLayer.Open();
            winformsMap1.EditOverlay.EditShapesLayer.Columns.Add(new FeatureSourceColumn("Edit"));
            winformsMap1.EditOverlay.EditShapesLayer.Close();
            winformsMap1.EditOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = new TextStyle("Edit", new GeoFont("Arial", 18), new GeoSolidBrush(GeoColor.StandardColors.Black));
            winformsMap1.EditOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            winformsMap1.EditOverlay.CalculateAllControlPoints();

            // Draw the map image on the screen
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
