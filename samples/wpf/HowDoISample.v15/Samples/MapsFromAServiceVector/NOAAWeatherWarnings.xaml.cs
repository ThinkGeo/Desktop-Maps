using System;
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
    /// NOAA weather warnings merged into the basemap's style - the active warning
    /// polygons colored by severity, labeled by event, straight from the feed's own
    /// attributes.
    /// </summary>
    public partial class NOAAWeatherWarnings
    {
        private readonly ThinkGeoVectorTileSource _cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);
        private readonly FeatureSourceVectorTileSource _warnings = new FeatureSourceVectorTileSource { MaxConcurrentEncodes = 1 };
        private readonly DispatcherTimer _applyTimer;
        private bool _initialized;

        public NOAAWeatherWarnings()
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

            _ = BuildMapAsync();
        }

        private async Task BuildMapAsync()
        {
            _warnings.FeatureSources.Add("warnings", new NoaaWeatherWarningsFeatureSource
            {
                ProjectionConverter = new ProjectionConverter(4326, 3857)
            });

            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, _cloud);
            style.AddStyle(Editor.Text, _warnings);
            Map.Basemap = new GpuBasemap(style);

            Map.CenterPoint = new PointShape(-10807050, 5045070);
            Map.CurrentScale = 34016000;
            await Map.RefreshAsync();
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
                var style = new MapStyle();
                style.AddStyle(ThinkGeoVectorStyles.Light, _cloud);
                style.AddStyle(Editor.Text, _warnings);
                await Map.Basemap.SetStyleAsync(style);
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }
    }
}