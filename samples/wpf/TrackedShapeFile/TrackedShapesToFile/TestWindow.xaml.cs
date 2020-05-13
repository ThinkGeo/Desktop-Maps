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
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace TrackedShapesToFile
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
            wpfMap1.CurrentExtent = new RectangleShape(-10780390.9954531, 3909501.7617133, -10778554.223855, 3907920.42392377);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            //Event handler for the TrackOverlay
            wpfMap1.TrackOverlay.TrackEnded += new EventHandler<TrackEndedTrackInteractiveOverlayEventArgs>(trackOverlay_TrackEnded);

            //Loops thru the files in the Data directory to create the features from WKT. Then, we add the features to the TrackShapeLayer
            //of the TrackOverlay.
            string[] filePaths = Directory.GetFiles(@"../../Data", "*.txt");
            foreach (string filePath in filePaths)
            {
                string WKT = File.ReadAllText(filePath);
                BaseShape baseShape = BaseShape.CreateShapeFromWellKnownData(WKT);

                Feature feature = new Feature(baseShape);
                wpfMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Add(feature);
            }

            wpfMap1.TrackOverlay.TrackMode = TrackMode.Line;

            wpfMap1.Refresh();
        }

        void trackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            //When tracking the shape is finished, we create a file with the WKT of the tracked shape.
            string WKT = e.TrackShape.GetWellKnownText();
            WellKnownType wellKnownType = e.TrackShape.GetWellKnownType();

            Guid guid = Guid.NewGuid();
            File.WriteAllText((@"../../data/" + wellKnownType.ToString() + guid.ToString()), WKT);
        }

        private void radioButtonTrackPolygon_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButtonTrackPolygon.IsChecked == true) wpfMap1.TrackOverlay.TrackMode = TrackMode.Polygon;
        }

        private void radioButtonTrackLine_Checked(object sender, RoutedEventArgs e)
        {
            if (radioButtonTrackLine.IsChecked == true) wpfMap1.TrackOverlay.TrackMode = TrackMode.Line;
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
