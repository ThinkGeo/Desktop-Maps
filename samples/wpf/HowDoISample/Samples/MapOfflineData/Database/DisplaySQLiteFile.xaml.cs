using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a SQLite Layer on the map
    /// </summary>
    public partial class DisplaySQLiteFile : IDisposable
    {

        private bool _initialized;
        public DisplaySQLiteFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the SQLite layer to the map
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
            var restaurantsOverlay = new LayerOverlay();
            Map.Overlays.Add(restaurantsOverlay);

            // Create the new layer and set the projection as the data is in srid 2276 as our background is srid 3857 (spherical mercator).
            var restaurantsLayer = new SqliteFeatureLayer(@"Data Source=./Data/SQLite/frisco-restaurants.sqlite;", "restaurants", "id", "geometry")
            {
                FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
            };

            // Add the layer to the overlay we created earlier.
            restaurantsOverlay.Layers.Add("Frisco Restaurants", restaurantsLayer);

            // Create a new text style and set various settings to make it look good.
            var textStyle = new TextStyle("Name", new GeoFont("Arial", 12), GeoBrushes.Black)
            {
                MaskType = MaskType.RoundedCorners,
                OverlappingRule = LabelOverlappingRule.NoOverlapping,
                Mask = new AreaStyle(GeoBrushes.WhiteSmoke),
                SuppressPartialLabels = true,
                YOffsetInPixel = -5
            };

            // Set a point style and the above text style to zoom level 1 and then apply it to all zoom levels up to 20.
            restaurantsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Green, new GeoPen(GeoColors.White, 2));
            restaurantsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = textStyle;
            restaurantsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Set the map view current extent to a bounding box that shows just a few restaurants.  
            Map.CenterPoint = new PointShape(-10776470, 3915060);
            Map.CurrentScale = 4100;
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