using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Two colors of dot mixed inside each state - countable, and a composition a
    /// heatmap cannot show. The dots are generated once and drawn as data.
    /// </summary>
    public partial class DisplayDotDensity
    {
        /// <summary>Housing units one dot stands for.</summary>
        private const int UnitsPerDot = 25_000;

        private readonly DispatcherTimer _applyTimer;
        private bool _initialized;

        public DisplayDotDensity()
        {
            InitializeComponent();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // USStates_3857.shp is already in the map's projection - no converter needed.
            var states = new ShapeFileFeatureSource(SampleShared.Shapefile("USStates_3857.shp"));
            states.Open();
            var features = states.GetAllFeatures(new[] { "OWNER_OCC", "RENTER_OCC" });
            states.Close();

            var tileSource = new FeatureSourceVectorTileSource();
            tileSource.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("USStates_3857.shp")));
            tileSource.FeatureSources.Add("owner", Scatter(features, "OWNER_OCC", seed: 1));
            tileSource.FeatureSources.Add("renter", Scatter(features, "RENTER_OCC", seed: 2));

            Map.Basemap = new GpuBasemap(new MapStyle(Editor.Text, tileSource));

            // Frame the lower 48; Alaska would stretch the box to the Aleutians.
            var lower48 = new RectangleShape(-13_900_000, 6_400_000, -7_400_000, 2_800_000);
            Map.CenterPoint = lower48.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, lower48, Map.MapWidth, Map.MapHeight);

            _ = Map.RefreshAsync();
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
                await Map.Basemap.SetStyleAsync(new MapStyle(Editor.Text));
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }

        /// <summary>
        /// One point per <see cref="UnitsPerDot"/> units, rejection-sampled inside each
        /// state. Seeded per state and category, so the pattern is identical on every
        /// run and in every tile - a dot field that re-rolled itself would be a
        /// different map each time you looked.
        /// </summary>
        private static FeatureSource Scatter(IEnumerable<Feature> states, string column, int seed)
        {
            var dots = new Collection<Feature>();
            var stateIndex = 0;
            foreach (var state in states)
            {
                stateIndex++;
                if (state.GetShape() is not AreaBaseShape area ||
                    !state.ColumnValues.TryGetValue(column, out var raw) ||
                    !double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out var units))
                {
                    continue;
                }

                var wanted = (int)Math.Round(units / UnitsPerDot);
                if (wanted <= 0)
                {
                    continue;
                }

                var box = area.GetBoundingBox();
                var random = new Random((seed * 7919) + stateIndex);
                var placed = 0;

                // The attempt ceiling keeps a thin or ragged state from spinning forever.
                var attempts = 0;
                var attemptCeiling = wanted * 60;
                while (placed < wanted && attempts < attemptCeiling)
                {
                    attempts++;
                    var candidate = new PointShape(
                        box.LowerLeftPoint.X + (random.NextDouble() * box.Width),
                        box.LowerLeftPoint.Y + (random.NextDouble() * box.Height));
                    if (!area.Contains(candidate))
                    {
                        continue;
                    }

                    dots.Add(new Feature(candidate.GetWellKnownBinary(),
                        FormattableString.Invariant($"{seed}-{stateIndex}-{placed}")));
                    placed++;
                }
            }

            return new InMemoryFeatureSource(Array.Empty<FeatureSourceColumn>(), dots);
        }
    }
}
