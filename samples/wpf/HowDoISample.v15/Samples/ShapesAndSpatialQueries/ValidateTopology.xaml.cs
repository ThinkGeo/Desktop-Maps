using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Every rule TopologyValidator can check, on shapes small enough to see the answer:
    /// what points may touch, what lines may cross, what polygons may leave between
    /// them. Each rule is one call that hands back the features - or the parts of them -
    /// that break it.
    /// </summary>
    public partial class ValidateTopology
    {
        private readonly InMemoryGeometrySource _against = new InMemoryGeometrySource();
        private readonly InMemoryGeometrySource _checked = new InMemoryGeometrySource();
        private readonly InMemoryGeometrySource _broken = new InMemoryGeometrySource();
        private readonly DispatcherTimer _applyTimer;
        private bool _ready;

        public ValidateTopology()
        {
            InitializeComponent();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ready) return;

            Map.MapUnit = GeographyUnit.Meter;

            // Three sets of shapes that change with every rule, so they are geometry
            // rather than an overlay's raster tiles, which a zoom between two levels
            // would stretch.
            Map.Basemap = new GpuBasemap(new MapStyle(Editor.Text).AddGeometry(_against).AddGeometry(_checked).AddGeometry(_broken));

            _ready = true;
            Run();
            await Map.RefreshAsync();
        }

        private void Rule_Checked(object sender, RoutedEventArgs e) => Run();

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_ready) return;
            _applyTimer.Stop();
            _applyTimer.Start();
        }

        // A document that does not compile changes nothing; the map keeps the last one that worked.
        private async Task ApplyEditorStyleAsync()
        {
            if (!IsLoaded) return;

            try
            {
                await Map.Basemap.SetStyleAsync(new MapStyle(Editor.Text).AddGeometry(_against).AddGeometry(_checked).AddGeometry(_broken));
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }

        private static Collection<Feature> Wkt(params string[] wellKnownText)
        {
            var features = new Collection<Feature>();
            foreach (var text in wellKnownText)
            {
                features.Add(new Feature(text));
            }

            return features;
        }

        /// <summary>
        /// The chosen rule, its sample data, and what it says. The shapes differ per
        /// rule because a rule needs data that both passes and fails it to show
        /// anything - a set of polygons that never overlap says nothing about the
        /// overlap rule.
        /// </summary>
        private void Run()
        {
            if (!_ready) return;

            Collection<Feature> against = null;
            Collection<Feature> subject;
            Collection<Feature> broken;
            string note;

            if (RulePointsTouchLines.IsChecked == true)
            {
                subject = Wkt("POINT(0 0)", "POINT(50 0)", "POINT(150 0)");
                against = Wkt("LINESTRING(0 0,100 0)");
                broken = TopologyValidator.PointsMustTouchLines(subject, against).InvalidFeatures;
                note = "Points that miss the line are red.";
            }
            else if (RulePointsTouchLineEndpoints.IsChecked == true)
            {
                subject = Wkt("POINT(0 0)", "POINT(50 0)", "POINT(100 0)");
                against = Wkt("LINESTRING(0 0,100 0,100 100,0 100)");
                broken = TopologyValidator.PointsMustTouchLineEndpoints(subject, against).InvalidFeatures;
                note = "A point on the line but not at an end is still wrong.";
            }
            else if (RulePointsTouchPolygonBoundaries.IsChecked == true)
            {
                subject = Wkt("POINT(150 0)", "POINT(50 50)", "POINT(0 0)");
                against = Wkt("POLYGON((0 0,100 0,100 100,0 100,0 0))");
                broken = TopologyValidator.PointsMustTouchPolygonBoundaries(subject, against).InvalidFeatures;
                note = "Inside is not on the boundary; nor is outside.";
            }
            else if (RulePointsWithinPolygons.IsChecked == true)
            {
                subject = Wkt("POINT(150 0)", "POINT(0 0)", "POINT(50 50)");
                against = Wkt("POLYGON((0 0,100 0,100 100,0 100,0 0))");
                broken = TopologyValidator.PointsMustBeWithinPolygons(subject, against).InvalidFeatures;
                note = "A point on the corner is not within.";
            }
            else if (RuleLineEndpointsTouchPoints.IsChecked == true)
            {
                subject = Wkt("LINESTRING(0 0,100 0,100 50)");
                against = Wkt("POINT(0 0)");
                broken = TopologyValidator.LineEndPointsMustTouchPoints(subject, against).InvalidFeatures;
                note = "One end has its point, the other does not.";
            }
            else if (RuleLinesOverlapPolygonBoundaries.IsChecked == true)
            {
                subject = Wkt("LINESTRING(-50 0,150 0)");
                against = Wkt("POLYGON((0 0,100 0,100 100,0 100,0 0))");
                broken = TopologyValidator.LinesMustOverlapPolygonBoundaries(subject, against).InvalidFeatures;
                note = "Only the stretch that runs along the boundary counts.";
            }
            else if (RuleLinesCoveredByLines.IsChecked == true)
            {
                subject = Wkt("LINESTRING(0 0,100 0,100 100,0 100)");
                against = Wkt("LINESTRING(0 -50,50 0,100 0,100 150)");
                broken = TopologyValidator.LinesMustBeCoveredByLines(against, subject).InvalidFeatures;
                note = "The parts with nothing under them are red.";
            }
            else if (RuleLinesSinglePart.IsChecked == true)
            {
                subject = Wkt(
                    "MULTILINESTRING((0 -50,100 -50,100 -100,0 -100))",
                    "MULTILINESTRING((0 0,100 0),(100 50,0 50))");
                broken = TopologyValidator.LinesMustBeSinglePart(subject).InvalidFeatures;
                note = "One multiline is really one strand; the other is two.";
            }
            else if (RuleLinesFormClosedPolygon.IsChecked == true)
            {
                subject = Wkt("LINESTRING(0 0,100 0,100 100,20 100)", "LINESTRING(0 0,-50 0,-50 100,0 100)");
                broken = TopologyValidator.LinesMustFormClosedPolygon(subject).InvalidFeatures;
                note = "The ends that do not meet are red.";
            }
            else if (RuleLinesNoPseudonodes.IsChecked == true)
            {
                subject = Wkt(
                    "LINESTRING(0 0,50 0,50 50,0 0)",
                    "LINESTRING(-50 0,-50 50)",
                    "LINESTRING(-100 0,-50 50)",
                    "LINESTRING(-50 -50,-50 -100)",
                    "LINESTRING(-100 -50,-50 -100)",
                    "LINESTRING(-50 -100,0 -100)");
                broken = TopologyValidator.LinesMustNotHavePseudonodes(subject).InvalidFeatures;
                note = "A node joining exactly two lines is a pseudonode.";
            }
            else if (RuleLinesNoIntersect.IsChecked == true)
            {
                subject = Wkt(
                    "LINESTRING(0 0,100 0,100 100)",
                    "LINESTRING(0 -50,30 0,60 0,100 50)",
                    "LINESTRING(20 50,20 -50)");
                broken = TopologyValidator.LinesMustNotIntersect(subject).InvalidFeatures;
                note = "Each crossing comes back as a point.";
            }
            else if (RuleLinesNoSelfIntersectOrTouch.IsChecked == true)
            {
                subject = Wkt(
                    "LINESTRING(0 0,100 0,100 100)",
                    "LINESTRING(0 -50,30 0,60 0,100 50)",
                    "LINESTRING(20 50,20 -50)");
                broken = TopologyValidator.LinesMustNotSelfIntersectOrTouch(subject).InvalidFeatures;
                note = "Crossings and shared stretches both count.";
            }
            else if (RuleLinesNoOverlap.IsChecked == true)
            {
                subject = Wkt(
                    "LINESTRING(0 0,100 0,100 100)",
                    "LINESTRING(0 -50,30 0,60 0,100 50)",
                    "LINESTRING(20 50,20 -50)");
                broken = TopologyValidator.LinesMustNotOverlap(subject).InvalidFeatures;
                note = "Only the shared stretches, not the crossings.";
            }
            else if (RuleLinesNoOverlapLines.IsChecked == true)
            {
                subject = Wkt("LINESTRING(150 0,100 30,100 60,150 100)");
                against = Wkt("LINESTRING(0 0,100 0,100 100,0 100)");
                broken = TopologyValidator.LinesMustNotOverlapLines(against, subject).InvalidFeatures;
                note = "The stretch lying on the blue line is red.";
            }
            else if (RuleLinesNoSelfIntersect.IsChecked == true)
            {
                subject = Wkt("LINESTRING(0 0,100 0,100 100,50 100,50 -50)");
                broken = TopologyValidator.LinesMustNotSelfIntersect(subject).InvalidFeatures;
                note = "One line, crossing itself once.";
            }
            else if (RuleLinesNoSelfOverlap.IsChecked == true)
            {
                subject = Wkt("LINESTRING(0 0,100 0,100 100,0 100,20 0,40 0,40 -50)");
                broken = TopologyValidator.LinesMustNotSelfOverlap(subject).InvalidFeatures;
                note = "One line, doubling back along itself.";
            }
            else if (RulePolygonBoundariesOverlapBoundaries.IsChecked == true)
            {
                subject = Wkt("POLYGON((0 0,50 0,50 50,0 50,0 0))");
                against = Wkt("POLYGON((0 0,100 0,100 100,0 100,0 0))");
                broken = TopologyValidator.PolygonBoundariesMustOverlapPolygonBoundaries(against, subject).InvalidFeatures;
                note = "Two corners are shared; the rest of the boundary is not.";
            }
            else if (RulePolygonBoundariesOverlapLines.IsChecked == true)
            {
                subject = Wkt("POLYGON((0 0,100 0,100 100,0 100,0 0))");
                against = Wkt("LINESTRING(-50 0,100 0,100 150)");
                broken = TopologyValidator.PolygonBoundariesMustOverlapLines(subject, against).InvalidFeatures;
                note = "The line covers two sides; the other two are red.";
            }
            else if (RulePolygonsOverlapPolygons.IsChecked == true)
            {
                subject = Wkt(
                    "POLYGON((25 25,50 25,50 50,25 50,25 25))",
                    "POLYGON((75 25,125 25,125 75,75 75,75 25))",
                    "POLYGON((150 25,200 25,200 75,150 75,150 25))");
                against = Wkt("POLYGON((0 0,100 0,100 100,0 100,0 0))");
                broken = TopologyValidator.PolygonsMustOverlapPolygons(against, subject).InvalidFeatures;
                note = "The one that never meets the blue square is red.";
            }
            else if (RulePolygonsWithinPolygons.IsChecked == true)
            {
                subject = Wkt(
                    "POLYGON((25 25,50 25,50 50,25 50,25 25))",
                    "POLYGON((75 25,125 25,125 75,75 75,75 25))",
                    "POLYGON((150 25,200 25,200 75,150 75,150 25))");
                against = Wkt("POLYGON((0 0,100 0,100 100,0 100,0 0))");
                broken = TopologyValidator.PolygonsMustBeWithinPolygons(against, subject).InvalidFeatures;
                note = "Overlapping is not enough - it has to be all the way inside.";
            }
            else if (RulePolygonsContainPoints.IsChecked == true)
            {
                subject = Wkt(
                    "POLYGON((150 0,250 0,250 100,150 100,150 0))",
                    "POLYGON((0 0,100 0,100 100,0 100,0 0))");
                against = Wkt("POINT(50 50)");
                broken = TopologyValidator.PolygonsMustContainPoint(subject, against).InvalidFeatures;
                note = "The empty polygon is red.";
            }
            else if (RulePolygonsOverlapEachOther.IsChecked == true)
            {
                var first = Wkt("POLYGON((0 0,100 0,100 100,0 100,0 0))");
                var second = Wkt("POLYGON((-50 -50,50 -50,50 50,-50 50,-50 -50))");
                subject = Wkt(
                    "POLYGON((0 0,100 0,100 100,0 100,0 0))",
                    "POLYGON((-50 -50,50 -50,50 50,-50 50,-50 -50))");
                broken = TopologyValidator.PolygonsMustOverlapEachOther(first, second).InvalidFeatures;
                note = "This one reports from both sets: everything outside the shared corner.";
            }
            else if (RulePolygonsNoGaps.IsChecked == true)
            {
                subject = Wkt(
                    "POLYGON((0 0,40 0,40 40,0 40,0 0))",
                    "POLYGON((30 30,70 30,70 70,30 70,30 30))",
                    "POLYGON((60 0,100 0,100 40,60 40,60 0))",
                    "POLYGON((30 10,70 10,70 -30,30 -30,30 10))");
                broken = TopologyValidator.PolygonsMustNotHaveGaps(subject).InvalidFeatures;
                note = "The hole left between them comes back as its own shape.";
            }
            else if (RulePolygonsNoOverlap.IsChecked == true)
            {
                subject = Wkt(
                    "POLYGON((25 25,50 25,50 50,25 50,25 25))",
                    "POLYGON((75 25,125 25,125 75,75 75,75 25))",
                    "POLYGON((150 25,200 25,200 75,150 75,150 25))",
                    "POLYGON((0 0,100 0,100 100,0 100,0 0))");
                broken = TopologyValidator.PolygonsMustNotOverlap(subject).InvalidFeatures;
                note = "Red is the overlap itself, not the polygons that made it.";
            }
            else
            {
                subject = Wkt(
                    "POLYGON((25 25,50 25,50 50,25 50,25 25))",
                    "POLYGON((75 25,125 25,125 75,75 75,75 25))",
                    "POLYGON((150 25,200 25,200 75,150 75,150 25))");
                against = Wkt("POLYGON((0 0,100 0,100 100,0 100,0 0))");
                broken = TopologyValidator.PolygonsMustNotOverlapPolygons(against, subject).InvalidFeatures;
                note = "The reverse of the rule above: overlapping is the fault.";
            }

            Show(subject, against, broken, note);
        }

        private void Show(Collection<Feature> subject, Collection<Feature> against,
            Collection<Feature> broken, string note)
        {
            // A rule's answer is whatever geometry it happens to be - a point where two
            // lines meet, a stretch of line, the hole between polygons - so each set is
            // handed over whole three times and each call takes what it understands.
            Paint(_against, against ?? new Collection<Feature>(), GeoColors.Blue);
            Paint(_checked, subject, GeoColors.Green);
            Paint(_broken, broken, GeoColors.Red);

            Result.Text = broken.Count == 0 ? "Nothing breaks this rule. " + note : broken.Count + " in red. " + note;

            // Every rule uses its own shapes, at its own size, so the view follows them.
            var bounds = Bounds(against, subject, broken);
            Map.CenterPoint = bounds.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, bounds, Map.MapWidth, Map.MapHeight) * 1.5;
            _ = Map.RefreshAsync();
        }

        private static void Paint(InMemoryGeometrySource into, Collection<Feature> features, GeoColor color)
        {
            into.UpdateAreas(features, new GeoColor(50, color), color, 2f);
            into.UpdateLines(features, color, 3f);
            into.UpdatePoints(features, color, 12f);
        }

        /// <summary>Around everything the rule put on screen.</summary>
        private static RectangleShape Bounds(params Collection<Feature>[] sets)
        {
            RectangleShape bounds = null;
            foreach (var feature in sets.Where(set => set != null).SelectMany(set => set))
            {
                var box = feature.GetBoundingBox();
                if (bounds == null)
                {
                    bounds = box;
                }
                else
                {
                    bounds.ExpandToInclude(box);
                }
            }

            return bounds ?? new RectangleShape(-100, 100, 100, -100);
        }
    }
}
