using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A flight from Los Angeles to Shanghai crosses the antimeridian. Everything
    /// about it - the great-circle route, the edges of its 100-nautical-mile
    /// corridor, the aircraft's position - is stored the way a navigator writes it,
    /// with continuous longitudes running past -180, and handed to the map as is.
    ///
    /// On the globe there is no seam: a sphere has no edge, and the data draws where
    /// it is. In Web Mercator there is one, at 180 degrees, and geometry stored past
    /// it used to vanish because tiles are only ever requested inside the world
    /// square. The tile cut now folds such a source back into the square - the
    /// representation every tile pipeline agrees on, one part each side of the seam
    /// - so the same data draws whole on both sides.
    /// </summary>
    public partial class CrossTheDateline : IDisposable
    {
        private const double HalfWorld = 20037508.342789244;
        private const double EarthRadius = 6378137.0;
        private const double CorridorHalfWidthMeters = 100 * 1852.0;
        private const int RouteSamples = 360;
        private static readonly TimeSpan MorphDuration = TimeSpan.FromMilliseconds(900);
        private static readonly TimeSpan FlightDuration = TimeSpan.FromSeconds(45);

        private static readonly Airport Origin = new Airport("LAX", "Los Angeles", -118.408, 33.942);
        private static readonly Airport Destination = new Airport("PVG", "Shanghai Pudong", 121.805, 31.143);

        private readonly ThinkGeoVectorTileSource _cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);

        // The route and the corridor's edges, in Web Mercator meters with the
        // western half past the world's west edge (x < -20037508). The tile cut
        // folds it back; the sample never splits anything itself.
        private readonly FeatureSourceVectorTileSource _flight = new FeatureSourceVectorTileSource { SharedSources = true };

        private readonly InMemoryGeometrySource _airports = new InMemoryGeometrySource();
        private readonly InMemoryGeometrySource _aircraft = new InMemoryGeometrySource();
        private readonly DispatcherTimer _clock = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(40) };
        private readonly DispatcherTimer _applyTimer;

        // The route, sampled evenly along the arc. Longitudes are continuous from the
        // origin westward: -118 at Los Angeles, -180 at the seam, -238 (= 122 E) at
        // Shanghai. Nothing here is ever wrapped by hand.
        private (double Lon, double Lat)[] _route;
        private double _routeLengthMeters;
        private double _flownMeters;
        private DateTime _flightStart;
        private double _flightStartMeters;
        private bool _flying;
        private bool _initialized;
        private string _projection = "globe";

        public CrossTheDateline()
        {
            InitializeComponent();
            _clock.Tick += (_, _) => Advance();
            _route = GreatCircle(Origin, Destination, RouteSamples, out _routeLengthMeters);
            ShowFlight();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;

            _flight.FeatureSources.Add("route", new InMemoryFeatureSource(Array.Empty<FeatureSourceColumn>(),
                new Collection<Feature> { new Feature(RouteLine()) }));
            _flight.FeatureSources.Add("corridor", new InMemoryFeatureSource(Array.Empty<FeatureSourceColumn>(),
                new Collection<Feature> { new Feature(CorridorEdges()) }));
            ShowAirports();
            ShowAircraft(0);

            Map.Basemap = new GpuBasemap(ComposeStyle());
            Map.Basemap.DisplayProjection = ThinkGeo.Gpu.DisplayProjection.Globe;
            FrameRoute();
            await Map.RefreshAsync();
            ShowSeam();
        }

        /// <summary>
        /// The flight's layers over the cloud basemap. The two airports and the
        /// aircraft ride the code geometry channel: drawn over everything, updated
        /// without a tile being cut.
        /// </summary>
        private MapStyle ComposeStyle()
        {
            var style = new MapStyle(ThinkGeoVectorStyles.Light, _cloud);
            style.AddStyle(Editor.Text, _flight);
            style.Images.Add("airport", RenderDotImage());
            style.Images.Add("plane", RenderPlaneImage());
            style.AddGeometry(_airports);
            style.AddGeometry(_aircraft);
            return style;
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_initialized) return;
            _applyTimer.Stop();
            _applyTimer.Start();
        }

        // A document that does not compile changes nothing; the map keeps the last one that worked.
        private async Task ApplyEditorStyleAsync()
        {
            if (!IsLoaded) return;

            try
            {
                await Map.Basemap.SetStyleAsync(ComposeStyle());
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }

        // ------------------------------------------------------------- projection ----

        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized || sender is not RadioButton button || button.Tag is not string definition || definition == _projection)
            {
                return;
            }

            _projection = definition;
            var target = definition == "globe"
                ? ThinkGeo.Gpu.DisplayProjection.Globe
                : ThinkGeo.Gpu.DisplayProjection.WebMercator;

            // The camera does not move: the screen center keeps the same ground point
            // and only the shape under it changes. Zoom-out on the globe stops where
            // the world fills the window; mercator keeps its freedom.
            var before = Map.CurrentExtent;
            Map.MaximumScale = definition == "3857" ? double.MaxValue : WorldFillScale();
            Map.CurrentExtent = before;
            await Map.RefreshAsync();
            await Map.AnimateDisplayProjectionAsync(target, MorphDuration);
            ShowSeam();
        }

        private double WorldFillScale()
        {
            var world = new RectangleShape(-HalfWorld, HalfWorld, HalfWorld, -HalfWorld);
            return MapUtil.GetScale(world, Math.Max(1, Map.ActualWidth), GeographyUnit.Meter) * 1.05;
        }

        /// <summary>The whole Pacific, centered on the route's midpoint.</summary>
        private void FrameRoute()
        {
            var middle = _route[_route.Length / 2];
            var (cx, cy) = Meters(middle.Lon, middle.Lat);
            const double widthInMeters = 22000000;
            var height = widthInMeters * Map.ActualHeight / Math.Max(1, Map.ActualWidth);
            Map.MaximumScale = _projection == "3857" ? double.MaxValue : WorldFillScale();
            Map.CurrentExtent = new RectangleShape(cx - widthInMeters * 0.5, cy + height * 0.5, cx + widthInMeters * 0.5, cy - height * 0.5);
        }

        // ----------------------------------------------------------------- flight ----

        private void Fly_Click(object sender, RoutedEventArgs e)
        {
            if (_flying)
            {
                _clock.Stop();
                _flying = false;
                FlyButton.Content = "Fly";
                return;
            }

            if (_flownMeters >= _routeLengthMeters)
            {
                _flownMeters = 0;
            }

            _flightStart = DateTime.UtcNow;
            _flightStartMeters = _flownMeters;
            _flying = true;
            FlyButton.Content = "Pause";
            _clock.Start();
        }

        private async void Reset_Click(object sender, RoutedEventArgs e)
        {
            _clock.Stop();
            _flying = false;
            FlyButton.Content = "Fly";
            _flownMeters = 0;
            ShowAircraft(0);
            ShowFlight();
            if (_initialized)
            {
                FrameRoute();
                await Map.RefreshAsync();
            }
        }

        private void Advance()
        {
            var elapsed = (DateTime.UtcNow - _flightStart).TotalSeconds / FlightDuration.TotalSeconds;
            _flownMeters = Math.Min(_routeLengthMeters, _flightStartMeters + elapsed * _routeLengthMeters);
            ShowAircraft(_flownMeters);
            ShowFlight();
            if (_flownMeters >= _routeLengthMeters)
            {
                _clock.Stop();
                _flying = false;
                FlyButton.Content = "Fly";
            }
        }

        /// <summary>Puts the aircraft at a distance along the route, nose along the arc.</summary>
        private void ShowAircraft(double meters)
        {
            var (lon, lat, heading) = PoseAt(meters);
            var (x, y) = Meters(lon, lat);
            _aircraft.UpdatePoints(new[]
            {
                new TrackPoint(x, y, (float)heading, GeoColors.White, 44f, "plane"),
            });
        }

        private void ShowAirports()
        {
            var points = new List<TrackPoint>();
            var labels = new List<GeometryLabel>();
            foreach (var airport in new[] { Origin, Destination })
            {
                var (x, y) = Meters(airport.Lon, airport.Lat);
                points.Add(new TrackPoint(x, y, 0f, GeoColors.White, 14f, "airport"));
                labels.Add(new GeometryLabel(x, y, airport.Code + "  " + airport.Name, GeoColor.FromHtml("#101317"), 13f, 0f, -14f));
            }

            _airports.UpdatePoints(points);
            _airports.UpdateLabels(labels);
        }

        /// <summary>Position and compass heading at a distance along the arc.</summary>
        private (double Lon, double Lat, double Heading) PoseAt(double meters)
        {
            var fraction = _routeLengthMeters <= 0 ? 0 : Math.Max(0, Math.Min(1, meters / _routeLengthMeters));
            var position = fraction * (_route.Length - 1);
            var index = Math.Min(_route.Length - 2, (int)Math.Floor(position));
            var t = position - index;
            var a = _route[index];
            var b = _route[index + 1];
            var lon = a.Lon + (b.Lon - a.Lon) * t;
            var lat = a.Lat + (b.Lat - a.Lat) * t;
            return (lon, lat, Bearing(a, b));
        }

        // -------------------------------------------------------------------- HUD ----

        private void ShowFlight()
        {
            if (PositionText == null) return;

            var (lon, lat, heading) = PoseAt(_flownMeters);
            var canonical = ((lon + 180) % 360 + 360) % 360 - 180;
            PositionText.Text = string.Create(CultureInfo.InvariantCulture,
                $"lon {lon,8:F2}  ({Math.Abs(canonical):F2} {(canonical < 0 ? "W" : "E")})\nlat {lat,8:F2}  hdg {heading,3:F0}");
            ProgressText.Text = string.Create(CultureInfo.InvariantCulture,
                $"{_flownMeters / 1000:N0} of {_routeLengthMeters / 1000:N0} km  ({_flownMeters / Math.Max(1, _routeLengthMeters):P0})");
        }

        private void ShowSeam()
        {
            if (SeamText == null) return;
            SeamText.Text = _projection == "globe"
                ? "Globe: a sphere has no seam. The route is one line and the corridor two edges, stored with longitudes running past -180."
                : "Web Mercator: the seam is the 180th meridian. The same data, longitudes past -180 and all, is folded back into the world at the tile cut - one part each side.";
        }

        // ------------------------------------------------------------- geometry ----

        /// <summary>The route as one line with continuous longitudes, in meters.</summary>
        private LineShape RouteLine()
        {
            var line = new LineShape();
            foreach (var (lon, lat) in _route)
            {
                var (x, y) = Meters(lon, lat);
                line.Vertices.Add(new Vertex(x, y));
            }

            return line;
        }

        /// <summary>
        /// The two edges of a band 100 nautical miles either side of the route. The
        /// offsets are geodesic and the longitudes stay continuous, like the route's.
        /// </summary>
        private MultilineShape CorridorEdges()
        {
            var left = new LineShape();
            var right = new LineShape();
            for (var i = 0; i < _route.Length; i++)
            {
                var here = _route[i];
                var heading = i < _route.Length - 1 ? Bearing(here, _route[i + 1]) : Bearing(_route[i - 1], here);
                var leftPoint = Destination_(here, heading - 90, CorridorHalfWidthMeters);
                var (lx, ly) = Meters(leftPoint.Lon, leftPoint.Lat);
                left.Vertices.Add(new Vertex(lx, ly));
                var rightPoint = Destination_(here, heading + 90, CorridorHalfWidthMeters);
                var (rx, ry) = Meters(rightPoint.Lon, rightPoint.Lat);
                right.Vertices.Add(new Vertex(rx, ry));
            }

            return new MultilineShape(new[] { left, right });
        }

        /// <summary>
        /// Points along the great circle from a to b, evenly spaced in arc length, with
        /// longitudes made continuous from a onward (no jump at the antimeridian).
        /// </summary>
        private static (double Lon, double Lat)[] GreatCircle(Airport a, Airport b, int count, out double lengthMeters)
        {
            var p = ToUnit(a.Lon, a.Lat);
            var q = ToUnit(b.Lon, b.Lat);
            var dot = Math.Max(-1, Math.Min(1, p.X * q.X + p.Y * q.Y + p.Z * q.Z));
            var omega = Math.Acos(dot);
            var sinOmega = Math.Sin(omega);
            lengthMeters = omega * EarthRadius;

            var result = new (double Lon, double Lat)[count + 1];
            var previousLon = a.Lon;
            for (var i = 0; i <= count; i++)
            {
                var t = (double)i / count;
                var k1 = Math.Sin((1 - t) * omega) / sinOmega;
                var k2 = Math.Sin(t * omega) / sinOmega;
                var x = k1 * p.X + k2 * q.X;
                var y = k1 * p.Y + k2 * q.Y;
                var z = k1 * p.Z + k2 * q.Z;
                var lat = Math.Asin(Math.Max(-1, Math.Min(1, z))) * 180 / Math.PI;
                var lon = Math.Atan2(y, x) * 180 / Math.PI;
                while (lon - previousLon > 180) lon -= 360;
                while (lon - previousLon < -180) lon += 360;
                result[i] = (lon, lat);
                previousLon = lon;
            }

            return result;
        }

        private static (double X, double Y, double Z) ToUnit(double lon, double lat)
        {
            var la = lat * Math.PI / 180;
            var lo = lon * Math.PI / 180;
            return (Math.Cos(la) * Math.Cos(lo), Math.Cos(la) * Math.Sin(lo), Math.Sin(la));
        }

        /// <summary>Initial compass bearing from a to b, degrees clockwise from north.</summary>
        private static double Bearing((double Lon, double Lat) a, (double Lon, double Lat) b)
        {
            var la1 = a.Lat * Math.PI / 180;
            var la2 = b.Lat * Math.PI / 180;
            var dLon = (b.Lon - a.Lon) * Math.PI / 180;
            var y = Math.Sin(dLon) * Math.Cos(la2);
            var x = Math.Cos(la1) * Math.Sin(la2) - Math.Sin(la1) * Math.Cos(la2) * Math.Cos(dLon);
            return (Math.Atan2(y, x) * 180 / Math.PI + 360) % 360;
        }

        /// <summary>The point a distance along a bearing, longitude kept continuous with the start.</summary>
        private static (double Lon, double Lat) Destination_((double Lon, double Lat) from, double bearingDegrees, double meters)
        {
            var la1 = from.Lat * Math.PI / 180;
            var brng = bearingDegrees * Math.PI / 180;
            var delta = meters / EarthRadius;
            var la2 = Math.Asin(Math.Sin(la1) * Math.Cos(delta) + Math.Cos(la1) * Math.Sin(delta) * Math.Cos(brng));
            var dLon = Math.Atan2(Math.Sin(brng) * Math.Sin(delta) * Math.Cos(la1), Math.Cos(delta) - Math.Sin(la1) * Math.Sin(la2));
            return (from.Lon + dLon * 180 / Math.PI, la2 * 180 / Math.PI);
        }

        /// <summary>Web Mercator meters; a longitude past +/-180 lands past the world edge on purpose.</summary>
        private static (double X, double Y) Meters(double lon, double lat)
        {
            var clamped = Math.Max(-85.05, Math.Min(85.05, lat));
            return (lon / 180 * HalfWorld, EarthRadius * Math.Log(Math.Tan(Math.PI / 4 + clamped * Math.PI / 360)));
        }

        // --------------------------------------------------------------- images ----

        /// <summary>A top-down airliner, nose up, drawn at 2x for crisp rotation.</summary>
        private static GeoImage RenderPlaneImage()
        {
            var path = new System.Windows.Shapes.Path
            {
                Data = Geometry.Parse("M32,2 C35,2 37,6 37,12 L37,26 L61,40 L61,45 L37,38 L36,52 L45,58 L45,61 L32,58 L19,61 L19,58 L28,52 L27,38 L3,45 L3,40 L27,26 L27,12 C27,6 29,2 32,2 Z"),
                Fill = Brushes.White,
                Stroke = Brush("#101317"),
                StrokeThickness = 1.5,
                StrokeLineJoin = PenLineJoin.Round,
            };
            return Rasterize(path, 64, 64);
        }

        private static GeoImage RenderDotImage()
        {
            var dot = new System.Windows.Shapes.Ellipse
            {
                Width = 14, Height = 14, Fill = Brushes.White, Stroke = Brush("#1f4fd6"), StrokeThickness = 3,
                HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center,
            };
            var shell = new Grid { Width = 20, Height = 20 };
            shell.Children.Add(dot);
            return Rasterize(shell, 20, 20);
        }

        private static GeoImage Rasterize(UIElement element, int width, int height)
        {
            element.Measure(new Size(width, height));
            element.Arrange(new Rect(0, 0, width, height));
            var bitmap = new RenderTargetBitmap(width * 2, height * 2, 192, 192, PixelFormats.Pbgra32);
            bitmap.Render(element);
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                return new GeoImage(stream.ToArray());
            }
        }

        private static System.Windows.Media.Brush Brush(string hex) =>
            (System.Windows.Media.Brush)new BrushConverter().ConvertFromString(hex);

        private sealed record Airport(string Code, string Name, double Lon, double Lat);

        public void Dispose()
        {
            _clock.Stop();
            Map.Dispose();
        }
    }
}
