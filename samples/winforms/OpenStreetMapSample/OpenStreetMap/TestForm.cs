using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Layers;

namespace OpenStreetMap
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
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            OpenStreetMapOverlay osmOvelerlay = new OpenStreetMapOverlay();
            osmOvelerlay.TileCache = new FileBitmapTileCache(@"C:\temp\OpenStreetMapLayerCache1");

            winformsMap1.Overlays.Add(osmOvelerlay);

            winformsMap1.CurrentExtent = new RectangleShape(-4352400.53526627, 8867834.11959479, 10425951.3896003, -2890780.54967516);
            winformsMap1.Refresh();
        }


        private void winformsMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Displays the X and Y in screen coordinates.
            statusStrip1.Items["toolStripStatusLabelScreen"].Text = "X:" + e.X + " Y:" + e.Y;

            //Gets the PointShape in world coordinates from screen coordinates.
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, new ScreenPointF(e.X, e.Y), winformsMap1.Width, winformsMap1.Height);
            Proj4Projection proj4 = new Proj4Projection();
            proj4.InternalProjectionParametersString = Proj4Projection.GetGoogleMapParametersString();
            proj4.ExternalProjectionParametersString = Proj4Projection.GetEpsgParametersString(4326);

            proj4.Open();
            PointShape projPointShape = (PointShape)proj4.ConvertToExternalProjection(pointShape);
            proj4.Close();

            //Displays world coordinates in decimal degrees from the OSM Mercator projection
            statusStrip1.Items["toolStripStatusLabelWorld"].Text = "(world) X:" + Math.Round(projPointShape.X, 4) + " Y:" + Math.Round(projPointShape.Y, 4);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
