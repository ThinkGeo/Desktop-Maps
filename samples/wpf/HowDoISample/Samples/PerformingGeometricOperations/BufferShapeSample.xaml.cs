﻿using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to buffer a shape
    /// </summary>
    public partial class BufferShapeSample : UserControl
    {
        private readonly ShapeFileFeatureLayer cityLimits = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/FriscoCityLimits.shp");
        private readonly InMemoryFeatureLayer bufferLayer = new InMemoryFeatureLayer();
        private readonly LayerOverlay layerOverlay = new LayerOverlay();
        public BufferShapeSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, add the cityLimits and bufferLayer layers into a grouped LayerOverlay and display them on the map.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Project cityLimits layer to Spherical Mercator to match the map projection
            cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style cityLimits layer
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style the bufferLayer
            bufferLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray);
            bufferLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add cityLimits to a LayerOverlay
            layerOverlay.Layers.Add(cityLimits);

            // Add bufferLayer to the layerOverlay
            layerOverlay.Layers.Add(bufferLayer);

            // Set the map extent to the cityLimits layer bounding box
            cityLimits.Open();
            mapView.CurrentExtent = cityLimits.GetBoundingBox();
            cityLimits.Close();

            // Add LayerOverlay to Map
            mapView.Overlays.Add(layerOverlay);
        }

        /// <summary>
        /// Buffers the first feature in the cityLimits layer and adds them to the bufferLayer to display on the map
        /// </summary>
        private void BufferShape_OnClick(object sender, RoutedEventArgs e)
        {
            // Query the cityLimits layer to get all the features
            cityLimits.Open();
            var features = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns);
            cityLimits.Close();

            // Buffer the first feature by the amount of the bufferDistance TextBox
            var buffer = features[0].Buffer(Convert.ToInt32(bufferDistance.Text), GeographyUnit.Meter, DistanceUnit.Meter);

            // Add the buffer shape into an InMemoryFeatureLayer to display the result.
            // If this were to be a permanent change to the cityLimits FeatureSource, you would modify the underlying data using BeginTransaction and CommitTransaction instead.
            bufferLayer.InternalFeatures.Clear();
            bufferLayer.InternalFeatures.Add(buffer);

            // Redraw the layerOverlay to see the buffered features on the map
            layerOverlay.Refresh();
        }
    }
}