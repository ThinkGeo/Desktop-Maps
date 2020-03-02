using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for CreateANoaaWeatherStationFeatureLayer.xaml
    /// </summary>
    public partial class CreateANoaaWeatherStationFeatureLayer : UserControl
    {
        public CreateANoaaWeatherStationFeatureLayer()
        {
            InitializeComponent();
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(baseOverlay);

            //Add LayerOverlay to map.
            LayerOverlay noaaWeatherStationOverlay = new LayerOverlay();
            mapView.Overlays.Add(noaaWeatherStationOverlay);

            //Add NoaaWeatherStationFeatureLayer to LayerOverlay.
            NoaaWeatherStationFeatureLayer noaaWeatherStationFeatureLayer = new NoaaWeatherStationFeatureLayer();
            noaaWeatherStationFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new NoaaWeatherStationStyle());
            noaaWeatherStationFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            noaaWeatherStationFeatureLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            noaaWeatherStationOverlay.Layers.Add(noaaWeatherStationFeatureLayer);

            //move map to usa.
            mapView.CurrentExtent = new RectangleShape(-14534042, 9147820, -4906645, 1270446);
            mapView.Refresh();
        }
    }
}
