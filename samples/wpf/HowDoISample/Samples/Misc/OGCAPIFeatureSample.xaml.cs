using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.Misc
{
    public partial class OGCAPIFeatureSample : UserControl
    {
        public OGCAPIFeatureSample()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
                ("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var ignLayer = new OgcApiFeaturesLayer("https://api-features.ign.es", "namedplace");
            ignLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            ignLayer.ZoomLevelSet.ZoomLevel13.DefaultPointStyle = PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.Black, 10);
            ignLayer.ZoomLevelSet.ZoomLevel13.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            Overlay overlay = null;
            overlay = new ProgressiveFeaturesOverlay()
            {
                FeatureLayer = ignLayer,
                DrawingBulkCount = 100
            };

            mapView.CurrentExtent = new RectangleShape(237159, 5069993, 247529, 5062228);
            mapView.Overlays.Add("LayerOverlay", overlay);
            await mapView.RefreshAsync();
        }

        private void SopimusLayer_SendingWebRequest(object sender, SendingWebRequestEventArgs e)
        {
            e.WebRequest.Headers["Authorization"] = "Basic VGhpbmtnZW9vYXBpZmtvZTpUaGlua0dlb1Bhc3N3b3JkITIzNA==";
        }
    }
}