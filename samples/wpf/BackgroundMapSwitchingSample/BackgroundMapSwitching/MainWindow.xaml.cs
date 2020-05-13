/*===========================================
   Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
   a Client ID and Secret. These were sent to you via email when you signed up
   with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/
using System.Windows;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.BackgroundMapSwitching
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OverlaySwitcher overlaySwitcher = new OverlaySwitcher();
            overlaySwitcher.SelectedOverlay += OverlaySwitcher_SelectedOverlay;
            overlaySwitcher.Initialize(wpfMap1);
            wpfMap1.MapTools.Add(overlaySwitcher);

            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap1.CurrentExtent = new RectangleShape(-10785241.6495495, 3916508.33762434, -10778744.5183967, 3912187.74540771); ;

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkgeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            thinkgeoCloudMapsOverlay.Name = "ThinkGeo Cloud Maps";
            wpfMap1.Overlays.Add(thinkgeoCloudMapsOverlay);

            OpenStreetMapOverlay openStreetMapOverlay = new OpenStreetMapOverlay();
            openStreetMapOverlay.Name = "Open Street Map";
            openStreetMapOverlay.IsVisible = false;
            wpfMap1.Overlays.Add(openStreetMapOverlay);

            BingMapsTileOverlay bingMapsOverlay = new BingMapsTileOverlay();
            bingMapsOverlay.MapStyle = BingMapsMapType.AerialWithLabels;
            bingMapsOverlay.Name = "Bing Maps Aerial";
            bingMapsOverlay.ApplicationId = "Place Bing Maps Key Here";
            bingMapsOverlay.IsVisible = false;
            wpfMap1.Overlays.Add(bingMapsOverlay);

            GoogleMapsOverlay googleMapsOverlay = new GoogleMapsOverlay();
            googleMapsOverlay.MapType = GoogleMapsMapType.Satellite;
            googleMapsOverlay.Name = "Google Maps Satellite";
            googleMapsOverlay.ClientId = "Place Google Maps ClientId Here";
            googleMapsOverlay.PrivateKey = "Place Google Maps PrivateKey Here";
            googleMapsOverlay.IsVisible = false;
            wpfMap1.Overlays.Add(googleMapsOverlay);
        }

        private void OverlaySwitcher_SelectedOverlay(object sender, SelectedOverlayEventArgs e)
        {
            if (e.Overlay.Name.Equals("ThinkGeo Cloud Maps"))
                wpfMap1.ZoomLevelSet = ThinkGeoCloudMapsOverlay.GetZoomLevelSet();
            else
                wpfMap1.ZoomLevelSet = new SphericalMercatorZoomLevelSet();
        }
    }
}