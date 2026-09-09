using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A live weather image draped over the basemap: one GeoImageRasterTileSource carries the
    /// NEXRAD composite PNG that Iowa State's Mesonet publishes - a plain HTTP
    /// download of one georeferenced image, no tile protocol - anchored by its WGS84
    /// extent. The image arrives in degrees and the source resamples it against the
    /// Mercator latitude curve, so the echoes land on the counties they are over.
    ///
    /// The last half hour animates by itself, and the animation is the point of the
    /// source OBJECT: Update swaps the bitmap and announces its own refresh - the map
    /// refetches, and nothing about the style changes. The same data-through-the-source
    /// pattern the isoline and weather-grid samples use.
    /// </summary>
    public partial class NexradRadar : IDisposable
    {
        private const string FrameUrlTemplate = "https://mesonet.agron.iastate.edu/data/gis/images/4326/USCOMP/n0q_{0}.png";
        private const int FrameCount = 6; // n0q_5 (25 minutes ago) .. n0q_0 (now), 5-minute steps
        private const string RadarSourceId = "radar";

        // The published CONUS composite: 0.005-degree pixels from (-126, 50) down to
        // (-65, 23) - 12200 x 5400, about 2.7 MB a frame.
        private static readonly RectangleShape ImageExtent = new RectangleShape(-126, 50, -65, 23);

        private static readonly HttpClient HttpClient = new HttpClient();

        private readonly DispatcherTimer _timer;
        private GpuBasemap _basemap;
        private GeoImageRasterTileSource _radar;
        private List<byte[]> _frames;
        private int _frameIndex;
        private bool _built;

        public NexradRadar()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(700) };
            _timer.Tick += (_, __) => ShowNextFrame();
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_built)
            {
                return;
            }

            _built = true;
            try
            {
                // The source object holds the image and where it lies; the style
                // holds that it draws as a raster layer at this opacity. The
                // mosaic is a black-backed palette PNG; black is its "no echo".
                _radar = GeoImageRasterTileSource.FromUri(
                    string.Format(FrameUrlTemplate, 0), ImageExtent, new Projection(4326));
                _radar.TransparentColor = GeoColors.Black;

                var style = new MapStyle();
                style.AddStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));
                style.AddRaster(_radar, RadarSourceId, opacity: 0.75f);
                _basemap = new GpuBasemap(style);
                Map.Basemap = _basemap;

                // The lower 48, where the composite has coverage.
                Map.CurrentExtent = new RectangleShape(-14026255, 6446275, -7235766, 2632018);
                await Map.RefreshAsync();

                // The newest frame is on screen; pull the last half hour behind it
                // and loop as soon as the frames are in.
                var frames = new List<byte[]>(FrameCount);
                for (var i = FrameCount - 1; i >= 0; i--)
                {
                    frames.Add(await HttpClient.GetByteArrayAsync(string.Format(FrameUrlTemplate, i)));
                }

                _frames = frames;
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
            // One Update: the source announces its own refresh, every map drawing it
            // refetches, and the style and its compiled state are untouched.
            _radar.Update(_frames[_frameIndex]);
            var minutesAgo = (_frames.Count - 1 - _frameIndex) * 5;
            Status.Text = minutesAgo == 0
                ? "NEXRAD composite, courtesy of Iowa State Mesonet - now"
                : FormattableString.Invariant($"NEXRAD composite, courtesy of Iowa State Mesonet - {minutesAgo} minutes ago");
            _frameIndex = (_frameIndex + 1) % _frames.Count;
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
