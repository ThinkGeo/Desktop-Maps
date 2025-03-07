﻿using System;
using System.Linq;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to clip a shape from another shape
    /// </summary>
    public partial class ClipShape : IDisposable
    {
        public ClipShape()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the cityLimits, westRegionLayer and clipLayer
        /// layers into a grouped LayerOverlay and display them on the map.
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
                var westRegionLayer = new InMemoryFeatureLayer();
                var clipLayer = new InMemoryFeatureLayer();
                var layerOverlay = new LayerOverlay();
                var clipOverlay = new LayerOverlay();

                // Project cityLimits layer to Spherical Mercator to match the map projection
                cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

                // Style cityLimits layer
                cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
                cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Style cityLimits layer
                westRegionLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Blue), GeoColors.DimGray);
                westRegionLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Style the clipLayer
                clipLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(64, GeoColors.Green), GeoColors.DimGray);
                clipLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add cityLimits to a LayerOverlay
                layerOverlay.Layers.Add("cityLimits", cityLimits);

                // Add cityLimits to a LayerOverlay
                layerOverlay.Layers.Add("westRegionLayer", westRegionLayer);

                // Add clipLayer to the layerOverlay
                clipOverlay.Layers.Add("clipLayer", clipLayer);

                // Set the map extent to the cityLimits layer bounding box
                cityLimits.Open();
                MapView.CurrentExtent = cityLimits.GetBoundingBox();
                cityLimits.Close();

                // Add LayerOverlay and clipOverlay to the Map
                MapView.Overlays.Add("layerOverlay", layerOverlay);
                MapView.Overlays.Add("clipOverlay", clipOverlay);

                // Add west region area to westRegionLayer
                westRegionLayer.InternalFeatures.Add(new Feature("Polygon ((-10780139.10763415694236755 3918539.43726690439507365, -10780206.68015497177839279 3915600.03261143481358886, -10780088.42824354581534863 3914687.80358042707666755, -10780037.7488529346883297 3913978.2921118657104671, -10779767.4587696734815836 3913555.96385676926001906, -10779176.19921253807842731 3913336.35316411964595318, -10778635.61904601566493511 3913049.16995065379887819, -10778280.86331173405051231 3911934.22335720015689731, -10778263.970181530341506 3910684.13172211544588208, -10778382.22209295630455017 3910447.6278992616571486, -10778382.22209295630455017 3909569.1851286618039012, -10778263.970181530341506 3909113.07061315793544054, -10778280.86331173405051231 3907356.18507195776328444, -10785595.58868999965488911 3904045.13155200378969312, -10786034.81007529981434345 3904822.21554138045758009, -10786001.02381489239633083 3908150.16219153860583901, -10786051.70320550352334976 3908690.7423580614849925, -10785933.45129407569766045 3909315.78817560384050012, -10786001.02381489239633083 3911275.39127924991771579, -10785882.77190346457064152 3912204.5134404618293047, -10785832.09251285344362259 3914485.08601798117160797, -10785832.09251285344362259 3917728.56701711937785149, -10785426.65738796070218086 3918117.10901180794462562, -10785460.44364836812019348 3919012.44491261197254062, -10782233.85577943362295628 3918995.55178240826353431, -10781642.59622230008244514 3918623.90291792340576649, -10780139.10763415694236755 3918539.43726690439507365))"));

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Clips the cityLimits and westRegion areas and displays the results on the map
        /// </summary>
        private async void ClipShape_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];
                var clipOverlay = (LayerOverlay)MapView.Overlays["clipOverlay"];

                var cityLimits = (ShapeFileFeatureLayer)layerOverlay.Layers["cityLimits"];
                var westRegionLayer = (InMemoryFeatureLayer)layerOverlay.Layers["westRegionLayer"];
                var clipLayer = (InMemoryFeatureLayer)clipOverlay.Layers["clipLayer"];

                // Query the cityLimits layer to get the first feature
                cityLimits.Open();
                var feature = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns).First();
                cityLimits.Close();

                // Get the westRegion area
                var westRegion = westRegionLayer.InternalFeatures[0];

                // Clips the cityLimits feature down to the common area of the westRegion
                var clip = feature.GetIntersection(westRegion);

                // Add the clip into an InMemoryFeatureLayer to display the result.
                clipLayer.InternalFeatures.Clear();
                clipLayer.InternalFeatures.Add(clip);

                // Redraw the layerOverlay to see the clip feature on the map
                await clipOverlay.RefreshAsync();
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