using System;
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
    /// A heatmap layer over point sightings: the document beside the map decides what
    /// each sighting weighs, and radius, intensity, opacity and color ramp are live
    /// controls - SetHeatmapPaint is a runtime call, the next frame draws with the new
    /// values and nothing blinks.
    /// </summary>
    public partial class Heatmap
    {
        private const string HeatLayerId = "coyote-heat";

        /// <summary>heatmap-color as density stops, 0 (no data) to 1 (densest). Stop 0
        /// is transparent in every ramp - it leaves the basemap visible where nothing
        /// was seen.</summary>
        private static readonly (string Name, HeatmapColorStop[] Stops)[] ColorRamps =
        {
            ("MapLibre default", new[]
            {
                 new HeatmapColorStop(0.0, GeoColor.FromArgb(0, 0, 0, 255)),
                 new HeatmapColorStop(0.1, GeoColor.FromArgb(255, 65, 105, 225)),
                 new HeatmapColorStop(0.3, GeoColor.FromArgb(255, 0, 255, 255)),
                 new HeatmapColorStop(0.5, GeoColor.FromArgb(255, 0, 255, 0)),
                 new HeatmapColorStop(0.7, GeoColor.FromArgb(255, 255, 255, 0)),
                 new HeatmapColorStop(1.0, GeoColor.FromArgb(255, 255, 0, 0)),
            }),
            ("Warm", new[]
            {
                 new HeatmapColorStop(0.0, GeoColor.FromArgb(0, 255, 255, 178)),
                 new HeatmapColorStop(0.15, GeoColor.FromArgb(180, 255, 255, 178)),
                 new HeatmapColorStop(0.4, GeoColor.FromArgb(255, 254, 204, 92)),
                 new HeatmapColorStop(0.7, GeoColor.FromArgb(255, 253, 141, 60)),
                 new HeatmapColorStop(1.0, GeoColor.FromArgb(255, 189, 0, 38)),
            }),
            ("Viridis", new[]
            {
                 new HeatmapColorStop(0.0, GeoColor.FromArgb(0, 68, 1, 84)),
                 new HeatmapColorStop(0.25, GeoColor.FromArgb(255, 59, 82, 139)),
                 new HeatmapColorStop(0.5, GeoColor.FromArgb(255, 33, 145, 140)),
                 new HeatmapColorStop(0.75, GeoColor.FromArgb(255, 94, 201, 98)),
                 new HeatmapColorStop(1.0, GeoColor.FromArgb(255, 253, 231, 37)),
            }),
            ("Blue to red", new[]
            {
                 new HeatmapColorStop(0.0, GeoColor.FromArgb(0, 33, 102, 172)),
                 new HeatmapColorStop(0.2, GeoColor.FromArgb(255, 103, 169, 207)),
                 new HeatmapColorStop(0.4, GeoColor.FromArgb(255, 209, 229, 240)),
                 new HeatmapColorStop(0.6, GeoColor.FromArgb(255, 253, 219, 199)),
                 new HeatmapColorStop(0.8, GeoColor.FromArgb(255, 239, 138, 98)),
                 new HeatmapColorStop(1.0, GeoColor.FromArgb(255, 178, 24, 43)),
            }),
        };

        private readonly ThinkGeoVectorTileSource _cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);
        private readonly FeatureSourceVectorTileSource _coyote = new FeatureSourceVectorTileSource();
        private readonly DispatcherTimer _applyTimer;
        private (string Name, HeatmapColorStop[] Stops) _ramp = ColorRamps[0];
        private MapStyle _style;
        private bool _initialized;

        public Heatmap()
        {
            InitializeComponent();

            RadiusSlider.ValueChanged += (_, _) => ApplyPaint();
            IntensitySlider.ValueChanged += (_, _) => ApplyPaint();
            OpacitySlider.ValueChanged += (_, _) => ApplyPaint();
            ShowPaintPreview();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.ZoomStep = 0.15;
            Map.CenterPoint = new PointShape(-10778000, 3912500);
            Map.CurrentScale = 68000;

            _coyote.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("Frisco_Coyote_Sightings.shp"))
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            });

            _style = new MapStyle(ThinkGeoVectorStyles.Light, _cloud).AddStyle(Editor.Text, _coyote);
            Map.Basemap = new GpuBasemap(_style);
            await Map.RefreshAsync();

            ApplyPaint();
        }

        private void Weight_Checked(object sender, RoutedEventArgs e)
        {
            if (Editor != null && ((RadioButton)sender).Tag is string key)
            {
                Editor.Text = (string)FindResource(key);
            }
        }

        private void Ramp_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            var name = (string)((RadioButton)sender).Content;
            _ramp = Array.Find(ColorRamps, ramp => ramp.Name == name);
            ApplyPaint();
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
                _style = new MapStyle(ThinkGeoVectorStyles.Light, _cloud).AddStyle(Editor.Text, _coyote);
                await Map.Basemap.SetStyleAsync(_style);
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }

        // One runtime call, four properties: no style rebuild, no re-fetch.
        private void ApplyPaint()
        {
            ShowPaintPreview();
            _style?.SetHeatmapPaint(
                HeatLayerId,
                radius: RadiusSlider.Value,
                intensity: IntensitySlider.Value,
                opacity: OpacitySlider.Value,
                colorRamp: _ramp.Stops);
        }

        private void ShowPaintPreview()
        {
            RadiusReadout.Text = RadiusSlider.Value.ToString("0", CultureInfo.InvariantCulture) + "px";
            IntensityReadout.Text = IntensitySlider.Value.ToString("0.00", CultureInfo.InvariantCulture);
            OpacityReadout.Text = OpacitySlider.Value.ToString("0.00", CultureInfo.InvariantCulture);
            PaintPreview.Text = FormattableString.Invariant(
                $@"style.SetHeatmapPaint(""{HeatLayerId}"", radius: {RadiusSlider.Value:0}, intensity: {IntensitySlider.Value:0.00}, opacity: {OpacitySlider.Value:0.00}, colorRamp: {_ramp.Name})");
        }
    }
}
