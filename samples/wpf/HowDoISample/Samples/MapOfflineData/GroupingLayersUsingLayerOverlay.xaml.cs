using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to group layers into logical groups using LayerOverlays.
    /// </summary>
    public partial class GroupingLayersUsingLayerOverlay : IDisposable
    {
        public GroupingLayersUsingLayerOverlay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, load landuse and POI layers into a grouped LayerOverlay and display them on the map.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set the map's unit of measurement to meters(Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

                // Add Cloud Maps as a background overlay
                var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudVectorMapsMapType.Light,
                    // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                    TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
                };
                MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

                /**********************
                 * Landuse LayerOverlay
                 **********************/

                // Create cityLimits layer
                var cityLimits = new ShapeFileFeatureLayer(@"./Data/Shapefile/FriscoCityLimits.shp");

                // Style cityLimits layer
                cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.DimGray, 2);
                cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Project cityLimits layer to Spherical Mercator to match the map projection
                cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

                var poiOverlay = new LayerOverlay();
                var landuseOverlay = new LayerOverlay();

                // Add cityLimits layer to the landuseGroup overlay
                landuseOverlay.Layers.Add(cityLimits);

                // Create Parks landuse layer
                var parks = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");

                // Style Parks landuse layer
                parks.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(128, GeoColors.Green), GeoColors.Transparent);
                parks.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Project Parks landuse layer to Spherical Mercator to match the map projection
                parks.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

                // Add Parks landuse layer to the landuseGroup overlay
                landuseOverlay.Layers.Add(parks);

                // Add Landuse overlay to the map
                MapView.Overlays.Add("landuseOverlay", landuseOverlay);

                /******************
                 * POI LayerOverlay
                 ******************/

                // Create Hotel POI layer
                var hotels = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hotels.shp");

                // Style Hotel POI layer
                hotels.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 8, GeoColors.White, 2);
                hotels.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Project Hotels POI layer to Spherical Mercator to match the map projection
                hotels.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

                // Add Hotel POI layer to the poiGroup overlay
                poiOverlay.Layers.Add(hotels);

                // Create School POI layer
                var schools = new ShapeFileFeatureLayer(@"./Data/Shapefile/Schools.shp");

                // Style School POI layer
                schools.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleSquareStyle(GeoColors.Red, 8, GeoColors.White, 2);
                schools.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Project Schools POI layer to Spherical Mercator to match the map projection
                schools.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

                // Add School POI layer to the poiGroup overlay
                poiOverlay.Layers.Add(schools);

                // Add POI overlay to the map
                MapView.Overlays.Add("poiOverlay", poiOverlay);

                // Set the map extent
                cityLimits.Open();
                var cityLimitsBBox = cityLimits.GetBoundingBox();
                MapView.CenterPoint = cityLimitsBBox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(cityLimitsBBox,MapView.ActualWidth,MapView.MapUnit);
                cityLimits.Close();

                ShowPoi.IsChecked = true;
                ShowLandUse.IsChecked = true;

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Show the Landuse overlay
        /// </summary>
        private void ShowLanduseGroup_Checked(object sender, RoutedEventArgs e)
        {
            var landuseOverlay = (LayerOverlay)MapView.Overlays["landuseOverlay"];
            landuseOverlay.IsVisible = true;
        }

        /// <summary>
        /// Show the Landuse overlay
        /// </summary>
        private void ShowLanduseGroup_Unchecked(object sender, RoutedEventArgs e)
        {
            var landuseOverlay = (LayerOverlay)MapView.Overlays["landuseOverlay"];
            landuseOverlay.IsVisible = false;
        }

        /// <summary>
        /// Show the POI overlay
        /// </summary>
        private void ShowPoiGroup_Checked(object sender, RoutedEventArgs e)
        {
            var poiOverlay = (LayerOverlay)MapView.Overlays["poiOverlay"];
            poiOverlay.IsVisible = true;
        }

        /// <summary>
        /// Show the POI overlay
        /// </summary>
        private void ShowPoiGroup_Unchecked(object sender, RoutedEventArgs e)
        {
            var poiOverlay = (LayerOverlay)MapView.Overlays["poiOverlay"];
            poiOverlay.IsVisible = false;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}