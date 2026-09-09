using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Aerial imagery under vector streets and labels, with a solid white wash
    /// interleaved above the roads and below the labels. No overlay can express
    /// that - an overlay is always above everything, labels included.
    /// </summary>
    public partial class InsertALayerUnderTheLabels
    {
        private readonly DispatcherTimer _applyTimer;

        private ThinkGeoRasterTileSource _aerial;
        private ThinkGeoVectorTileSource _vector;
        private FeatureSourceVectorTileSource _washQuad;
        private string _streetStyleJson;
        private bool _initialized;

        public InsertALayerUnderTheLabels()
        {
            InitializeComponent();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(300) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyWashAsync(); };
            WashSlider.ValueChanged += (_, _) =>
            {
                ShowWash();
                _applyTimer.Stop();
                _applyTimer.Start();
            };
            ShowWash();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.CenterPoint = new PointShape(-8238310, 4970071);
            Map.CurrentScale = 75000;

            _ = BuildMapAsync();
        }

        private async Task BuildMapAsync()
        {
            _aerial = new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoRasterMapType.Aerial);
            _vector = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);
            await _vector.OpenAsync();

            // The street style as text, fetched once: every wash change composes a
            // fresh style from it.
            using (var http = new HttpClient())
            {
                _streetStyleJson = await http.GetStringAsync(ThinkGeoVectorStyles.Light);
            }

            // The wash is one world-covering polygon, cut into tiles like any other
            // vector data - which is what keeps it whole at every zoom.
            var world = new InMemoryFeatureSource(Array.Empty<FeatureSourceColumn>(), new Collection<Feature>
            {
                new Feature(new RectangleShape(-20037000, 20037000, 20037000, -20037000).GetWellKnownBinary(), "world"),
            });
            _washQuad = new FeatureSourceVectorTileSource();
            _washQuad.FeatureSources.Add("world", world);

            Map.Basemap = new GpuBasemap(await ComposeStyleAsync(WashSlider.Value));
            await Map.RefreshAsync();
        }

        private async Task<MapStyle> ComposeStyleAsync(double washOpacity)
        {
            var style = new MapStyle()
                .AddRaster(_aerial, "aerial");

            // Only the street style's lines and symbols: its fills would paint an
            // opaque land base over the imagery.
            style.AddStyle(_streetStyleJson, _vector, "line", "symbol");

            // A slot is found by walking the layers already loaded, so the document
            // must be open before inserting into it.
            await style.OpenAsync();
            style.InsertStyleLayersAt(StyleLayerSlot.UnderLabels, _washQuad, "washsrc",
                index => StyleLayer.CreateFill("wash", index, "washsrc", "world",
                    paint: (_, _) => new FillPaint(GeoColors.White, (float)washOpacity, null, false, 0f, 0f)));
            return style;
        }

        private async Task ApplyWashAsync()
        {
            if (!IsLoaded || _streetStyleJson == null) return;
            await Map.Basemap.SetStyleAsync(await ComposeStyleAsync(WashSlider.Value));
        }

        private void ShowWash() => WashReadout.Text =
            WashSlider.Value.ToString("P0", System.Globalization.CultureInfo.InvariantCulture);

    }
}
