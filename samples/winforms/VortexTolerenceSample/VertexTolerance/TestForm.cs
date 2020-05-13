/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace VertexTolerance
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

            string fileName1 = @"..\..\data\polygon.txt";
            StreamReader sr1 = new StreamReader(fileName1);

            //VertexToleranceEditInteractiveOverlay to keep a set tolerance between the dragged control point and other control points of the edit shape.
            VertexToleranceEditInteractiveOverlay vertexToleranceEditInteractiveOverlay = new VertexToleranceEditInteractiveOverlay();

            vertexToleranceEditInteractiveOverlay.EditShapesLayer.InternalFeatures.Add("Polygon", new Feature(BaseShape.CreateShapeFromWellKnownData(sr1.ReadLine())));

            //Sets the PointStyle for the non dragged control points.
            vertexToleranceEditInteractiveOverlay.ControlPointStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.StandardColors.PaleGoldenrod), new GeoPen(GeoColor.StandardColors.Black), 8);
            //Sets the PointStyle for the dragged control points.
            vertexToleranceEditInteractiveOverlay.DraggedControlPointStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.StandardColors.Red), new GeoPen(GeoColor.StandardColors.Orange, 2), 10);

            vertexToleranceEditInteractiveOverlay.CanDrag = false;

            vertexToleranceEditInteractiveOverlay.Tolerance = 100;
            vertexToleranceEditInteractiveOverlay.ToleranceUnit = DistanceUnit.Meter;
            vertexToleranceEditInteractiveOverlay.CalculateAllControlPoints();



            winformsMap1.EditOverlay = vertexToleranceEditInteractiveOverlay;

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

        private void winformsMap1_MouseUp(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, new ScreenPointF(e.X, e.Y), winformsMap1.Width, winformsMap1.Height);

            System.Diagnostics.Debug.WriteLine(pointShape.X + " " + pointShape.Y);
        }
    }
}
