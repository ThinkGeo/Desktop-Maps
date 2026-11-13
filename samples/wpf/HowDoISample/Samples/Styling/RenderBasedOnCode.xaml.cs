using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Two things a style document cannot hold: a clock that sweeps day and night across the world's capitals, and a choropleth painted by a C# lambda.
    /// </summary>
    public partial class RenderBasedOnCode : IDisposable
    {
        private const string CapitalsLayer = "WorldCapitals";
        private const string StatesLayer = "states";
        private const string RampFillId = "states-fill";
        private const string RampOutlineId = "states-outline";
        private const string RampMode = "Ramp";

        // A full day sweeps by in about 24 seconds; drag the slider to take over.
        private readonly DispatcherTimer _clock = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };

        private readonly List<(double Offset, string DayId, string NightId, bool? IsDay)> _zones = new();

        private MapStyle _style;
        private string _mode = "Clock";
        private double _utcHour = DateTime.UtcNow.Hour + (DateTime.UtcNow.Minute / 60.0);
        private bool _settingSlider;
        private bool _initialized;

        public RenderBasedOnCode()
        {
            InitializeComponent();

            HourSlider.Value = _utcHour;
            HourSlider.ValueChanged += (_, e) =>
            {
                if (_settingSlider) return;
                _utcHour = e.NewValue;
                ShowHour();
                ApplyHour();
            };

            _clock.Tick += (_, _) =>
            {
                _utcHour = (_utcHour + 0.05) % 24;
                _settingSlider = true;
                HourSlider.Value = _utcHour;
                _settingSlider = false;
                ShowHour();
                ApplyHour();
            };

            ShowHour();
            ShowMode();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            FrameMode();

            var capitals = new FeatureSourceVectorTileSource();
            capitals.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("WorldCapitals.shp"))
            {
                ProjectionConverter = new ProjectionConverter(4326, 3857),
            });

            var states = new FeatureSourceVectorTileSource();
            states.FeatureSources.Add(StatesLayer, new ShapeFileFeatureSource(SampleShared.Shapefile("USStates_3857.shp")));

            // A paint is baked into a tile when the tile is built, so a paint that read the
            // clock would freeze at the hour each tile was cut. Each zone gets a fixed day
            // layer and a fixed night layer instead, and the clock only switches which one
            // is visible.
            var layers = new List<StyleLayer>();
            foreach (var offset in ReadTimeZoneOffsets())
            {
                var zoneOffset = offset;
                var dayId = FormattableString.Invariant($"utc{zoneOffset:+0.0;-0.0}-day");
                var nightId = FormattableString.Invariant($"utc{zoneOffset:+0.0;-0.0}-night");
                _zones.Add((zoneOffset, dayId, nightId, null));

                layers.Add(StyleLayer.CreateCircle(
                    dayId, CapitalsLayer,
                    paint: (_, _) => new CirclePaint(GeoColor.FromArgb(255, 255, 215, 0), GeoColors.Black, 8f, 2f, 1f),
                    filter: (feature, _) => TryReadTimezone(feature, out var value) &&
                                            Math.Abs(value - zoneOffset) < 0.01));
                layers.Add(StyleLayer.CreateCircle(
                    nightId, CapitalsLayer,
                    paint: (_, _) => new CirclePaint(GeoColor.FromArgb(255, 70, 80, 95), GeoColors.Black, 8f, 2f, 1f),
                    filter: (feature, _) => TryReadTimezone(feature, out var value) &&
                                            Math.Abs(value - zoneOffset) < 0.01));
            }

            _style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });

            _style.AddVectorTiles(states, "usstates", StyleLayerSlot.AboveFills,
                StyleLayer.CreateFill(RampFillId, StatesLayer,
                    paint: (feature, _) => new FillPaint(DensityColor(feature), opacity: 0.60f)),
                StyleLayer.CreateLine(RampOutlineId, StatesLayer,
                    paint: (_, zoom) => new LinePaint(GeoColors.White, zoom < 5d ? 0.6f : 1.4f, opacity: 0.9f)));
            _style.AddVectorTiles(capitals, "features", layers.ToArray());

            ShowMode();
            Map.Basemap = new GpuBasemap(_style);

            await Map.RefreshAsync();
            ShowMode();
        }

        private async void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radio && radio.Tag is string mode)
            {
                _mode = mode;
                ShowMode();
                if (_initialized)
                {
                    FrameMode();
                    await Map.RefreshAsync();
                }
            }
        }

        /// <summary>The world for the clock, the lower 48 for the ramp.</summary>
        private void FrameMode()
        {
            if (_mode == RampMode)
            {
                Map.CenterPoint = new PointShape(-10800000, 4700000);
                Map.CurrentScale = 30000000;
            }
            else
            {
                Map.CenterPoint = new PointShape(450060, 1074670);
                Map.CurrentScale = 147914800;
            }
        }

        /// <summary>Shows one group of code layers and hides the other; the clock only
        /// runs while its circles are on screen.</summary>
        private void ShowMode()
        {
            var ramp = _mode == RampMode;
            if (RampText != null)
            {
                ClockRow.Visibility = ramp ? Visibility.Collapsed : Visibility.Visible;
                RampText.Visibility = ramp ? Visibility.Visible : Visibility.Collapsed;
            }

            if (_style == null) return;

            _style.SetLayerVisibility(RampFillId, ramp);
            _style.SetLayerVisibility(RampOutlineId, ramp);
            if (ramp)
            {
                _clock.Stop();
                for (var i = 0; i < _zones.Count; i++)
                {
                    var zone = _zones[i];
                    _style.SetLayerVisibility(zone.DayId, false);
                    _style.SetLayerVisibility(zone.NightId, false);
                    _zones[i] = (zone.Offset, zone.DayId, zone.NightId, null);
                }
            }
            else
            {
                ApplyHour();
                _clock.Start();
            }
        }

        /// <summary>Flips each zone between its day and night layer; only the zones
        /// whose answer changed are touched.</summary>
        private void ApplyHour()
        {
            if (_style == null || _mode == RampMode) return;

            for (var i = 0; i < _zones.Count; i++)
            {
                var zone = _zones[i];
                var localHour = (((_utcHour + zone.Offset) % 24) + 24) % 24;
                var isDay = localHour >= 7 && localHour <= 19;
                if (zone.IsDay == isDay)
                {
                    continue;
                }

                _style.SetLayerVisibility(zone.DayId, isDay);
                _style.SetLayerVisibility(zone.NightId, !isDay);
                _zones[i] = (zone.Offset, zone.DayId, zone.NightId, isDay);
            }
        }

        /// <summary>Five-step blue ramp over people per square mile.</summary>
        private static GeoColor DensityColor(Feature feature)
        {
            var density = 0d;
            if (feature.ColumnValues.TryGetValue("POP90_SQMI", out var raw))
            {
                double.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out density);
            }

            if (density < 10d) return GeoColor.FromHtml("#EFF3FF");
            if (density < 50d) return GeoColor.FromHtml("#BDD7E7");
            if (density < 150d) return GeoColor.FromHtml("#6BAED6");
            if (density < 500d) return GeoColor.FromHtml("#3182BD");
            return GeoColor.FromHtml("#08519C");
        }

        /// <summary>The offsets the data actually uses - half-hour zones included.</summary>
        private static IEnumerable<double> ReadTimeZoneOffsets()
        {
            var source = new ShapeFileFeatureSource(SampleShared.Shapefile("WorldCapitals.shp"));
            source.Open();
            var features = source.GetAllFeatures(new[] { "TIMEZONE" });
            source.Close();

            return features
                .Select(feature => TryReadTimezone(feature, out var value) ? value : double.NaN)
                .Where(value => !double.IsNaN(value))
                .Distinct()
                .OrderBy(value => value)
                .ToList();
        }

        private static bool TryReadTimezone(Feature feature, out double value)
        {
            value = 0;
            return feature?.ColumnValues != null &&
                   feature.ColumnValues.TryGetValue("TIMEZONE", out var raw) &&
                   double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }

        private void ShowHour() => HourLabel.Text = FormattableString.Invariant(
            $"{(int)_utcHour:00}:{(int)((_utcHour - (int)_utcHour) * 60):00}");

        public void Dispose()
        {
            _clock.Stop();
            Map.Dispose();
        }
    }
}
