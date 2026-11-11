using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Group layers into LayerOverlays and show or hide each group, over the map's background overlay.
    /// </summary>
    public partial class GroupLayersInOverlays : IDisposable
    {
        private bool _initialized;
        private ThinkGeoVectorTileOverlay _basemapOverlay;
        private LayerOverlay _landUseOverlay;
        private LayerOverlay _poiOverlay;

        public GroupLayersInOverlays()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.BackgroundOverlay.BackgroundBrush = new GeoLinearGradientBrush(GeoColors.Blue, GeoColors.White, 90);

            _basemapOverlay = new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey);
            Map.Overlays.Add(_basemapOverlay);

            var cityLimits = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "FriscoCityLimits.shp"));
            cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.DimGray, 2);
            cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var parks = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Parks.shp"));
            parks.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            parks.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(128, GeoColors.Green), GeoColors.Transparent);
            parks.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _landUseOverlay = new LayerOverlay();
            _landUseOverlay.Layers.Add(cityLimits);
            _landUseOverlay.Layers.Add(parks);
            Map.Overlays.Add(_landUseOverlay);

            var hotels = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Hotels.shp"));
            hotels.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            hotels.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 8, GeoColors.White, 2);
            hotels.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var schools = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Schools.shp"));
            schools.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            schools.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleSquareStyle(GeoColors.Red, 8, GeoColors.White, 2);
            schools.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _poiOverlay = new LayerOverlay();
            _poiOverlay.Layers.Add(hotels);
            _poiOverlay.Layers.Add(schools);
            Map.Overlays.Add(_poiOverlay);

            cityLimits.Open();
            var extent = cityLimits.GetBoundingBox();
            cityLimits.Close();
            extent.ScaleUp(50);
            Map.CurrentExtent = extent;

            _ = Map.RefreshAsync();
        }

        private void Group_Changed(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _basemapOverlay.IsVisible = ShowBasemap.IsChecked == true;
            _landUseOverlay.IsVisible = ShowLandUse.IsChecked == true;
            _poiOverlay.IsVisible = ShowPoi.IsChecked == true;
            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
