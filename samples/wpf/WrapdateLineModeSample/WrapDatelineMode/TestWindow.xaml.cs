/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace WrapDatelineMode
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

            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap1.CurrentExtent = new RectangleShape(-20037508, 8189037, -11667395, -4753233);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            baseOverlay.WrappingMode = WrappingMode.WrapDateline;
            wpfMap1.Overlays.Add(baseOverlay);

            InMemoryFeatureLayer inMemoryFeatureLayer = new InMemoryFeatureLayer();
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle
                                                         (GeoColor.FromArgb(120, GeoColor.StandardColors.Red), 12);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.Transparent, GeoColor.StandardColors.Green, 2);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            inMemoryFeatureLayer.Open();
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(new PointShape(-13176888, 3988590))); //Los Angeles
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(new PointShape(-8236854, 4972687))); //New York
            inMemoryFeatureLayer.Close();

            LayerOverlay dynamicOverlay = new LayerOverlay();
            //Sets the WrapDatelineMode to WrapDateline for the overlay.
            dynamicOverlay.WrappingMode = WrappingMode.WrapDateline;
            dynamicOverlay.Layers.Add(inMemoryFeatureLayer);
            wpfMap1.Overlays.Add("DynamicOverlay", dynamicOverlay);

            wpfMap1.Refresh();
        }



        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);

            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.ActualWidth, (float)wpfMap1.ActualHeight);
            Proj4Projection proj4Projection = new Proj4Projection(3857, 4326);

            double equatorLength = 20037508.2314698 * 2;

            pointShape.X = pointShape.X % equatorLength;
            if (pointShape.X > 20037508.2314698)
                pointShape.X -= equatorLength;
            if (pointShape.X < -20037508.2314698)
                pointShape.X += equatorLength;

            proj4Projection.Open();
            pointShape = (PointShape)proj4Projection.ConvertToExternalProjection(pointShape);

            textBox3.Text = "Long.: " + pointShape.X +
                          "  Lat.: " + pointShape.Y;
        }
    }
}
