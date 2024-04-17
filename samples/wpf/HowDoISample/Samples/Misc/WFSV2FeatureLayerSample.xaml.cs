using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.Misc
{
    public partial class WFSV2FeatureLayerSample : UserControl
    {
        public WFSV2FeatureLayerSample()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var helsinkiParcelsLayer = new WfsV2FeatureLayer("https://inspire-wfs.maanmittauslaitos.fi/inspire-wfs/cp/ows", "cp:CadastralParcel");
            helsinkiParcelsLayer.TimeoutInSeconds = 500;
            helsinkiParcelsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.OrangeRed, 4);
            helsinkiParcelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            helsinkiParcelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(3067, 3857);

            Overlay overlay = null;
            overlay = new WfsV2Overlay()
            {
                FeatureLayer = helsinkiParcelsLayer,
                DrawingBulkCount = 500
            };

            mapView.CurrentExtent = new RectangleShape(2775135, 8437158, 2780320, 8433276);
            mapView.Overlays.Add("LayerOverlay", overlay);
            await mapView.RefreshAsync();
        }

        private void SopimusLayer_SendingWebRequest(object sender, SendingWebRequestEventArgs e)
        {
            e.WebRequest.Headers["Authorization"] = "Basic VGhpbmtnZW9vYXBpZmtvZTpUaGlua0dlb1Bhc3N3b3JkITIzNA==";
        }
    }
}