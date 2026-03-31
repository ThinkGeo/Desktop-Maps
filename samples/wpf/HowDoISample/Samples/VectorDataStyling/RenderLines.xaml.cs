using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Render lines using a LineStyle.
    /// </summary>
    public partial class RenderLines : IDisposable
    {
        private bool _initialized;

        public RenderLines()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, load Frisco Railroad shapefile data and add it to the map
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // Set the map's unit of measurement to meters(Spherical Mercator)
            Map.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Aerial2_V2_X1,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            Map.CenterPoint = new PointShape(-10779420, 3914420);
            Map.CurrentScale = 2260;

            // Create a layer with line data
            var friscoRailroad = new ShapeFileFeatureLayer(@"./Data/Railroad/Railroad.shp");
            var layerOverlay = new LayerOverlay();

            // Project the layer's data to match the projection of the map
            friscoRailroad.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add the layer to a layer overlay
            layerOverlay.Layers.Add("Railroad", friscoRailroad);

            // Add the overlay to the map
            Map.Overlays.Add("overlay", layerOverlay);

            // Create a line style
            var lineStyle = new LineStyle(new GeoPen(GeoBrushes.DimGray, 10), new GeoPen(GeoBrushes.WhiteSmoke, 6));

            // Add the line style to the collection of custom styles for ZoomLevel 1.
            friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(lineStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the line style on every zoom level on the map. 
            friscoRailroad.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            RbLineStyle.IsChecked = true;

            _initialized = true;
            _ = Map.RefreshAsync();
        }

        private void RbLineStyle_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            if (Map.Overlays.Count <= 0) return;
            var layerOverlay = (LayerOverlay)Map.Overlays["overlay"];
            var friscoRailroad = (ShapeFileFeatureLayer)layerOverlay.Layers["Railroad"];

            // Create a line style
            var lineStyle = new LineStyle(new GeoPen(GeoBrushes.DimGray, 10), new GeoPen(GeoBrushes.WhiteSmoke, 6));

            // Add the line style to the collection of custom styles for ZoomLevel 1.
            friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(lineStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the line style on every zoom level on the map. 
            friscoRailroad.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Refresh the layerOverlay to show the new style
            _ = layerOverlay.RefreshAsync();
        }

        private void RbDashedLineStyle_OnChecked(object sender, RoutedEventArgs e)
        {
            if (Map.Overlays.Count <= 0) return;
            var layerOverlay = (LayerOverlay)Map.Overlays["overlay"];
            var friscoRailroad = (ShapeFileFeatureLayer)layerOverlay.Layers["Railroad"];

            var lineStyle = new LineStyle(
                outerPen: new GeoPen(GeoColors.Black, 12),
                innerPen: new GeoPen(GeoColors.White, 6)
                {
                    DashStyle = LineDashStyle.Custom,
                    DashPattern = { 18f, 18f },
                    StartCap = DrawingLineCap.Flat,
                    EndCap = DrawingLineCap.Flat
                }
            );
            // Add the line style to the collection of custom styles for ZoomLevel 1.
            friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(lineStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the line style on every zoom level on the map. 
            friscoRailroad.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Refresh the layerOverlay to show the new style
            _ = layerOverlay.RefreshAsync();
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