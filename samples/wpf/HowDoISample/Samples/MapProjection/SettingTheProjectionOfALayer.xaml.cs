﻿using System;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to automatically reproject a layer using the ProjectionConverter class
    /// </summary>
    public partial class SettingTheProjectionOfALayer : IDisposable
    {
        public SettingTheProjectionOfALayer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
                var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudVectorMapsMapType.Light,
                    // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                    TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
                };
                MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

                // Set the Map Unit to meters (Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

                // Create an overlay that we can add feature layers to, and add it to the MapView
                var subdivisionsOverlay = new LayerOverlay();
                MapView.Overlays.Add("Frisco Subdivisions Overlay", subdivisionsOverlay);

                // Reproject a shapefile and set the extent
                await ReprojectFeaturesFromShapefileAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Use the ProjectionConverter class to reproject features in a ShapeFileFeatureLayer
        /// </summary>
        private async Task ReprojectFeaturesFromShapefileAsync()
        {
            // Create a feature layer to hold the Frisco subdivisions data
            var subdivisionsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Subdivisions.shp");

            // Create a new ProjectionConverter to convert between Texas North Central (2276) and Spherical Mercator (3857)
            var projectionConverter = new ProjectionConverter(2276, 3857);
            subdivisionsLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Add a style for drawing Frisco subdivisions polygons
            subdivisionsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

            // Apply the styles across all zoom levels
            subdivisionsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Get the overlay we prepared from the MapView, and add the subdivisions ShapeFileFeatureLayer to it
            var subdivisionsOverlay = (LayerOverlay)MapView.Overlays["Frisco Subdivisions Overlay"];
            subdivisionsOverlay.Layers.Clear();
            subdivisionsOverlay.Layers.Add("Frisco Subdivisions", subdivisionsLayer);

            // Set the map to the extent of the subdivisions features and refresh the map
            subdivisionsLayer.Open();
            MapView.CurrentExtent = subdivisionsLayer.GetBoundingBox();
            subdivisionsLayer.Close();

            await MapView.RefreshAsync();
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