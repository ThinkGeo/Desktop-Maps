using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display an Oracle Spatial layer on the map.
    /// </summary>
    public partial class DisplayTableFromOracle : IDisposable
    {
        private bool _initialized;

        public DisplayTableFromOracle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the Oracle Spatial layer to the map.
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            Map.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache.
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            var countriesOverlay = new LayerOverlay();
            countriesOverlay.TileType = TileType.SingleTile;
            Map.Overlays.Add(countriesOverlay);

            // Create the Oracle Spatial layer. The COUNTRIES02 table is stored in srid 4326 (decimal
            // degrees) while our background is srid 3857 (spherical mercator), so we reproject on the fly.
            // Connection string is the GDAL OCI format: OCI:user/password@//host/service.
            var countriesLayer = new OracleFeatureLayer(
                "OCI:system/password@//54.241.242.131/XEPDB1",
                "COUNTRIES02",
                "OGR_FID")
            {
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(4326, 3857)
                }
            };

            // Add the layer to the overlay we created earlier.
            countriesOverlay.Layers.Add("Countries", countriesLayer);

            // Create an area style on zoom level 1 and then apply it to all zoom levels up to 20.
            countriesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle =
                new AreaStyle(new GeoPen(GeoColors.White, 1), new GeoSolidBrush(new GeoColor(120, GeoColors.MidnightBlue)));
            countriesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Open the layer and set the map view current extent to the bounding box of the layer.
            countriesLayer.Open();
            Map.CurrentExtent = countriesLayer.GetBoundingBox();

            // Refresh the map.
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
