using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;


namespace NumberedGrid
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
            winformsMap1.CurrentExtent = new RectangleShape(-125, 47, -67, 25);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));
            winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.None;

            string shapeFilePath = (@"..\..\data\USStates.shp");
            ShapeFileFeatureLayer stateLayer = new ShapeFileFeatureLayer(shapeFilePath);
            stateLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.LightGreen, GeoColor.StandardColors.Black);
            stateLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay stateOverlay = new LayerOverlay();
            stateOverlay.Layers.Add("StateLayer", stateLayer);
            winformsMap1.Overlays.Add("StateOverlay", stateOverlay);

            InMemoryFeatureLayer highlightLayer = new InMemoryFeatureLayer();
            highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(100, GeoColor.StandardColors.DarkGreen));

            InMemoryFeatureLayer gridLayer = new InMemoryFeatureLayer();
            gridLayer.Open();
            gridLayer.Columns.Add(new FeatureSourceColumn("PageNumber", "string", 20));
            gridLayer.Close();

            gridLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            gridLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColor.StandardColors.Red), GeoColor.SimpleColors.Black);
            gridLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.City1("PageNumber");

            LayerOverlay highlightOverlay = new LayerOverlay();
            highlightOverlay.Layers.Add("HighlightLayer", highlightLayer);
            highlightOverlay.Layers.Add("GridLayer", gridLayer);
            winformsMap1.Overlays.Add("HighlightOverlay", highlightOverlay);

            //Creates the numbered grid for the feature id "44" (Georgia). The grid is made of 9 columns and 8 rows.
            CreateGrid("44", 9, 8);

            winformsMap1.Refresh();
        }

        private void CreateGrid(string featureID, int columnCount, int rowCount)
        {
            FeatureLayer stateLayer = winformsMap1.FindFeatureLayer("StateLayer");
            stateLayer.Open();
            Feature feature = stateLayer.QueryTools.GetFeatureById(featureID, new string[0]);
            stateLayer.Close();
            RectangleShape featureBoundingBox = feature.GetBoundingBox();
            featureBoundingBox.ScaleUp(5);

            InMemoryFeatureLayer highlightLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("HighlightLayer");
            highlightLayer.InternalFeatures.Clear();
            highlightLayer.InternalFeatures.Add(featureID, feature);

            InMemoryFeatureLayer gridLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("GridLayer");
            gridLayer.InternalFeatures.Clear();

            Collection<RectangleShape> allPageExtents = GetGridCells(featureBoundingBox, rowCount, columnCount);
            Collection<RectangleShape> allPageIntessectsExtents = GetIntersectionCells(feature.GetShape(), allPageExtents);
            int pageNumber = 1;
            foreach (RectangleShape pageExtent in allPageIntessectsExtents)
            {
                Feature pageFeature = new Feature(pageExtent);
                pageFeature.ColumnValues.Add("PageNumber", pageNumber.ToString());
                pageNumber++;
                gridLayer.InternalFeatures.Add(pageFeature);
            }
            featureBoundingBox.ScaleUp(5);
            winformsMap1.CurrentExtent = featureBoundingBox;
            winformsMap1.Refresh(winformsMap1.Overlays["HighlightOverlay"]);
        }

        private Collection<RectangleShape> GetGridCells(RectangleShape boundingBox, int columnCount, int rowCount)
        {
            double upperLefX = boundingBox.UpperLeftPoint.X;
            double upperLeftY = boundingBox.UpperLeftPoint.Y;
            double width = boundingBox.Width / columnCount;
            double height = boundingBox.Height / rowCount;

            Collection<RectangleShape> returnCells = new Collection<RectangleShape>();
            for (int i = 0; i <= columnCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    double x1 = upperLefX + j * width;
                    double y1 = upperLeftY - i * height;

                    double x2 = x1 + width;
                    double y2 = y1 - height;

                    RectangleShape returnCell = new RectangleShape(x1, y1, x2, y2);
                    returnCells.Add(returnCell);
                }
            }
            return returnCells;
        }

        private Collection<RectangleShape> GetIntersectionCells(BaseShape baseShape, Collection<RectangleShape> gridCells)
        {
            Collection<RectangleShape> returnCells = new Collection<RectangleShape>();
            foreach (RectangleShape gridCell in gridCells)
            {
                if (baseShape.Intersects(gridCell))
                {
                    returnCells.Add(gridCell);
                }
            }
            return returnCells;

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
