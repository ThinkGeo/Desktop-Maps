using System;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace TrackOverlayWithEsc
{
    public partial class TestForm : Form
    {
        private bool cancel;
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            winformsMap1.TrackOverlay.TrackMode = TrackMode.Polygon;

            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            LoadBackgroundLayer();
            winformsMap1.CurrentExtent = new RectangleShape(-90, 30, -40, -20);
            winformsMap1.Refresh();
        }

        //Get the KeyDown event of the map to abort tracking according to the key pressed.
        private void winformsMap1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                cancel = true;
                TrackOverlayTrackEnding();
            }
        }

        private void TrackOverlayTrackEnding()
        {
            if (cancel)
            {
                //Set TrackOverlay to write.
                winformsMap1.TrackOverlay.Lock.EnterWriteLock();
                try
                {
                    int count = winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Count;
                    if (count > 0)
                    {
                        //Remove the feature being currently tracked. The currently tracked feature has the key "InTrackingFeature".
                        winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Remove("InTrackingFeature");
                    }
                }
                finally
                {
                    //Commit the changes.
                    winformsMap1.TrackOverlay.Lock.ExitWriteLock();
                }

                //Call the function to exit from the tracking mode.
                cancel = false;
                if (winformsMap1.TrackOverlay.TrackMode == TrackMode.Polygon)
                {
                    //Polygon needs to call MouseDoubleClick. Event that marks finishing tracking a Polygon. 
                    //Same thing for Line.
                    winformsMap1.TrackOverlay.MouseDoubleClick(new InteractionArguments());
                }
                else
                {
                    //All other shapes have their tracking finalized at MouseUp event.
                    winformsMap1.TrackOverlay.MouseUp(new InteractionArguments());
                }

                winformsMap1.Refresh();
            }
        }

        private void btnTrackPolygon_Click(object sender, EventArgs e)
        {
            winformsMap1.TrackOverlay.TrackMode = TrackMode.Polygon;
        }

        private void btnTrackCircle_Click(object sender, EventArgs e)
        {
            winformsMap1.TrackOverlay.TrackMode = TrackMode.Circle;
        }


        private void LoadBackgroundLayer()
        {
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(@"../../data/Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 1);
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(staticOverlay);
        }
    }
}
