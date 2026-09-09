using System;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Paint the map's background yourself - here a blue-to-white gradient behind
    /// everything else, set on Map.BackgroundOverlay.
    /// <para>
    /// There is deliberately no basemap: a basemap covers the whole view, and a
    /// background you cannot see is not a sample. The census layer is here only so
    /// there is something drawn over the gradient.
    /// </para>
    /// </summary>
    public partial class CustomBackground
    {

        private bool _initialized;
        public CustomBackground()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the background brush and one layer to sit over it.
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
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

            housingUnitsLayer.Open();
            var housingUnitsLayerBBox = housingUnitsLayer.GetBoundingBox();
            Map.CenterPoint = housingUnitsLayerBBox.GetCenterPoint();
            var MapScale = MapUtil.GetScale(Map.MapUnit, housingUnitsLayerBBox, Map.MapWidth, Map.MapHeight);
            Map.CurrentScale = MapScale * 1.5; // Multiply the current scale by 1.5 to zoom out 50%.
            housingUnitsLayer.Close();

            _ = Map.RefreshAsync();
        }
    }
}
