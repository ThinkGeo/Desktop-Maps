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
        public RenderLines()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, load Frisco Railroad shapefile data and add it to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set the map's unit of measurement to meters(Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

                // Add Cloud Maps as a background overlay
                var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudRasterMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1,
                    // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                    TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
                };
                MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

                // Set the map extent
                MapView.CurrentExtent = new RectangleShape(-10779675.1746605, 3914631.77546835, -10779173.5566652, 3914204.80300804);

                // Create a layer with line data
                var friscoRailroad = new ShapeFileFeatureLayer(@"./Data/Railroad/Railroad.shp");
                var layerOverlay = new LayerOverlay();

                // Project the layer's data to match the projection of the map
                friscoRailroad.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

                // Add the layer to a layer overlay
                layerOverlay.Layers.Add("Railroad", friscoRailroad);

                // Add the overlay to the map
                MapView.Overlays.Add("overlay", layerOverlay);

                RbLineStyle.IsChecked = true;

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private async void RbLineStyle_OnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MapView.Overlays.Count <= 0) return;
                var layerOverlay = (LayerOverlay)MapView.Overlays["overlay"];
                var friscoRailroad = (ShapeFileFeatureLayer)layerOverlay.Layers["Railroad"];

                // Create a line style
                var lineStyle = new LineStyle(new GeoPen(GeoBrushes.DimGray, 10), new GeoPen(GeoBrushes.WhiteSmoke, 6));

                // Add the line style to the collection of custom styles for ZoomLevel 1.
                friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
                friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(lineStyle);

                // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the line style on every zoom level on the map. 
                friscoRailroad.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Refresh the layerOverlay to show the new style
                await layerOverlay.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private async void RbDashedLineStyle_OnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MapView.Overlays.Count <= 0) return;
                var layerOverlay = (LayerOverlay)MapView.Overlays["overlay"];
                var friscoRailroad = (ShapeFileFeatureLayer)layerOverlay.Layers["Railroad"];

                var lineStyle = new LineStyle(
                    outerPen: new GeoPen(GeoColors.Black, 12),
                    innerPen: new GeoPen(GeoColors.White, 6)
                    {
                        DashStyle = LineDashStyle.Custom,
                        DashPattern = { 3f, 3f },
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
                await layerOverlay.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }
    }
}