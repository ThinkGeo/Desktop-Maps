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
using ThinkGeo.MapSuite.WinForms;

namespace TrackedShapesToWKT
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
            winformsMap1.CurrentExtent = new RectangleShape(-10453078, 3636477, -9665471, 3094267);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            //Event handlers for the TrackOverlay.
            winformsMap1.TrackOverlay.TrackEnded += new EventHandler<TrackEndedTrackInteractiveOverlayEventArgs>(trackOverlay_TrackEnded);
            winformsMap1.TrackOverlay.TrackEnding += new EventHandler<TrackEndingTrackInteractiveOverlayEventArgs>(trackOverlay_TrackEnding);
            winformsMap1.TrackOverlay.TrackStarting += new EventHandler<TrackStartingTrackInteractiveOverlayEventArgs>(trackOverlay_TrackStarting);

            winformsMap1.TrackOverlay.TrackMode = TrackMode.Polygon;

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            winformsMap1.Refresh();
        }

        //At the mouse down event when in track polygon mode, we clear the shape already tracked.
        void trackOverlay_TrackStarting(object sender, TrackStartingTrackInteractiveOverlayEventArgs e)
        {
            richTextBoxFinished.Text = "";
            winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            winformsMap1.Refresh(winformsMap1.TrackOverlay);
        }

        //At the double click event to finish tracking the shape, we get the WKT of the result shape.
        void trackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            richTextBoxCurrent.Text = "";
            richTextBoxFinished.Text = e.TrackShape.GetWellKnownText();
        }

        //At mouse move event while the shape is being tracked, we get its WKT.
        void trackOverlay_TrackEnding(object sender, TrackEndingTrackInteractiveOverlayEventArgs e)
        {
            richTextBoxCurrent.Text = e.TrackShape.GetWellKnownText();
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
