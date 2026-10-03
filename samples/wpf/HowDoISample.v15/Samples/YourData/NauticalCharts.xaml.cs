using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// An S-57 chart drawn with its embedded S-52 styling, and a ship under way that asks the
    /// chart at every step about the water she is in: the depth area and the under-keel
    /// clearance it leaves, the aids to navigation and the hazards nearby, the restricted areas.
    /// </summary>
    public partial class NauticalCharts : IDisposable
    {
        private static readonly string ChartFile = Path.Combine(AppContext.BaseDirectory, "Data", "S57", "US4IL10M", "US4IL10M.000");
        private static readonly HashSet<string> Aids = new HashSet<string> { "LIGHTS", "BOYLAT", "BOYSAW", "BOYSPP", "BOYCAR", "BOYISD", "BCNLAT", "BCNSPP", "BCNCAR", "BCNISD", "DAYMAR", "LNDMRK" };
        private static readonly HashSet<string> Hazards = new HashSet<string> { "UWTROC", "WRECKS", "OBSTRN" };
        private static readonly GeoColor Navy = GeoColor.FromHtml("#1F3A93");
        private const double StepInMeters = 60;

        private readonly DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };
        private readonly ProjectionConverter _toMeters = new ProjectionConverter(4326, 3857);
        private readonly InMemoryFeatureLayer _track = new InMemoryFeatureLayer();
        private readonly InMemoryFeatureLayer _shipLayer = new InMemoryFeatureLayer();
        private readonly LayerOverlay _trackOverlay = new LayerOverlay { TileType = TileType.SingleTile };
        private readonly PointStyle _shipStyle = new PointStyle(PointSymbolType.Triangle, 22, new GeoSolidBrush(Navy), new GeoPen(GeoColors.White, 2));
        private readonly object _helm = new object();

        private NauticalChartsFeatureSource _chart;
        private PointShape[] _routeInDegrees;
        private PointShape _lastPosition;
        private int _leg;
        private double _along;
        private bool _asking;
        private bool _built;

        private sealed class Reading
        {
            public string Status;
            public double? Shallowest;
            public double? Deepest;
            public double? UnderKeel;
            public List<string> Aids = new List<string>();
            public List<string> Hazards = new List<string>();
            public List<string> Restricted = new List<string>();
        }

        public NauticalCharts()
        {
            InitializeComponent();
            Map.MapUnit = GeographyUnit.Meter;
            _toMeters.Open();
            _timer.Tick += (_, __) => Tick();
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_built) return;
            _built = true;

            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));

            var chart = new NauticalChartsFeatureLayer(ChartFile)
            {
                IsDepthContourTextVisible = true,
                IsLightDescriptionVisible = true,
                StylingType = NauticalChartsStylingType.EmbeddedStyling,
                SymbolTextDisplayMode = NauticalChartsSymbolTextDisplayMode.None,
                DisplayCategory = NauticalChartsDisplayCategory.All,
                DefaultColorSchema = NauticalChartsDefaultColorSchema.DayBright,
                SymbolDisplayMode = NauticalChartsSymbolDisplayMode.Simplified,
                BoundaryDisplayMode = NauticalChartsBoundaryDisplayMode.Plain,
                DrawingMode = NauticalChartsDrawingMode.Optimized,
                IsFullLightLineVisible = true,
                IsMetaObjectsVisible = false,
            };
            // The depths that decide the colour of the water, in meters.
            chart.SafetyDepthInMeter = NauticalChartsFeatureLayer.ConvertDistanceToMeters(28, NauticalChartsDepthUnit.Meter);
            chart.ShallowDepthInMeter = NauticalChartsFeatureLayer.ConvertDistanceToMeters(3, NauticalChartsDepthUnit.Meter);
            chart.DeepDepthInMeter = NauticalChartsFeatureLayer.ConvertDistanceToMeters(10, NauticalChartsDepthUnit.Meter);
            chart.SafetyContourDepthInMeter = NauticalChartsFeatureLayer.ConvertDistanceToMeters(10, NauticalChartsDepthUnit.Meter);
            // The chart is in latitude and longitude; the map is in meters.
            chart.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            var chartOverlay = new LayerOverlay();
            chartOverlay.Layers.Add(chart);
            Map.Overlays.Add(chartOverlay);

            // The route, the wake coloured by what the chart said there, and the ship, over the chart.
            _track.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            _track.Columns.Add(new FeatureSourceColumn("status"));
            var lines = new ValueStyle("status", new Collection<ValueItem>
            {
                new ValueItem("route", new LineStyle(new GeoPen(new GeoColor(140, Navy), 2) { DashStyle = LineDashStyle.Dash })),
                new ValueItem("ok", new LineStyle(new GeoPen(GeoColor.FromHtml("#2E7D32"), 4))),
                new ValueItem("caution", new LineStyle(new GeoPen(GeoColor.FromHtml("#E69100"), 4))),
                new ValueItem("danger", new LineStyle(new GeoPen(GeoColor.FromHtml("#C62828"), 4))),
                new ValueItem("unknown", new LineStyle(new GeoPen(GeoColor.FromHtml("#888888"), 4))),
            });
            _track.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(lines);
            _track.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            _shipLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            _shipLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = _shipStyle;
            _shipLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            _trackOverlay.Layers.Add(_track);
            _trackOverlay.Layers.Add(_shipLayer);
            Map.Overlays.Add(_trackOverlay);

            // The chart opened once more for the questions the helm asks; a source answers one at a time.
            _chart = new NauticalChartsFeatureSource(ChartFile) { ProjectionConverter = new ProjectionConverter(4326, 3857) };
            _chart.Open();

            Map.TrackOverlay.TrackEnded += RouteDrawn;

            Map.CenterPoint = (PointShape)_toMeters.ConvertToExternalProjection(new PointShape(-87.578, 41.880));
            Map.CurrentScale = 36000;
            await Map.RefreshAsync();

            SetRoute(new[]
            {
                new PointShape(-87.597, 41.893), new PointShape(-87.575, 41.897), new PointShape(-87.558, 41.882),
                new PointShape(-87.560, 41.864), new PointShape(-87.586, 41.871), new PointShape(-87.597, 41.893),
            });
            _timer.Start();
        }

        private void SetRoute(PointShape[] routeInDegrees)
        {
            _routeInDegrees = routeInDegrees;
            _leg = 0;
            _along = 0;
            _lastPosition = null;
            _track.InternalFeatures.Clear();
            _track.InternalFeatures.Add("route", new Feature(new LineShape(routeInDegrees.Select(point => new Vertex(point.X, point.Y))), new Dictionary<string, string> { ["status"] = "route" }));
            Place(routeInDegrees[0], 0);
        }

        private void Place(PointShape positionInDegrees, float headingInDegrees)
        {
            _shipStyle.RotationAngle = -headingInDegrees;
            _shipLayer.InternalFeatures.Clear();
            _shipLayer.InternalFeatures.Add(new Feature(positionInDegrees));
        }

        // The ship moves a step along the route every tick; at each step the chart is asked what
        // it holds there for a ship of this draft, and the answer is shown and left in the wake.
        // One question at a time: a step whose answer is late is not asked twice.
        private async void Tick()
        {
            if (_asking) return;

            var from = _routeInDegrees[_leg];
            var to = _routeInDegrees[(_leg + 1) % _routeInDegrees.Length];
            var length = MetersBetween(from, to);
            _along += StepInMeters;
            if (_along >= length)
            {
                _along = 0;
                _leg = (_leg + 1) % _routeInDegrees.Length;
                return;
            }

            var t = _along / length;
            var here = new PointShape(from.X + (to.X - from.X) * t, from.Y + (to.Y - from.Y) * t);
            Place(here, Bearing(from, to));

            if (!double.TryParse(DraftBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var draft)) draft = 6.5;
            _asking = true;
            try
            {
                var reading = await Task.Run(() => Ask(here, draft));
                Show(reading);

                var wake = new LineShape(new[] { new Vertex(_lastPosition ?? here), new Vertex(here) });
                _track.InternalFeatures.Add(new Feature(wake, new Dictionary<string, string> { ["status"] = reading.Status }));
                if (_track.InternalFeatures.Count > 400) _track.InternalFeatures.RemoveAt(1);
                _lastPosition = here;
                await _trackOverlay.RefreshAsync();
            }
            catch (Exception ex)
            {
                Hint.Text = ex.Message;
            }
            _asking = false;
        }

        // What the chart says about one place, for a ship of the draft given: the depth area it
        // is in, the under-keel clearance that leaves, the aids to navigation within 800 m and the
        // hazards within 500 m, each with its distance, and any restricted area it is in. Every
        // object comes off the chart's own S-57 data, named by the S-57 catalogue.
        private Reading Ask(PointShape positionInDegrees, double draft)
        {
            var reading = new Reading();
            var here = (PointShape)_toMeters.ConvertToExternalProjection(positionInDegrees);
            lock (_helm)
            {
                var around = _chart.GetFeaturesWithinDistanceOf(here, GeographyUnit.Meter, DistanceUnit.Meter, 800, ReturningColumnsType.AllColumns);
                var aids = new List<(string name, double distance)>();
                var hazards = new List<(string name, double distance)>();
                var nearestHazard = double.MaxValue;
                foreach (var feature in around)
                {
                    var kind = feature.ColumnValues.TryGetValue("OBJCLS", out var acronym) ? acronym : "";
                    var shape = feature.GetShape();
                    if (kind == "DEPARE" && shape is AreaBaseShape area && area.Contains(here))
                    {
                        if (Number(feature, "DRVAL1") is double low && (reading.Shallowest == null || low < reading.Shallowest)) reading.Shallowest = low;
                        if (Number(feature, "DRVAL2") is double high && (reading.Deepest == null || high > reading.Deepest)) reading.Deepest = high;
                    }
                    else if (kind == "RESARE" && shape is AreaBaseShape zone && zone.Contains(here))
                    {
                        var details = Details(feature);
                        reading.Restricted.Add(details.Length == 0 ? Name(feature) : Name(feature) + " - " + details);
                    }
                    else if (Aids.Contains(kind))
                    {
                        aids.Add((Name(feature), here.GetDistanceTo(shape, GeographyUnit.Meter, DistanceUnit.Meter)));
                    }
                    else if (Hazards.Contains(kind))
                    {
                        var distance = here.GetDistanceTo(shape, GeographyUnit.Meter, DistanceUnit.Meter);
                        if (distance > 500) continue;
                        nearestHazard = Math.Min(nearestHazard, distance);
                        hazards.Add((Name(feature), distance));
                    }
                }

                reading.UnderKeel = reading.Shallowest == null ? (double?)null : Math.Round(reading.Shallowest.Value - draft, 1);
                reading.Status = reading.Shallowest == null ? "unknown"
                    : reading.UnderKeel < 0 ? "danger"
                    : reading.UnderKeel < 2 || nearestHazard < 300 ? "caution" : "ok";
                reading.Aids = aids.OrderBy(aid => aid.distance).Take(3).Select(aid => $"{aid.name} - {aid.distance:0} m").ToList();
                reading.Hazards = hazards.OrderBy(hazard => hazard.distance).Take(3).Select(hazard => $"{hazard.name} - {hazard.distance:0} m").ToList();
            }
            return reading;
        }

        private void Show(Reading reading)
        {
            StatusPill.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(reading.Status == "ok" ? "#2E7D32" : reading.Status == "caution" ? "#E69100" : reading.Status == "danger" ? "#C62828" : "#888888"));
            StatusText.Text = reading.Status == "ok" ? "clear water" : reading.Status == "caution" ? "caution" : reading.Status == "danger" ? "grounding" : "no depth charted";
            DepthText.Text = reading.Shallowest == null ? "-" : $"{reading.Shallowest} - {reading.Deepest} m";
            UnderKeelText.Text = reading.UnderKeel == null ? "-" : $"{(reading.UnderKeel >= 0 ? "+" : "")}{reading.UnderKeel} m";
            AidsList.ItemsSource = reading.Aids.Count > 0 ? reading.Aids : new List<string> { "none within 800 m" };
            HazardsList.ItemsSource = reading.Hazards.Count > 0 ? reading.Hazards : new List<string> { "none" };
            RestrictedList.ItemsSource = reading.Restricted.Count > 0 ? reading.Restricted : new List<string> { "-" };
        }

        private static double? Number(Feature feature, string column) =>
            feature.ColumnValues.TryGetValue(column, out var text) && double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value) ? value : (double?)null;

        // What a restricted area restricts, and the rule behind it, in the catalogue's words.
        private string Details(Feature feature)
        {
            var description = _chart.GetFeatureDescription(feature);
            if (description == null) return "";
            var told = new[] { "CATREA", "RESTRN", "INFORM" };
            return string.Join("; ", description.Attributes.Where(attribute => told.Contains(attribute.Acronym) && !string.IsNullOrWhiteSpace(attribute.Value)).Select(attribute => attribute.Name + ": " + attribute.Value));
        }

        // The object's class name from the S-57 catalogue - "Light", "Lateral buoy", "Underwater
        // rock" - and its own name when the chart gives it one.
        private string Name(Feature feature)
        {
            var description = _chart.GetFeatureDescription(feature);
            var kind = description?.ObjectClassName ?? (feature.ColumnValues.TryGetValue("OBJCLS", out var acronym) ? acronym : "Object");
            return feature.ColumnValues.TryGetValue("OBJNAM", out var own) && !string.IsNullOrWhiteSpace(own) ? kind + " " + own.Trim() : kind;
        }

        private double MetersBetween(PointShape a, PointShape b)
        {
            var am = (PointShape)_toMeters.ConvertToExternalProjection(a);
            var bm = (PointShape)_toMeters.ConvertToExternalProjection(b);
            return Math.Sqrt((bm.X - am.X) * (bm.X - am.X) + (bm.Y - am.Y) * (bm.Y - am.Y)) * Math.Cos(a.Y * Math.PI / 180);
        }

        private static float Bearing(PointShape a, PointShape b) =>
            (float)(Math.Atan2((b.X - a.X) * Math.Cos(a.Y * Math.PI / 180), b.Y - a.Y) * 180 / Math.PI);

        private void SailButton_Click(object sender, RoutedEventArgs e)
        {
            if (_timer.IsEnabled) Stop(); else Start();
        }

        private void Stop()
        {
            _timer.Stop();
            SailButton.Content = "Sail";
        }

        private void Start()
        {
            _timer.Start();
            SailButton.Content = "Stop";
        }

        // A route of your own: click the vertices, double-click to finish; the ship sails it in a loop.
        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            Stop();
            Map.TrackOverlay.TrackMode = TrackMode.Line;
        }

        private void RouteDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            Map.TrackOverlay.TrackMode = TrackMode.None;
            Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            if (!(e.TrackShape is LineShape line) || line.Vertices.Count < 2) return;

            var vertices = line.Vertices.Select(vertex => (PointShape)_toMeters.ConvertToInternalProjection(new PointShape(vertex.X, vertex.Y))).ToList();
            vertices.Add(vertices[0]);
            SetRoute(vertices.ToArray());
            Start();
        }

        public void Dispose()
        {
            _timer.Stop();
            _chart?.Close();
            Map.Dispose();
        }
    }
}
