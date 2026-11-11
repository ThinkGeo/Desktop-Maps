using System;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A small overview map whose frame follows the main map's view, turning with it; click the overview to recenter the main map.
    /// </summary>
    public partial class OverviewMap
    {
        private readonly DynamicSource _frame = new DynamicSource();
        private bool _initialized;

        public OverviewMap()
        {
            InitializeComponent();
        }

        private void MainMap_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!_initialized)
            {
                if (e.NewSize.Width <= 0 || e.NewSize.Height <= 0 ||
                    PreviewMap.ActualWidth <= 0 || PreviewMap.ActualHeight <= 0)
                {
                    return;
                }

                _initialized = true;

                MainMap.MapUnit = GeographyUnit.Meter;
                MainMap.MapTools.PanZoomBar.Visibility = Visibility.Collapsed;
                MainMap.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) }));
                MainMap.CenterPoint = new PointShape(-9338030, 3300450);
                MainMap.CurrentScale = 4622340;

                PreviewMap.MapUnit = GeographyUnit.Meter;
                PreviewMap.MapTools.PanZoomBar.Visibility = Visibility.Collapsed;
                PreviewMap.MapTools.Logo.IsEnabled = false;
                PreviewMap.ExtentOverlay.RotationMouseButton = MapMouseButton.None;
                PreviewMap.ExtentOverlay.PanMode = MapPanMode.Disabled;
                PreviewMap.ExtentOverlay.DoubleLeftClickMode = MapDoubleClickMode.Disabled;
                PreviewMap.ExtentOverlay.MouseWheelMode = MapMouseWheelMode.Disabled;

                var previewStyle = new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
                previewStyle.AddDynamic(_frame);
                PreviewMap.Basemap = new GpuBasemap(previewStyle);
                PreviewMap.CenterPoint = new PointShape(-10502930, 4330070);
                PreviewMap.CurrentScale = MainMap.CurrentScale * 50;

                _ = MainMap.RefreshAsync();
                _ = PreviewMap.RefreshAsync();
            }

            UpdateFrame(MainMap.CenterPoint, MainMap.RotationAngle);
        }

        private void MainMap_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            if (!_initialized) return;

            UpdateFrame(MainMap.CenterPoint, MainMap.RotationAngle);
        }

        private void MainMap_RotationAngleChanging(object sender, RotationAngleChangingMapViewEventArgs e)
        {
            if (!_initialized) return;

            UpdateFrame(MainMap.CenterPoint, e.NewRotationAngle);
        }

        private void PreviewMap_MapMouseDown(object sender, MapMouseDownMapViewEventArgs e)
        {
            if (!_initialized) return;

            _ = MainMap.ZoomToAsync(e.WorldLocation, MainMap.CurrentScale, MainMap.RotationAngle,
                new MapAnimationSettings { Duration = 10 });
        }

        private void CompassButton_Click(object sender, RoutedEventArgs e)
        {
            _ = MainMap.ZoomToAsync(MainMap.CenterPoint, MainMap.CurrentScale, 0);
        }

        // The main map's view as a rectangle turned by its rotation, drawn on the overview.
        private void UpdateFrame(PointShape centerPoint, double rotationAngle)
        {
            if (centerPoint == null || MainMap.ActualWidth <= 0 || MainMap.ActualHeight <= 0) return;

            var resolution = MapUtil.GetResolutionFromScale(MainMap.CurrentScale, MainMap.MapUnit);
            var halfWidth = MainMap.ActualWidth / 2 * resolution;
            var halfHeight = MainMap.ActualHeight / 2 * resolution;
            var radians = rotationAngle * Math.PI / 180.0;
            var cos = Math.Cos(radians);
            var sin = Math.Sin(radians);

            var ring = new RingShape();
            foreach (var (x, y) in new[] { (-halfWidth, halfHeight), (halfWidth, halfHeight), (halfWidth, -halfHeight), (-halfWidth, -halfHeight), (-halfWidth, halfHeight) })
            {
                ring.Vertices.Add(new Vertex(centerPoint.X + x * cos - y * sin, centerPoint.Y + x * sin + y * cos));
            }

            _frame.SetArea("frame", new Feature(new PolygonShape(ring)), new GeoColor(32, GeoColors.Red), GeoColors.Red, 2f);
        }
    }
}
