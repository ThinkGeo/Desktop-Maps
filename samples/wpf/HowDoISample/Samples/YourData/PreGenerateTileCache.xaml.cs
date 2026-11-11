using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Render a tile cache ahead of time - a folder of z/x/y PNG tiles drawn from your
    /// own data with CpuTileRenderer - and draw the map from that folder.
    /// </summary>
    public partial class PreGenerateTileCache
    {
        private const int MinZoom = 10;
        private const int MaxZoom = 14;

        // The user's own folder, not the application's: an installed application's
        // folder is usually read-only.
        private static readonly string CacheFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ThinkGeo", "HowDoI", "TileCache", "Frisco");

        private CancellationTokenSource _cancel;
        private RectangleShape _extent;
        private bool _initialized;

        public PreGenerateTileCache()
        {
            InitializeComponent();
            Folder.Text = CacheFolder;
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            var cityLimits = new ShapeFileFeatureSource(SampleShared.Shapefile("FriscoCityLimits.shp")) { ProjectionConverter = new ProjectionConverter(2276, 3857) };
            cityLimits.Open();
            _extent = cityLimits.GetBoundingBox();
            cityLimits.Close();

            Map.CurrentExtent = _extent;
            Status.Text = Directory.Exists(CacheFolder) ? "Showing the tiles written last time." : "No tiles yet - generate them.";
            ShowCache();
        }

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateButton.IsEnabled = false;
            CancelButton.IsEnabled = true;
            _cancel = new CancellationTokenSource();
            var progress = new Progress<(int Done, int Total)>(report =>
            {
                GenerateProgress.Maximum = report.Total;
                GenerateProgress.Value = report.Done;
                Status.Text = FormattableString.Invariant($"{report.Done} of {report.Total} tiles written");
            });

            try
            {
                var written = await GenerateTilesAsync((string)Resources["CacheStyle"], progress, _cancel.Token);
                Status.Text = FormattableString.Invariant($"{written} tiles written. The map now draws them from the folder.");
            }
            catch (OperationCanceledException)
            {
                Status.Text = "Cancelled - the map draws the tiles written so far.";
            }
            catch (Exception exception)
            {
                Status.Text = "Stopped: " + exception.Message.Split('\n')[0];
            }

            GenerateButton.IsEnabled = true;
            CancelButton.IsEnabled = false;
            ShowCache();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _cancel?.Cancel();
        }

        /// <summary>
        /// Every tile of the 512-pixel web-mercator grid that touches Frisco at zoom 10 to
        /// 14, rendered off the UI thread and written as {z}/{x}/{y}.png.
        /// </summary>
        private Task<int> GenerateTilesAsync(string styleJson, IProgress<(int Done, int Total)> progress, CancellationToken cancellationToken)
        {
            var data = new FeatureSourceVectorTileSource();
            data.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("FriscoCityLimits.shp")) { ProjectionConverter = new ProjectionConverter(2276, 3857) });
            data.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("Parks.shp")) { ProjectionConverter = new ProjectionConverter(2276, 3857) });
            data.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("Streets.shp")) { ProjectionConverter = new ProjectionConverter(2276, 3857) });

            var tiles = new List<(int Zoom, MatrixCell Cell)>();
            for (var zoom = MinZoom; zoom <= MaxZoom; zoom++)
            {
                foreach (var cell in data.TileMatrixSet.TileMatrices[zoom].GetIntersectingCells(_extent))
                {
                    tiles.Add((zoom, cell));
                }
            }

            return Task.Run(async () =>
            {
                using var renderer = await CpuTileRenderer.CreateAsync(new IVectorTileSource[] { data }, new MapStyle(styleJson, data), cancellationToken: cancellationToken);
                if (Directory.Exists(CacheFolder))
                {
                    Directory.Delete(CacheFolder, true);
                }

                for (var i = 0; i < tiles.Count; i++)
                {
                    var (zoom, cell) = tiles[i];
                    var png = await renderer.RenderTileAsync(zoom, cell.X, cell.Y, cancellationToken);
                    var folder = Path.Combine(CacheFolder, zoom.ToString(CultureInfo.InvariantCulture), cell.X.ToString(CultureInfo.InvariantCulture));
                    Directory.CreateDirectory(folder);
                    await File.WriteAllBytesAsync(Path.Combine(folder, cell.Y.ToString(CultureInfo.InvariantCulture) + ".png"), png, cancellationToken);
                    progress.Report((i + 1, tiles.Count));
                }

                return tiles.Count;
            }, cancellationToken);
        }

        private void ShowCache()
        {
            Map.Basemap = new GpuBasemap(new MapStyle().AddRaster(new TileFolderSource(CacheFolder, MinZoom, MaxZoom)));
            _ = Map.RefreshAsync();
        }

        /// <summary>
        /// The generated folder as a tile source: the same kind of z/x/y folder reader as
        /// Offline Tiles uses, on the 512-pixel grid the tiles were rendered on.
        /// </summary>
        private sealed class TileFolderSource : RasterTileSource
        {
            private readonly string _root;

            public TileFolderSource(string root, int minZoom, int maxZoom)
                : base(512)
            {
                _root = root;
                SetDataZoomRange(minZoom, maxZoom);
            }

            public override bool IsOpen => true;

            public override Task OpenAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

            protected override Task<byte[]> GetTileImageAsyncCore(int zoom, long x, long y, CancellationToken cancellationToken)
            {
                var path = Path.Combine(_root, zoom.ToString(CultureInfo.InvariantCulture), x.ToString(CultureInfo.InvariantCulture), y.ToString(CultureInfo.InvariantCulture) + ".png");
                return Task.FromResult(File.Exists(path) ? File.ReadAllBytes(path) : null);
            }
        }
    }
}
