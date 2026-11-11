using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a FileGeoDatabase Layer on the map
    /// </summary>
    public partial class DisplayFileGeoDatabase : IDisposable
    {

        private bool _initialized;
        public DisplayFileGeoDatabase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the FileGeoDatabase layer to the map
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            Map.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoVectorTileOverlay = new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey)
            {
                // Set up the tile cache for the ThinkGeoVectorTileOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_tile_light")
            };
            Map.Overlays.Add(thinkGeoVectorTileOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            var fileGeoDatabaseOverlay = new LayerOverlay();
            fileGeoDatabaseOverlay.TileType = TileType.SingleTile;
            Map.Overlays.Add("overlay", fileGeoDatabaseOverlay);

            // Create the new layer and set the projection as the data is in srid 2276 and our background is srid 3857 (spherical mercator).
            var fileGeoDatabaseFeatureLayer = new FileGeoDatabaseFeatureLayer(@"./Data/FileGeoDatabase/zoning.gdb")
            {
                FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    },
                ActiveLayer = "zoning"
            };

            // Add the layer to the overlay we created earlier.
            fileGeoDatabaseOverlay.Layers.Add("Zoning", fileGeoDatabaseFeatureLayer);

            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            fileGeoDatabaseFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.Black, new GeoSolidBrush(new GeoColor(50, GeoColors.Blue)));
            fileGeoDatabaseFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Open the layer and set the map view current extent to the bounding box of the layer.  
            fileGeoDatabaseFeatureLayer.Open();
            var fileGeoDatabaseFeatureLayerBBox = fileGeoDatabaseFeatureLayer.GetBoundingBox();
            Map.CenterPoint = fileGeoDatabaseFeatureLayerBBox.GetCenterPoint();
            var MapScale = MapUtil.GetScale(Map.MapUnit, fileGeoDatabaseFeatureLayerBBox, Map.MapWidth, Map.MapHeight);
            Map.CurrentScale = MapScale * 1.5; // Multiply the current scale by 1.5 to zoom out 50%.

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}