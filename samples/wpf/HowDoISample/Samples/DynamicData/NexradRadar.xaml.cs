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
    /// The NEXRAD radar composite, one georeferenced PNG per GeoImageRasterTileSource, looping through the last half hour.
    /// </summary>
    public partial class NexradRadar : IDisposable
    {
        private const string FrameUrlTemplate = "https://mesonet.agron.iastate.edu/data/gis/images/4326/USCOMP/n0q_{0}.png";
        private const int FrameCount = 6; // n0q_5 (25 minutes ago) .. n0q_0 (now), 5-minute steps

        // The published CONUS composite: 0.005-degree pixels from (-126, 50) down to (-65, 23).
        private static readonly RectangleShape ImageExtent = new RectangleShape(-126, 50, -65, 23);

        private static readonly HttpClient HttpClient = new HttpClient();

        private readonly DispatcherTimer _timer;
        private readonly GeoImageRasterTileSource[] _frames = new GeoImageRasterTileSource[FrameCount];
        private MapStyle _style;
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
                // One source and one layer per frame, oldest first. Stepping the loop only
                // switches which layer is visible: a frame's tiles stay loaded, so nothing is
                // fetched again and the map never shows a gap between two frames.
                _style = new MapStyle();
                _style.AddStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
                for (var i = 0; i < FrameCount; i++)
                {
                    var minutesAgo = (FrameCount - 1 - i) * 5;
                    Status.Text = FormattableString.Invariant($"Loading the radar, {i + 1} of {FrameCount}…");
                    var png = await HttpClient.GetByteArrayAsync(string.Format(FrameUrlTemplate, minutesAgo / 5));

                    // The mosaic is a black-backed palette PNG; black is its "no echo".
                    _frames[i] = new GeoImageRasterTileSource(png, ImageExtent, new Projection(4326)) { TransparentColor = GeoColors.Black };
                    _style.AddRaster(_frames[i], new RasterPaint(0.75f), FrameId(i));
                    _style.SetLayerVisibility(FrameId(i), i == FrameCount - 1);
                }

                Map.Basemap = new GpuBasemap(_style);
                Map.CurrentExtent = new RectangleShape(-14026255, 6446275, -7235766, 2632018);
                ShowStatus(FrameCount - 1);
                await Map.RefreshAsync();

                _frameIndex = FrameCount - 1;
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
            var next = (_frameIndex + 1) % FrameCount;
            _style.SetLayerVisibility(FrameId(_frameIndex), false);
            _style.SetLayerVisibility(FrameId(next), true);
            _frameIndex = next;
            ShowStatus(next);
        }

        private void ShowStatus(int frame)
        {
            var minutesAgo = (FrameCount - 1 - frame) * 5;
            Status.Text = minutesAgo == 0
                ? "NEXRAD composite, courtesy of Iowa State Mesonet - now"
                : FormattableString.Invariant($"NEXRAD composite, courtesy of Iowa State Mesonet - {minutesAgo} minutes ago");
        }

        private static string FrameId(int frame) => FormattableString.Invariant($"radar-{frame}");

        public void Dispose()
        {
            _timer.Stop();
            Map.Dispose();
            foreach (var frame in _frames)
            {
                frame?.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
