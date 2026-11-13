using System;
using System.Windows;
using System.Windows.Media;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A small overview map whose frame follows the main map's view as it pans, turns and tilts; click the overview to recenter the main map.
    /// </summary>
    public partial class OverviewMap
    {
        private readonly DynamicSource _frame = new DynamicSource();
        private bool _initialized;
        private (double X, double Y, double Scale, double Rotation, double Tilt) _shown;

        public OverviewMap()
        {
            InitializeComponent();
            Loaded += (_, _) => CompositionTarget.Rendering += OnRendering;
            Unloaded += (_, _) => CompositionTarget.Rendering -= OnRendering;
        }

        // The frame follows every move of the main map's camera, mid-drag included: a frame
        // drawn only when an event says the extent has changed waits for the mouse button.
        private void OnRendering(object sender, EventArgs e)
        {
            if (!_initialized || MainMap.CenterPoint == null) return;

            var now = (MainMap.CenterPoint.X, MainMap.CenterPoint.Y, MainMap.CurrentScale, MainMap.RotationAngle, MainMap.TiltAngle);
            if (now == _shown) return;

            _shown = now;
            UpdateFrame();
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
                // Tilt with the right mouse button; the frame shows the ground the tilted view covers.
                MainMap.IsTiltEnabled = true;
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

            UpdateFrame();
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

        // The ground the main map shows, drawn on the overview: a rectangle turned by the
        // rotation, or, tilted, the trapezoid the four screen corners land on.
        private void UpdateFrame()
        {
            var centerPoint = MainMap.CenterPoint;
            var width = MainMap.ActualWidth;
            var height = MainMap.ActualHeight;
            if (centerPoint == null || width <= 0 || height <= 0) return;

            var resolution = MapUtil.GetResolutionFromScale(MainMap.CurrentScale, MainMap.MapUnit);
            var halfWidth = width / 2 * resolution;
            var halfHeight = height / 2 * resolution;
            var ring = new RingShape();
            if (MainMap.TiltAngle > 0)
            {
                // Past the horizon a corner has no ground under it; the frame then stops a few
                // views out rather than running off the overview.
                var reach = 3 * Math.Sqrt(halfWidth * halfWidth + halfHeight * halfHeight);
                foreach (var (x, y) in new[] { (0d, 0d), (width, 0d), (width, height), (0d, height), (0d, 0d) })
                {
                    var ground = MainMap.ToWorldCoordinate(x, y, 0);
                    var dx = ground.X - centerPoint.X;
                    var dy = ground.Y - centerPoint.Y;
                    var distance = Math.Sqrt(dx * dx + dy * dy);
                    var clip = distance > reach ? reach / distance : 1;
                    ring.Vertices.Add(new Vertex(centerPoint.X + dx * clip, centerPoint.Y + dy * clip));
                }
            }
            else
            {
                var radians = MainMap.RotationAngle * Math.PI / 180.0;
                var cos = Math.Cos(radians);
                var sin = Math.Sin(radians);
                foreach (var (x, y) in new[] { (-halfWidth, halfHeight), (halfWidth, halfHeight), (halfWidth, -halfHeight), (-halfWidth, -halfHeight), (-halfWidth, halfHeight) })
                {
                    ring.Vertices.Add(new Vertex(centerPoint.X + x * cos - y * sin, centerPoint.Y + x * sin + y * cos));
                }
            }

            _frame.SetArea("frame", new Feature(new PolygonShape(ring)), new GeoColor(32, GeoColors.Red), GeoColors.Red, 2f);
        }
    }
}
