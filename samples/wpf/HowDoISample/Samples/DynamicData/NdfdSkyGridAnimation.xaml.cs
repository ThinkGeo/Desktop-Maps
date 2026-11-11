using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
    /// The NDFD sky-cover forecast, one forecast hour at a time through an InMemoryGridSource.
    /// </summary>
    public partial class NdfdSkyGridAnimation : IDisposable
    {
        private bool _initialized;
        private bool _playing;
        private bool _loading;
        private int _wantedFrame;
        private string _path;
        private float[][,] _frames;
        private string[] _frameTimes;
        private readonly GpuBasemap _basemap;
        private readonly MapStyle _style;
        private InMemoryGridSource _grid;
        private IReadOnlyList<ColorStop> _palette;
        private readonly DispatcherTimer _timer;

        public NdfdSkyGridAnimation()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            _style = new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
            _basemap = new GpuBasemap(_style);
            Map.Basemap = _basemap;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(600) };
            _timer.Tick += (_, __) =>
            {
                // A frame still being read holds the animation, so no hour is skipped.
                if (_playing && !_loading)
                {
                    FrameSlider.Value = (FrameSlider.Value + 1) % _frames.Length;
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

            _path = Path.Combine(AppContext.BaseDirectory, "Data", "Ndfd", "ds.sky.bin");
            GdalGridBand first;
            try
            {
                (first, _frameTimes) = await Task.Run(() => (GdalGrid.ReadBand(_path, 1), FrameTimes(_path)));
            }
            catch (Exception ex)
            {
                TimeText.Text = "Failed to load the NDFD grid: " + ex.Message;
                return;
            }

            _frames = new float[_frameTimes.Length][,];
            _frames[0] = SkyCover(first.Values);
            _grid = new InMemoryGridSource(_frames[0], first.Extent, first.Projection);
            _palette ??= CreatePalette("Weather Rainbow");
            _style.AddGridFill(_grid, new GridFillStyle { ColorStops = _palette });
            await _basemap.SetStyleAsync(_style);
            ShowTime(0);

            FrameSlider.Maximum = _frames.Length - 1;
            FrameSlider.IsEnabled = true;
            PlayButton.IsEnabled = true;
            _playing = true;
            _timer.Start();
        }

        // Dragging the slider asks for an hour; the one asked for last is read off the UI
        // thread and shown, and the hours dragged past on the way are skipped.
        private async void FrameSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_grid == null)
            {
                return;
            }

            _wantedFrame = (int)e.NewValue;
            if (_loading)
            {
                return;
            }

            _loading = true;
            try
            {
                int index;
                do
                {
                    index = _wantedFrame;
                    _frames[index] ??= await Task.Run(() => SkyCover(GdalGrid.ReadBand(_path, index + 1).Values));
                }
                while (index != _wantedFrame);

                _grid.SetValues(_frames[index]);
                ShowTime(index);
            }
            catch (Exception ex)
            {
                TimeText.Text = "Failed to read the forecast hour: " + ex.Message;
            }
            finally
            {
                _loading = false;
            }
        }

        private async void Palette_Checked(object sender, RoutedEventArgs e)
        {
            _palette = CreatePalette((sender as RadioButton)?.Content as string);
            if (_grid != null)
            {
                _style.RemoveGridFill(_grid).AddGridFill(_grid, new GridFillStyle { ColorStops = _palette });
                await _basemap.SetStyleAsync(_style);
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            _playing = !_playing;
            PlayButton.Content = _playing ? "Pause" : "Play";
        }

        private void ShowTime(int index)
        {
            TimeText.Text = _frameTimes[index] ?? "(no time)";
            HourText.Text = string.Format("forecast hour {0} of {1}", index + 1, _frames.Length);
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
            _playing = false;
            _timer.Stop();
            Map.Dispose();
        }
    }
}
