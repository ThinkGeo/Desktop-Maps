/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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
        private Projections.CustomRotationProjection customRotationProjection;

        public TestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Sets the correct map unit and the extent of the map.
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap1.CurrentExtent = new RectangleShape(-10782920, 3912077, -10779783, 3910188);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            //Custom projection that will allow to project from State Plane Central North Texas to Spherical Mercator while applying a rotation.
            customRotationProjection = new Projections.CustomRotationProjection(Proj4Projection.GetEsriParametersString(102738),
                Proj4Projection.GetGoogleMapParametersString());

            ShapeFileFeatureLayer streetLayer = new ShapeFileFeatureLayer(@"../../data/Streets_subset.shp");
            streetLayer.DrawingMarginInPixel = 100;
            streetLayer.FeatureSource.Projection = customRotationProjection;
            streetLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle
                (GeoColor.StandardColors.Red, 3, GeoColor.FromArgb(255, GeoColor.StandardColors.Black), 5, true);
            streetLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("StreetLayer", streetLayer);
            wpfMap1.Overlays.Add("StreetOverlay", layerOverlay);

            //Sets the pivot point to be the center of the layer.
            streetLayer.Open();
            customRotationProjection.PivotVertex = new Vertex(streetLayer.GetBoundingBox().GetCenterPoint());
            streetLayer.Close();

            customRotationProjection.Angle = 20;

            wpfMap1.Refresh();
        }

        private void btnRotateClockWise_Click(object sender, RoutedEventArgs e)
        {
            LayerOverlay streetLayerOverlay = (LayerOverlay)wpfMap1.Overlays["StreetOverlay"];
            ShapeFileFeatureLayer streetLayer = (ShapeFileFeatureLayer)streetLayerOverlay.Layers["StreetLayer"];

            //Set the Angle property of RotationProjection. Substract 5 to go clockwise.
            streetLayer.Open();
            customRotationProjection.Angle = customRotationProjection.Angle - 5;
            streetLayer.Close();

            wpfMap1.Refresh(streetLayerOverlay);
        }

        private void btnRotateCounterClockWise_Click(object sender, RoutedEventArgs e)
        {
            LayerOverlay streetLayerOverlay = (LayerOverlay)wpfMap1.Overlays["StreetOverlay"];
            ShapeFileFeatureLayer streetLayer = (ShapeFileFeatureLayer)streetLayerOverlay.Layers["StreetLayer"];

            //Set the Angle property of RotationProjection. Adds 5 to go clockwise.
            streetLayer.Open();
            customRotationProjection.Angle = customRotationProjection.Angle + 5;
            streetLayer.Close();

            wpfMap1.Refresh(streetLayerOverlay);
        }


        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);

            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.ActualWidth, (float)wpfMap1.ActualHeight);

            textBox1.Text = "X: " + Math.Round(pointShape.X) +
                          "  Y: " + Math.Round(pointShape.Y);

        }
    }
}
