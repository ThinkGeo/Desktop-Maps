using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// State populations from 1950 to 2020 as circles in a DynamicSource, sized and tinted by the year's population.
    /// </summary>
    public partial class DynamicRendering
    {
        private DynamicSource _circles;
        private List<(double X, double Y, string Abbreviation)> _stateCenters;
        private Dictionary<(string State, int Year), int> _population;
        private bool _initialized;

        public DynamicRendering()
        {
            InitializeComponent();
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

                _circles = new DynamicSource();
                var states = new FeatureSourceVectorTileSource();
                states.FeatureSources.Add("states", new ShapeFileFeatureSource(shapefilePath));

                // One white disc image is every circle; the marker's color tints it.
                var style = new MapStyle()
                    .SetBackground(GeoColor.FromHtml("#1F2226"))
                    .AddStyle((string)Resources["StyleJson"], states)
                    .AddDynamic(_circles);
                style.Images.Add("disc", new PointStyle(PointSymbolType.Circle, 62, GeoBrushes.White));
                Map.Basemap = new ThinkGeo.UI.Wpf.GpuBasemap(style);

                Map.CenterPoint = new PointShape(-10650000, 4770000);
                Map.CurrentScale = 37000000;
                ShowYear(YearSlider.Value);
            }
            catch (Exception ex)
            {
                YearLabel.Text = "!";
                YearLabel.ToolTip = ex.Message;
            }

            await Map.RefreshAsync();
        }

        private void YearSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_initialized)
                return;

            YearLabel.Text = ((int)e.NewValue).ToString();
            ShowYear(e.NewValue);
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
        /// Every state's marker, kept under the state's abbreviation, takes the size
        /// and color of its population at that moment - between two census years the
        /// population is interpolated, so a played animation grows smoothly instead of
        /// jumping once a year.
        /// </summary>
        private void ShowYear(double moment)
        {
            var year = (int)Math.Floor(moment);
            var fraction = moment - year;
            if (_circles == null || _stateCenters == null)
            {
                return;
            }

            foreach (var (x, y, abbreviation) in _stateCenters)
            {
                if (!_population.TryGetValue((abbreviation, year), out var before))
                {
                    _circles.Remove(abbreviation);
                    continue;
                }

                var after = _population.TryGetValue((abbreviation, year + 1), out var next) ? next : before;
                var population = before + ((after - before) * fraction);

                var strength = (float)Math.Min(population / 300000d, 150d);
                var color = GeoColor.FromArgb(150, (byte)(255 * (1 - (strength / 150))), (byte)(255 * strength / 150), 0);
                _circles.SetPoint(abbreviation, new Feature(x, y), 0f, color, Math.Max(4f, strength), "disc");
            }
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
