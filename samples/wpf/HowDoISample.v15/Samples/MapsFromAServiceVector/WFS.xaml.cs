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
    /// A WFS V2 service merged into the basemap's style and drawn by the renderer -
    /// the Dutch national cadastral parcels, from PDOK.
    /// </summary>
    public partial class WFS
    {
        private readonly ThinkGeoVectorTileSource _cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);

        // One query at a time: a public service is not a shapefile, and four
        // concurrent bounding-box requests per screenful is not politeness.
        private readonly FeatureSourceVectorTileSource _parcels = new FeatureSourceVectorTileSource { MinDataZoom = 15, MaxConcurrentEncodes = 1 };

        private readonly DispatcherTimer _applyTimer;
        private bool _initialized;

        public WFS()
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
            // The service's own CRS is EPSG:28992, but it also publishes EPSG:3857 -
            // ask it for web mercator rather than converting afterwards: the server
            // holds the datum transformation for its national grid, and a generic
            // client-side conversion lands the parcels about 170 m off the basemap.
            _parcels.FeatureSources.Add("parcels", new WfsV2FeatureSource(
                "https://service.pdok.nl/kadaster/kadastralekaart/wfs/v5_0",
                "kadastralekaart:Perceel")
            {
                TimeoutInSeconds = 120,
                Crs = "urn:ogc:def:crs:EPSG::3857",
            });

            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, _cloud);
            style.AddStyle(Editor.Text, _parcels);
            Map.Basemap = new GpuBasemap(style);

            Map.CenterPoint = new PointShape(544950, 6867650);
            Map.CurrentScale = 4000;
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
                style.AddStyle(Editor.Text, _parcels);
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