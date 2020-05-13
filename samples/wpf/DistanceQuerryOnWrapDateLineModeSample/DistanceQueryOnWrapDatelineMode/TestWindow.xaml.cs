/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

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

namespace DistanceQueryOnWrapDatelineMode
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
            //Sets the correct map unit 
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            //Sets the map extent in real coordinates.
            wpfMap1.CurrentExtent = new RectangleShape(-14440660, 12417042, 16006959, -9812068);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            //Sets the WrapDatelineMode to WrapDateline for the overlay.
            baseOverlay.WrappingMode = WrappingMode.WrapDateline;
            wpfMap1.Overlays.Add("WMK", baseOverlay);

            //InMemoryFeatureLayer to put the feature as the result of the spatial query.
            InMemoryFeatureLayer inMemoryFeatureLayer = new InMemoryFeatureLayer();
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle
                (GeoColor.FromArgb(150, GeoColor.StandardColors.Yellow), GeoColor.StandardColors.DarkGray, 2);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            //Sets the WrapDatelineMode to WrapDateline for the overlay with the InMemoryFeatureLayer.
            layerOverlay.WrappingMode = WrappingMode.WrapDateline;
            layerOverlay.Layers.Add("SelectLayer", inMemoryFeatureLayer);
            wpfMap1.Overlays.Add("DynamicOverlay", layerOverlay);

            wpfMap1.Refresh();
        }

        private void wpfMap1_MapClick(object sender, MapClickWpfMapEventArgs e)
        {
            try
            {
                //Uses the WrapDatelineProjection to create the target pointshape for GetFeatureNearestTo spatial query function.
                WrapDatelineProjection wrapDatelineProjection = new WrapDatelineProjection();
                //Sets the HalfExtentWidth of the wrapdateline overlay we want to apply the projection on.
                //Here it is 180 because, the full extent width is 360.
                wrapDatelineProjection.HalfExtentWidth = wpfMap1.Overlays["WMK"].GetBoundingBox().Width / 2;//180;

                //Gets the valid world coordinate regardless to where the user click on the map
                wrapDatelineProjection.Open();
                Vertex projVertex = wrapDatelineProjection.ConvertToExternalProjection(e.WorldX, e.WorldY);
                wrapDatelineProjection.Close();

                //Here we just use the feature source of the shapefile because we don't display it, we just use it for doing the spatial query.
                ShapeFileFeatureSource shapeFileFeatureSource = new ShapeFileFeatureSource(@"../../data/countries02.shp");
                shapeFileFeatureSource.Open();
                //Uses the projected X and Y values for the Spatial Query.
                Collection<Feature> features = shapeFileFeatureSource.GetFeaturesNearestTo(new PointShape(projVertex), wpfMap1.MapUnit, 1, ReturningColumnsType.NoColumns);
                shapeFileFeatureSource.Close();

                LayerOverlay dynamicOverlay = (LayerOverlay)wpfMap1.Overlays["DynamicOverlay"];
                InMemoryFeatureLayer inMemoryFeatureLayer = (InMemoryFeatureLayer)dynamicOverlay.Layers["SelectLayer"];

                //Clears the InMemoryFeatureLayer and add the feature as the result of the spatial query.
                inMemoryFeatureLayer.Open();
                inMemoryFeatureLayer.InternalFeatures.Clear();
                inMemoryFeatureLayer.InternalFeatures.Add(features[0]);
                inMemoryFeatureLayer.Close();

                //Refreshes only the overlay with the updated InMemoryFeatureLayer.
                wpfMap1.Refresh(dynamicOverlay);
            }
            catch { }
        }

        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {

            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);

            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.ActualWidth, (float)wpfMap1.ActualHeight);

            //Uses the WrapDatelineProjection to get the proper decimal degree value on the virtual maps to the right and left of the central map.
            WrapDatelineProjection wrapDatelineProjection = new WrapDatelineProjection();
            //Sets the HalfExtentWidth of the wrapdateline overlay we want to apply the projection on.
            //Here it is 180 because, the full extent width is 360.
            wrapDatelineProjection.HalfExtentWidth = wpfMap1.Overlays["WMK"].GetBoundingBox().Width / 2;//180;

            wrapDatelineProjection.Open();
            Vertex projVertex = wrapDatelineProjection.ConvertToExternalProjection(pointShape.X, pointShape.Y);
            wrapDatelineProjection.Close();

            try
            {
                //Shows the real coordinates.
                textBox1.Text = "real X: " + string.Format("{0}", System.Math.Round(pointShape.X)) +
                             "  real Y: " + string.Format("{0}", System.Math.Round(pointShape.Y));
                //Shows the Long Lat after the WrapDatelineProjection
                textBox3.Text = "Long.: " + System.Math.Round(projVertex.X) +
                              "  Lat.: " + System.Math.Round(projVertex.Y);
            }
            catch { }
        }
    }
}
