using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace DraggableLabels
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private static readonly RectangleShape maximumExtent = new RectangleShape(-180, 90, 180, -90);

        public TestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Sets the correct map unit and the extent of the map.
            wpfMap1.MapUnit = GeographyUnit.DecimalDegree;
            wpfMap1.CurrentExtent = new RectangleShape(-128, 51, -65, 19);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            //Adds the countries shapefile
            ShapeFileFeatureLayer shapeFileFeatureLayer1 = new ShapeFileFeatureLayer(@"..\..\Data\Countries02.shp");
            shapeFileFeatureLayer1.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = WorldStreetsAreaStyles.Pitch();
            shapeFileFeatureLayer1.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Adds the Shapefile MajorCities as a ShapeFileFeatureLayer between zoom levels 01 and 04.
            ShapeFileFeatureLayer shapeFileFeatureLayer2 = new ShapeFileFeatureLayer(@"..\..\Data\MajorCities.shp");
            shapeFileFeatureLayer2.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.StandardColors.Turquoise, 8, GeoColor.StandardColors.Black);
            shapeFileFeatureLayer2.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Adds the ShapeFileFeatureLayer to an LayerOverlay.
            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(shapeFileFeatureLayer1);
            layerOverlay.Layers.Add(shapeFileFeatureLayer2);
            wpfMap1.Overlays.Add(layerOverlay);

            //SimpleMarkerOverlay for dragging labels.
            SimpleMarkerOverlay simpleMarkerOverlay = new SimpleMarkerOverlay();
            simpleMarkerOverlay.DragMode = MarkerDragMode.Drag;

            wpfMap1.Overlays.Add("SimpleMarkerOverlay", simpleMarkerOverlay);

            //Gets all the features of the cities shapefile and loops thru them to add the label as a Marker of the SimpleMarkerOverlay.
            shapeFileFeatureLayer2.Open();
            Collection<Feature> features = shapeFileFeatureLayer2.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns);
            shapeFileFeatureLayer2.Close();

            foreach (Feature feature in features)
            {
                PointShape pointShape = (PointShape)feature.GetShape();
                Marker marker = new Marker(pointShape.X, pointShape.Y);
                marker.ImageSource = null;
                marker.Content = feature.ColumnValues["AREANAME"];
                marker.FontSize = 14;
                marker.FontStyle = FontStyles.Oblique;
                simpleMarkerOverlay.Markers.Add(marker);
            }

            wpfMap1.Refresh();
        }


        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);

            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.ActualWidth, (float)wpfMap1.ActualHeight);

            if (maximumExtent.Contains(pointShape))
            {
                textBox1.Text = "X: " + DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(pointShape.X) +
                          "  Y: " + DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(pointShape.Y);
            }
        }
    }
}
