using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a CloudMapsVector Layer on the map
    /// </summary>
    public partial class CustomBackground : IDisposable
    {

        private bool _initialized;
        public CustomBackground()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            Map.MapUnit = GeographyUnit.Meter;

            var housingUnitsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco 2010 Census Housing Units.shp")
            {
                FeatureSource =
                    {
                        // Project the layer's data to match the projection of the map
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
            };

            // Add and apply the ClassBreakStyle to the housingUnitsLayer
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new AreaStyle(GeoPens.Black));
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add housingUnitsLayer to a LayerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(housingUnitsLayer);

            // Add layerOverlay to the map
            Map.Overlays.Add(layerOverlay);

            Map.BackgroundOverlay.BackgroundBrush = new GeoLinearGradientBrush(GeoColors.Blue, GeoColors.White, 90);

            // Set the map extent
            housingUnitsLayer.Open();
            var housingUnitsLayerBBox = housingUnitsLayer.GetBoundingBox();
            Map.CenterPoint = housingUnitsLayerBBox.GetCenterPoint();
            var MapScale = MapUtil.GetScale(Map.MapUnit, housingUnitsLayerBBox, Map.MapWidth, Map.MapHeight);
            Map.CurrentScale = MapScale * 1.5; // Multiply the current scale by 1.5 to zoom out 50%.
            housingUnitsLayer.Close();

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
