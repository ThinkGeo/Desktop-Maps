using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Two things a style document cannot hold, one style, one map.
    ///
    /// The clock: each timezone is a pair of code-built circle layers, day and night,
    /// selected by a filter delegate; the rolling UTC hour flips their visibility at
    /// runtime, so day and night sweep the globe with no tile rebuilt and no style
    /// recompiled.
    ///
    /// The data: US states shaded by 1990 population density, where the paint is a
    /// lambda that reads the feature - the choropleth ramp is ordinary C#. The
    /// style-JSON equivalent would be
    /// <c>["interpolate", ["linear"], ["get", "POP90_SQMI"], 0, "#eff3ff", 500, "#08519c"]</c>,
    /// written as text with no compiler checking the column name. What a lambda gives
    /// up is being DATA: it cannot be downloaded, hot-swapped or handed to a
    /// cartographer, which is why the basemap underneath still arrives as a
    /// style.json. The ramp is inserted above the basemap's fills, so its roads and
    /// labels stay on top of the shading.
    /// </summary>
    public partial class RenderBasedOnCode : IDisposable
    {
        private const string CapitalsLayer = "WorldCapitals";
        private const string StatesLayer = "states";
        private const string RampFillId = "states-fill";
        private const string RampOutlineId = "states-outline";
        private const string RampMode = "Ramp";

        private const string ClockBlurb =
            "A style document has no clock. Each timezone is a pair of code-built circle layers - day and night, selected by a C# filter delegate - and the rolling UTC hour flips their visibility at runtime, so the sweep never rebuilds a tile. Drag the slider to take over.";

        private const string RampBlurb =
            "A paint delegate reads each state's 1990 population density and answers with a color from a five-step ramp - the choropleth is ordinary C#, with the column name checked by the compiler. The fill and its white outline are inserted above the basemap's own fills, so the roads and labels stay on top.";

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

            // Paints - layer or per-feature - are baked into a tile's buffers when the
            // tile is built, so a paint that read the clock would freeze at whatever
            // hour each tile was cut. Visibility is the per-frame runtime channel:
            // each zone gets a static day layer and a static night layer (same filter),
            // and the clock only flips which one shows.
            var layers = new List<Func<int, StyleLayer>>();
            foreach (var offset in ReadTimeZoneOffsets())
            {
                var zoneOffset = offset;
                var dayId = FormattableString.Invariant($"utc{zoneOffset:+0.0;-0.0}-day");
                var nightId = FormattableString.Invariant($"utc{zoneOffset:+0.0;-0.0}-night");
                _zones.Add((zoneOffset, dayId, nightId, null));

                layers.Add(index => StyleLayer.CreateCircle(
                    dayId, index, "features", CapitalsLayer,
                    paint: (_, _) => new CirclePaint(GeoColor.FromArgb(255, 255, 215, 0), GeoColors.Black, 8f, 2f, 1f),
                    filter: (feature, _) => TryReadTimezone(feature, out var value) &&
                                            Math.Abs(value - zoneOffset) < 0.01));
                layers.Add(index => StyleLayer.CreateCircle(
                    nightId, index, "features", CapitalsLayer,
                    paint: (_, _) => new CirclePaint(GeoColor.FromArgb(255, 70, 80, 95), GeoColors.Black, 8f, 2f, 1f),
                    filter: (feature, _) => TryReadTimezone(feature, out var value) &&
                                            Math.Abs(value - zoneOffset) < 0.01));
            }

            _style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));

            // A slot is found by walking the layers already loaded, so the document
            // must be open before inserting into it. The ramp goes above the basemap's
            // fills - under its roads and labels; the capitals go on top of everything.
            await _style.OpenAsync();
            _style.InsertStyleLayersAt(StyleLayerSlot.AboveFills, states, "usstates",
                index => StyleLayer.CreateFill(RampFillId, index, "usstates", StatesLayer,
                    paint: (feature, _) => new FillPaint(
                        color: DensityColor(feature), opacity: 0.60f, outlineColor: null,
                        antialias: true, translateX: 0f, translateY: 0f)),
                index => StyleLayer.CreateLine(RampOutlineId, index, "usstates", StatesLayer,
                    paint: (_, zoom) => new LinePaint(
                        color: GeoColors.White, width: zoom < 5d ? 0.6f : 1.4f, opacity: 0.9f,
                        blur: 0f, gapWidth: 0f, offset: 0f)));
            _style.AddStyleLayers(capitals, "features", layers.ToArray());

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
            if (ClockRow != null)
            {
                ClockRow.Visibility = ramp ? Visibility.Collapsed : Visibility.Visible;
                Blurb.Text = ramp ? RampBlurb : ClockBlurb;
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
