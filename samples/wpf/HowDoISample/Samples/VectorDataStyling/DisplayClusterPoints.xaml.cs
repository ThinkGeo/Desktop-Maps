using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Demonstrates how to cluster points using a ClusterPointStyle.
    /// </summary>
    public partial class DisplayClusterPoints : IDisposable
    {
        public DisplayClusterPoints()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the map and overlays when the MapView is loaded.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add ThinkGeo Cloud Vector Maps as the background overlay.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Load coyote sightings shapefile (projected to match the map projection).
            var coyoteSightings = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Coyote_Sightings.shp")
            {
                FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
            };

            var layerOverlay = new LayerOverlay {TileType = TileType.SingleTile};
            layerOverlay.Layers.Add(coyoteSightings);
            MapView.Overlays.Add(layerOverlay);

            // Apply Cluster Point Style
            AddClusterPointStyle(coyoteSightings);

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10780320, 3915120);
            MapView.CurrentScale = 288900;

            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Creates and applies a cluster style to the given layer.
        /// </summary>
        private static void AddClusterPointStyle(FeatureLayer layer)
        {
            // Setup the un-clustered point style 
            var unclusteredPointStyle = PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.Orange, 10);

            // Setup the clustered point style (paw icon).
            var clusteredPointStyle = new PointStyle(new GeoImage(@"./Resources/coyote_paw.png"))
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

            // Cluster style definition.
            var clusterPointStyle = new ClusterPointStyle(unclusteredPointStyle, textStyle)
            {
                MinimumFeaturesPerCellToCluster = 2,
                ClusteredPointStyle = clusteredPointStyle
            };

            // Apply cluster style across all zoom levels.
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(clusterPointStyle);
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