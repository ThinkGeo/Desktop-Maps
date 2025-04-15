using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to calculate the center point of a feature
    /// </summary>
    public partial class CalculateCenterPoint : IDisposable
    {
        public CalculateCenterPoint()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the censusHousing and centerPointLayer layers
        /// into a grouped LayerOverlay and display it on the map.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
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

            // Create a feature layer to hold the Census Housing data
            var censusHousingLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco 2010 Census Housing Units.shp")
            {
                FeatureSource =
                    {
                        // Project censusHousing layer to Spherical Mercator to match the map projection
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
            };

            // Add a style to use to draw the censusHousing layer
            censusHousingLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            censusHousingLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var censusHousingOverlay = new LayerOverlay();
            censusHousingOverlay.Layers.Add("CensusHousingLayer", censusHousingLayer);
            MapView.Overlays.Add("CensusHousingOverlay", censusHousingOverlay);

            // Create a layer to hold the centerPointLayer and Style it
            var centerPointLayer = new InMemoryFeatureLayer();
            centerPointLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Green, 12, GeoColors.White, 4);
            centerPointLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(64, GeoColors.Green), GeoColors.Black, 2);
            centerPointLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var centerPointOverlay = new LayerOverlay();
            centerPointOverlay.TileType = TileType.SingleTile;
            centerPointOverlay.Layers.Add("CenterPointLayer", centerPointLayer);
            MapView.Overlays.Add("CenterPointOverlay", centerPointOverlay);

            // Set the map extent to the censusHousing layer bounding box
            censusHousingLayer.Open();
            var censusHousingLayerBBox = censusHousingLayer.GetBoundingBox();
            MapView.CenterPoint = censusHousingLayerBBox.GetCenterPoint();
            var MapScale = MapUtil.GetScale(MapView.MapUnit, censusHousingLayerBBox, MapView.MapWidth, MapView.MapHeight);
            MapView.CurrentScale = MapScale * 1.5; // Multiply the current scale by 1.5 to zoom out 50%.
            censusHousingLayer.Close();

            // Add LayerOverlay to Map          
            CentroidCenter.IsChecked = true;

            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Calculates the center point of a feature
        /// </summary>
        /// <param name="feature"> The target feature to calculate its center point</param>
        private async Task CalculateCenterPointAsync(Feature feature)
        {
            var centerPointOverlay = (LayerOverlay)MapView.Overlays["CenterPointOverlay"];
            var centerPointLayer = (InMemoryFeatureLayer)centerPointOverlay.Layers["CenterPointLayer"];

            PointShape centerPoint;

            // Get the CenterPoint of the selected feature
            if (CentroidCenter.IsChecked != null && (bool)CentroidCenter.IsChecked)
            {
                // Centroid, or geometric center, method. Accurate, but can be relatively slower on extremely complex shapes
                centerPoint = feature.GetShape().GetCenterPoint();
            }
            else
            {
                // BoundingBox method. Less accurate, but much faster
                centerPoint = feature.GetBoundingBox().GetCenterPoint();
            }

            // Show the centerPoint on the map
            centerPointLayer.InternalFeatures.Clear();
            centerPointLayer.InternalFeatures.Add("selectedFeature", feature);
            centerPointLayer.InternalFeatures.Add("centerPoint", new Feature(centerPoint));

            // Refresh the overlay to show the results
            await centerPointOverlay.RefreshAsync();

        }

        /// <summary>
        /// Map event that fires whenever the user clicks on the map. Gets the closest feature from the click event and calculates the center point
        /// </summary>
        private void MapView_OnMapClick(object sender, MapClickMapViewEventArgs e)
        {
            var censusHousingOverlay = (LayerOverlay)MapView.Overlays["CensusHousingOverlay"];
            var censusHousingLayer = (ShapeFileFeatureLayer)censusHousingOverlay.Layers["CensusHousingLayer"];

            // Query the censusHousing layer to get the first feature closest to the map click event
            var feature = censusHousingLayer.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1,
                ReturningColumnsType.NoColumns).First();

            _ = CalculateCenterPointAsync(feature);
        }

        /// <summary>
        /// RadioButton checked event that will recalculate the center point so long as a feature was already selected
        /// </summary>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var centerPointOverlay = (LayerOverlay)MapView.Overlays["CenterPointOverlay"];
            var centerPointLayer = (InMemoryFeatureLayer)centerPointOverlay.Layers["CenterPointLayer"];

            // Recalculate the center point if a feature has already been selected
            if (centerPointLayer.InternalFeatures.Contains("selectedFeature"))
            {
                _ = CalculateCenterPointAsync(centerPointLayer.InternalFeatures["selectedFeature"]);
            }
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