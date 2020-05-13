/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System.Collections.ObjectModel;
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

namespace PolygonShapesToMultipolygonShape
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
            wpfMap1.CurrentExtent = new RectangleShape(-10780691.5580783, 3912133.36988498, -10778877.0503783, 3910578.25699327);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            InMemoryFeatureLayer inMemoryFeatureLayer = new InMemoryFeatureLayer();
            AreaStyle areaStyle = new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(150, GeoColor.StandardColors.Green)));
            TextStyle textStyle = new TextStyle("Name", new GeoFont("Arial", 12), new GeoSolidBrush(GeoColor.StandardColors.Black));

            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(areaStyle);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Adds the Polygons from the Well Known Text in text files.
            string[] filePaths = Directory.GetFiles(@"../../Data/", "*.txt");

            inMemoryFeatureLayer.Open();
            inMemoryFeatureLayer.Columns.Add(new FeatureSourceColumn("Name"));
            inMemoryFeatureLayer.EditTools.BeginTransaction();

            int i = 1;
            foreach (string filePath in filePaths)
            {
                StreamReader streamReader = new StreamReader(filePath);
                string WKT = streamReader.ReadLine();
                streamReader.Close();
                PolygonShape polygonShape = new PolygonShape(WKT);
                Feature feature = new Feature(polygonShape);
                feature.ColumnValues["Name"] = "Polygon " + i.ToString();
                inMemoryFeatureLayer.EditTools.Add(feature);
                i = i + 1;
            }

            inMemoryFeatureLayer.EditTools.CommitTransaction();
            inMemoryFeatureLayer.Close();

            LayerOverlay dynamicOverlay = new LayerOverlay();
            dynamicOverlay.Layers.Add("Polygons", inMemoryFeatureLayer);
            wpfMap1.Overlays.Add("DynamicOverlay", dynamicOverlay);

            wpfMap1.Refresh();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //Gets all the PolygonShapes from the InMemoryFeatureLayer and creates a single MultipolygonShape.
            LayerOverlay dynamicOverlay = (LayerOverlay)wpfMap1.Overlays["DynamicOverlay"];
            InMemoryFeatureLayer inMemoryFeatureLayer = (InMemoryFeatureLayer)dynamicOverlay.Layers["Polygons"];

            inMemoryFeatureLayer.Open();

            Collection<Feature> features = inMemoryFeatureLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns);
            Collection<PolygonShape> polygonShapes = new Collection<PolygonShape>();

            foreach (Feature feature in features)
            {
                polygonShapes.Add((PolygonShape)feature.GetShape());
            }

            MultipolygonShape multipolygonShape = new MultipolygonShape(polygonShapes);

            inMemoryFeatureLayer.InternalFeatures.Clear();
            inMemoryFeatureLayer.EditTools.BeginTransaction();

            Feature newFeature = new Feature(multipolygonShape);
            newFeature.ColumnValues["Name"] = "MultiPolygonShape";
            inMemoryFeatureLayer.EditTools.Add(newFeature);

            inMemoryFeatureLayer.EditTools.CommitTransaction();
            inMemoryFeatureLayer.Close();

            wpfMap1.Refresh(dynamicOverlay);

            button1.IsEnabled = false;
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
