/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System.Windows;
using System.Windows.Input;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace CustomParametersProjection
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

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(@"../../data/2000house_districts.shp");
            shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(
                GeoColor.FromArgb(100, GeoColor.StandardColors.Pink), GeoColor.StandardColors.Black);
            shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            Proj4Projection proj4 = new Proj4Projection();
            //Internal projection string from the PRJ file. Note that the false easting value (x_0) has to be expressed in meter for proj4 string.
            string internalProjectionString = "+proj=lcc +lat_1=43 +lat_2=45.5 +lat_0=41.75 +lon_0=-120.5 +x_0=400000 +y_0=0 +ellps=GRS80 +datum=NAD83 +to_meter=0.3048 +no_defs";
            proj4.InternalProjectionParametersString = internalProjectionString;
            //External projection string as Geodetic (WGS84).
            proj4.ExternalProjectionParametersString = Proj4Projection.GetEpsgParametersString(3857);

            proj4.Open();
            Vertex projVertex = proj4.ConvertToExternalProjection(747032, 1297787);
            proj4.Close();

            shapeFileFeatureLayer.FeatureSource.Projection = proj4;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("HouseDistricts", shapeFileFeatureLayer);

            wpfMap1.Overlays.Add(layerOverlay);

            shapeFileFeatureLayer.Open();
            wpfMap1.CurrentExtent = shapeFileFeatureLayer.GetBoundingBox();
            shapeFileFeatureLayer.Close();

            wpfMap1.Refresh();
        }


        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);

            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.ActualWidth, (float)wpfMap1.ActualHeight);

            textBox1.Text = $"X: {pointShape.X}  Y: {pointShape.Y}";

        }
    }
}
