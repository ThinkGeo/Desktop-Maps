/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
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

namespace MultiLineLabeling
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
            wpfMap1.CurrentExtent = new RectangleShape(-10780647.0302819, 3910578.25699327, -10776995.7509839, 3906937.1789594);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            StreamReader streamReader = new StreamReader(@"../../Data/MultilineShape.txt");
            String WKT = streamReader.ReadLine();
            MultilineShape multilineShape = (MultilineShape)BaseShape.CreateShapeFromWellKnownData(WKT);
            Feature feature = new Feature(multilineShape);
            feature.ColumnValues.Add("Name", "My Label");
            
            InMemoryFeatureLayer inMemoryFeatureLayer = new InMemoryFeatureLayer();
            inMemoryFeatureLayer.Open();
            inMemoryFeatureLayer.Columns.Add(new FeatureSourceColumn("Name"));
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.Red, 4, true);
            //Adds the custom TextStyle for displaying the text at each LineShape making up the MultilineShape for the feature to label.
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = new MultiLineTextStyle("Name", new GeoFont("Arial", 12), new GeoSolidBrush(GeoColor.StandardColors.Navy));
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            inMemoryFeatureLayer.InternalFeatures.Add(feature);

            LayerOverlay dynamicOverlay = new LayerOverlay();
            dynamicOverlay.TileType = TileType.SingleTile;
            dynamicOverlay.Layers.Add(inMemoryFeatureLayer);
            wpfMap1.Overlays.Add(dynamicOverlay);
          
            wpfMap1.Refresh();
        }

       
        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);
            
            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.Width, (float)wpfMap1.Height);

            textBox1.Text = $"X: {pointShape.X}  Y: {pointShape.Y}";
        }
    }
}
