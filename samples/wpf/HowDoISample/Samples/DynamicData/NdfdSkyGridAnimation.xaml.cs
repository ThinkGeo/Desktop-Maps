using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using OSGeo.GDAL;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// The NDFD sky-cover forecast (the classic PerfTest grid sample), re-based on the
    /// value-grid layer. GDAL decodes each GRIB frame once; after that a frame of
    /// animation is one small texture upload - the Lambert reprojection, the resampling
    /// and the color ramp all run in the fragment shader, so pan/zoom never rebuild a
    /// projection cache and the palette swaps instantly.
    /// </summary>
    public partial class NdfdSkyGridAnimation : IDisposable
    {
        private bool _initialized;
        private bool _disposed;
        private bool _playing;
        private string _path;
        private float[][,] _frames;
        private string[] _frameTimes;
        private int _gridWidth;
        private int _gridHeight;
        private int _frameIndex;
        private readonly GpuBasemap _overlay;
        private readonly MapStyle _style;
        private InMemoryGridSource _grid;
        private IReadOnlyList<ColorStop> _palette;
        private readonly DispatcherTimer _timer;

        public NdfdSkyGridAnimation()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            _style = new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
            _overlay = new GpuBasemap(_style);
            Map.Basemap = _overlay;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(33) };
            _timer.Tick += (_, __) =>
            {
                if (_playing)
                {
                    ShowFrame(_frameIndex + 1);
                }
            };
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0)
            {
                return;
            }

            _initialized = true;
            Map.CurrentExtent = new RectangleShape(-14_100_000, 6_800_000, -7_300_000, 2_500_000);
            await Map.RefreshAsync();
            try
            {
                LoadGrid();
            }
            catch (Exception ex)
            {
                Status.Text = "Failed to load NDFD grid: " + ex.Message;
                return;
            }

            _playing = true;
            _timer.Start();
        }

        // A palette change is a 256-texel ramp upload; the values never move.
        private async void Palette_Checked(object sender, RoutedEventArgs e)
        {
            _palette = CreatePalette((sender as RadioButton)?.Content as string);
            if (_grid != null)
            {
                _style.RemoveGridFill(_grid).AddGridFill(_grid, new GridFillStyle { ColorStops = _palette });
                await _overlay.SetStyleAsync(_style);
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            _playing = !_playing;
            PlayButton.Content = _playing ? "Pause" : "Play";
        }

        private void LoadGrid()
        {
            _path = Path.Combine(AppContext.BaseDirectory, "Data", "Ndfd", "ds.sky.bin");
            var first = GdalGrid.ReadBand(_path, 1);
            _gridHeight = first.Values.GetLength(0);
            _gridWidth = first.Values.GetLength(1);
            _frameTimes = FrameTimes(_path);
            _frames = new float[_frameTimes.Length][,];
            _frames[0] = SkyCover(first.Values);

            // One call bakes the Lambert->Mercator mesh; every animation frame after
            // this is SetValues - a single texture upload.
            _grid = new InMemoryGridSource(_frames[0], first.Extent, first.Projection);
            _palette ??= CreatePalette("Weather Rainbow");
            _style.AddGridFill(_grid, new GridFillStyle { ColorStops = _palette });
            _ = _overlay.SetStyleAsync(_style);
            ShowFrame(0);
        }

        // Each band is one forecast hour. GdalGrid reads the band; the hour it is valid for is
        // GRIB metadata on the band, which GdalGrid does not carry, so that one item is read here.
        private static string[] FrameTimes(string path)
        {
            using var dataset = Gdal.Open(path, Access.GA_ReadOnly)
                ?? throw new InvalidOperationException("GDAL could not open " + path);
            var times = new string[dataset.RasterCount];
            for (var i = 0; i < times.Length; i++)
            {
                using var band = dataset.GetRasterBand(i + 1);
                var meta = band.GetMetadataItem("GRIB_VALID_TIME", string.Empty);
                if (!string.IsNullOrEmpty(meta) && long.TryParse(meta.Split(' ')[0].Trim(), out var unixSeconds))
                {
                    times[i] = DateTimeOffset.FromUnixTimeSeconds(unixSeconds).UtcDateTime.ToString("yyyy-MM-dd HH:mm 'UTC'");
                }
            }

            return times;
        }

        private float[,] GetFrame(int index) => _frames[index] ??= SkyCover(GdalGrid.ReadBand(_path, index + 1).Values);

        // GRIB sky cover is a percentage, 1..100; 0 and anything above 100 is no data,
        // which the grid draws as nothing.
        private static float[,] SkyCover(float[,] band)
        {
            var values = new float[band.GetLength(0), band.GetLength(1)];
            for (var row = 0; row < values.GetLength(0); row++)
            {
                for (var column = 0; column < values.GetLength(1); column++)
                {
                    var v = band[row, column];
                    values[row, column] = float.IsNaN(v) || v <= 0 || v > 100 ? float.NaN : v;
                }
            }

            return values;
        }

        private void ShowFrame(int index)
        {
            if (_disposed || _frames == null || _frames.Length == 0)
            {
                return;
            }

            var bounded = ((index % _frames.Length) + _frames.Length) % _frames.Length;
            _frameIndex = bounded;
            _grid.SetValues(GetFrame(bounded));

            Status.Text = string.Format(
                "Frame {0}/{1}\n{2}\nGrid {3} x {4}",
                bounded + 1,
                _frames.Length,
                _frameTimes[bounded] ?? "(no time)",
                _gridWidth,
                _gridHeight);
        }

        private static IReadOnlyList<ColorStop> CreatePalette(string name)
        {
            var table = new GeoColor[101];
            for (var v = 1; v <= 100; v++)
            {
                var ratio = v / 100d;
                switch (name)
                {
                    case "Cloud Gray":
                        table[v] = Lerp(new[] { new byte[] { 200, 215, 230 }, new byte[] { 130, 145, 160 } }, ratio, (byte)(ratio * 210));
                        break;
                    case "Warm Tones":
                        table[v] = Lerp(new[] { new byte[] { 255, 245, 180 }, new byte[] { 255, 200, 0 }, new byte[] { 240, 100, 0 }, new byte[] { 180, 0, 0 } }, ratio, (byte)(40 + (ratio * 195)));
                        break;
                    case "Cool Tones":
                        table[v] = Lerp(new[] { new byte[] { 190, 230, 255 }, new byte[] { 0, 190, 240 }, new byte[] { 0, 80, 200 }, new byte[] { 0, 10, 110 } }, ratio, (byte)(40 + (ratio * 195)));
                        break;
                    case "Viridis":
                        table[v] = Lerp(new[] { new byte[] { 68, 1, 84 }, new byte[] { 59, 82, 139 }, new byte[] { 33, 145, 140 }, new byte[] { 94, 201, 98 }, new byte[] { 253, 231, 37 } }, ratio, (byte)(50 + (ratio * 185)));
                        break;
                    default:
                        table[v] = Lerp(new[] { new byte[] { 24, 64, 196 }, new byte[] { 0, 174, 239 }, new byte[] { 0, 186, 124 }, new byte[] { 246, 224, 52 }, new byte[] { 246, 143, 31 }, new byte[] { 214, 37, 35 } }, ratio, (byte)(48 + (ratio * 180)));
                        break;
                }
            }

            // Ten stops over the percentage; the fill blends between them.
            var stops = new List<ColorStop> { new ColorStop(1, table[1]) };
            for (var v = 10; v <= 100; v += 10)
            {
                stops.Add(new ColorStop(v, table[v]));
            }

            return stops;
        }

        private static GeoColor Lerp(byte[][] stops, double ratio, byte alpha)
        {
            var scaled = ratio * (stops.Length - 1);
            var lo = Math.Max(0, Math.Min(stops.Length - 1, (int)Math.Floor(scaled)));
            var hi = Math.Min(stops.Length - 1, lo + 1);
            var t = scaled - lo;
            return GeoColor.FromArgb(
                alpha,
                (byte)(stops[lo][0] + (t * (stops[hi][0] - stops[lo][0]))),
                (byte)(stops[lo][1] + (t * (stops[hi][1] - stops[lo][1]))),
                (byte)(stops[lo][2] + (t * (stops[hi][2] - stops[lo][2]))));
        }

        public void Dispose()
        {
            _disposed = true;
            _playing = false;
            _timer.Stop();
            Map.Dispose();
        }
    }
}
