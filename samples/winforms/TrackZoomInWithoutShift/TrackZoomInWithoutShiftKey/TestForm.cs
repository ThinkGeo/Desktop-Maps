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

namespace TrackZoomInWithoutShiftKey
{
    public partial class TestForm : Form
    {
        ModeInteractiveOverlay modeInteractiveOverlay = null;
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-13914936, 5942074, -7458406, 2875745);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            //Adds the ModeInteractiveOverlay to the InteractiveOverlays collection of the map.
            winformsMap1.InteractiveOverlays.Clear();
            modeInteractiveOverlay = new ModeInteractiveOverlay();
            modeInteractiveOverlay.MapMode = ModeInteractiveOverlay.Mode.TrackZoomIn;
            winformsMap1.InteractiveOverlays.Add(modeInteractiveOverlay);

            winformsMap1.Refresh();
        }

        private void ToolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            foreach (ToolBarButton toolBarButton in ToolBar1.Buttons)
            {
                toolBarButton.Pushed = false;
            }
            switch (e.Button.ToolTipText)
            {
                case "Track Zoom In":
                    ToolBar1.Buttons[0].Pushed = true;
                    modeInteractiveOverlay.MapMode = ModeInteractiveOverlay.Mode.TrackZoomIn;
                    break;
                case "Pan":
                    ToolBar1.Buttons[2].Pushed = true;
                    modeInteractiveOverlay.MapMode = ModeInteractiveOverlay.Mode.Pan;
                    break;
                default:
                    break;
            }
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
