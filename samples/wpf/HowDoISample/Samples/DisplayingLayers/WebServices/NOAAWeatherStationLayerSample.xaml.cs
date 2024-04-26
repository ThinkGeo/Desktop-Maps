using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a NOAA Weather Station Layer on the map
    /// </summary>
    public partial class NoaaWeatherStationLayerSample
    {
        public NoaaWeatherStationLayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the NOAA Weather Station layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.Meter;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the extent to a view of the US
            MapView.CurrentExtent = new RectangleShape(-14927495, 8262593, -6686622, 1827556);
            await MapView.RefreshAsync();

            // Create a new overlay that will hold our new layer and add it to the map.
            var weatherOverlay = new LayerOverlay
            {
                TileType = TileType.SingleTile
            };
            MapView.Overlays.Add("Weather", weatherOverlay);

            // Create the new layer and set the projection as the data is in srid 4326 and our background is srid 3857 (spherical mercator).
            var noaaWeatherStationLayer = new NoaaWeatherStationFeatureLayer
            {
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(4326, 3857)
                }
            };
            // Create the weather stations style and add it on zoom level 1 and then apply it to all zoom levels up to 20.
            noaaWeatherStationLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new NoaaWeatherStationStyle());
            noaaWeatherStationLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the new layer to the overlay we created earlier
            weatherOverlay.Layers.Add(noaaWeatherStationLayer);

            await MapView.RefreshAsync();

            LoadingImage.Visibility = Visibility.Hidden;
        }
    }
}