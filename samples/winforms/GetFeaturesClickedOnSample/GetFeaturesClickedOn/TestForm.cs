using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;


namespace GetFeaturesClickedOn
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
            winformsMap1.CurrentExtent = new RectangleShape(-97.7583, 30.2714, -97.7444, 30.2632);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.StandardColors.LightSlateGray);

            ShapeFileFeatureLayer streetLayer = new ShapeFileFeatureLayer(@"../../data/streets.shp");
            streetLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = WorldStreetsLineStyles.RoadFill(12);
            streetLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("StreetLayer", streetLayer);
            winformsMap1.Overlays.Add("LayerOverlay", layerOverlay);

            //InMemoryFeature to show the selected feature (the feature clicked on).
            InMemoryFeatureLayer selectLayer = new InMemoryFeatureLayer();
            selectLayer.Open();
            selectLayer.Columns.Add(new FeatureSourceColumn("FENAME"));
            selectLayer.Close();
            selectLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromArgb(150, GeoColor.StandardColors.Red), 10, true);
            selectLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = WorldStreetsTextStyles.Poi("FENAME", 8, -12);
            selectLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay selectOverlay = new LayerOverlay();
            selectOverlay.Layers.Add("SelectLayer", selectLayer);
            winformsMap1.Overlays.Add("SelectOverlay", selectOverlay);

            winformsMap1.Refresh();
        }

        private void winformsMap1_MapClick(object sender, MapClickWinformsMapEventArgs e)
        {
            //Here we use a buffer of 15 in screen coordinate. This means that regardless of the zoom level, we will always find the nearest feature
            //within 15 pixels to where we clicked.
            int screenBuffer = 15;

            //Logic for converting screen coordinate values to world coordinate for the spatial query. Notice that the distance buffer for the spatial query
            //will change according to the zoom level while it remains the same for the screen buffer distance.
            ScreenPointF clickedPointF = new ScreenPointF(e.ScreenX, e.ScreenY);
            ScreenPointF bufferPointF = new ScreenPointF(clickedPointF.X + screenBuffer, clickedPointF.Y);

            double distanceBuffer = ExtentHelper.GetWorldDistanceBetweenTwoScreenPoints(winformsMap1.CurrentExtent, clickedPointF, bufferPointF,
                                                                winformsMap1.Width, winformsMap1.Height, winformsMap1.MapUnit, DistanceUnit.Meter);

            ShapeFileFeatureLayer streetLayer = (ShapeFileFeatureLayer)winformsMap1.FindFeatureLayer("StreetLayer");
            Collection<string> columnNames = new Collection<string>();
            columnNames.Add("FENAME");

            Collection<Feature> features = streetLayer.FeatureSource.GetFeaturesNearestTo(new PointShape(e.WorldX, e.WorldY), winformsMap1.MapUnit, 1, columnNames, distanceBuffer, DistanceUnit.Meter);


            //Adds the feature clicked on to the selected layer to be displayed as highlighed and with the name labeled.
            InMemoryFeatureLayer selectLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("SelectLayer");

            selectLayer.InternalFeatures.Clear();

            if (features.Count > 0)
            {
                selectLayer.InternalFeatures.Add(features[0]);
            }

            //Refreshes only the select layer.
            winformsMap1.Refresh(winformsMap1.Overlays["SelectOverlay"]);

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
