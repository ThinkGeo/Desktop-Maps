using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;


namespace EditGeometryOfShapefile
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
            winformsMap1.CurrentExtent = new RectangleShape(-97.8035, 30.2901, -97.797, 30.2861);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.StandardColors.LightSlateGray);

            ShapeFileFeatureLayer layer1 = new ShapeFileFeatureLayer(@"data/Austinstreets.shp", GeoFileReadWriteMode.ReadWrite);
            layer1.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = WorldStreetsLineStyles.RoadFill(12f);
            layer1.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("Streets", layer1);
            winformsMap1.Overlays.Add("StreetsOverlay", layerOverlay);

            winformsMap1.Refresh();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)winformsMap1.Overlays["StreetsOverlay"];
            ShapeFileFeatureLayer layer1 = (ShapeFileFeatureLayer)layerOverlay.Layers["Streets"];

            //Edit the geometry of a street feature.
            layer1.Open();
            layer1.FeatureSource.BeginTransaction();

            Collection<Feature> features = layer1.FeatureSource.GetFeaturesByColumnValue("RECID", "13762", ReturningColumnsType.AllColumns);

            //Gets the MultilineShape and add a vertes
            MultilineShape newMultiLineShape = (MultilineShape)features[0].GetShape();
            newMultiLineShape.Lines[0].Vertices.Add(new Vertex(-97.8015, 30.2880));
            //Sets the Id of the MultilineShape the same as the feature
            newMultiLineShape.Id = features[0].Id;
            //Updates the feature with the new geometry.
            layer1.FeatureSource.UpdateFeature(newMultiLineShape, features[0].ColumnValues);

            layer1.FeatureSource.CommitTransaction();
            layer1.Close();

            winformsMap1.Refresh(layerOverlay);

            btnEdit.Enabled = false;
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
