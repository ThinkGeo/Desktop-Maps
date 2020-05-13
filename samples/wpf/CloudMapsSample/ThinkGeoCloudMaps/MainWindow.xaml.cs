/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Linq;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeoCloudMapsSample
{
    public partial class MainWindow : Window
    {
        private string clientId;
        private string clientSecret;

        ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetClientIdSecret();
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            map.MapUnit = GeographyUnit.Meter;
            map.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
            {
                thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay();
            }
            else
            {
                thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(clientId, clientSecret);
            }
            thinkGeoCloudMapsOverlay.WrappingMode = WrappingMode.WrapDateline;
            // Tiles will be cached in the TEMP folder (%USERPROFILE%\AppData\Local\Temp\MapSuite\PersistentCaches) by default if the TileCache property is not set.
            //thinkGeoCloudMapsOverlay.TileCache = new XyzFileBitmapTileCache("ThinkGeoCloudMapsTileCache");
            map.Overlays.Add(thinkGeoCloudMapsOverlay);

            map.CurrentExtent = new ThinkGeo.MapSuite.Shapes.RectangleShape(-13086298.60, 7339062.72, -8111177.75, 2853137.62);
            map.Refresh();
        }


        private void ChangeMapType(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                RadioButton radioButton = sender as RadioButton;
                if (thinkGeoCloudMapsOverlay != null)
                {
                    thinkGeoCloudMapsOverlay.MapType = (ThinkGeoCloudRasterMapsMapType)Enum.Parse(typeof(ThinkGeoCloudRasterMapsMapType), radioButton.Content.ToString());
                    map.Refresh();
                }
            }
        }

        private void GetClientIdSecret()
        {
            var id = ConfigurationManager.AppSettings["ClientId"];
            var secret = ConfigurationManager.AppSettings["ClientSecret"];
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(secret))
            {
                var clientIdSecretInputer = new ClientIdSecretInputer
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                clientIdSecretInputer.ShowDialog();
                clientId = clientIdSecretInputer.ClientId;
                clientSecret = clientIdSecretInputer.ClientSecret;
            }
            else
            {
                clientId = id;
                clientSecret = secret;
            }
        }
    }
}
