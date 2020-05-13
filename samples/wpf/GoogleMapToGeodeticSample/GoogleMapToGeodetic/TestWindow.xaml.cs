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

namespace GoogleMapToGeodetic
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private System.Drawing.Bitmap bitmap = null;
        Proj4Projection proj4 = new Proj4Projection(Proj4Projection.GetGoogleMapParametersString(), Proj4Projection.GetWgs84ParametersString());

        public TestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Sets the correct map unit and the extent of the map.
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.MapTools.PanZoomBar.Visibility = Visibility.Hidden;
            wpfMap1.CurrentExtent = new RectangleShape(-10777282, 3911656, -10775455, 3910330);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            GoogleMapsOverlay googleMapsOverlay = new GoogleMapsOverlay("your id", "your key");
            googleMapsOverlay.MapType = GoogleMapsMapType.Satellite;
            wpfMap1.Overlays.Add(googleMapsOverlay);

            wpfMap1.Refresh();
        }

        private void LoadThinkGeoCloud()
        {
            wpfMap1.Overlays.Clear();

            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap1.MapTools.PanZoomBar.Visibility = Visibility.Hidden;
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret") { MapType = ThinkGeoCloudRasterMapsMapType.Aerial };
            wpfMap1.Overlays.Add(baseOverlay);

            wpfMap1.Refresh();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            //Saves the bitmap for the current extent of the map.
            bitmap = wpfMap1.GetBitmap((int)wpfMap1.ActualWidth, (int)wpfMap1.ActualHeight);
            bitmap.Save(@"../../data/mymap_geo.bmp");

            using (StreamWriter worldFile = new StreamWriter(@"../../data/mymap_geo.bpw"))
            {
                //Calculates the pixel size in the x-direction in map unit.
                double PixelSizeX = wpfMap1.CurrentExtent.Width / wpfMap1.Width;
                //Calculates the pixel size in the y-direction in map unit.
                double PixelSizeY = wpfMap1.CurrentExtent.Height / wpfMap1.Height;

                //Calculates the x-coordinate of the center of the upper left pixel.
                double XCoord = wpfMap1.CurrentExtent.UpperLeftPoint.X + (PixelSizeX / 2);
                //Calculates the y-coordinate of the center of the upper left pixel.
                double YCoord = wpfMap1.CurrentExtent.UpperLeftPoint.Y - (PixelSizeY / 2);

                //Converts to Geodetic the different coordinates and pixels sizes for the world file parameters.
                proj4.Open();
                Vertex geoULVertex = proj4.ConvertToExternalProjection(wpfMap1.CurrentExtent.UpperLeftPoint.X,
                                                                    wpfMap1.CurrentExtent.UpperLeftPoint.Y);
                Vertex geoURVertex = proj4.ConvertToExternalProjection(wpfMap1.CurrentExtent.UpperRightPoint.X,
                                                                    wpfMap1.CurrentExtent.UpperRightPoint.Y);
                Vertex geoLLVertex = proj4.ConvertToExternalProjection(wpfMap1.CurrentExtent.LowerLeftPoint.X,
                                                                    wpfMap1.CurrentExtent.LowerLeftPoint.Y);

                double geoPixelSizeX = Math.Round((geoURVertex.X - geoULVertex.X) / wpfMap1.Width, 12);
                double geoPixelSizeY = Math.Round((geoURVertex.Y - geoLLVertex.Y) / wpfMap1.Height, 12);

                Vertex geoVertex = proj4.ConvertToExternalProjection(XCoord, YCoord);
                proj4.Close();

                //Writes those parameters to the world file.
                worldFile.WriteLine(geoPixelSizeX.ToString());
                worldFile.WriteLine("0");
                worldFile.WriteLine("0");
                worldFile.WriteLine(geoPixelSizeY.ToString());
                worldFile.WriteLine(geoVertex.X);
                worldFile.WriteLine(geoVertex.Y);
            }

            LoadThinkGeoCloud();

            Button1.IsEnabled = false;
        }


        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);

            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.ActualWidth, (float)wpfMap1.ActualHeight);

            textBox1.Text = "X: " + Math.Round(pointShape.X, 4) +
                         "  Y: " + Math.Round(pointShape.Y, 4);

        }
    }
}
