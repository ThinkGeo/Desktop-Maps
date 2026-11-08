using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A powers-of-ten dive into galaxy M87, down to the shadow of its black hole, through ten pictures nested inside each other to illustrate the scale.
    /// </summary>
    public partial class ZoomToBlackHole
    {
        // Where each picture lies, each one inside the one before it. The pictures are
        // arranged, not measured, so the numbers are in no real unit.
        private static readonly (string Id, RectangleShape Extent)[] Pictures =
        {
            ("m87_0", new RectangleShape(-9121.505491, 7004.839967, 9121.505491, -7004.839967)),
            ("m87_1", new RectangleShape(113.333791, 3997.249537, 1806.666209, 2282.750463)),
            ("m87_2", new RectangleShape(661.536655, 3515.692504, 1390.463345, 2760.307496)),
            ("m87_3", new RectangleShape(958.298048, 3167.258683, 1057.701952, 3112.741317)),
            ("m87_4", new RectangleShape(998.599842, 3170.1752, 1039.400158, 3147.8248)),
            ("m87_5", new RectangleShape(999.994603, 3154.709799, 1010.205397, 3150.290201)),
            ("m87_6", new RectangleShape(1000.395001, 3152.892729, 1004.204999, 3150.707271)),
            ("m87_7", new RectangleShape(1000.540677, 3151.408154, 1001.559323, 3150.891846)),
            ("m87_8", new RectangleShape(1000.604466, 3151.195571, 1000.945534, 3150.958029)),
            ("m87_9", new RectangleShape(1000.69292, 3151.051128, 1000.70308, 3151.040872))
        };

        // The view the dive starts from, a stop for each picture as it comes in, and the black hole last.
        private static readonly (double X, double Y, double Scale)[] Stops =
        {
            (0, 0, 50000),
            (960, 3140, 20000),
            (1026, 3138, 7000),
            (1008, 3140, 1000),
            (1019, 3159, 400),
            (1005.1, 3152.5, 100),
            (1002.3, 3151.8, 40),
            (1001.05, 3151.15, 8),
            (1000.775, 3151.0768, 2),
            (1000.698, 3151.046, 0.7),
            (1000.698, 3151.046, 0.03)
        };

        private CancellationTokenSource _dive = new CancellationTokenSource();
        private bool _initialized;

        public ZoomToBlackHole()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;

            // The galaxy's own grid: the outermost picture, deep enough for the last one.
            var grid = TileMatrixSet.CreateTileMatrixSet(512, new RectangleShape(-9121.505491, 9121.505491, 9121.505491, -9121.505491), GeographyUnit.Meter, 24);

            var basemap = new GpuBasemap(new MapStyle((string)Resources["StyleJson"]));
            foreach (var (id, extent) in Pictures)
            {
                var image = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Data", "Jpg", id + ".jpg"));
                basemap.RasterTileSources.Add(id, new GeoImageRasterTileSource(image, extent, tileMatrixSet: grid));
            }

            Map.Basemap = basemap;
            Map.CenterPoint = new PointShape(Stops[0].X, Stops[0].Y);
            Map.CurrentScale = Stops[0].Scale;
            _ = Map.RefreshAsync();
        }

        private async void DiveButton_Click(object sender, RoutedEventArgs e)
        {
            _dive.Cancel();
            _dive = new CancellationTokenSource();
            var cancellationToken = _dive.Token;

            try
            {
                for (var i = 1; i < Stops.Length; i++)
                {
                    await Map.ZoomToAsync(new PointShape(Stops[i].X, Stops[i].Y), Stops[i].Scale, 0, cancellationToken);
                }
            }
            catch (TaskCanceledException)
            {
            }
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _dive.Cancel();
            _dive = new CancellationTokenSource();

            try
            {
                await Map.ZoomToAsync(new PointShape(Stops[0].X, Stops[0].Y), Stops[0].Scale, 0, _dive.Token);
            }
            catch (TaskCanceledException)
            {
            }
        }
    }
}
