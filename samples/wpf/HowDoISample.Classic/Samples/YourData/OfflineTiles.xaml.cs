using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show tiles stored on disk: raster or vector MBTiles and PMTiles, or a folder of z/x/y image tiles.
    /// </summary>
    public partial class OfflineTiles : IDisposable
    {
        private bool _initialized;
        private int _request;

        public OfflineTiles()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            await ShowChosenAsync();
        }

        private async void Archive_Checked(object sender, RoutedEventArgs e)
        {
            if (_initialized) await ShowChosenAsync();
        }

        private async Task ShowChosenAsync()
        {
            var request = ++_request;
            Map.Overlays.Clear();
            Status.Text = "Opening...";

            try
            {
                AsyncLayer tilesLayer;
                if (RasterPmTiles.IsChecked == true)
                    tilesLayer = new RasterPmTilesAsyncLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Pmtiles", "stamen_toner.pmtiles"));
                else if (FileTiles.IsChecked == true)
                    tilesLayer = await CreateFileTilesLayerAsync();
                else if (VectorMbTiles.IsChecked == true)
                    tilesLayer = new VectorMbTilesAsyncLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Mbtiles", "maplibre.mbtiles"), Path.Combine(AppContext.BaseDirectory, "Data", "Mbtiles", "style.json"));
                else if (VectorPmTiles.IsChecked == true)
                    tilesLayer = new VectorPmTilesAsyncLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Pmtiles", "frisco.pmtiles"), Path.Combine(AppContext.BaseDirectory, "Data", "Pmtiles", "style.json"));
                else
                    tilesLayer = new RasterMbTilesAsyncLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Mbtiles", "test.mbtiles"));

                await tilesLayer.OpenAsync();
                if (request != _request) return;

                var layerOverlay = new LayerOverlay();
                layerOverlay.Layers.Add(tilesLayer);
                Map.Overlays.Add(layerOverlay);
                Map.CurrentExtent = tilesLayer.GetBoundingBox();
                Status.Text = tilesLayer.GetType().Name;
            }
            catch (Exception exception)
            {
                if (request != _request) return;
                Status.Text = "Unavailable: " + exception.Message;
            }

            _ = Map.RefreshAsync();
        }

        private static async Task<AsyncLayer> CreateFileTilesLayerAsync()
        {
            // The tiles ship zipped; they are unpacked once into the user's own folder, not the application's.
            var tilesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ThinkGeo", "HowDoI", "OSM_Tiles_z0-z5_Created_By_QGIS");
            if (!Directory.Exists(tilesFolder))
            {
                var zipFile = Path.Combine(AppContext.BaseDirectory, "Data", "OSM_Tiles_z0-z5_Created_By_QGIS.zip");
                await Task.Run(() => ZipFile.ExtractToDirectory(zipFile, tilesFolder));
            }

            return new XyzFileTilesAsyncLayer(tilesFolder) { MaxZoomOfTheData = 5 };
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Reads image tiles from a folder laid out as {zoom}\{x}\{y}.jpg.
    /// </summary>
    internal class XyzFileTilesAsyncLayer : RasterXyzTileAsyncLayer
    {
        private readonly string _root;

        public XyzFileTilesAsyncLayer(string tilesFolder)
        {
            _root = tilesFolder;
        }

        protected override Task<RasterTile> GetTileAsyncCore(int zoomLevel, long x, long y, float resolutionFactor, CancellationToken cancellationToken)
        {
            var path = Path.Combine(_root, zoomLevel.ToString(), x.ToString(), y + ".jpg");
            var bytes = File.Exists(path) ? File.ReadAllBytes(path) : null;
            return Task.FromResult(new RasterTile(bytes, zoomLevel, x, y));
        }
    }
}
