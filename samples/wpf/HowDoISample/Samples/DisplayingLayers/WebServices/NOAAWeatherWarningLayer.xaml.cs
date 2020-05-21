using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class NOAAWeatherWarningLayer : UserControl
    {
        public NOAAWeatherWarningLayer()
        {
            InitializeComponent();
        }

        public delegate void RefreshWeatherWarning();

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            LayerOverlay noaaWeatherWarningsOverlay = new LayerOverlay();

            mapView.Overlays.Add("Noaa Weather Warning", noaaWeatherWarningsOverlay);

            NoaaWeatherWarningsMonitor.WarningsUpdated += NoaaWeatherWarningsMonitor_WarningsUpdated;

            NoaaWeatherWarningsFeatureLayer noaaWeatherWarningsFeatureLayer = new NoaaWeatherWarningsFeatureLayer();

            noaaWeatherWarningsFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new NoaaWeatherWarningsStyle());
            noaaWeatherWarningsFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            noaaWeatherWarningsFeatureLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            noaaWeatherWarningsOverlay.Layers.Add(noaaWeatherWarningsFeatureLayer);

            mapView.CurrentExtent = new RectangleShape(-14534042, 9147820, -4906645, 1270446);
        }
        private void NoaaWeatherWarningsMonitor_WarningsUpdated(object sender, WarningsUpdatedNoaaWeatherWarningsMonitorEventArgs e)
        {
            mapView.Dispatcher.Invoke(new RefreshWeatherWarning(this.UpdateWeatherWarning), new object[] { });
        }

        private void UpdateWeatherWarning()
        {
            mapView.Refresh(mapView.Overlays["Noaa Weather Warning"]);
        }

    }
}
