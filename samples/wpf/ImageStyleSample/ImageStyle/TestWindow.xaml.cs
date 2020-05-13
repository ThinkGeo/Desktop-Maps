using System.Windows;
using System.Windows.Input;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace ImageStyle
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
            //Sets the correct map unit and the extent of the map.
            wpfMap1.MapUnit = GeographyUnit.DecimalDegree;
            wpfMap1.CurrentExtent = new RectangleShape(-96.982,32.7925,-96.9657,32.7808);
            BackgroundOverlay backGroundOverlay = new BackgroundOverlay();
            backGroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.StandardColors.Blue);
            wpfMap1.BackgroundOverlay = backGroundOverlay;

            ShapeFileFeatureLayer waterLayer = new ShapeFileFeatureLayer(@"..\..\Data\Water.shp");
            ImageAreaStyle waterImageAreaStyle = new ImageAreaStyle(new GeoImage(@"..\..\Data\Water.png"));
            waterLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(waterImageAreaStyle);
            waterLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer forestLayer = new ShapeFileFeatureLayer(@"..\..\Data\Forest.shp");
            ImageAreaStyle forestImageAreaStyle = new ImageAreaStyle(new GeoImage(@"..\..\Data\Forest.png"));
            forestLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(forestImageAreaStyle);
            forestLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer streetLayer = new ShapeFileFeatureLayer(@"..\..\Data\Roads.shp");
            ImageLineStyle streetLineAreaStyle = new ImageLineStyle(new GeoImage(@"..\..\Data\Pavement.png"),3);
            streetLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(streetLineAreaStyle);

            TextStyle textStyle = new TextStyle("name", new GeoFont("Arial", 10), new GeoSolidBrush(GeoColor.StandardColors.Maroon));
            textStyle.HaloPen = new GeoPen(GeoColor.StandardColors.White, 3);
            streetLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);

            streetLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add(waterLayer);
            staticOverlay.Layers.Add(forestLayer);
            staticOverlay.Layers.Add(streetLayer);
           
            wpfMap1.Overlays.Add(staticOverlay);
          
            wpfMap1.Refresh();
        }

       
        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);
            
            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.Width, (float)wpfMap1.Height);

            textBox1.Text = "X: " + DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(pointShape.X) +
                          "  Y: " + DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(pointShape.Y);
        }
    }
}
