using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a NOAA Weather Warning Layer on the map
    /// </summary>
    public partial class NOAAWeatherWarnings
    {
        public NOAAWeatherWarnings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the NOAA Weather Warning layer to the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
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

            // Create a new overlay that will hold our new layer and add it to the map.
            var noaaWeatherWarningsOverlay = new LayerOverlay();
            noaaWeatherWarningsOverlay.TileType = TileType.SingleTile;
            MapView.Overlays.Add("Noaa Weather Warning", noaaWeatherWarningsOverlay);

            // Create the new layer and set the projection as the data is in srid 4326 and our background is srid 3857 (spherical mercator).
            var noaaWeatherWarningsFeatureLayer = new NoaaWeatherWarningsFeatureLayer
            {
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(4326, 3857)
                }
            };

            // Add the new layer to the overlay we created earlier
            noaaWeatherWarningsOverlay.Layers.Add("Noaa Weather Warning", noaaWeatherWarningsFeatureLayer);

            // Get the layers feature source and set up an event that will refresh the map when the data refreshes
            var featureSource = (NoaaWeatherWarningsFeatureSource)noaaWeatherWarningsFeatureLayer.FeatureSource;

            // Create the weather warnings style and add it on zoom level 1 and then apply it to all zoom levels up to 20.
            noaaWeatherWarningsFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new NoaaWeatherWarningsStyle());
            noaaWeatherWarningsFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Set the extent to a view of the US
            MapView.CenterPoint = new PointShape(-10807050, 5045070);
            MapView.CurrentScale = 34016000;

            // Add a PopupOverlay to the map, to display feature information
            var popupOverlay = new PopupOverlay();
            MapView.Overlays.Add("Info Popup Overlay", popupOverlay);

            featureSource.Open();
            if (featureSource.GetCount() > 0)
            {
                LoadingImage.Visibility = Visibility.Hidden;
            }

            _ = MapView.RefreshAsync();
        }

        private void MapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            // Get the parks layer from the MapView
            var weatherWarnings = MapView.FindFeatureLayer("Noaa Weather Warning");

            // Find the feature that was clicked on by querying the layer for features containing the clicked coordinates            
            var selectedFeatures = weatherWarnings.QueryTools.GetFeaturesContaining(e.WorldLocation, ReturningColumnsType.AllColumns);

            // If a feature was selected, get the data from it and display it
            if (selectedFeatures != null)
            {
                _ = DisplayFeatureInfoAsync(selectedFeatures);
            }
        }

        private async Task DisplayFeatureInfoAsync(Collection<Feature> features)
        {
            if (features.Count > 0)
            {
                var weatherWarningString = new StringBuilder();

                // Each column in a feature is a data attribute
                // Add all attribute pairs to the info string

                foreach (var feature in features)
                {
                    weatherWarningString.AppendLine($"{feature.ColumnValues["TITLE"]}");
                }

                // Create a new popup with the park info string
                var popupOverlay = (PopupOverlay)MapView.Overlays["Info Popup Overlay"];
                var popup = new Popup(features[0].GetShape().GetCenterPoint())
                {
                    Content = weatherWarningString.ToString(),
                    FontSize = 10d,
                    FontFamily = new System.Windows.Media.FontFamily("Verdana")
                };

                // Clear the popup overlay and add the new popup to it
                popupOverlay.Popups.Clear();
                popupOverlay.Popups.Add(popup);

                await popupOverlay.RefreshAsync();
            }
        }
    }
}