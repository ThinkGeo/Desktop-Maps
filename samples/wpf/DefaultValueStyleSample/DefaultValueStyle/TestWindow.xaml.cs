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

namespace DefaultValueStyle
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private static readonly RectangleShape maximumExtent = new RectangleShape(-20037508.3427891, 20037508.2314698, 20037508.3427891, -20037508.2314698);

        public TestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Sets the correct map unit and the extent of the map.
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap1.CurrentExtent = new RectangleShape(-13469658.3859861, 10750786.5345999, 15807367.6926448, -10750786.5345999);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            ShapeFileFeatureLayer worldCapitalsLayer = new ShapeFileFeatureLayer(@"..\..\Data\WorldCapitals.shp");
            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Draw features based on values.
            CustomDefaultValueStyle customDefaultValueStyle = new CustomDefaultValueStyle();
            customDefaultValueStyle.ColumnName = "POP_RANK";
            //Default style if no value found.
            customDefaultValueStyle.DefaultStyle = PointStyles.CreateSimpleSquareStyle(GeoColor.StandardColors.Navy, 4);
            customDefaultValueStyle.ValueItems.Add(new ValueItem("1", PointStyles.CreateCompoundPointStyle(PointSymbolType.Square, GeoColor.StandardColors.White, GeoColor.StandardColors.Black, 1F, 10F, PointSymbolType.Square, GeoColor.StandardColors.Navy, GeoColor.StandardColors.Transparent, 0F, 6F)));
            customDefaultValueStyle.ValueItems.Add(new ValueItem("2", PointStyles.CreateCompoundPointStyle(PointSymbolType.Square, GeoColor.StandardColors.White, GeoColor.StandardColors.Black, 1F, 8F, PointSymbolType.Square, GeoColor.StandardColors.Navy, GeoColor.StandardColors.Transparent, 0F, 4F)));

            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(customDefaultValueStyle);

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldCaptials", worldCapitalsLayer);
            wpfMap1.Overlays.Add(worldOverlay);

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
                textBox1.Text = $"X: {pointShape.X}  Y: {pointShape.Y}";
            }
        }
    }
}
