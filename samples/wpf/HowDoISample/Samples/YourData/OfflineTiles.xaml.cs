using System;
using System.IO;
using System.IO.Compression;
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
    /// Tiles served off disk with no network: an MBTiles or PMTiles archive, vector or
    /// raster, or a plain folder of z/x/y images. Opening the archive is one line; a
    /// vector archive brings a style document that says how to draw it - or shows as
    /// bare geometry without one - and a raster archive is the picture itself.
    /// </summary>
    public partial class OfflineTiles
    {
        private bool _ready;

        public OfflineTiles()
        {
            InitializeComponent();
            Map.MapUnit = GeographyUnit.Meter;
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_ready || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _ready = true;
            await ShowChosenAsync();
        }

        private async void Archive_Checked(object sender, RoutedEventArgs e)
        {
            if (_ready) await ShowChosenAsync();
        }

        /// <summary>
        /// A different archive is a different basemap - nothing carries over - so each
        /// choice is a new one, opened at the archive's own extent.
        /// </summary>
        private async Task ShowChosenAsync()
        {
            ApplyStyle.Visibility = SrcVectorMbTiles.IsChecked == true || SrcVectorPmTiles.IsChecked == true
                ? Visibility.Visible
                : Visibility.Collapsed;
            try
            {
                var (tiles, style, extent) = await OpenChosenAsync();
                Map.Basemap = new GpuBasemap(style);
                Map.CurrentExtent = extent;
                Status.Text = tiles.GetType().Name;
            }
            catch (Exception exception)
            {
                // The archives come from the HowDoI sample repo; without them there is
                // nothing to draw, and saying so beats an empty window.
                Status.Text = "Unavailable: " + exception.Message.Split('\n')[0];
            }

            await Map.RefreshAsync();
        }

        /// <summary>The line that differs: which class reads which archive.</summary>
        private async Task<(object Tiles, MapStyle Style, RectangleShape Extent)> OpenChosenAsync()
        {
            if (SrcVectorMbTiles.IsChecked == true)
            {
                var tiles = new MbTilesVectorTileSource(Data("Mbtiles", "maplibre.mbtiles"));
                await tiles.OpenAsync();
                return (tiles, VectorStyle(Data("Mbtiles", "style.json"), tiles), tiles.GetBoundingBox());
            }

            if (SrcVectorPmTiles.IsChecked == true)
            {
                var tiles = new PmTilesVectorTileSource(Data("Pmtiles", "frisco.pmtiles"));
                await tiles.OpenAsync();
                return (tiles, VectorStyle(Data("Pmtiles", "style.json"), tiles), tiles.GetBoundingBox());
            }

            // The raster archives are opened before they are handed over: open is where
            // they read their own zoom range, so the renderer stretches the last level
            // instead of asking for levels the file does not have.
            if (SrcRasterMbTiles.IsChecked == true)
            {
                var tiles = new MbTilesRasterTileSource(Data("Mbtiles", "test.mbtiles"));
                await tiles.OpenAsync();
                return (tiles, new MapStyle().AddRaster(tiles), MaxExtents.SphericalMercator);
            }

            if (SrcRasterPmTiles.IsChecked == true)
            {
                var tiles = new PmTilesRasterTileSource(Data("Pmtiles", "stamen_toner.pmtiles"));
                await tiles.OpenAsync();
                return (tiles, new MapStyle().AddRaster(tiles), MaxExtents.SphericalMercator);
            }

            // The shape any tile writer produces: z/x/y image files. This folder came
            // out of QGIS and stops at zoom 5.
            var folder = Data("OSM_Tiles_z0-z5_Created_By_QGIS");
            if (!Directory.Exists(folder))
            {
                ZipFile.ExtractToDirectory(folder + ".zip", folder);
            }

            var files = new FolderRasterTileSource(folder, maxZoom: 5);
            return (files, new MapStyle().AddRaster(files), MaxExtents.SphericalMercator);
        }

        /// <summary>
        /// The archive's own style document, or - with the box unchecked - the default
        /// look, which draws every layer of the archive by its geometry.
        /// </summary>
        private MapStyle VectorStyle(string stylePath, IVectorTileSource tiles)
        {
            return ApplyStyle.IsChecked == true
                ? new MapStyle().AddStyle(stylePath, tiles)
                : new MapStyle().AddVectorTiles(tiles);
        }

        private static string Data(params string[] path) =>
            Path.Combine(AppContext.BaseDirectory, "Data", Path.Combine(path));
    }

    /// <summary>
    /// A folder of z/x/y image files as a tile source. There is no SDK class for this
    /// because there is nothing to it: the contract is one method.
    /// </summary>
    internal sealed class FolderRasterTileSource : RasterTileSource
    {
        private readonly string _root;

        public FolderRasterTileSource(string root, int maxZoom)
        {
            _root = root;
            SetDataZoomRange(0, maxZoom);
        }

        public override bool IsOpen => true;

        public override Task OpenAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        protected override Task<byte[]> GetTileImageAsyncCore(int zoom, long x, long y, CancellationToken cancellationToken)
        {
            var path = Path.Combine(_root, zoom.ToString(), x.ToString(), y + ".jpg");
            return Task.FromResult(File.Exists(path) ? File.ReadAllBytes(path) : null);
        }
    }
}
