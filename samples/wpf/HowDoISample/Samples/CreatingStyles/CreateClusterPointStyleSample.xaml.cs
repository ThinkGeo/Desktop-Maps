using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to cluster point data dynamically using a ClusterPointStyle
    /// </summary>
    public partial class CreateClusterPointStyleSample : IDisposable
    {
        public CreateClusterPointStyleSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Project the layer's data to match the projection of the map
            var coyoteSightings = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Coyote_Sightings.shp")
            {
                FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
            };

            // Add the layer to a layer overlay
            var layerOverlay = new LayerOverlay
            {
                TileType = TileType.SingleTile
            };
            layerOverlay.Layers.Add(coyoteSightings);

            // Add the overlay to the map
            MapView.Overlays.Add(layerOverlay);

            // Apply Cluster Point Style
            AddClusterPointStyle(coyoteSightings);

            // Set the map extent
            MapView.CurrentExtent = new RectangleShape(-10812042.5236828, 3942445.36497713, -10748599.7905585, 3887792.89005685);
            await MapView.RefreshAsync();
        }

        /// <summary>
        /// Create and add a cluster style to the coyote layer
        /// </summary>
        private static void AddClusterPointStyle(FeatureLayer layer)
        {
            // Create the point style that will serve as the basis of the cluster style
            var pointStyle = new PointStyle(new GeoImage(@"./Resources/coyote_paw.png"))
            {
                ImageScale = .25,
                Mask = new AreaStyle(GeoPens.Black, GeoBrushes.White),
                MaskType = MaskType.RoundedCorners
            };

            // Create a text style that will display the number of features within a clustered point
            var textStyle = new TextStyle("FeatureCount", new GeoFont("Segoe UI", 12, DrawingFontStyles.Bold), GeoBrushes.DimGray)
            {
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                YOffsetInPixel = 12
            };

            // Create the cluster point style
            var clusterPointStyle = new ClusterPointStyle(pointStyle, textStyle)
            {
                MinimumFeaturesPerCellToCluster = 2
            };

            // Add the point style to the collection of custom styles for ZoomLevel 1.
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(clusterPointStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the point style on every zoom level on the map.
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
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