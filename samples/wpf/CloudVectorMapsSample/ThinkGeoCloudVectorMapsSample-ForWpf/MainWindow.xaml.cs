using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeoCloudVectorMapsOverlayOnlineSample_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string cloudServiceClientId = "Your-ThinkGeo-Cloud-Service-Cliend-ID";    // Get it from https://cloud.thinkgeo.com
        private const string cloudServiceClientSecret = "Your-ThinkGeo-Cloud-Service-Cliend-Secret";

        private ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay;
        private ThinkGeoCloudMapsOverlay satelliteOverlay;

        private BitmapTileCache bitmapTileCache;
        private VectorTileCache vectorTileCache;

        public MainWindow()
        {
            InitializeComponent();

            WindowState = WindowState.Maximized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            wpfMap.MapUnit = GeographyUnit.Meter;
            wpfMap.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Create the overlay for satellite and overlap with trasparent_background as hybrid map.
            satelliteOverlay = new ThinkGeoCloudMapsOverlay(cloudServiceClientId, cloudServiceClientSecret, ThinkGeoCloudMapsMapType.Aerial)
            {
                IsVisible = false
            };
            wpfMap.Overlays.Add(satelliteOverlay);

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(cloudServiceClientId, cloudServiceClientSecret);
            wpfMap.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            wpfMap.CurrentExtent = new RectangleShape(-12922411.9716445, 8734539.23446158, -8568181.07911278, 687275.650686126);
        }

        private void radioButtonLight_Checked(object sender, RoutedEventArgs e)
        {
            if (thinkGeoCloudVectorMapsOverlay != null)
            {
                satelliteOverlay.IsVisible = false;
                thinkGeoCloudVectorMapsOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Light;
                wpfMap.Refresh();
            }
        }

        private void radioButtionDark_Checked(object sender, RoutedEventArgs e)
        {
            if (thinkGeoCloudVectorMapsOverlay != null)
            {
                satelliteOverlay.IsVisible = false;
                thinkGeoCloudVectorMapsOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Dark;
                wpfMap.Refresh();
            }
        }

        private void radioButtionHybrid_Checked(object sender, RoutedEventArgs e)
        {
            if (thinkGeoCloudVectorMapsOverlay != null)
            {
                satelliteOverlay.IsVisible = true;
                thinkGeoCloudVectorMapsOverlay.MapType = ThinkGeoCloudVectorMapsMapType.TransparentBackground;
                wpfMap.Refresh();
            }
        }

        private void radioButtionCustom_Checked(object sender, RoutedEventArgs e)
        {
            satelliteOverlay.IsVisible = false;

            // Relative Path.
            var uri0 = new Uri("mutedblue.json", UriKind.Relative);
            //// Web Address.
            //var uri1 = new Uri("http://cdn.thinkgeo.com/worldstreets-styles/1.0.0/mutedblue.json");
            //// Absolute Path
            //var uri2 = new Uri("C:/temp/mutedblue.json");

            thinkGeoCloudVectorMapsOverlay.StyleJsonUri = uri0;
            wpfMap.Refresh();
            radioButtionDark.IsChecked = false;
            radioButtonLight.IsChecked = false;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var hyperlink = e.OriginalSource as Hyperlink;
            Process.Start(hyperlink.NavigateUri.ToString());
        }
    }
}