/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace WeatherLineStyle
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
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap1.CurrentExtent = new RectangleShape(-5550725, 9115955, 1910721, 3705430);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            LineStyle lineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.Black, 2, false);

            //Cold Front
            InMemoryFeatureLayer inMemoryFeatureLayerColdFront = new InMemoryFeatureLayer();
            customGeoImageLineStyle coldFrontLineStyle = new customGeoImageLineStyle(lineStyle, new GeoImage(@"..\..\data\offset_triangle.png"),
                                                                                                    25, customGeoImageLineStyle.SymbolSide.Right);
            inMemoryFeatureLayerColdFront.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(coldFrontLineStyle);
            inMemoryFeatureLayerColdFront.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            StreamReader streamReader2 = new StreamReader(@"..\..\data\ColdFront2.txt");
            LineShape lineShape2 = new LineShape(streamReader2.ReadLine());
            inMemoryFeatureLayerColdFront.InternalFeatures.Add(new Feature(lineShape2));

            StreamReader streamReader3 = new StreamReader(@"..\..\data\ColdFront3.txt");
            LineShape lineShape3 = new LineShape(streamReader3.ReadLine());
            inMemoryFeatureLayerColdFront.InternalFeatures.Add(new Feature(lineShape3));




            //Warm Front
            InMemoryFeatureLayer inMemoryFeatureLayerWarmFront = new InMemoryFeatureLayer();
            customGeoImageLineStyle warmFrontLineStyle = new customGeoImageLineStyle(lineStyle, new GeoImage(@"..\..\data\offset_circle.png"),
                                                                                                    25, customGeoImageLineStyle.SymbolSide.Right);
            inMemoryFeatureLayerWarmFront.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(warmFrontLineStyle);
            inMemoryFeatureLayerWarmFront.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            StreamReader streamReader5 = new StreamReader(@"..\..\data\WarmFront5.txt");
            LineShape lineShape5 = new LineShape(streamReader5.ReadLine());
            inMemoryFeatureLayerWarmFront.InternalFeatures.Add(new Feature(lineShape5));

            StreamReader streamReader6 = new StreamReader(@"..\..\data\WarmFront6.txt");
            LineShape lineShape6 = new LineShape(streamReader6.ReadLine());
            inMemoryFeatureLayerWarmFront.InternalFeatures.Add(new Feature(lineShape6));

            StreamReader streamReader7 = new StreamReader(@"..\..\data\WarmFront7.txt");
            LineShape lineShape7 = new LineShape(streamReader7.ReadLine());
            inMemoryFeatureLayerWarmFront.InternalFeatures.Add(new Feature(lineShape7));

            StreamReader streamReader8 = new StreamReader(@"..\..\data\WarmFront8.txt");
            LineShape lineShape8 = new LineShape(streamReader8.ReadLine());
            inMemoryFeatureLayerWarmFront.InternalFeatures.Add(new Feature(lineShape8));

            //Occluded Front
            InMemoryFeatureLayer inMemoryFeatureLayerOccludedFront = new InMemoryFeatureLayer();
            customGeoImageLineStyle occludedFrontLineStyle = new customGeoImageLineStyle(lineStyle, new GeoImage(@"..\..\data\offset_triangle_and_circle.png"),
                                                                                                    40, customGeoImageLineStyle.SymbolSide.Right);
            inMemoryFeatureLayerOccludedFront.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(occludedFrontLineStyle);
            inMemoryFeatureLayerOccludedFront.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            StreamReader streamReader9 = new StreamReader(@"..\..\data\OccludedFront9.txt");
            LineShape lineShape9 = new LineShape(streamReader9.ReadLine());
            inMemoryFeatureLayerOccludedFront.InternalFeatures.Add(new Feature(lineShape9));

            StreamReader streamReader10 = new StreamReader(@"..\..\data\OccludedFront10.txt");
            LineShape lineShape10 = new LineShape(streamReader10.ReadLine());
            inMemoryFeatureLayerOccludedFront.InternalFeatures.Add(new Feature(lineShape10));

            StreamReader streamReader11 = new StreamReader(@"..\..\data\OccludedFront11.txt");
            LineShape lineShape11 = new LineShape(streamReader11.ReadLine());
            inMemoryFeatureLayerOccludedFront.InternalFeatures.Add(new Feature(lineShape11));



            LayerOverlay dynamicOverlay = new LayerOverlay();
            dynamicOverlay.Layers.Add("ColdFront", inMemoryFeatureLayerColdFront);
            dynamicOverlay.Layers.Add("WarmFront", inMemoryFeatureLayerWarmFront);
            dynamicOverlay.Layers.Add("OccludedFront", inMemoryFeatureLayerOccludedFront);
            wpfMap1.Overlays.Add("Dynamic", dynamicOverlay);

            wpfMap1.Refresh();
        }


        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);

            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.Width, (float)wpfMap1.Height);

            textBox1.Text = "X: " + pointShape.X + "  Y: " + pointShape.Y;
        }
    }
}
