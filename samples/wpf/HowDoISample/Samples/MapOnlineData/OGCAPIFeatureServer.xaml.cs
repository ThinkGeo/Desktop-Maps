using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class OGCAPIFeatureServer
    {
        public OGCAPIFeatureServer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var ignLayer = new OgcApiFeatureLayer("https://api-features.ign.es", "namedplace")
            {
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(4326, 3857)
                }
            };

            // Create a new text style and set various settings to make it look good.
            var ignNamedPlacesTextStyle = new TextStyle("etiqueta", new GeoFont("Arial", 14), GeoBrushes.DarkRed)
            {
                MaskType = MaskType.RoundedCorners,
                OverlappingRule = LabelOverlappingRule.NoOverlapping,
                Mask = new AreaStyle(GeoBrushes.WhiteSmoke),
                SuppressPartialLabels = true,
                YOffsetInPixel = -12
            };


            ignLayer.ZoomLevelSet.ZoomLevel13.DefaultPointStyle = PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.DarkRed, 10);
            ignLayer.ZoomLevelSet.ZoomLevel13.DefaultTextStyle = ignNamedPlacesTextStyle;
            ignLayer.ZoomLevelSet.ZoomLevel13.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var overlay = new OgcApiFeaturesOverlay()
            {
                FeatureLayer = ignLayer,
                DrawingBulkCount = 100
            };

            MapView.CenterPoint = new PointShape(235690, 5057360);
            MapView.CurrentScale = 36200;
            MapView.Overlays.Add("LayerOverlay", overlay);

            _ = MapView.RefreshAsync();
        }
    }
}