using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class GPXLayer : UserControl
    {
        public GPXLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            LayerOverlay gpxOverlay = new LayerOverlay();
            mapView.Overlays.Add(gpxOverlay);

            GpxFeatureLayer gpxLayer = new GpxFeatureLayer(@"../../../Data/Hike_Bike.gpx");
            gpxLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            gpxLayer.Open();

            gpxLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            gpxLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.Black);

            gpxOverlay.Layers.Add("Hike Bike Trails", gpxLayer);

            mapView.CurrentExtent = gpxLayer.GetBoundingBox();

            mapView.Refresh();
        }
    }
}
