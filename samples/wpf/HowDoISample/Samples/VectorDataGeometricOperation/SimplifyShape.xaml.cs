﻿using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to simplify a shape's geometry
    /// </summary>
    public partial class SimplifyShape : IDisposable
    {
        public SimplifyShape()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the cityLimits and simplifyLayer layers into a grouped LayerOverlay and display them on the map.
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

                var cityLimits = new ShapeFileFeatureLayer(@"./Data/Shapefile/FriscoCityLimits.shp");
                var simplifyLayer = new InMemoryFeatureLayer();
                var layerOverlay = new LayerOverlay();

                // Project cityLimits layer to Spherical Mercator to match the map projection
                cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

                // Style cityLimits layer
                cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
                cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Style simplifyLayer
                simplifyLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray);
                simplifyLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add cityLimits layer to a LayerOverlay
                layerOverlay.Layers.Add("cityLimits", cityLimits);

                // Add simplifyLayer to the layerOverlay
                layerOverlay.Layers.Add("simplifyLayer", simplifyLayer);

                // Set the map extent to the cityLimits layer bounding box
                cityLimits.Open();
                MapView.CurrentExtent = cityLimits.GetBoundingBox();
                cityLimits.Close();

                // Add LayerOverlay to Map
                MapView.Overlays.Add("layerOverlay", layerOverlay);

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Simplifies the first feature in the cityLimits layer and displays the results on the map.
        /// </summary>
        private async void SimplifyShape_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];

                var cityLimits = (ShapeFileFeatureLayer)layerOverlay.Layers["cityLimits"];
                var simplifyLayer = (InMemoryFeatureLayer)layerOverlay.Layers["simplifyLayer"];

                // Query the cityLimits layer to get all the features
                cityLimits.Open();
                var features = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns);
                cityLimits.Close();

                // Simplify the first feature using the Douglas Peucker method
                var simplify = AreaBaseShape.Simplify(features[0].GetShape() as AreaBaseShape, Convert.ToInt32(Tolerance.Text), SimplificationType.DouglasPeucker);

                // Add the simplified shape into simplifyLayer to display the result.
                // If this were to be a permanent change to the cityLimits FeatureSource, you would modify the underlying data using BeginTransaction and CommitTransaction instead.
                simplifyLayer.InternalFeatures.Clear();
                simplifyLayer.InternalFeatures.Add(new Feature(simplify));

                // Redraw the layerOverlay to see the simplified feature on the map
                await layerOverlay.RefreshAsync();
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