using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using OSGeo.GDAL;
using OSGeo.OSR;
using ThinkGeo.Core;
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
        private Dataset _dataset;
        private byte[][] _frames;
        private string[] _frameTimes;
        private int _gridWidth;
        private int _gridHeight;
        private int _frameIndex;
        private readonly GpuBasemap _overlay;
        private readonly MapStyle _style;
        private GridSource _grid;
        private Func<byte, GeoColor> _palette;
        private readonly DispatcherTimer _timer;

        public NdfdSkyGridAnimation()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            Map.ZoomStep = 0.15;
            _style = new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));
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

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_initialized)
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
                _style.AddGridFill(_grid, new GridFillStyle { Ramp = _palette, MeshDensity = 96 });
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
            GdalManager.ConfigureGdal();
            var path = Path.Combine(AppContext.BaseDirectory, "Data", "Ndfd", "ds.sky.bin");
            _dataset = Gdal.Open(path, Access.GA_ReadOnly);
            if (_dataset == null)
            {
                throw new InvalidOperationException("GDAL could not open " + path);
            }

            _gridWidth = _dataset.RasterXSize;
            _gridHeight = _dataset.RasterYSize;
            _frames = new byte[_dataset.RasterCount][];
            _frameTimes = new string[_frames.Length];
            for (var i = 0; i < _frames.Length; i++)
            {
                var band = _dataset.GetRasterBand(i + 1);
                try
                {
                    var meta = band.GetMetadataItem("GRIB_VALID_TIME", string.Empty);
                    if (!string.IsNullOrEmpty(meta) && long.TryParse(meta.Split(' ')[0].Trim(), out var unixSeconds))
                    {
                        _frameTimes[i] = DateTimeOffset.FromUnixTimeSeconds(unixSeconds).UtcDateTime.ToString("yyyy-MM-dd HH:mm \'UTC\'");
                    }
                }
                finally
                {
                    band.Dispose();
                }
            }

            // Source extent and projection straight from the dataset.
            var gt = new double[6];
            _dataset.GetGeoTransform(gt);
            var x0 = gt[0];
            var y0 = gt[3];
            var x1 = gt[0] + (_gridWidth * gt[1]) + (_gridHeight * gt[2]);
            var y1 = gt[3] + (_gridWidth * gt[4]) + (_gridHeight * gt[5]);
            var sourceExtent = new RectangleShape(Math.Min(x0, x1), Math.Max(y0, y1), Math.Max(x0, x1), Math.Min(y0, y1));

            var wkt = _dataset.GetProjection();
            var sr = new SpatialReference(string.Empty);
            sr.ImportFromWkt(ref wkt);
            sr.ExportToProj4(out var proj4);
            sr.Dispose();

            // One call bakes the Lambert->Mercator mesh; every animation frame after
            // this is UpdateValueGridValues - a single texture upload.
            _grid = GridSource.FromBytes(GetFrame(0), _gridWidth, _gridHeight, sourceExtent, new Projection(proj4));
            _palette ??= CreatePalette("Weather Rainbow");
            _style.AddGridFill(_grid, new GridFillStyle { Ramp = _palette, MeshDensity = 96 });
            _ = _overlay.SetStyleAsync(_style);
            ShowFrame(0);
        }

        private byte[] GetFrame(int index)
        {
            if (_frames[index] == null)
            {
                var band = _dataset.GetRasterBand(index + 1);
                try
                {
                    var values = new byte[_gridWidth * _gridHeight];
                    band.ReadRaster(0, 0, _gridWidth, _gridHeight, values, _gridWidth, _gridHeight, 0, 0);
                    _frames[index] = values;
                }
                finally
                {
                    band.Dispose();
                }
            }

            return _frames[index];
        }

        private void ShowFrame(int index)
        {
            if (_disposed || _frames == null || _frames.Length == 0)
            {
                return;
            }

            var bounded = ((index % _frames.Length) + _frames.Length) % _frames.Length;
            _frameIndex = bounded;
            _grid.UpdateBytes(GetFrame(bounded));

            Status.Text = string.Format(
                "Frame {0}/{1}\n{2}\nGrid {3} x {4}",
                bounded + 1,
                _frames.Length,
                _frameTimes[bounded] ?? "(no time)",
                _gridWidth,
                _gridHeight);
        }

        private static Func<byte, GeoColor> CreatePalette(string name)
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

            return value => value == 0 || value > 100 ? GeoColors.Transparent : table[value];
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
            _dataset?.Dispose();
            _dataset = null;
            Map.Dispose();
        }
    }
}
