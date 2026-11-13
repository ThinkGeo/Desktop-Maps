using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Fill census areas with one dot per 10 housing units using a DotDensityStyle.
    /// </summary>
    public partial class DisplayDotDensity : IDisposable
    {
        private bool _initialized;

        public DisplayDotDensity()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            var housingUnitsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Frisco 2010 Census Housing Units.shp"));
            // The shapefile is in Texas North Central feet (EPSG:2276); the map is in EPSG:3857.
            housingUnitsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // 0.1 dots per housing unit, each dot a blue point 1 pixel across.
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new DotDensityStyle("H_UNITS", .1, 1, GeoColors.Blue));
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(housingUnitsLayer);
            Map.Overlays.Add(layerOverlay);

            housingUnitsLayer.Open();
            var boundingBox = housingUnitsLayer.GetBoundingBox();
            housingUnitsLayer.Close();
            Map.CenterPoint = boundingBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, boundingBox, Map.MapWidth, Map.MapHeight);

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
