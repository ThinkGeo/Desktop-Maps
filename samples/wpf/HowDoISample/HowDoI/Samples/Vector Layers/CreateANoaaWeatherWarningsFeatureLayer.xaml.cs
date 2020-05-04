using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for CreateANoaaWeatherWarningsFeatureLayer.xaml
    /// </summary>
    public partial class CreateANoaaWeatherWarningsFeatureLayer : UserControl
    {
        public CreateANoaaWeatherWarningsFeatureLayer()
        {
            InitializeComponent();
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            //Add LayerOverlay to map.
            LayerOverlay noaaWeatherStationOverlay = new LayerOverlay();
            mapView.Overlays.Add(noaaWeatherStationOverlay);

            //Add NoaaWeatherStationFeatureLayer to LayerOverlay.
            NoaaWeatherWarningsFeatureLayer noaaWeatherStationFeatureLayer = new NoaaWeatherWarningsFeatureLayer();
            noaaWeatherStationFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new NoaaWeatherWarningsStyle());
            noaaWeatherStationFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            noaaWeatherStationFeatureLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            noaaWeatherStationOverlay.Layers.Add(noaaWeatherStationFeatureLayer);

            //move map to usa.
            mapView.CurrentExtent = new RectangleShape(-14534042, 9147820, -4906645, 1270446);
            mapView.Refresh();
        }
    }
}
