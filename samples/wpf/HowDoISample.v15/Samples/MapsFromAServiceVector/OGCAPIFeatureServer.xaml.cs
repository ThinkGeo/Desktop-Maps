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
    /// An OGC API Features collection merged into the basemap's style and drawn by the
    /// renderer - the named places of Spain from IGN, as dots with labels.
    /// </summary>
    public partial class OGCAPIFeatureServer
    {
        private readonly ThinkGeoVectorTileSource _cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);

        // One query at a time: a public service is not a shapefile, and four
        // concurrent bounding-box requests per screenful is not politeness.
        private readonly FeatureSourceVectorTileSource _places = new FeatureSourceVectorTileSource { MinDataZoom = 12, MaxConcurrentEncodes = 1 };

        private readonly DispatcherTimer _applyTimer;
        private bool _initialized;

        public OGCAPIFeatureServer()
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
            _places.FeatureSources.Add("places", new OgcApiFeatureSource(
                "https://api-features.ign.es", "namedplace", 1000)
            {
                ProjectionConverter = new ProjectionConverter(4326, 3857)
            });

            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, _cloud);
            style.AddStyle(Editor.Text, _places);
            Map.Basemap = new GpuBasemap(style);

            Map.CenterPoint = new PointShape(235690, 5057360);
            Map.CurrentScale = 36200;
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
                style.AddStyle(Editor.Text, _places);
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