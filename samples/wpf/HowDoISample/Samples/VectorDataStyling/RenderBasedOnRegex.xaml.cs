using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Render certain features using regular expressions.
    /// </summary>
    public partial class RenderBasedOnRegex : IDisposable
    {
        public RenderBasedOnRegex()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay.
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

                // Project the layer's data to match the projection of the map
                var coyoteSightings = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Coyote_Sightings.shp")
                {
                    FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
                };

                // Add the layer to a layer overlay
                var layerOverlay = new LayerOverlay();
                layerOverlay.Layers.Add("coyoteSightings", coyoteSightings);

                // Add the overlay to the map
                MapView.Overlays.Add(layerOverlay);

                // Create a regex style and item that looks for big / large / huge based on the comments
                // from users and draws them differently
                var regexStyle = new RegexStyle
                {
                    ColumnName = "Comments"
                };

                var largeItem = new RegexItem("big|large|huge", new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Red));
                regexStyle.RegexItems.Add(largeItem);

                // We have a default drawing style for every sighting
                var allSightingsStyle = new PointStyle(PointSymbolType.Circle, 5, GeoBrushes.Green);

                // Add the point style to the collection of custom styles for ZoomLevel 1.
                coyoteSightings.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(allSightingsStyle);
                coyoteSightings.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(regexStyle);

                // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the point style on every zoom level on the map.
                coyoteSightings.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Set the map extent
                MapView.CurrentExtent = new RectangleShape(-10781794.4716492, 3917077.66579861, -10775416.8466492, 3913528.63559028);

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
    }
}