using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class GPXLayerSample : UserControl
    {
        public GPXLayerSample()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay gpxOverlay = new LayerOverlay();
            mapView.Overlays.Add(gpxOverlay);

            // Create the new layer and set the projection as the data is in srid 4326 and our background is srid 3857 (spherical mercator).
            GpxFeatureLayer gpxLayer = new GpxFeatureLayer(@"../../../Data/Gpx/Hike_Bike.gpx");
            gpxLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);

            // Add the layer to the overlay we created earlier.
            gpxOverlay.Layers.Add("Hike Bike Trails", gpxLayer);

            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            gpxLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.Black);
            gpxLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Open the layer and set the map view current extent to the bounding box of the layer.  
            gpxLayer.Open();
            mapView.CurrentExtent = gpxLayer.GetBoundingBox();

            // Refresh the map.
            mapView.Refresh();
        }
    }
}
