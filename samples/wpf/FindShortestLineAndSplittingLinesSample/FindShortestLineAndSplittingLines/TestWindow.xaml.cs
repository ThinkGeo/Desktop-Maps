/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System.Linq;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;


namespace CustomRotationProjection
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        
        public TestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            //Add a base map (ThinkGeo's online World Map Kit).
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add( baseOverlay);

            //Build up the test data.
            InMemoryFeatureLayer testData = setupTestData();
            //Make test line red in color.
            testData.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.SimpleColors.Red, 6, false);
            //Make test point dark green in color.
            testData.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.StandardColors.DarkGreen, 12);
            //Apply the above styles to all zoom levels.
            testData.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Create a MapShapeLayer that we can style individually and which will hold the results of the shortest line and split lines.
            MapShapeLayer results = new MapShapeLayer();

            //Add the test data and results layers to a new layer overlay so they will show on top of the base map.
            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("TestData", testData);
            layerOverlay.Layers.Add("Results", results);
            wpfMap1.Overlays.Add("layerOverlay", layerOverlay);

            //Zoom into the test data area.
            wpfMap1.CurrentExtent = testData.GetBoundingBox();
            wpfMap1.Refresh();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {

            //Get a reference to the results MapShapeLayer and clear it so we can display our new results.
            MapShapeLayer results = (MapShapeLayer)((LayerOverlay)wpfMap1.Overlays["layerOverlay"]).Layers["Results"];
            results.MapShapes.Clear();

            //Get a reference to the test data layer.
            InMemoryFeatureLayer testDataLayer = (InMemoryFeatureLayer)((LayerOverlay)wpfMap1.Overlays["layerOverlay"]).Layers["TestData"];
            //Check to see if the test data has been edited.
            if (wpfMap1.EditOverlay.EditShapesLayer.InternalFeatures.Count > 0)
            {
                //Loop through the edited test data and update the test data layer.
                testDataLayer.EditTools.BeginTransaction();
                foreach (Feature feature in wpfMap1.EditOverlay.EditShapesLayer.InternalFeatures)
                {
                    testDataLayer.EditTools.Update(feature.CloneDeep());
                }
                testDataLayer.EditTools.CommitTransaction();
                //Clear out the EditOverlay since we are done editing.
                wpfMap1.EditOverlay.EditShapesLayer.InternalFeatures.Clear();
                //Since we are done editing, turn visibility back on the for the layer overlay so we can see our test data again.
                wpfMap1.Overlays["layerOverlay"].IsVisible = true;
            }

            //Get the test point feature from the test data layer.
            Feature testPointFeature = testDataLayer.QueryTools.GetFeaturesByColumnValue("Name", "TestPoint", ReturningColumnsType.AllColumns)[0];
            //Get the test line feature from the test data layer.
            Feature testLineFeature = testDataLayer.QueryTools.GetFeaturesByColumnValue("Name", "TestLine", ReturningColumnsType.AllColumns)[0];
            //Take the test line feature and create a line shape so we can use line-specific APIs for finding the shortest line and line-on-line.
            LineShape testLine = new MultilineShape(testLineFeature.GetWellKnownText()).Lines[0];
            
            //Calculate the shortest line between our test line and test point.
            LineShape shortestLineResult = testLine.GetShortestLineTo(testPointFeature.GetShape(), GeographyUnit.Meter).Lines[0];
            //Take the result and create a MapShape so we can display the shortest line on the map in a blue color.
            MapShape shortestLine = new MapShape(new Feature(shortestLineResult));
            shortestLine.ZoomLevels.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.SimpleColors.Blue, 4, false);
            shortestLine.ZoomLevels.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            results.MapShapes.Add("ShortestLine", shortestLine);
            string message = "Length of Blue Line is: " + shortestLineResult.GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer).ToString() + " KM" + '\n';

            //Split the test line from the left side based on where the shortest line touches it.
            LineBaseShape leftSideOfLineResult = testLine.GetLineOnALine(new PointShape(testLine.Vertices[0]), new PointShape(shortestLineResult.Vertices[0]));
            //Make sure the split was valid and the closest point wasn't at the beginning or end of the line.
            if (leftSideOfLineResult.Validate(ShapeValidationMode.Simple).IsValid)
            {
                MapShape leftSideOfLine = new MapShape(new Feature(leftSideOfLineResult));
                leftSideOfLine.ZoomLevels.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.Green, 2, false);
                leftSideOfLine.ZoomLevels.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                results.MapShapes.Add("LeftSideofLine", leftSideOfLine);
                message += "Length of Green Line is: " + leftSideOfLineResult.GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer).ToString() + " KM" + '\n';
            }

            //Split the test line from the right side based on where the shortest line touches it.
            LineBaseShape rightSideOfLineResult = testLine.GetLineOnALine(new PointShape(shortestLineResult.Vertices[0]), new PointShape(testLine.Vertices[testLine.Vertices.Count - 1]));
            //Make sure the split was valid and the closest point wasn't at the beginning or end of the line.
            if (rightSideOfLineResult.Validate(ShapeValidationMode.Simple).IsValid)
            {
                MapShape rightSideOfLine = new MapShape(new Feature(rightSideOfLineResult));
                rightSideOfLine.ZoomLevels.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.SimpleColors.Yellow, 2, false);
                rightSideOfLine.ZoomLevels.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                results.MapShapes.Add("RightSideofLine", rightSideOfLine);
                message += "Length of Yellow Line is: " + rightSideOfLineResult.GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer).ToString() + " KM" + '\n';
            }
            //Display the length of each line in kilometers.
            message += "Length of Red Line is: " + testLine.GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer).ToString() + " KM";
            wpfMap1.Refresh();
            MessageBox.Show(message, "Results");            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //Get a reference to our overlay that has the test data in it.
            LayerOverlay layerOverlay = (LayerOverlay)wpfMap1.Overlays["layerOverlay"];
            //Get a refrence to the results MapShapeLayer and clear out any prior results if they exist.
            MapShapeLayer results = (MapShapeLayer)layerOverlay.Layers["Results"];
            results.MapShapes.Clear();
            wpfMap1.Refresh();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //Check to make sure we haven't already added our test features to the EditShapesLayer.
            if (wpfMap1.EditOverlay.EditShapesLayer.InternalFeatures.Count == 0)
            {
                //Get a reference to the test data Layer.
                InMemoryFeatureLayer testData = ((InMemoryFeatureLayer)((LayerOverlay)wpfMap1.Overlays["layerOverlay"]).Layers["TestData"]);
                wpfMap1.EditOverlay.EditShapesLayer.Open();
                //Check to see if this the first time we have ever edited the features. If so, add a column so that our name is stored correctly.
                if (wpfMap1.EditOverlay.EditShapesLayer.Columns.Count == 0)
                {
                    wpfMap1.EditOverlay.EditShapesLayer.Columns.Add(new FeatureSourceColumn("Name"));
                }

                //Loop through all of our test data and add it to the edit overlay so we can interact with the test data.
                foreach (Feature feature in testData.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns))
                {
                    wpfMap1.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.CloneDeep());
                }
                wpfMap1.EditOverlay.CalculateAllControlPoints();
                //Turn off the LayerOverlay with our test data while we are editing it.
                wpfMap1.Overlays["layerOverlay"].IsVisible = false;
                //Refresh the map.
                wpfMap1.Refresh();
            }
        }
        
        private InMemoryFeatureLayer setupTestData()
        {
            //Set up an in-memory layer with our test point and test line.
            InMemoryFeatureLayer testData = new InMemoryFeatureLayer();
            testData.Open();
            //Create a column on the layer to hold the name of the test feature.
            testData.Columns.Add(new FeatureSourceColumn("Name"));
            //Begin adding the test features to the layer.
            testData.EditTools.BeginTransaction();

            //Create a test point based on an X and Y coordinate.
            Feature testPoint = new Feature(-15070.5885108805, 6720330);
            //Set the name of the test feature to "TestPoint".
            testPoint.ColumnValues["Name"] = "TestPoint";
            //Add the test point to the layer.
            testData.EditTools.Add(testPoint);

            //Create a test line based on a well-known text string containing the coordinates of the line.
            Feature testLine = new Feature("MULTILINESTRING((-44015.9315859346 6703106.47996295,-27782.0392653608 6721316.27636318,-9097.74810394567 6702860.68002627,3919.9957380239 6719838.28915546,18775.5387106245 6701877.55436246,30874.3829872785 6719838.28915546,43738.9769016955 6704089.75381025))");

            //Set the name of the test feature to "TestLine".
            testLine.ColumnValues["Name"] = "TestLine";
            //Add the test line to the layer.
            testData.EditTools.Add(testLine);
            //Commit the changes.
            testData.EditTools.CommitTransaction();

            return testData;
        }

      }

}
