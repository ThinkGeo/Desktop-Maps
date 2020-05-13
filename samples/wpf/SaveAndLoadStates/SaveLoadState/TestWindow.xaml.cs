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

namespace SaveLoadState
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        //private byte[] savedStates;

        public TestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Sets the correct map unit and the extent of the map.
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap1.CurrentExtent = new RectangleShape(-14248894.821539, 6621293.72274017, -7235766.90156278, 2154935.91508589);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            //SimpleMarkerOverlay for points.
            SimpleMarkerOverlay simpleMarkerOverlay = new SimpleMarkerOverlay();
            simpleMarkerOverlay.DragMode = MarkerDragMode.Drag;

            wpfMap1.Overlays.Add("SimpleMarkerOverlay", simpleMarkerOverlay);

            wpfMap1.Refresh();

            btnSave.IsEnabled = false;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            //Loads the SimpleMakerOverlay from file
            SimpleMarkerOverlay simpleMarkerOverlay = (SimpleMarkerOverlay)wpfMap1.Overlays["SimpleMarkerOverlay"];

            //Gets the byte array from file to load the SimpleMarkerOverlay
            Byte[] savedStates = ByteArrayFromFile(@"..\..\data\points.dat");
            simpleMarkerOverlay.LoadState(savedStates);
            simpleMarkerOverlay.Refresh();

            btnSave.IsEnabled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SimpleMarkerOverlay simpleMarkerOverlay = (SimpleMarkerOverlay)wpfMap1.Overlays["SimpleMarkerOverlay"];

            //Gets the byte array of SimpleMarkerOverlay to save to file.
            Byte[] savedStates = simpleMarkerOverlay.SaveState();
            ByteArrayToFile(@"..\..\data\points.dat", savedStates);

        }

        public void ByteArrayToFile(string fileName, byte[] byteArray)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            fileStream.Write(byteArray, 0, byteArray.Length);
            fileStream.Close();
        }

        public byte[] ByteArrayFromFile(string fileName)
        {
            byte[] byteArray = null;
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            long numBytes = new FileInfo(fileName).Length;
            byteArray = binaryReader.ReadBytes((int)numBytes);
            return byteArray;
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
