using System;
using System.Drawing;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace DynamicTrackShapes
{
    public partial class  TestForm: Form
    {
       
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            //Sets the MapUnit and Extent of the Map.
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-100.4, 50, -60, -10);
           
            //Adds the ECW
            EcwRasterLayer ecwImageLayer = new EcwRasterLayer("../../Data/World.ecw");
            ecwImageLayer.UpperThreshold = double.MaxValue;
            ecwImageLayer.LowerThreshold = 0;
            LayerOverlay imageOverlay = new LayerOverlay();
            imageOverlay.Layers.Add("EcwImageLayer", ecwImageLayer);
            winformsMap1.Overlays.Add(imageOverlay);

            //Sets the events of TrackOverlay
            winformsMap1.TrackOverlay.TrackStarted += new EventHandler<TrackStartedTrackInteractiveOverlayEventArgs>(TrackStarted);
            winformsMap1.TrackOverlay.MouseMoved += new EventHandler<MouseMovedTrackInteractiveOverlayEventArgs>(MouseMoved);
            winformsMap1.TrackOverlay.TrackEnded += new EventHandler<TrackEndedTrackInteractiveOverlayEventArgs>(TrackEnded);

            groupBoxInfo.Visible = false;
          
            winformsMap1.Refresh();
        }

        private void TrackStarted(object sender, TrackStartedTrackInteractiveOverlayEventArgs e)
        {
            //Sets to visible the groupbox with dynamic info of tracked shape when starting shape tracking.
            groupBoxInfo.Visible = true;
        }

        private void MouseMoved(object sender, MouseMovedTrackInteractiveOverlayEventArgs e)
        {
            //Gets the shape as it is being tracked and gets its properties to be displayed in labels og groupbox
            AreaBaseShape  areaBaseShape = (AreaBaseShape)winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures[0].GetShape();
            double Perimeter = Math.Round(areaBaseShape.GetPerimeter(winformsMap1.MapUnit, DistanceUnit.Kilometer));
            double Area = Math.Round(areaBaseShape.GetArea(winformsMap1.MapUnit, AreaUnit.SquareKilometers));
            lblPerimeter.Text = String.Format("{0:0,0}", Perimeter) + " km";
            lblArea.Text = String.Format("{0:0,0}", Area) + " km2";

            //Sets the location (in screen coordinates) of the groupbox according to last moved vertex of the tracked shape (in world coordinates).
            ScreenPointF screenPointF = ExtentHelper.ToScreenCoordinate(winformsMap1.CurrentExtent, e.MovedVertex.X, e.MovedVertex.Y, winformsMap1.Width, winformsMap1.Height);
            groupBoxInfo.Location = new Point((int)screenPointF.X + 30, (int)screenPointF.Y + 10);
        }

        private void TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            //Set to invisible the groupbox with dynamic info after ending the tracking
            groupBoxInfo.Visible = false;
            //Clears the Shapes from TrackOverlay.
            winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
        }

        private void btnTrackPolygon_Click(object sender, EventArgs e)
        {
            winformsMap1.TrackOverlay.TrackMode = TrackMode.Polygon;
        }

        private void btnTrackCircle_Click(object sender, EventArgs e)
        {
            winformsMap1.TrackOverlay.TrackMode = TrackMode.Circle;
        }

   }
}
