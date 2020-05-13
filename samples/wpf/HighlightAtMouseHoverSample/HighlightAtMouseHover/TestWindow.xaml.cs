/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace HighlightAtMouseHover
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private PointShape mousePosition = null;
        private DispatcherTimer highlightTimer = null; 

        public TestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Sets the correct map unit and the extent of the map.
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap1.CurrentExtent = new RectangleShape(-14248894, 6621293, -7235766, 2154935);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            LayerOverlay layerOverlay = new LayerOverlay();
            ShapeFileFeatureLayer shapeFileLayer = new ShapeFileFeatureLayer(@"../../data/USStates.shp");
            shapeFileLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(20, GeoColor.StandardColors.LightGreen),
                                                                                                        GeoColor.StandardColors.LightGray);
            shapeFileLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            layerOverlay.Layers.Add("StatesLayer", shapeFileLayer);
            wpfMap1.Overlays.Add("LayerFeatureOverlay", layerOverlay);

            //Highlight layer
            InMemoryFeatureLayer highlightLayer = new InMemoryFeatureLayer();
            highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(120, GeoColor.StandardColors.Red),
                                                                                                        GeoColor.StandardColors.LightGray);
            highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay highlightOverlay = new LayerOverlay();
            highlightOverlay.Layers.Add("HighlightLayer", highlightLayer);
            wpfMap1.Overlays.Add("HighlightOverlay", highlightOverlay);

          
            wpfMap1.Refresh();
        }

       
        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Logic for Highlighting
            mousePosition = wpfMap1.ToWorldCoordinate(e.GetPosition(wpfMap1));

            if (highlightTimer != null)
            {
                highlightTimer.Stop();
            }

            highlightTimer = new DispatcherTimer();
            highlightTimer.Interval = new TimeSpan(1000000);
            highlightTimer.Tick += new System.EventHandler(timer_Tick);
            highlightTimer.Start();   


            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);
            
            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.Width, (float)wpfMap1.Height);

            textBox1.Text = $"X: {pointShape.X}  Y: {pointShape.Y}";

        }

        void timer_Tick(object sender, System.EventArgs e)
        {
            HighlightAFeatureContainingPoint(mousePosition);
            highlightTimer.Stop();
        }

        private void HighlightAFeatureContainingPoint(PointShape point)
        {
            LayerOverlay highlightOverlay = (LayerOverlay)wpfMap1.Overlays["HighlightOverlay"];
            ShapeFileFeatureLayer featureLayer = (ShapeFileFeatureLayer)((LayerOverlay)wpfMap1.Overlays["LayerFeatureOverlay"]).Layers["StatesLayer"];
            InMemoryFeatureLayer highlightLayer = (InMemoryFeatureLayer)highlightOverlay.Layers["HighlightLayer"];

            featureLayer.Open();
            Collection<Feature> features = featureLayer.QueryTools.GetFeaturesContaining(point, ReturningColumnsType.NoColumns);
            featureLayer.Close();

            if (features.Count != 0)
            {
                highlightLayer.Open();
                highlightLayer.InternalFeatures.Clear();
                highlightLayer.InternalFeatures.Add(features[0]);
                highlightLayer.Close();
            }
            else
            {
                highlightLayer.Open();
                highlightLayer.InternalFeatures.Clear();
                highlightLayer.Close();
            }
            highlightOverlay.Refresh();
        }
    }
}
