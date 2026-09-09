using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Lane-level in-car navigation, the way a car's own screen draws the road:
    /// every lane as surveyed, with the paint that is really on it, a follow
    /// camera the engine flies, the vehicle in the style's marker batch, and 3D
    /// buildings standing in front of the road behind them.
    /// <para>
    /// The lanes come from the DLR's ASAM OpenDRIVE HD map of Brunswick, evaluated
    /// offline into GeoJSON (Data\OpenDrive\tools\xodr-to-lanes.py): lane polygons,
    /// road marks by type, curbs, and the vertical level of each road section
    /// where one road passes over another. The GeoJSON is served as vector tiles by
    /// <see cref="FeatureSourceVectorTileSource"/> and styled by the document in
    /// the resources, composed over the ThinkGeo dark basemap - background only,
    /// so the scene is dark ground, the surveyed lanes and the buildings.
    /// </para>
    /// <para>
    /// Motion: one camera flight per stretch of route through
    /// <see cref="MapView.ZoomToAsync(PointShape, double, double, MapAnimationSettings, CancellationToken)"/>,
    /// and the vehicle placed from the flight's own progress
    /// (<see cref="MapView.CurrentExtentChangedInAnimation"/>), so car and
    /// camera never drift apart and nothing is refreshed per step.
    /// </para>
    /// </summary>
    public partial class LaneLevelNavigation : IDisposable
    {
        // The follow camera: ground height of the view and how far ahead of the
        // vehicle it is centered, the length of one camera flight, and the speed.
        private const double GroundHeightMeters = 60;
        private const double StretchMeters = 24;
        private const double GroundSpeedMetersPerSecond = 12.7;

        private static readonly MapAnimationSettings ViewChange = new MapAnimationSettings
        {
            Type = MapAnimationType.DrawWithAnimation,
            Duration = 1800,
            Easing = new System.Windows.Media.Animation.CubicEase { EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut }
        };

        private readonly ThinkGeoVectorTileSource _cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);
        private bool _initialized;
        private bool _disposed;
        private LaneMap _laneMap;
        private FeatureSourceVectorTileSource _laneTiles;
        private InMemoryGeometrySource _vehicle;
        private GeoImage _vehicleImage;
        private RoutePlayback _playback;
        private RoutePose _pose;
        private bool _trackingMode = true;
        private double _cameraScale;
        private double _stretchStart;
        private double _stretchLength;
        private CancellationTokenSource _flight;
        private CancellationTokenSource _drive;

        public LaneLevelNavigation()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.TiltInteractionEnabled = true;
            _vehicleImage = RenderVehicleImage();
            Map.CurrentExtentChangedInAnimation += Map_CurrentExtentChangedInAnimation;
            Map.PreviewMouseWheel += Map_PreviewMouseWheel;

            await LoadAsync();
        }

        /// <summary>
        /// Builds the whole scene: the lanes as a tile source, the style over the dark
        /// basemap, the vehicle, and the camera at the start of the route - then drives.
        /// </summary>
        private async Task LoadAsync()
        {
            var folder = Path.Combine(AppContext.BaseDirectory, "Data", "OpenDrive");
            _laneMap = LaneMap.Load(Path.Combine(folder, "XodrBrunswickLanes.geojson"), Path.Combine(folder, "XodrBrunswickRoute.json"));

            // Every source-layer the style names is one in-memory feature source,
            // cut into vector tiles on the fly - the same path a shapefile or a
            // database would take.
            _laneTiles = new FeatureSourceVectorTileSource
            {
                SourceId = "features",
                MaxConcurrentEncodes = 1
            };
            foreach (var pair in _laneMap.Sources)
            {
                _laneTiles.FeatureSources.Add(pair.Key, pair.Value);
            }

            await _laneTiles.OpenAsync();
            if (_disposed) return;

            _vehicle = new InMemoryGeometrySource();

            var style = ComposeStyle();
            await style.OpenAsync();
            if (_disposed) return;

            Map.Basemap = new GpuBasemap(style);

            _playback = new RoutePlayback(_laneMap.RouteTrace);
            _pose = _playback.PoseAtDistance(0);
            _cameraScale = 0;
            ApplyPitch(PitchSlider.Value);
            var scale = CameraScale();
            var (center, rotation) = CameraTarget(_pose, scale);
            Map.RotationAngle = rotation;
            Map.CenterPoint = center;
            Map.CurrentScale = scale;
            ShowVehicle(_pose);
            TitleText.Text = _laneMap.Title;
            StatsText.Text = string.Format(CultureInfo.InvariantCulture, "{0} lane markings, {1:0.0} km route",
                _laneMap.MarkingCount, _laneMap.RouteLength * _laneMap.GroundScale / 1000);
            AttributionText.Text = _laneMap.Attribution;
            await Map.RefreshAsync();

            _drive = new CancellationTokenSource();
            _ = DriveAsync(_drive.Token);
        }

        /// <summary>
        /// The basemap contributes its background and, after the lanes, its own 3D
        /// buildings - drawn with depth after the ground layers, so they stand in
        /// front of the road behind them rather than under it. No roads, no labels,
        /// no land use: the lanes come from the document in the resources.
        /// </summary>
        private MapStyle ComposeStyle()
        {
            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Dark, _cloud, "background");
            style.SetBackground(GeoColor.FromHtml("#101317"));
            style.AddStyle((string)Resources["StyleJson"], _laneTiles);
            style.AddStyle(ThinkGeoVectorStyles.Dark, _cloud, "fill-extrusion");
            style.Images.Add("vehicle", _vehicleImage);
            style.AddGeometry(_vehicle);
            return style;
        }

        // ------------------------------------------------------------ camera --

        private void ApplyPitch(double degrees)
        {
            degrees = Math.Max(0, Math.Min(60, degrees));
            PitchValueText.Text = string.Format(CultureInfo.InvariantCulture, "{0:0}°", degrees);
            Map.TiltAngle = degrees;
        }

        /// <summary>
        /// The scale of a view GroundHeightMeters tall on the ground. The map works
        /// in Web Mercator meters, which are longer than the ground by 1/cos(lat),
        /// so the ground height is inflated by the lane map's GroundScale first.
        /// </summary>
        private double DefaultCameraScale()
        {
            var height = GroundHeightMeters / _laneMap.GroundScale;
            var aspect = Math.Max(1.1, Map.ActualWidth / Math.Max(1.0, Map.ActualHeight));
            var extent = new RectangleShape(
                _pose.Position.X - height * aspect * 0.5, _pose.Position.Y + height * 0.5,
                _pose.Position.X + height * aspect * 0.5, _pose.Position.Y - height * 0.5);
            return MapUtil.GetScale(Map.MapUnit, extent, Map.MapWidth, Map.MapHeight);
        }

        // Owned by the mouse wheel once the user has touched it.
        private double CameraScale() => _cameraScale > 0 ? _cameraScale : (_cameraScale = DefaultCameraScale());

        /// <summary>
        /// Where the camera goes for a pose: the map rotated so the heading points
        /// up (RotationAngle is clockwise-positive with 0 = north, the heading
        /// counter-clockwise from east, hence heading - 90), and centered a quarter
        /// of the screen ahead of the vehicle. The rotation is taken the short way
        /// round from wherever the map is, so a heading crossing south never spins.
        /// </summary>
        private (PointShape Center, double Rotation) CameraTarget(RoutePose at, double scale)
        {
            var rotation = at.HeadingDegrees - 90;
            var current = Map.RotationAngle;
            rotation = current + ((rotation - current + 540) % 360 - 180);
            var center = MapUtil.OffsetPointWithScreenOffset(
                new PointShape(at.Position.X, at.Position.Y), 0, Map.ActualHeight * 0.25, rotation, scale, Map.MapUnit);
            return (center, rotation);
        }

        // ------------------------------------------------------------- drive --

        /// <summary>
        /// The vehicle is a <see cref="TrackPoint"/> in the style's marker batch:
        /// the GPU draws it every frame from this one call, and the batch counters
        /// the view rotation itself, so the heading is a plain compass bearing.
        /// </summary>
        private void ShowVehicle(RoutePose at)
        {
            _pose = at;
            _vehicle.UpdatePoints(new[]
            {
                new TrackPoint(at.Position.X, at.Position.Y, (float)(90 - at.HeadingDegrees), GeoColors.White, 100f, "vehicle"),
            });
            UpdateHud(at);
        }

        private void Map_CurrentExtentChangedInAnimation(object sender, CurrentExtentChangedInAnimationMapViewEventArgs e)
        {
            // A flight in progress: the vehicle sits at the flight's progress along
            // the exact trace, whatever chord the camera itself is interpolating.
            if (!_trackingMode || _stretchLength <= 0 || _playback == null) return;
            ShowVehicle(_playback.PoseAtDistance(_stretchStart + e.Progress * _stretchLength));
        }

        /// <summary>
        /// One camera flight per stretch of the trace, at constant speed, the
        /// engine interpolating position, rotation and scale per frame. In
        /// overview mode the camera holds still and the clock moves the vehicle.
        /// </summary>
        private async Task DriveAsync(CancellationToken token)
        {
            var metersPerSecond = GroundSpeedMetersPerSecond / _laneMap.GroundScale;
            while (!token.IsCancellationRequested)
            {
                if (_trackingMode)
                {
                    var from = _pose.DistanceMeters;
                    if (from >= _playback.Length - 0.5)
                    {
                        from = 0;
                    }

                    var to = Math.Min(_playback.Length, from + StretchMeters);
                    _stretchStart = from;
                    _stretchLength = to - from;
                    var target = _playback.PoseAtDistance(to);
                    var scale = CameraScale();
                    var (center, rotation) = CameraTarget(target, scale);
                    var settings = new MapAnimationSettings
                    {
                        Type = MapAnimationType.DrawWithAnimation,
                        Duration = (uint)Math.Max(16, _stretchLength / metersPerSecond * 1000),
                        Easing = null
                    };

                    _flight = CancellationTokenSource.CreateLinkedTokenSource(token);
                    try
                    {
                        await Map.ZoomToAsync(center, scale, rotation, settings, _flight.Token);
                        if (!token.IsCancellationRequested)
                        {
                            ShowVehicle(target);
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        // Re-zoomed or switched to overview mid-flight: the vehicle
                        // stays where the last progress event put it.
                    }
                    finally
                    {
                        _stretchLength = 0;
                    }
                }
                else
                {
                    var clock = Stopwatch.StartNew();
                    var start = _pose.DistanceMeters;
                    while (!_trackingMode && !token.IsCancellationRequested)
                    {
                        var distance = start + clock.Elapsed.TotalSeconds * metersPerSecond;
                        if (distance >= _playback.Length)
                        {
                            start = 0;
                            clock.Restart();
                            distance = 0;
                        }

                        ShowVehicle(_playback.PoseAtDistance(distance));
                        await Task.Delay(16, token).ContinueWith(_ => { });
                    }
                }
            }
        }

        private async void OverviewButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_initialized || _playback == null) return;
            _trackingMode = !_trackingMode;
            OverviewButton.Content = _trackingMode ? "Overview Mode" : "Tracking Mode";
            _flight?.Cancel();
            if (_trackingMode)
            {
                // The next flight brings the camera home.
                ApplyPitch(PitchSlider.Value);
            }
            else
            {
                Map.TiltAngle = 45;
                var extent = _laneMap.OverviewExtent;
                var scale = MapUtil.GetScale(Map.MapUnit, extent, Map.MapWidth, Map.MapHeight);
                await Map.ZoomToAsync(extent.GetCenterPoint(), scale, 0, ViewChange);
            }
        }

        private void PitchSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_initialized || !_trackingMode) return;
            ApplyPitch(e.NewValue);
        }

        /// <summary>
        /// Wheel zoom while driving. The map must not zoom itself under a running
        /// flight, so the wheel changes the follow scale instead and the next flight
        /// applies it. Overview mode leaves the wheel to the map.
        /// </summary>
        private void Map_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!_trackingMode || _playback == null) return;
            var floor = DefaultCameraScale();
            _cameraScale = Math.Max(floor / 4, Math.Min(floor * 24, CameraScale() * (e.Delta > 0 ? 0.8 : 1.25)));
            e.Handled = true;
            _flight?.Cancel();
        }

        // --------------------------------------------------------------- HUD --

        private void UpdateHud(RoutePose pose)
        {
            var scale = _laneMap.GroundScale;
            ProgressText.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.0} mi complete", pose.DistanceMeters * scale / 1609.344);
            SpeedText.Text = string.Format(CultureInfo.InvariantCulture, "{0:0} mph", GroundSpeedMetersPerSecond * 2.23694 + Math.Sin(pose.DistanceMeters * 0.018) * 3);

            // The leg the vehicle is on names the lane; the leg after it is the
            // maneuver, with the distance to it.
            var legs = _laneMap.RouteLegs;
            var current = 0;
            while (current + 1 < legs.Count && pose.DistanceMeters >= legs[current + 1].StartDistance)
            {
                current++;
            }

            StreetText.Text = legs[current].Label;
            if (current + 1 < legs.Count)
            {
                var upcoming = legs[current + 1];
                var feet = Math.Max(0, upcoming.StartDistance - pose.DistanceMeters) * scale * 3.28084;
                InstructionText.Text = upcoming.Instruction;
                NextText.Text = upcoming.Lane.Length == 0
                    ? string.Format(CultureInfo.InvariantCulture, "In {0:0} ft", feet)
                    : string.Format(CultureInfo.InvariantCulture, "In {0:0} ft, use the {1} lane", feet, upcoming.Lane);
            }
            else
            {
                var remaining = Math.Max(0, _laneMap.RouteLength - pose.DistanceMeters) * scale * 3.28084;
                InstructionText.Text = "Destination ahead";
                NextText.Text = string.Format(CultureInfo.InvariantCulture, "In {0:0} ft", remaining);
            }
        }

        // ----------------------------------------------------------- vehicle --

        /// <summary>
        /// The vehicle seen from above - a light body with dark glass, drawn in WPF
        /// and rasterized once at 2x for the marker batch. Front is up.
        /// </summary>
        private static GeoImage RenderVehicleImage()
        {
            var shell = new Grid { Width = 60, Height = 100 };
            shell.Children.Add(new System.Windows.Shapes.Ellipse
            {
                Width = 52,
                Height = 90,
                Fill = new RadialGradientBrush(Color.FromArgb(0x70, 0x3f, 0x7d, 0xff), Color.FromArgb(0x00, 0x3f, 0x7d, 0xff)),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            });
            shell.Children.Add(new System.Windows.Shapes.Rectangle
            {
                Width = 30, Height = 66, RadiusX = 9, RadiusY = 9,
                Fill = Brush("#d9dee4"), Stroke = Brush("#f4f6f8"), StrokeThickness = 1.2,
                HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center
            });
            shell.Children.Add(new System.Windows.Shapes.Rectangle
            {
                Width = 22, Height = 12, RadiusX = 4, RadiusY = 4, Fill = Brush("#23282e"),
                HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(0, 30, 0, 0)
            });
            shell.Children.Add(new System.Windows.Shapes.Rectangle
            {
                Width = 20, Height = 9, RadiusX = 3, RadiusY = 3, Fill = Brush("#23282e"),
                HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(0, 0, 0, 26)
            });

            shell.Measure(new Size(60, 100));
            shell.Arrange(new Rect(0, 0, 60, 100));
            var bitmap = new RenderTargetBitmap(120, 200, 192, 192, PixelFormats.Pbgra32);
            bitmap.Render(shell);
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

        public void Dispose()
        {
            _disposed = true;
            _drive?.Cancel();
            _flight?.Cancel();
            _laneTiles?.Dispose();
            Map.Dispose();
            GC.SuppressFinalize(this);
        }

        // -------------------------------------------------------------- data --

        /// <summary>
        /// One HD map as the converter wrote it: features grouped by the
        /// source-layer the style draws them in, plus the drive.
        /// </summary>
        private sealed class LaneMap
        {
            public IReadOnlyDictionary<string, InMemoryFeatureSource> Sources { get; private set; }
            public IReadOnlyList<Pt> RouteTrace { get; private set; }
            public IReadOnlyList<RouteLeg> RouteLegs { get; private set; }
            public RectangleShape OverviewExtent { get; private set; }
            public double RouteLength { get; private set; }
            public int MarkingCount { get; private set; }
            public string Title { get; private set; }
            public string Attribution { get; private set; }

            /// <summary>Ground meters per Web Mercator meter at the route's latitude.</summary>
            public double GroundScale { get; private set; }

            public static LaneMap Load(string lanesFile, string routeFile)
            {
                var lanes = JsonNode.Parse(File.ReadAllText(lanesFile));
                var byLayer = new Dictionary<string, List<Feature>>(StringComparer.Ordinal);
                var columnsByLayer = new Dictionary<string, HashSet<string>>(StringComparer.Ordinal);
                foreach (var node in lanes["features"].AsArray())
                {
                    var properties = node["properties"].AsObject();
                    var layer = properties["layer"].GetValue<string>();
                    var geometry = node["geometry"];
                    var values = new Dictionary<string, string>(StringComparer.Ordinal);
                    foreach (var pair in properties)
                    {
                        values[pair.Key] = pair.Value?.ToString() ?? string.Empty;
                    }

                    BaseShape shape = geometry["type"].GetValue<string>() == "Polygon"
                        ? Polygon(ProjectCoordinates(geometry["coordinates"][0].AsArray()))
                        : Line(ProjectCoordinates(geometry["coordinates"].AsArray()));

                    if (!byLayer.TryGetValue(layer, out var list))
                    {
                        byLayer[layer] = list = new List<Feature>();
                        columnsByLayer[layer] = new HashSet<string>(StringComparer.Ordinal);
                    }

                    columnsByLayer[layer].UnionWith(values.Keys);
                    list.Add(new Feature(shape, values));
                }

                var sources = new Dictionary<string, InMemoryFeatureSource>(StringComparer.Ordinal);
                foreach (var pair in byLayer)
                {
                    var source = new InMemoryFeatureSource(columnsByLayer[pair.Key].Select(c => new FeatureSourceColumn(c)).ToArray(), pair.Value);
                    source.BuildIndex();
                    sources[pair.Key] = source;
                }

                // The drive: lane centerline points, each tagged with the lane it
                // lies in, plus that lane's position across the road.
                var routeJson = JsonNode.Parse(File.ReadAllText(routeFile));
                var points = new List<Pt>();
                var ordinals = new List<int>();
                foreach (var p in routeJson["points"].AsArray())
                {
                    points.Add(Project(p[0].GetValue<double>(), p[1].GetValue<double>()));
                    ordinals.Add(p[2].GetValue<int>());
                }

                var laneInfo = routeJson["lanes"].AsArray()
                    .Select(l => (Index: l["index"].GetValue<int>(), Count: l["count"].GetValue<int>(), Intersection: l["intersection"].GetValue<bool>()))
                    .ToList();

                // A leg per run of lanes with the same position on the road, so the
                // card names the lane to be in and warns before each intersection.
                var legs = new List<RouteLeg>();
                string previousLabel = null;
                for (var i = 0; i < points.Count; i++)
                {
                    var lane = laneInfo[ordinals[i]];
                    var label = lane.Intersection
                        ? "Intersection"
                        : string.Format(CultureInfo.InvariantCulture, "Lane {0} of {1}", lane.Index, lane.Count);
                    if (label == previousLabel) continue;
                    previousLabel = label;

                    var position = lane.Count <= 1 ? 0.5 : (lane.Index - 1) / (double)(lane.Count - 1);
                    var side = lane.Intersection ? string.Empty : position < 0.34 ? "left" : position > 0.66 ? "right" : "center";
                    var instruction = legs.Count == 0 ? "Start in " + label.ToLowerInvariant()
                        : lane.Intersection ? "Cross the intersection"
                        : "Keep to " + label.ToLowerInvariant();
                    legs.Add(new RouteLeg(label, PolylineLength(points.Take(i + 1).ToList()), instruction, side));
                }

                var trace = Densify(points, 8);
                var routeFeature = new Feature(Line(trace), new Dictionary<string, string> { ["level"] = "0" });
                var route = new InMemoryFeatureSource(new[] { new FeatureSourceColumn("level") }, new[] { routeFeature });
                route.BuildIndex();
                sources["route"] = route;

                var midY = trace[trace.Count / 2].Y;
                var attribution = routeJson["attribution"]?.GetValue<string>() ?? string.Empty;
                var name = Path.GetFileNameWithoutExtension(lanesFile).Replace("Lanes", string.Empty).Replace("Xodr", string.Empty);
                return new LaneMap
                {
                    Sources = sources,
                    RouteTrace = trace,
                    RouteLegs = legs,
                    OverviewExtent = ExtentFor(trace, 220),
                    RouteLength = PolylineLength(trace),
                    MarkingCount = byLayer.Where(p => p.Key.StartsWith("lane_marking", StringComparison.Ordinal)).Sum(p => p.Value.Count),
                    Title = name + ", OpenDRIVE HD map",
                    Attribution = attribution,
                    GroundScale = 1.0 / Math.Cosh(midY / 6378137.0)
                };
            }

            private static IReadOnlyList<Pt> ProjectCoordinates(JsonArray coordinates)
            {
                var result = new List<Pt>(coordinates.Count);
                foreach (var c in coordinates)
                {
                    result.Add(Project(c[0].GetValue<double>(), c[1].GetValue<double>()));
                }

                return result;
            }

            private static Pt Project(double longitude, double latitude)
            {
                const double radius = 6378137.0;
                var x = radius * longitude * Math.PI / 180.0;
                var lat = Math.Max(-85.05112878, Math.Min(85.05112878, latitude));
                var y = radius * Math.Log(Math.Tan(Math.PI / 4.0 + lat * Math.PI / 360.0));
                return new Pt(x, y);
            }

            private static LineShape Line(IEnumerable<Pt> points) =>
                new LineShape(points.Select(p => new Vertex(p.X, p.Y)).ToArray());

            private static PolygonShape Polygon(IEnumerable<Pt> points)
            {
                var vertices = points.Select(p => new Vertex(p.X, p.Y)).ToList();
                if (vertices.Count > 0 && (vertices[0].X != vertices[vertices.Count - 1].X || vertices[0].Y != vertices[vertices.Count - 1].Y))
                {
                    vertices.Add(vertices[0]);
                }

                return new PolygonShape(new RingShape(vertices));
            }

            private static IReadOnlyList<Pt> Densify(IReadOnlyList<Pt> points, double spacing)
            {
                var result = new List<Pt>();
                for (var i = 0; i < points.Count - 1; i++)
                {
                    var a = points[i];
                    var b = points[i + 1];
                    var steps = Math.Max(1, (int)Math.Ceiling(Pt.Distance(a, b) / spacing));
                    for (var s = 0; s < steps; s++)
                    {
                        result.Add(Pt.Lerp(a, b, s / (double)steps));
                    }
                }

                result.Add(points[points.Count - 1]);
                return result;
            }

            private static double PolylineLength(IReadOnlyList<Pt> points)
            {
                var total = 0d;
                for (var i = 0; i < points.Count - 1; i++)
                {
                    total += Pt.Distance(points[i], points[i + 1]);
                }

                return total;
            }

            private static RectangleShape ExtentFor(IEnumerable<Pt> points, double padding)
            {
                var array = points.ToArray();
                return new RectangleShape(
                    array.Min(p => p.X) - padding, array.Max(p => p.Y) + padding,
                    array.Max(p => p.X) + padding, array.Min(p => p.Y) - padding);
            }
        }

        /// <summary>One stretch of the route: what the card calls it, where it
        /// starts, the instruction that gets the driver onto it, and the lane to be in.</summary>
        private readonly struct RouteLeg
        {
            public RouteLeg(string label, double startDistance, string instruction, string lane)
            {
                Label = label;
                StartDistance = startDistance;
                Instruction = instruction;
                Lane = lane;
            }

            public string Label { get; }
            public double StartDistance { get; }
            public string Instruction { get; }
            public string Lane { get; }
        }

        /// <summary>Position and heading at any distance along the trace.</summary>
        private sealed class RoutePlayback
        {
            private readonly IReadOnlyList<Pt> _route;

            public RoutePlayback(IReadOnlyList<Pt> route)
            {
                _route = route;
                var total = 0d;
                for (var i = 0; i < route.Count - 1; i++)
                {
                    total += Pt.Distance(route[i], route[i + 1]);
                }

                Length = total;
            }

            public double Length { get; }

            public RoutePose PoseAtDistance(double targetDistance)
            {
                var walked = 0d;
                for (var i = 0; i < _route.Count - 1; i++)
                {
                    var a = _route[i];
                    var b = _route[i + 1];
                    var segment = Pt.Distance(a, b);
                    if (walked + segment >= targetDistance)
                    {
                        var t = segment < 0.001 ? 0 : (targetDistance - walked) / segment;
                        var heading = Math.Atan2(b.Y - a.Y, b.X - a.X) * 180 / Math.PI;
                        return new RoutePose(Pt.Lerp(a, b, t), heading, targetDistance);
                    }

                    walked += segment;
                }

                var end = _route[_route.Count - 1];
                var previous = _route[Math.Max(0, _route.Count - 2)];
                return new RoutePose(end, Math.Atan2(end.Y - previous.Y, end.X - previous.X) * 180 / Math.PI, Length);
            }
        }

        private readonly struct RoutePose
        {
            public RoutePose(Pt position, double headingDegrees, double distanceMeters)
            {
                Position = position;
                HeadingDegrees = headingDegrees;
                DistanceMeters = distanceMeters;
            }

            public Pt Position { get; }
            public double HeadingDegrees { get; }
            public double DistanceMeters { get; }
        }

        private readonly struct Pt
        {
            public Pt(double x, double y)
            {
                X = x;
                Y = y;
            }

            public double X { get; }
            public double Y { get; }

            public static double Distance(Pt a, Pt b) => Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));

            public static Pt Lerp(Pt a, Pt b, double t) => new Pt(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t);
        }
    }
}
