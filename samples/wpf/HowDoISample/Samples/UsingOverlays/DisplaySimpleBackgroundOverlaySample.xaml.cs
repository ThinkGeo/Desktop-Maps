using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
using System;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a CloudMapsVector Layer on the map
    /// </summary>
    public partial class DisplaySimpleBackgroundOverlaySample : UserControl, IDisposable
    {
        public DisplaySimpleBackgroundOverlaySample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            ShapeFileFeatureLayer housingUnitsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco 2010 Census Housing Units.shp");

            // Project the layer's data to match the projection of the map
            housingUnitsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add and apply the ClassBreakStyle to the housingUnitsLayer
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new AreaStyle(GeoPens.Black));
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add housingUnitsLayer to a LayerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(housingUnitsLayer);

            // Add layerOverlay to the mapView
            mapView.Overlays.Add(layerOverlay);

            mapView.BackgroundOverlay.BackgroundBrush = new GeoLinearGradientBrush(GeoColors.Blue, GeoColors.White, 90);

            // Set the map extent
            housingUnitsLayer.Open();
            mapView.CurrentExtent = housingUnitsLayer.GetBoundingBox();
            housingUnitsLayer.Close();
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
