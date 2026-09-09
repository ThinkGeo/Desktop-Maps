using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Transform a shape: buffer, simplify, rotate, scale, translate, convex hull,
    /// envelope, intersection, difference and union. Every one of them is a single
    /// call on the shape - what changes between them is which call, which is why they
    /// are one sample rather than ten.
    /// <para>
    /// Only the shapes an operation actually reads are drawn. Nine of the ten work on
    /// one polygon, so one polygon is what you see; union is the exception and comes
    /// last for that reason.
    /// </para>
    /// <para>
    /// The five that take a number animate by default. Each frame runs the operation
    /// again on a swept value and hands the polygons straight to the GPU, which is
    /// what shows the difference between an operation you read about and one you can
    /// see the shape of.
    /// </para>
    /// </summary>
    public partial class ShapeOperations : IDisposable
    {
        // The area the intersection and difference work against, drawn in blue.
        private const string WestRegionWkt =
            "POLYGON((-10780139 3918539, -10780206 3915600, -10780037 3913978, -10779176 3913336, " +
            "-10778280 3911934, -10778263 3910684, -10778382 3909569, -10778280 3907356, " +
            "-10785595 3904045, -10786034 3904822, -10786001 3908150, -10785933 3909315, " +
            "-10786001 3911275, -10785832 3914485, -10785832 3917728, -10785460 3919012, " +
            "-10782233 3918995, -10780139 3918539))";

        private static readonly GeoColor SourceColor = new GeoColor(128, GeoColors.LightOrange);
        private static readonly GeoColor ResultColor = new GeoColor(96, GeoColors.Green);
        private static readonly GeoColor RegionColor = new GeoColor(32, GeoColors.Blue);

        private readonly PolygonShape _westRegion = new PolygonShape(WestRegionWkt);

        // What the operation reads, what it works against, and what it returned. All
        // three change as you move down the list, and the result changes on every frame
        // of the animation - which is what this channel is for. An Update replaces the
        // whole snapshot and it is on screen the next frame, with nothing re-cut.
        private readonly InMemoryGeometrySource _sourceArea = new InMemoryGeometrySource();
        private readonly InMemoryGeometrySource _regionArea = new InMemoryGeometrySource();
        private readonly InMemoryGeometrySource _resultArea = new InMemoryGeometrySource();

        private Collection<Feature> _pieces;
        private DispatcherTimer _timer;
        private double _phase;
        private bool _ready;

        // Whether union has been asked to run yet. It opens showing what it is about to
        // read, because that is the half of the operation you cannot see afterwards.
        private bool _merged;

        public ShapeOperations()
        {
            InitializeComponent();
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ready) return;

            Map.MapUnit = GeographyUnit.Meter;

            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));
            style.AddGeometry(_sourceArea).AddGeometry(_regionArea).AddGeometry(_resultArea);
            Map.Basemap = new GpuBasemap(style);

            // The city limits arrive cut into pieces, which is what gives Union
            // something to do. Read once and kept, then handed to whichever operation
            // is chosen - ten polygons do not need a tile pipeline.
            var source = new ShapeFileFeatureSource(SampleShared.Shapefile("FriscoCityLimitsDivided.shp"))
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            };
            source.Open();
            _pieces = source.GetAllFeatures(ReturningColumnsType.NoColumns);
            var bounds = source.GetBoundingBox();
            source.Close();

            Map.CenterPoint = bounds.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, bounds, Map.MapWidth, Map.MapHeight) * 1.5;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(33) };
            _timer.Tick += (s, args) => Advance();

            _ready = true;
            ShowOperation();
            await Map.RefreshAsync();
        }

        private void Operation_Checked(object sender, RoutedEventArgs e) => ShowOperation();

        private void Parameter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => Apply();

        private void Animate_Changed(object sender, RoutedEventArgs e) => SyncAnimation();

        /// <summary>
        /// A union drawn on its own looks like the shapes it came from, so it says
        /// nothing: the pieces are adjacent and the merged outline is the same outline.
        /// What tells you it happened is that two shapes in two colors became one
        /// shape in one - so the sample shows the before, and this runs it.
        /// </summary>
        private void MergeOne_Click(object sender, RoutedEventArgs e)
        {
            _merged = true;
            Apply();
        }

        private void MergeReset_Click(object sender, RoutedEventArgs e)
        {
            _merged = false;
            Apply();
        }

        /// <summary>
        /// Reveals the parameters the chosen operation takes - most take none - and
        /// runs it. Only an operation with a number to sweep can be animated.
        /// </summary>
        private void ShowOperation()
        {
            if (!_ready) return;

            var (first, firstValue, second, secondValue) = Parameters();
            ParameterPanel.Visibility = first == null ? Visibility.Collapsed : Visibility.Visible;
            if (first != null)
            {
                ParameterLabel.Text = first;
                ParameterBox.Text = firstValue;
            }

            SecondParameterLabel.Visibility = SecondParameterBox.Visibility =
                second == null ? Visibility.Collapsed : Visibility.Visible;
            if (second != null)
            {
                SecondParameterLabel.Text = second;
                SecondParameterBox.Text = secondValue;
            }

            UnionPanel.Visibility = OpUnion.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            _merged = false;

            _phase = 0;
            SyncAnimation();
            Apply();
        }

        /// <summary>
        /// The timer runs when the box is ticked and the chosen operation has something
        /// to sweep. Either can change without the other, so this is asked rather than
        /// tracked.
        /// </summary>
        private void SyncAnimation()
        {
            if (!_ready) return;

            if (Animate.IsChecked == true && ParameterPanel.Visibility == Visibility.Visible)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
        }

        private (string First, string FirstValue, string Second, string SecondValue) Parameters()
        {
            if (OpBuffer.IsChecked == true) return ("Distance (meters)", "1000", null, null);
            if (OpSimplify.IsChecked == true) return ("Tolerance (meters)", "500", null, null);
            if (OpRotate.IsChecked == true) return ("Degrees", "90", null, null);
            if (OpScale.IsChecked == true) return ("Percentage", "50", null, null);
            if (OpTranslate.IsChecked == true) return ("X offset (meters)", "3000", "Y offset (meters)", "3000");
            return (null, null, null, null);
        }

        /// <summary>
        /// The sweep each operation's number runs through, and whether it wraps or
        /// bounces. A rotation that bounced would rock rather than turn.
        /// </summary>
        private (double From, double To, bool Wraps) Sweep()
        {
            if (OpBuffer.IsChecked == true) return (100, 4000, false);
            if (OpSimplify.IsChecked == true) return (0, 4000, false);
            if (OpRotate.IsChecked == true) return (0, 360, true);
            if (OpScale.IsChecked == true) return (20, 200, false);
            return (-6000, 6000, false);
        }

        /// <summary>
        /// One frame: move the number along and write it back. Nothing else - the box
        /// already recomputes the shape when its text changes, so the animation and a
        /// typed value take the same path.
        /// </summary>
        private void Advance()
        {
            var (from, to, wraps) = Sweep();
            _phase = (_phase + 0.006) % 1.0;

            // Bouncing is the triangle wave; wrapping is the ramp itself.
            var travel = wraps ? _phase : 1 - Math.Abs((2 * _phase) - 1);
            ParameterBox.Text = (from + ((to - from) * travel)).ToString("0", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Runs the chosen operation and draws what it read beside what it returned.
        /// </summary>
        private void Apply()
        {
            if (!_ready) return;

            var first = _pieces[0];
            var shape = first.GetShape();
            var union = OpUnion.IsChecked == true;
            var paired = OpIntersection.IsChecked == true || OpDifference.IsChecked == true;

            BaseShape result;
            if (OpBuffer.IsChecked == true) result = shape.Buffer(Number(ParameterBox, 1000), GeographyUnit.Meter, DistanceUnit.Meter);
            else if (OpSimplify.IsChecked == true) result = AreaBaseShape.Simplify((AreaBaseShape)shape, Number(ParameterBox, 500), SimplificationType.DouglasPeucker);
            else if (OpRotate.IsChecked == true) result = BaseShape.Rotate(shape, shape.GetCenterPoint(), (float)Number(ParameterBox, 90));
            else if (OpScale.IsChecked == true) result = BaseShape.ScaleTo(shape, Number(ParameterBox, 50) / 100.0);
            else if (OpTranslate.IsChecked == true) result = BaseShape.TranslateByOffset(shape, Number(ParameterBox, 3000), Number(SecondParameterBox, 3000), GeographyUnit.Meter, DistanceUnit.Meter);
            else if (OpConvexHull.IsChecked == true) result = first.GetConvexHull().GetShape();
            else if (OpEnvelope.IsChecked == true) result = shape.GetBoundingBox();
            else if (OpIntersection.IsChecked == true) result = ((AreaBaseShape)shape).GetIntersection(_westRegion);
            else if (OpDifference.IsChecked == true) result = ((AreaBaseShape)shape).GetDifference(_westRegion);
            else result = _merged ? AreaBaseShape.Union(_pieces) : null;

            // Union is the only one that reads more than one shape, and so the only one
            // drawn with more than the polygon it worked on. Until it runs, its pieces
            // are shown in a color each - two shapes look like one blob otherwise, and
            // then merging them would look like nothing at all. The blue area appears
            // for the two operations that have a second operand, and not otherwise.
            var read = union && !_merged
                ? _pieces.SelectMany((piece, index) => Areas(piece.GetShape(), PieceColors[index % PieceColors.Length]))
                : union ? Enumerable.Empty<FillArea>()
                : Areas(shape, SourceColor);

            _sourceArea.UpdateAreas(read.ToList());
            _regionArea.UpdateAreas(paired ? Areas(_westRegion, RegionColor) : Array.Empty<FillArea>());
            // The result carries an outline: what an operation returned is easier to
            // read against what it read when its edge is drawn.
            _resultArea.UpdateAreas(result == null
                ? Array.Empty<FillArea>()
                : Areas(result, ResultColor, GeoColors.DarkGreen));

            if (union)
            {
                UnionCount.Text = _merged
                    ? _pieces.Count + " shapes became " + Polygons(result).Count() + " - one color, one outline"
                    : _pieces.Count + " separate shapes, each its own color";
                MergeOne.IsEnabled = !_merged;
                MergeReset.IsEnabled = _merged;
            }
        }

        /// <summary>One color per piece, so "separate" is something you can see.</summary>
        private static readonly GeoColor[] PieceColors =
        {
            new GeoColor(128, GeoColors.LightOrange),
            new GeoColor(128, GeoColors.MediumPurple),
        };

        /// <summary>
        /// The polygons of whatever the operation returned, as the GPU wants them:
        /// an outer ring, its holes, and a color. An operation that returns nothing -
        /// an intersection of things that do not meet - draws nothing.
        /// </summary>
        private static IReadOnlyList<FillArea> Areas(BaseShape shape, GeoColor color, GeoColor outline = null)
        {
            var areas = new List<FillArea>();
            foreach (var polygon in Polygons(shape))
            {
                areas.Add(new FillArea(
                    Ring(polygon.OuterRing),
                    color,
                    polygon.InnerRings.Select(Ring),
                    outline,
                    outline == null ? (float?)null : 2f));
            }

            return areas;
        }

        private static IEnumerable<PolygonShape> Polygons(BaseShape shape)
        {
            switch (shape)
            {
                case PolygonShape polygon: return new[] { polygon };
                case MultipolygonShape multipolygon: return multipolygon.Polygons;
                case RectangleShape rectangle: return new[] { rectangle.ToPolygon() };
                case RingShape ring: return new[] { new PolygonShape(ring) };
                default: return Enumerable.Empty<PolygonShape>();
            }
        }

        private static IEnumerable<PointShape> Ring(RingShape ring) =>
            ring.Vertices.Select(vertex => new PointShape(vertex.X, vertex.Y));

        /// <summary>A half-typed number is a number the user is still writing, not an error.</summary>
        private static double Number(System.Windows.Controls.TextBox box, double fallback) =>
            double.TryParse(box.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value) ? value : fallback;

        public void Dispose()
        {
            _timer?.Stop();
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
