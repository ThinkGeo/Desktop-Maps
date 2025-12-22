using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for OverviewMap.xaml
    /// </summary>
    public partial class OverviewMap : IDisposable
    {
        private const double MiniMapScaleRatio = 50.0;
        private double initScale = 2311168;
        private bool _updatingOverlay;
        private bool _dragging;
        private Point _lastMouse;

        public OverviewMap()
        {
            InitializeComponent();

            MapView.MapTools.PanZoomBar.Visibility = Visibility.Collapsed;
            MiniMapView.MapTools.PanZoomBar.Visibility = Visibility.Collapsed;

            MapView.CurrentExtentChanged += OnMainViewportChanged;
            MapView.RotationAngleChanging += OnRotationAngleChanging;
            MapView.SizeChanged += OnViewportSizeChanged;
            MiniMapView.SizeChanged += OnViewportSizeChanged;
            SizeChanged += OnViewportSizeChanged;

            MiniHud.MouseLeftButtonDown += MiniHud_MouseLeftButtonDown;
            MiniHud.MouseMove += MiniHud_MouseMove;
            MiniHud.MouseLeftButtonUp += MiniHud_MouseLeftButtonUp;
            MiniHud.MouseLeave += MiniHud_MouseLeftButtonUp;
        }

        private async void CompassButton_Click(object sender, RoutedEventArgs e)
        {
            await MapView.ZoomToAsync(MapView.CenterPoint, MapView.CurrentScale, 0);
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeMap(MapView, initScale, new PointShape(-9008435.44382651, 2973578.1226677005));
        }

        private async void MiniMapView_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeMap(MiniMapView, initScale * MiniMapScaleRatio, new PointShape(-10502932.186987586, 4330072.447083719));

            UpdateMiniHud();
        }

        private void OnMainViewportChanged(object sender, EventArgs e)
        {
            UpdateMiniHud();
        }

        private void OnRotationAngleChanging(object sender, RotationAngleChangingMapViewEventArgs e)
        {
            UpdateMiniHud(e.NewRotationAngle);
        }

        private void OnViewportSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateMiniHud();
        }

        private async Task InitializeMap(MapView map, double scale, PointShape centerPoint)
        {
            map.MapUnit = GeographyUnit.Meter;

            var layerOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X2,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };

            map.Overlays.Add(layerOverlay);
            map.CenterPoint = centerPoint;
            map.CurrentScale = scale;

            await map.RefreshAsync();
        }

        private void UpdateMiniHud(double? rotationOverride = null)
        {
            if (_updatingOverlay || MiniMapView.ActualWidth < 1 || MiniMapView.ActualHeight < 1)
                return;

            var center = MapView.CenterPoint;
            var extent = MapView.CurrentExtent;
            var miniExtent = MiniMapView.CurrentExtent;
            if (center == null || extent == null || miniExtent == null)
                return;

            _updatingOverlay = true;

            var rotation = rotationOverride ?? MapView.RotationAngle;
            var corners = GetRotatedViewportCorners(extent, center, rotation);

            var pts = corners
                .Select(p => WorldToMiniPixel(p, miniExtent, MiniMapView.ActualWidth, MiniMapView.ActualHeight))
                .ToArray();

            var fig = new PathFigure { StartPoint = pts[0], IsClosed = true, IsFilled = false };
            for (int i = 1; i < pts.Length; i++)
                fig.Segments.Add(new LineSegment(pts[i], true));
            var geo = new PathGeometry();
            geo.Figures.Add(fig);
            ViewportBox.Data = geo;

            var starPt = WorldToMiniPixel(center, miniExtent, MiniMapView.ActualWidth, MiniMapView.ActualHeight);
            StarTranslate.X = starPt.X - 5;
            StarTranslate.Y = starPt.Y - 5;
            StarRotate.Angle = rotation;
           
            _updatingOverlay = false;
        }

        private static PointShape[] GetRotatedViewportCorners(RectangleShape extent, PointShape center, double angleDeg)
        {
            var p1 = extent.UpperLeftPoint;
            var p2 = extent.UpperRightPoint;
            var p3 = extent.LowerRightPoint;
            var p4 = extent.LowerLeftPoint;

            if (Math.Abs(angleDeg) < 1e-6) return new[] { p1, p2, p3, p4 };

            return new[]  {
                RotatePoint(p1, center, angleDeg),
                RotatePoint(p2, center, angleDeg),
                RotatePoint(p3, center, angleDeg),
                RotatePoint(p4, center, angleDeg)
             };
        }

        private static PointShape RotatePoint(PointShape p, PointShape c, double angleDeg)
        {
            double rad = (-angleDeg) * Math.PI / 180.0;
            double cos = Math.Cos(rad);
            double sin = Math.Sin(rad);
            double dx = p.X - c.X;
            double dy = p.Y - c.Y;
            return new PointShape(
                c.X + dx * cos - dy * sin,
                c.Y + dx * sin + dy * cos
            );
        }

        private static Point WorldToMiniPixel(PointShape world, RectangleShape miniExtent, double w, double h)
        {
            var x = (world.X - miniExtent.LowerLeftPoint.X) / (miniExtent.Width);
            var y = (miniExtent.UpperLeftPoint.Y - world.Y) / (miniExtent.Height);
            return new Point(x * w, y * h);
        }

        private static PointShape MiniPixelToWorld(Point pixel, RectangleShape miniExtent, double w, double h)
        {
            var lon = miniExtent.LowerLeftPoint.X + (pixel.X / w) * miniExtent.Width;
            var lat = miniExtent.UpperLeftPoint.Y - (pixel.Y / h) * miniExtent.Height;
            return new PointShape(lon, lat);
        }

        private void MiniHud_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragging = true;
            _lastMouse = e.GetPosition(MiniHud);
            MiniHud.CaptureMouse();

            CenterMainMapToPixel(_lastMouse);
        }

        private void MiniHud_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragging) return;
            var pos = e.GetPosition(MiniHud);
            if ((pos - _lastMouse).Length < 1) return;
            _lastMouse = pos;
            CenterMainMapToPixel(pos);
        }

        private void MiniHud_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            if (!_dragging) return;
            _dragging = false;
            MiniHud.ReleaseMouseCapture();
        }

        private void CenterMainMapToPixel(Point pixelOnMini)
        {
            var world = MiniPixelToWorld(pixelOnMini, MiniMapView.CurrentExtent, MiniMapView.ActualWidth, MiniMapView.ActualHeight);
            MapView.CenterPoint = world;
            _ = MapView.RefreshAsync();

            UpdateMiniHud();
        }

        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            MapView.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}