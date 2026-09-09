using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Isolines computed ON the GPU: the classic DynamicIsoLineLayer pipeline
    /// (IDW grid interpolation, then marching squares, both on the CPU and both
    /// re-run per redraw) becomes two fragment passes - the scalar field is
    /// generated per texel and contoured per pixel, re-evaluated every frame.
    /// The point-count combo scales the same mosquito data up to 100k synthetic
    /// points; the classic layer takes seconds per redraw at 10k, the GPU field
    /// rebuilds in about a millisecond and pan/zoom/tilt stay at frame rate.
    /// </summary>
    public partial class GpuIsolines : IDisposable
    {
        private const int MaxDrawnDataPoints = 2_000;

        private readonly GpuBasemap _overlay;
        private readonly MapStyle _style;
        private readonly Dictionary<PointShape, double> _seedPoints;
        private readonly RectangleShape _dataBounds;
        private readonly System.Windows.Threading.DispatcherTimer _costTimer;

        private GridSource _isolineSource;
        private bool _initialized;

        // The live knobs: every change re-issues the isoline registration and the next frame shows
        // it - which is the demonstration. The classic layer would re-interpolate its
        // grid and re-run marching squares for each of these.
        private Dictionary<PointShape, double> _currentPoints;
        private bool _fillBands;
        private double _searchRadius;   // 0 = every point
        private bool _showLabels = true;
        private bool _rebuildEveryFrame;
        private SimpleMarkerOverlay _pointOverlay;
        private LayerOverlay _exportOverlay;
        private InMemoryFeatureLayer _exportLayer;
        private int _levelCount = 25;
        private float _lineWidth = 3f;
        private double _power = 2.0;

        public GpuIsolines()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            Map.TiltInteractionEnabled = true;
            _style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));
            _overlay = new GpuBasemap(_style);
            Map.Basemap = _overlay;

            _seedPoints = LoadCsv(@"./Data/Csv/Frisco_Mosquitos.csv");
            var xs = _seedPoints.Keys.Select(p => p.X).ToList();
            var ys = _seedPoints.Keys.Select(p => p.Y).ToList();
            _dataBounds = new RectangleShape(xs.Min(), ys.Max(), xs.Max(), ys.Min());

            ((ComboBoxItem)PointCountCombo.Items[0]).Content = $"{_seedPoints.Count} points (real sample data)";
            PointCountCombo.SelectedIndex = 0;

            _costTimer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500),
            };
            _costTimer.Tick += (_, __) => UpdateCost();
            _costTimer.Start();
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            var start = (RectangleShape)_dataBounds.CloneDeep();
            start.ScaleUp(60);
            Map.CenterPoint = start.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, start, Map.MapWidth, Map.MapHeight);
            _currentPoints = _seedPoints;
            ApplyIsolines();
            ShowDataPoints(PointToggle.IsChecked == true);
            await Map.RefreshAsync();
        }

        private void PointCountCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_initialized)
            {
                return;
            }

            _currentPoints = PointsForSelection(PointCountCombo.SelectedIndex);
            ApplyIsolines();
            ShowDataPoints(PointToggle.IsChecked == true);
        }

        private void FillToggle_Changed(object sender, RoutedEventArgs e)
        {
            _fillBands = (sender as CheckBox)?.IsChecked == true;
            ApplyIsolines();
        }

        private void LabelToggle_Changed(object sender, RoutedEventArgs e)
        {
            _showLabels = (sender as CheckBox)?.IsChecked == true;
            ApplyIsolines();
        }

        private void PointToggle_Changed(object sender, RoutedEventArgs e)
        {
            if (_initialized)
            {
                ShowDataPoints(PointToggle.IsChecked == true);
            }
        }

        private void EveryFrameToggle_Changed(object sender, RoutedEventArgs e)
        {
            _rebuildEveryFrame = (sender as CheckBox)?.IsChecked == true;
            ApplyIsolines();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e) => ExportFeatures();

        private void LevelSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _levelCount = (int)e.NewValue;
            ApplyIsolines();
        }

        private void WidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _lineWidth = (float)e.NewValue;
            ApplyIsolines();
        }

        private void PowerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _power = e.NewValue;
            ApplyIsolines();
        }

        private void RadiusSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _searchRadius = e.NewValue * (_dataBounds?.Width ?? 0);
            ApplyIsolines();
        }

        private void ApplyIsolines()
        {
            if (!_initialized)
            {
                return;
            }

            var points = _currentPoints ?? _seedPoints;

            // Levels and colors exactly as the classic DisplayISOLine sample builds
            // them: breaks over the data, colored blue to red.
            var levels = IsoLineLayer.GetIsoLineLevels(points, _levelCount);
            var colors = GeoColor.GetColorsInQualityFamily(
                GeoColors.Blue, GeoColors.Red, levels.Count, ColorWheelDirection.Clockwise);

            // The two halves of the old options bag, split where they belong: how the
            // field is MADE rides the data source, how the contours are DRAWN rides
            // the style.
            var interpolation = new IsolineInterpolationOptions
            {
                Power = _power,
                SearchRadius = _searchRadius,
                RebuildEveryFrame = _rebuildEveryFrame,
                // The field pass is O(texels x points); at 100k points a 1024^2 field
                // costs ~1.8s per rebuild on an RTX 2070, 512^2 a quarter of that.
                // Rebuilds happen on zoom or data/parameter changes, not per frame,
                // so this is the hitch budget, not the frame budget.
                FieldResolution = points.Count > 20_000 ? 512 : 1024,
            };
            var display = new IsolineDisplayOptions
            {
                Levels = levels,
                LevelColors = colors.ToList(),
                LineWidthInPixels = _lineWidth,
                FillOpacity = _fillBands ? 0.45f : 0f,
                // Labels every second level, the usual contour-map cadence; the value
                // is the mosquito count the level stands for.
                ShowLabels = _showLabels,
                LabelEveryNthLevel = 2,
            };

            // Levels and colors are the style's; the points ride a source object. The
            // document did not change, so SetStyleAsync takes the cheap path - it only
            // re-pushes the isoline registration, no tile is rebuilt.
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            if (_isolineSource != null)
            {
                _style.RemoveIsolines(_isolineSource);
            }

            _isolineSource = new GridSource(points, interpolation);
            _style.AddIsolines(_isolineSource, display);
            _ = _overlay.SetStyleAsync(_style);
            stopwatch.Stop();
            var path = string.Format(
                CultureInfo.InvariantCulture,
                "IDW on the GPU - AddIsolines took {0:0.0} ms on the UI thread, the field builds on the next frame",
                stopwatch.Elapsed.TotalMilliseconds);

            Status.Text = string.Format(
                CultureInfo.InvariantCulture,
                "{0:N0} points, {1} levels - {2}",
                points.Count, levels.Count, path);
        }

        // A field built ANYWHERE ELSE reaches the same contour passes through
        // SetIsolineField(values, width, height, extent, levels, colors):
        // a grid from any GridInterpolationModel, a .grd file, a weather or DEM grid.
        // It is not in this sample's UI because for THIS data the CPU grid is both
        // slower and coarser than the field shader, and a dropdown that makes the
        // sample worse teaches the wrong thing.
        //
        // If you do reach for kriging there, note that it takes its points in its OWN
        // constructor and ignores GridDefinition.DataPoints - unlike IDW. Constructed
        // empty it has nothing to interpolate from and hangs (see
        // KrigingGridInterpolationModel.ListPointAvailables, whose search loop has no
        // exit when the dataset holds fewer points than referencingPointCount). Given
        // its points it is fast: a 16x12 grid over this data takes about 30 ms.

        /// <summary>
        /// The seed data as-is, or a synthetic scale-up: random points across the data
        /// area whose values follow a quick CPU IDW over the seeds plus noise, so the
        /// large sets contour like a real measurement campaign instead of white noise.
        /// </summary>
        private Dictionary<PointShape, double> PointsForSelection(int selectedIndex)
        {
            var count = selectedIndex switch { 1 => 1_000, 2 => 10_000, 3 => 100_000, _ => 0 };
            if (count == 0)
            {
                return _seedPoints;
            }

            var rng = new Random(42);
            var seeds = _seedPoints.ToList();
            var points = new Dictionary<PointShape, double>(count);
            while (points.Count < count)
            {
                var x = _dataBounds.MinX + rng.NextDouble() * _dataBounds.Width;
                var y = _dataBounds.MinY + rng.NextDouble() * _dataBounds.Height;

                double top = 0, bottom = 0;
                foreach (var seed in seeds)
                {
                    var dx = x - seed.Key.X;
                    var dy = y - seed.Key.Y;
                    var w = 1.0 / (dx * dx + dy * dy + 1.0);
                    top += w * seed.Value;
                    bottom += w;
                }

                var value = top / bottom + (rng.NextDouble() - 0.5) * 12.0;
                points[new PointShape(x, y)] = value;
            }

            return points;
        }

        private static Dictionary<PointShape, double> LoadCsv(string path)
        {
            var points = new Dictionary<PointShape, double>();
            foreach (var line in File.ReadLines(path))
            {
                var parts = line.Split(',');
                if (parts.Length < 3)
                {
                    continue;
                }

                points[new PointShape(
                    double.Parse(parts[0], CultureInfo.InvariantCulture),
                    double.Parse(parts[1], CultureInfo.InvariantCulture))] =
                    double.Parse(parts[2], CultureInfo.InvariantCulture);
            }

            return points;
        }

        /// <summary>
        /// Draws the data points themselves, as screen-space markers.
        /// </summary>
        private void ShowDataPoints(bool visible)
        {
            var points = _currentPoints ?? _seedPoints;
            if (_pointOverlay == null)
            {
                _pointOverlay = new SimpleMarkerOverlay();
                Map.Overlays.Add("dataPoints", _pointOverlay);
            }

            _pointOverlay.Markers.Clear();
            if (visible && points.Count <= MaxDrawnDataPoints)
            {
                foreach (var pair in points)
                {
                    _pointOverlay.Markers.Add(new Marker(pair.Key)
                    {
                        Width = 11,
                        Height = 11,
                        Content = new System.Windows.Shapes.Ellipse
                        {
                            Width = 11,
                            Height = 11,
                            Fill = Brushes.Black,
                            Stroke = Brushes.White,
                            StrokeThickness = 2,
                        },
                    });
                }
            }

            _ = _pointOverlay.RefreshAsync();
        }

        /// <summary>
        /// Draws the exported geometry over the GPU contours, in black, so the two can be
        /// compared directly - the traced polylines should sit on the per-pixel lines
        /// they were read from, and where they visibly cut corners is where the export
        /// grid was coarser than the screen. Clicking export again replaces them.
        /// </summary>
        private void ShowExportedFeatures(System.Collections.ObjectModel.Collection<Feature> features)
        {
            if (_exportOverlay == null)
            {
                _exportLayer = new InMemoryFeatureLayer();
                // Dashed on purpose: the traced geometry lands exactly on the lines it
                // was read from, and a solid stroke would simply hide them - there would
                // be nothing left to compare. Through the gaps you see the GPU contour
                // underneath, and any place the two part company stands out.
                var exportPen = new GeoPen(GeoColors.Black, 1.5f);
                exportPen.DashPattern.Add(3f);
                exportPen.DashPattern.Add(3f);
                _exportLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(exportPen);
                _exportLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                _exportOverlay = new LayerOverlay();
                _exportOverlay.Layers.Add("exported", _exportLayer);
                Map.Overlays.Add("exportedFeatures", _exportOverlay);
            }

            _exportLayer.InternalFeatures.Clear();
            foreach (var feature in features)
            {
                _exportLayer.InternalFeatures.Add(feature);
            }

            _ = _exportOverlay.RefreshAsync();
        }

        private void UpdateCost()
        {
            var stats = _overlay.GetIsolineStats();
            if (stats.FieldWidth == 0)
            {
                return;
            }

            Cost.Text = string.Format(
                CultureInfo.InvariantCulture,
                "field {0}x{1}{2} | last build {3:0.0} ms | rebuilds {4} | points {5:N0}",
                stats.FieldWidth, stats.FieldHeight,
                stats.FieldSupplied ? " (supplied)" : string.Empty,
                stats.LastFieldBuildMilliseconds, stats.FieldRebuilds, stats.PointCount);
        }

        /// <summary>
        /// The per-pixel drawing never makes geometry, so this is how you get some: read
        /// the field back and hand it to the SDK's own marching squares. Note what is
        /// NOT happening - the surface is not interpolated a second time, which is the
        /// whole reason the two halves are worth separating.
        /// </summary>
        private async void ExportFeatures()
        {
            Status.Text = "reading the field back...";
            var field = await _overlay.GetIsolineFieldAsync();
            var levels = IsoLineLayer.GetIsoLineLevels(_currentPoints ?? _seedPoints, _levelCount);

            // Off the UI thread: marching squares over a screen-resolution field is
            // SECONDS of work (the readout says how many), which is the point worth
            // taking away - the display never pays this, and an export should choose
            // a grid sized for the geometry it wants rather than for the screen.
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var (features, vertices) = await System.Threading.Tasks.Task.Run(() =>
            {
                var extent = field.Extent;
                var width = field.Width;
                var height = field.Height;
                var cellWidth = extent.Width / width;
                var cellHeight = extent.Height / height;
                var matrix = new GridCell[height, width];
                for (var row = 0; row < height; row++)
                {
                    for (var column = 0; column < width; column++)
                    {
                        // GridCell wants the cell's CENTER; row 0 is the north edge.
                        var x = extent.MinX + (column + 0.5) * cellWidth;
                        var y = extent.MaxY - (row + 0.5) * cellHeight;
                        var value = field.Values[row * width + column];
                        matrix[row, column] = new GridCell(x, y, float.IsNaN(value) ? double.NaN : value);
                    }
                }

                var isoFeatures = IsoLineLayer.GetIsoFeatures(
                    matrix, levels, "Value", IsoLineType.LinesOnly, double.NaN);
                var vertexCount = isoFeatures.Sum(f => f.GetShape() switch
                {
                    LineShape line => line.Vertices.Count,
                    MultilineShape multiline => multiline.Lines.Sum(l => l.Vertices.Count),
                    _ => 0,
                });
                return (isoFeatures, vertexCount);
            });
            stopwatch.Stop();

            Status.Text = string.Format(
                CultureInfo.InvariantCulture,
                "exported {0:N0} features / {1:N0} vertices from the {2}x{3} field in {4:N0} ms - " +
                "drawn in black over the GPU contours; marching squares, no second interpolation",
                features.Count, vertices, field.Width, field.Height, stopwatch.Elapsed.TotalMilliseconds);
            ShowExportedFeatures(features);
        }

        public void Dispose()
        {
            _costTimer?.Stop();
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
