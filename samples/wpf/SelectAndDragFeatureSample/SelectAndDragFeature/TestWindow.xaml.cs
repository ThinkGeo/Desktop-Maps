using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace SelectAndDragFeature
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
            wpfMap1.MapUnit = GeographyUnit.DecimalDegree;
            wpfMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            //Adds counties shapefiles.
            ShapeFileFeatureLayer Layer1 = new ShapeFileFeatureLayer(@"..\..\data\counties.shp");
            Layer1.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.LightGreen, GeoColor.StandardColors.Black);
            Layer1.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("Layer1", Layer1);
         
            wpfMap1.Overlays.Add("Layers", layerOverlay);

            //Gets the "Milam" county feature and add it to the EditShapeLayer of the EditOverlay
            Layer1.Open();
            Collection<Feature> features = Layer1.QueryTools.GetFeaturesByColumnValue("Name", "Milam");
            Layer1.Close();
            wpfMap1.EditOverlay.EditShapesLayer.InternalFeatures.Add(features[0]);

            //Sets the properties so that the features are only draggable.
            wpfMap1.EditOverlay.CanAddVertex = false;
            wpfMap1.EditOverlay.CanRemoveVertex = false;
            wpfMap1.EditOverlay.CanReshape = false;
            wpfMap1.EditOverlay.CanResize = false;
            wpfMap1.EditOverlay.CanRotate = false;
            //Sets the edit overlay so that its features are editable.
            wpfMap1.EditOverlay.CalculateAllControlPoints();

            wpfMap1.CurrentExtent = new RectangleShape(-98.6609,31.4118,-94.4971,28.4071);

            wpfMap1.Refresh();
        }

        //Function for getting the extent based on a collection of layers.
        //It gets the overall extent of all the layers.
        private RectangleShape GetFullExtent(GeoCollection<Layer> Layers)
        {
            Collection<BaseShape> rectangleShapes = new Collection<BaseShape>();

            foreach (Layer layer in Layers)
            {
                layer.Open();
                if (layer.HasBoundingBox == true) rectangleShapes.Add(layer.GetBoundingBox());
            }
            return ExtentHelper.GetBoundingBoxOfItems(rectangleShapes);
        }

        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);
            
            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.Width, (float)wpfMap1.Height);

            textBox1.Text = "Long: " + DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(pointShape.X) + 
                          "  Lat: " + DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(pointShape.Y);

           }
        }
}
