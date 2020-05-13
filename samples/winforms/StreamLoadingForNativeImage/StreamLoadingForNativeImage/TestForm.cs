using System;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace  StreamLoading
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-122,74,106,-62);

            NativeImageRasterLayer worldImageLayer = new NativeImageRasterLayer(@"world.tif");
            ((NativeImageRasterSource)(worldImageLayer.ImageSource)).StreamLoading += new EventHandler<StreamLoadingEventArgs>(MainForm_StreamLoading);
            worldImageLayer.UpperThreshold = double.MaxValue;
            worldImageLayer.LowerThreshold = 0;
            worldImageLayer.IsGrayscale = false;

            LayerOverlay testOverlay = new LayerOverlay();
            testOverlay.Layers.Add("worldImageLayer", worldImageLayer);
            winformsMap1.Overlays.Add("testOverlay", testOverlay);

            winformsMap1.Refresh();
        }

        void MainForm_StreamLoading(object sender, StreamLoadingEventArgs e)
        {
            if (e.StreamType == "Image File")
            {
                Stream stream = new FileStream(@"../../data/World.tif", FileMode.Open, FileAccess.Read);
                e.AlternateStream = stream;
            }

            if (e.StreamType == "World File")
            {
                Stream stream = new FileStream(@"../../data/World.tfw", FileMode.Open, FileAccess.Read);
                e.AlternateStream = stream;
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
