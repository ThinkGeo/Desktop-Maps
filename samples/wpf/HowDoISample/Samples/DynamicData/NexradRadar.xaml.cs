using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// The NEXRAD radar composite, one georeferenced PNG in a GeoImageRasterTileSource, looping through the last half hour.
    /// </summary>
    public partial class NexradRadar : IDisposable
    {
        private const string FrameUrlTemplate = "https://mesonet.agron.iastate.edu/data/gis/images/4326/USCOMP/n0q_{0}.png";
        private const int FrameCount = 6; // n0q_5 (25 minutes ago) .. n0q_0 (now), 5-minute steps

        // The published CONUS composite: 0.005-degree pixels from (-126, 50) down to (-65, 23).
        private static readonly RectangleShape ImageExtent = new RectangleShape(-126, 50, -65, 23);

        private static readonly HttpClient HttpClient = new HttpClient();

        private readonly DispatcherTimer _timer;
        private GeoImageRasterTileSource _radar;
        private byte[][] _frames;
        private int _frameIndex;
        private bool _built;

        public NexradRadar()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(700) };
            _timer.Tick += (_, __) => ShowNextFrame();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_built || e.NewSize.Width <= 0 || e.NewSize.Height <= 0)
            {
                return;
            }

            _built = true;
            try
            {
                // Oldest first, so the newest frame is the last one in the loop.
                _frames = new byte[FrameCount][];
                _frames[FrameCount - 1] = await HttpClient.GetByteArrayAsync(string.Format(FrameUrlTemplate, 0));

                // The mosaic is a black-backed palette PNG; black is its "no echo".
                _radar = new GeoImageRasterTileSource(_frames[FrameCount - 1], ImageExtent, new Projection(4326));
                _radar.TransparentColor = GeoColors.Black;

                var style = new MapStyle();
                style.AddStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
                style.AddRaster(_radar, new RasterPaint(0.75f), "radar");
                Map.Basemap = new GpuBasemap(style);

                Map.CurrentExtent = new RectangleShape(-14026255, 6446275, -7235766, 2632018);
                Status.Text = "NEXRAD composite, courtesy of Iowa State Mesonet - now";
                await Map.RefreshAsync();

                for (var i = 1; i < FrameCount; i++)
                {
                    _frames[FrameCount - 1 - i] = await HttpClient.GetByteArrayAsync(string.Format(FrameUrlTemplate, i));
                }

                _frameIndex = 0;
                _timer.Start();
            }
            catch (Exception ex)
            {
                Status.Text = "Radar unavailable: " + ex.Message;
                await Map.RefreshAsync();
            }
        }

        private void ShowNextFrame()
        {
            // Update swaps the image; the source tells every map drawing it to refetch.
            _radar.Update(_frames[_frameIndex]);
            var minutesAgo = (_frames.Length - 1 - _frameIndex) * 5;
            Status.Text = minutesAgo == 0
                ? "NEXRAD composite, courtesy of Iowa State Mesonet - now"
                : FormattableString.Invariant($"NEXRAD composite, courtesy of Iowa State Mesonet - {minutesAgo} minutes ago");
            _frameIndex = (_frameIndex + 1) % _frames.Length;
        }

        public void Dispose()
        {
            _timer.Stop();
            Map.Dispose();
            _radar?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
