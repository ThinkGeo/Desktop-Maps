/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace DraggingIcon
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
            winformsMap1.CurrentExtent = new RectangleShape(-10882493, 3543795, -10879443, 3541706);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            //EditInteractiveOverlay used because it already have the logic for dragging.
            EditInteractiveOverlay editInteractiveOverlay = new EditInteractiveOverlay();

            //Sets the property IsActive for DragControlPointsLayer to false so that the control point (as four arrows) is not visible.
            editInteractiveOverlay.DragControlPointsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.IsActive = false;
            editInteractiveOverlay.DragControlPointsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Sets the property IsActive for all the Styles of EditShapesLayer because we are using a ValueStyle instead.
            editInteractiveOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.IsActive = false;
            editInteractiveOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.IsActive = false;
            editInteractiveOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.IsActive = false;

            //ValueStyle used for displaying the feature according to the value of the column "Type" for displaying with a bus or car icon.
            ValueStyle valueStyle = new ValueStyle();
            valueStyle.ColumnName = "Type";

            PointStyle carPointStyle = new PointStyle(new GeoImage("../../data/car_normal.png"));
            carPointStyle.PointType = PointType.Bitmap;
            PointStyle busPointStyle = new PointStyle(new GeoImage("../../data/bus_normal.png"));
            busPointStyle.PointType = PointType.Bitmap;

            valueStyle.ValueItems.Add(new ValueItem("Car", carPointStyle));
            valueStyle.ValueItems.Add(new ValueItem("Bus", busPointStyle));

            editInteractiveOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);
            editInteractiveOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            editInteractiveOverlay.EditShapesLayer.Open();
            editInteractiveOverlay.EditShapesLayer.Columns.Add(new FeatureSourceColumn("Type"));
            editInteractiveOverlay.EditShapesLayer.Close();

            Feature carFeature = new Feature(new PointShape(-10881558, 3543357));
            carFeature.ColumnValues["Type"] = "Car";

            Feature busFeature = new Feature(new PointShape(-10880679, 3542854));
            busFeature.ColumnValues["Type"] = "Bus";

            editInteractiveOverlay.EditShapesLayer.InternalFeatures.Add("Car", carFeature);
            editInteractiveOverlay.EditShapesLayer.InternalFeatures.Add("Bus", busFeature);

            //Sets the properties of EditInteractiveOverlay to have the appropriate behavior.
            //Make sure CanDrag is set to true.
            editInteractiveOverlay.CanAddVertex = false;
            editInteractiveOverlay.CanDrag = true;
            editInteractiveOverlay.CanRemoveVertex = false;
            editInteractiveOverlay.CanResize = false;
            editInteractiveOverlay.CanRotate = false;
            editInteractiveOverlay.CalculateAllControlPoints();

            winformsMap1.EditOverlay = editInteractiveOverlay;

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
