/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace NOAAGlobelWeatherStationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            map.MapUnit = GeographyUnit.Meter;
            map.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            map.Overlays.Add(baseOverlay);

            //Add LayerOverlay to map.
            LayerOverlay noaaWeatherStationOverlay = new LayerOverlay();
            map.Overlays.Add(noaaWeatherStationOverlay);

            //Add NoaaWeatherStationFeatureLayer to LayerOverlay.
            NoaaWeatherStationFeatureLayer noaaWeatherStationFeatureLayer = new NoaaWeatherStationFeatureLayer();
            noaaWeatherStationFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new NoaaWeatherStationStyle());
            noaaWeatherStationFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            noaaWeatherStationFeatureLayer.FeatureSource.Projection = new Proj4Projection(4326, 3857);
            noaaWeatherStationOverlay.Layers.Add(noaaWeatherStationFeatureLayer);
            
            //move map to usa.
            map.CurrentExtent = new RectangleShape(-14534042, 9147820, -4906645, 1270446);
            map.Refresh();
        }
    }
}