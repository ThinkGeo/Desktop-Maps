using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Open a raster file - GeoTIFF, ECW, JPEG 2000, MrSID, or anything else GDAL
    /// reads - and draw it. Each format has a classic raster source named after it;
    /// WarpToWebMercator warps the file once at open through a virtual dataset, and
    /// one ClassicRasterTileSource serves its windowed reads as tiles, so a
    /// multi-gigabyte image with overviews draws at every zoom without ever being
    /// loaded whole.
    /// </summary>
    public partial class RasterFileFormats
    {
        // Frisco, TX - the NAIP aerial's own ground.
        private static readonly RectangleShape Frisco =
            new RectangleShape(-10784079, 3920222, -10777116, 3911905);

        // The basemap under the imagery. One source for the life of the sample: a
        // restyle keeps the sources it recognizes, so the map below holds still
        // while the image above it changes file.
        private readonly ThinkGeoVectorTileSource _cloud =
            new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);

        private GpuBasemap _basemap;
        private IRasterTileSource _current;
        private bool _ready;

        public RasterFileFormats()
        {
            InitializeComponent();
            Map.MapUnit = GeographyUnit.Meter;
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ready) return;

            _ready = true;
            _basemap = new GpuBasemap(ComposeStyle());
            Map.Basemap = _basemap;
            Map.CurrentExtent = Frisco;
            await Map.RefreshAsync();
        }

        private async void Format_Checked(object sender, RoutedEventArgs e)
        {
            if (!_ready) return;

            // Restyling in place, not a new basemap: assigning a basemap starts from
            // an empty scene, and that blank moment is the flash a viewer sees when
            // switching files.
            var retired = _current;
            await _basemap.SetStyleAsync(ComposeStyle());
            Map.CurrentExtent = StartExtent();
            await Map.RefreshAsync();
            (retired as IDisposable)?.Dispose();
        }

        /// <summary>
        /// The chosen file as a tile source, read by the class named after its
        /// format, over the cloud basemap.
        /// </summary>
        private MapStyle ComposeStyle()
        {
            try
            {
                var source = OpenChosen();
                Status.Text = source.GetType().Name;
                source.WarpToWebMercator = true;
                _current = new ClassicRasterTileSource(source);
            }
            catch (Exception exception)
            {
                // The data comes from the HowDoI sample repo; without it there is
                // nothing to draw, and saying so beats an empty window. Formats read
                // through a plugin wrap the real reason, so unwrap to it.
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }

                _current = null;
                Status.Text = "Unavailable: " + exception.Message.Split('\n')[0];
            }

            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, _cloud);
            if (_current != null)
            {
                style.AddRaster(_current, "raster");
            }

            return style;
        }

        private GdalRasterSource OpenChosen()
        {
            // The sample world files say where the image sits but not which plane its
            // numbers are in, so several of these need the projection stated.
            if (FmtGeoTiff.IsChecked == true)
                return new GeoTiffGdalRasterSource(Data("GeoTiff", "m_3309650_sw_14_1_20160911_20161121.tif")) { SourceProjection = new Projection(3857) };
            if (FmtJpeg2000.IsChecked == true)
                return new Jpeg2000GdalRasterSource(Data("Jp2", "m_3309650_sw_14_1_20160911_20161121.jp2")) { SourceProjection = new Projection(3857) };
            if (FmtJpeg.IsChecked == true)
                // Not a GIS format at all: an ordinary .jpg whose .jgw says where it
                // sits. The base class reads it like any other.
                return new GdalRasterSource(Data("Jpg", "m_3309650_sw_14_1_20160911_20161121.jpg")) { SourceProjection = new Projection(3857) };
            if (FmtMrSid.IsChecked == true)
                // No projection in the file; its degree-shaped extent reads as WGS84.
                return new MrSidRasterSource(Data("MrSid", "World.sid"));
            if (FmtEcw.IsChecked == true)
                // This ECW's own projection record is broken; its world file is degrees.
                return new EcwGdalRasterSource(Data("Ecw", "World.ecw")) { SourceProjection = new Projection(4326) };

            return new GdalRasterSource(Data("GeoTiff", "World.tif")) { SourceProjection = new Projection(4326) };
        }

        private RectangleShape StartExtent() =>
            FmtGeoTiff.IsChecked == true || FmtJpeg2000.IsChecked == true || FmtJpeg.IsChecked == true
                ? Frisco
                : MaxExtents.SphericalMercator;

        private static string Data(string folder, string file) =>
            Path.Combine(AppContext.BaseDirectory, "Data", folder, file);
    }
}
