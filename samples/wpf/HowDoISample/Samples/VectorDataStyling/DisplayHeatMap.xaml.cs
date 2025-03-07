﻿using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Render a heatmap using a HeatStyle.
    /// </summary>
    public partial class DisplayHeatMap : IDisposable
    {
        public DisplayHeatMap()
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

                // Set the map extent
                MapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

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

                // Apply HeatStyle
                AddHeatStyle(coyoteSightings);

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Create a heat style that bases the color intensity on the proximity of surrounding points
        /// </summary>
        private static void AddHeatStyle(FeatureLayer layer)
        {
            // Create the heat style
            var heatStyle = new HeatStyle(20, 1, DistanceUnit.Kilometer);

            // Add the point style to the collection of custom styles for ZoomLevel 1.
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(heatStyle);

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