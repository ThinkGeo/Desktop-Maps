using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
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

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ready) return;

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
                return (tiles, VectorStyle(Data("Mbtiles", "style.json"), tiles, tiles.MetadataJson), await tiles.GetBoundingBoxAsync());
            }

            if (SrcVectorPmTiles.IsChecked == true)
            {
                var tiles = new PmTilesVectorTileSource(Data("Pmtiles", "frisco.pmtiles"));
                await tiles.OpenAsync();
                return (tiles, VectorStyle(Data("Pmtiles", "style.json"), tiles, tiles.MetadataJson), tiles.GetBoundingBox());
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

        // The bare look for an archive drawn without its document: a fill, a line and a
        // circle layer per source-layer it declares, each kept to its own geometry.
        private const string BareLayers = @",
    { ""type"": ""fill"",
      ""source-layer"": ""LAYER"",
      ""filter"": [""=="", [""geometry-type""], ""Polygon""],
      ""paint"": {
          ""fill-color"": ""rgba(90,140,200,0.35)"",
          ""fill-outline-color"": ""#2A4A7A"" }
    },

    { ""type"": ""line"",
      ""source-layer"": ""LAYER"",
      ""filter"": [""=="", [""geometry-type""], ""LineString""],
      ""paint"": {
          ""line-color"": ""#2A4A7A"",
          ""line-width"": 1 }
    },

    { ""type"": ""circle"",
      ""source-layer"": ""LAYER"",
      ""filter"": [""=="", [""geometry-type""], ""Point""],
      ""paint"": {
          ""circle-radius"": 4,
          ""circle-color"": ""#C0392B"",
          ""circle-stroke-color"": ""#FFFFFF"",
          ""circle-stroke-width"": 1.5 }
    }";

        /// <summary>
        /// The archive's own style document, or - with the box unchecked - the bare
        /// layers over every source-layer the archive declares, so the geometry shows
        /// as geometry.
        /// </summary>
        private MapStyle VectorStyle(string stylePath, IVectorTileSource tiles, string metadataJson)
        {
            if (ApplyStyle.IsChecked == true)
            {
                return new MapStyle().AddStyle(stylePath, tiles);
            }

            var layers = new StringBuilder(@"{ ""layers"": [
    { ""type"": ""background"",
      ""paint"": { ""background-color"": ""#F0F0EC"" }
    }");
            using (var metadata = JsonDocument.Parse(metadataJson))
            {
                foreach (var declared in metadata.RootElement.GetProperty("vector_layers").EnumerateArray())
                {
                    layers.Append(BareLayers.Replace("LAYER", declared.GetProperty("id").GetString()));
                }
            }

            return new MapStyle().AddStyle(layers.Append(" ] }").ToString(), tiles);
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

        public override Task<byte[]> GetTileImageAsync(int zoom, int x, int y, CancellationToken cancellationToken)
        {
            var path = Path.Combine(_root, zoom.ToString(), x.ToString(), y + ".jpg");
            return Task.FromResult(File.Exists(path) ? File.ReadAllBytes(path) : null);
        }
    }
}
