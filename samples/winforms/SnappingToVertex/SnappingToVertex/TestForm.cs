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
using ThinkGeo.MapSuite.WinForms;

namespace SnappingToVertex
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

            string fileName2 = @"..\..\data\line.txt";
            StreamReader sr2 = new StreamReader(fileName2);

            //InteractiveOverlay for snapping the mouse pointer to the nearest vertex if within tolerance.
            SnappingToVertexInteractiveOverlay snappingToVertexInteractiveOverlay = new SnappingToVertexInteractiveOverlay();
            //Sets tolerance to 75 meters.
            snappingToVertexInteractiveOverlay.Tolerance = 75;
            snappingToVertexInteractiveOverlay.ToleranceUnit = DistanceUnit.Meter;

            winformsMap1.EditOverlay = snappingToVertexInteractiveOverlay;
           
            winformsMap1.EditOverlay.EditShapesLayer.InternalFeatures.Add("Polygon", new Feature(BaseShape.CreateShapeFromWellKnownData(sr1.ReadLine())));
            winformsMap1.EditOverlay.EditShapesLayer.InternalFeatures.Add("MultiLine", new Feature(BaseShape.CreateShapeFromWellKnownData(sr2.ReadLine())));
            
            winformsMap1.EditOverlay.CanAddVertex = false;
            winformsMap1.EditOverlay.CanDrag = false;
            winformsMap1.EditOverlay.CanRemoveVertex = false;
            winformsMap1.EditOverlay.CanResize = false;
            winformsMap1.EditOverlay.CanRotate = false;
            winformsMap1.EditOverlay.CalculateAllControlPoints();
  
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
