﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to get the envelope of a shape
    /// </summary>
    public partial class GetEnvelopeSample : UserControl, IDisposable
    {
        public GetEnvelopeSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, add the cityLimits and envelopeLayer layers
        /// into a grouped LayerOverlay and display them on the map.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer cityLimits = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/FriscoCityLimits.shp");
            InMemoryFeatureLayer envelopeLayer = new InMemoryFeatureLayer();
            LayerOverlay layerOverlay = new LayerOverlay();

            // Project cityLimits layer to Spherical Mercator to match the map projection
            cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style cityLimits layer
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style the envelopeLayer
            envelopeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray);
            envelopeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add cityLimits to a LayerOverlay
            layerOverlay.Layers.Add("cityLimits", cityLimits);

            // Add envelopeLayer to the layerOverlay
            layerOverlay.Layers.Add("envelopeLayer", envelopeLayer);

            // Set the map extent to the cityLimits layer bounding box
            cityLimits.Open();
            mapView.CurrentExtent = cityLimits.GetBoundingBox();
            cityLimits.Close();

            // Add LayerOverlay to Map
            mapView.Overlays.Add("layerOverlay",layerOverlay);
        }

        /// <summary>
        /// Gets the Envelope of the first feature in the cityLimits layer and adds them to the envelopeLayer to display on the map
        /// </summary>
        private void ShapeEnvelope_OnClick(object sender, RoutedEventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];

            ShapeFileFeatureLayer cityLimits = (ShapeFileFeatureLayer)layerOverlay.Layers["cityLimits"];
            InMemoryFeatureLayer envelopeLayer = (InMemoryFeatureLayer)layerOverlay.Layers["envelopeLayer"];

            // Query the cityLimits layer to get the first feature
            cityLimits.Open();
            var feature = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns).First();
            cityLimits.Close();

            // Get the bounding box (or envelope) of the feature
            var envelope = feature.GetBoundingBox();

            // Add the envelope shape into an InMemoryFeatureLayer to display the result.
            envelopeLayer.InternalFeatures.Clear();
            envelopeLayer.InternalFeatures.Add(new Feature(envelope));

            // Redraw the layerOverlay to see the envelope feature on the map
            layerOverlay.Refresh();
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

    }
}