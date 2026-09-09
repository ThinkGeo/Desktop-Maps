using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A century of state populations as animated circles, entirely. Each
    /// circle is a TrackPoint whose size and tint are data; the circle SHAPE is one
    /// white image registered with AddImage and instanced by the renderer - any
    /// marker shape is an image, so the renderer never grows per-shape code. Dragging
    /// the year is one Update over fifty points: no recompile, no tile rebuilds, and
    /// none of the tile-edge clipping that kept the old screen-space circles on the
    /// per-frame CPU overlay.
    /// </summary>
    public partial class DynamicRendering
    {
        private readonly DispatcherTimer _applyTimer;
        private FeatureSourceVectorTileSource _states;
        private InMemoryGeometrySource _circles;
        private List<(double X, double Y, string Abbreviation)> _stateCenters;
        private Dictionary<(string State, int Year), int> _population;
        private bool _initialized;

        public DynamicRendering()
        {
            InitializeComponent();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            try
            {
                var shapefilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "USStates_3857.shp");
                _stateCenters = ReadStateCenters(shapefilePath);
                _population = ReadPopulation(Path.Combine(AppContext.BaseDirectory, "Data", "historical_state_population_by_year.csv"));

                _circles = new InMemoryGeometrySource();
                _states = new FeatureSourceVectorTileSource();
                _states.FeatureSources.Add("states", new ShapeFileFeatureSource(shapefilePath));
                Map.Basemap = new ThinkGeo.UI.Wpf.GpuBasemap(ComposeStyle());

                Map.CenterPoint = new PointShape(-10650000, 4770000);
                Map.CurrentScale = 37000000;
                ShowYear((int)YearSlider.Value);
            }
            catch (Exception ex)
            {
                YearLabel.Text = "!";
                YearLabel.ToolTip = ex.Message;
            }

            await Map.RefreshAsync();
        }

        /// <summary>
        /// The state polygons cut into tiles; the circles ride the marker batch over
        /// them. One white disc image is every circle.
        /// </summary>
        private MapStyle ComposeStyle()
        {
            var style = new MapStyle()
                .SetBackground(GeoColors.White)
                .AddStyle(Editor.Text, _states)
                .AddGeometry(_circles);
            style.Images.Add("disc", CreateDiscImage());
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

        private void YearSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_initialized)
                return;

            YearLabel.Text = ((int)e.NewValue).ToString();
            ShowYear((int)e.NewValue);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            YearSlider.BeginAnimation(Slider.ValueProperty, new DoubleAnimation
            {
                From = YearSlider.Minimum,
                To = YearSlider.Maximum,
                Duration = TimeSpan.FromSeconds(4),
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut },
            });
        }

        /// <summary>
        /// One year: every state contributes one TrackPoint whose size and color are
        /// its population, and the whole set goes down in a single Update.
        /// </summary>
        private void ShowYear(int year)
        {
            if (_circles == null || _stateCenters == null)
            {
                return;
            }

            var points = new List<TrackPoint>(_stateCenters.Count);
            foreach (var (x, y, abbreviation) in _stateCenters)
            {
                if (!_population.TryGetValue((abbreviation, year), out var population))
                {
                    continue;
                }

                var strength = Math.Min(population / 300000f, 150f);
                points.Add(new TrackPoint(x, y, 0,
                    GeoColor.FromArgb(150, (byte)(255 * (1 - (strength / 150))), (byte)(255 * strength / 150), 0),
                    Math.Max(4f, strength), "disc"));
            }

            _circles.UpdatePoints(points);
        }

        /// <summary>One antialiased white disc; every circle on the map is this image, tinted and scaled.</summary>
        private static GeoImage CreateDiscImage()
        {
            using var bitmap = new SkiaSharp.SKBitmap(new SkiaSharp.SKImageInfo(64, 64,
                SkiaSharp.SKColorType.Rgba8888, SkiaSharp.SKAlphaType.Premul));
            using (var canvas = new SkiaSharp.SKCanvas(bitmap))
            using (var paint = new SkiaSharp.SKPaint { Color = SkiaSharp.SKColors.White, IsAntialias = true })
            {
                canvas.Clear(SkiaSharp.SKColors.Transparent);
                canvas.DrawCircle(32, 32, 31, paint);
            }

            using var image = SkiaSharp.SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SkiaSharp.SKEncodedImageFormat.Png, 100);
            return new GeoImage(data.ToArray());
        }

        private static List<(double, double, string)> ReadStateCenters(string shapefilePath)
        {
            var source = new ShapeFileFeatureSource(shapefilePath);
            source.Open();
            try
            {
                var centers = new List<(double, double, string)>();
                foreach (var feature in source.GetAllFeatures(ReturningColumnsType.AllColumns))
                {
                    var center = feature.GetShape().GetCenterPoint();
                    centers.Add((center.X, center.Y, feature.ColumnValues["STATE_ABBR"]));
                }

                return centers;
            }
            finally
            {
                source.Close();
            }
        }

        private static Dictionary<(string, int), int> ReadPopulation(string csvPath)
        {
            var population = new Dictionary<(string, int), int>();
            foreach (var line in File.ReadAllLines(csvPath))
            {
                var fields = line.Split(',');
                population[(fields[0], int.Parse(fields[1]))] = int.Parse(fields[2]);
            }

            return population;
        }
    }
}
