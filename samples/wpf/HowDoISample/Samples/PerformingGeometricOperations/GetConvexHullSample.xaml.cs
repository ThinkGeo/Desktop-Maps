﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to get the convex hull of a shape
    /// </summary>
    public partial class GetConvexHullSample : UserControl
    {
        private readonly ShapeFileFeatureLayer cityLimits = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/FriscoCityLimits.shp");
        private readonly InMemoryFeatureLayer convexHullLayer = new InMemoryFeatureLayer();
        private readonly LayerOverlay layerOverlay = new LayerOverlay();

        public GetConvexHullSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, add the cityLimits and convexHullLayer layers
        /// into a grouped LayerOverlay and display them on the map.
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

            // Style the convexHullLayer
            convexHullLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray);
            convexHullLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add cityLimits to a LayerOverlay
            layerOverlay.Layers.Add(cityLimits);

            // Add convexHullLayer to the layerOverlay
            layerOverlay.Layers.Add(convexHullLayer);

            // Set the map extent to the cityLimits layer bounding box
            cityLimits.Open();
            mapView.CurrentExtent = cityLimits.GetBoundingBox();
            cityLimits.Close();

            // Add LayerOverlay to Map
            mapView.Overlays.Add(layerOverlay);
        }

        /// <summary>
        /// Gets Convex Hull of the first feature in the cityLimits layer and adds them to the convexHullLayer to display on the map
        /// </summary>
        private void ShapeConvexHull_OnClick(object sender, RoutedEventArgs e)
        {
            // Query the cityLimits layer to get the first feature
            cityLimits.Open();
            var feature = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns).First();
            cityLimits.Close();

            // Get the convex hull of the feature
            var convexHull = feature.GetConvexHull();

            // Add the convexHull into an InMemoryFeatureLayer to display the result.
            convexHullLayer.InternalFeatures.Clear();
            convexHullLayer.InternalFeatures.Add(convexHull);

            // Redraw the layerOverlay to see the convexHull feature on the map
            layerOverlay.Refresh();
        }
    }
}
