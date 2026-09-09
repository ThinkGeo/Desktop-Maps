using System;
using System.IO;
using System.Windows;
using OSGeo.GDAL;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// The NDFD wind forecast as a VectorFieldSource, drawn the way weather apps
    /// draw wind: sixty-five thousand tracer particles advected through the field,
    /// streaming and fading continuously - AddParticleFlow is one call and the
    /// simulation runs itself from there. The toggle switches to the
    /// field's other rendering, magnitude-scaled arrows, and both read the same
    /// source object: one field, two renderings, the GridSource pattern again.
    /// </summary>
    public partial class NdfdWindField
    {
        private GpuBasemap _basemap;
        private MapStyle _style;
        private VectorFieldSource _field;
        private bool _built;

        public NdfdWindField()
        {
            InitializeComponent();
            Map.MapUnit = GeographyUnit.Meter;
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
                LoadField();

                _style = new MapStyle();
                _style.AddStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));
                _style.AddParticleFlow(_field);
                _basemap = new GpuBasemap(_style);
                Map.Basemap = _basemap;

                // The lower 48, where the forecast grid lives.
                Map.CurrentExtent = new RectangleShape(-14026255, 6446275, -7235766, 2632018);
                Status.Text = "NDFD surface wind - 65k particles advected, color is speed";
            }
            catch (Exception ex)
            {
                Status.Text = "Wind data unavailable: " + ex.Message;
            }

            await Map.RefreshAsync();
        }

        /// <summary>
        /// Reads the first forecast hour of speed and direction into one
        /// VectorFieldSource, in the grid's own Lambert projection - the renderings
        /// reproject, not this code.
        /// </summary>
        private void LoadField()
        {
            GdalManager.ConfigureGdal();
            var speeds = ReadFirstBand(Path.Combine(AppContext.BaseDirectory, "Data", "Ndfd", "ds.wspd.tif"),
                out var width, out var height, out var extent, out var proj4);
            var from = ReadFirstBand(Path.Combine(AppContext.BaseDirectory, "Data", "Ndfd", "ds.wdir.tif"),
                out _, out _, out _, out _);

            // A meteorological wind direction is where the wind comes FROM; the arrow
            // points where it is going.
            var directions = new float[from.Length];
            for (var i = 0; i < from.Length; i++)
            {
                directions[i] = (from[i] + 180f) % 360f;
            }

            _field = VectorFieldSource.FromSpeedDirection(
                speeds, directions, width, height, extent, new Projection(proj4));
        }

        private static float[] ReadFirstBand(string path, out int width, out int height,
            out RectangleShape extent, out string proj4)
        {
            var dataset = Gdal.Open(path, Access.GA_ReadOnly)
                ?? throw new InvalidOperationException("GDAL could not open " + path);
            try
            {
                width = dataset.RasterXSize;
                height = dataset.RasterYSize;

                var gt = new double[6];
                dataset.GetGeoTransform(gt);
                var x1 = gt[0] + (width * gt[1]) + (height * gt[2]);
                var y1 = gt[3] + (width * gt[4]) + (height * gt[5]);
                extent = new RectangleShape(Math.Min(gt[0], x1), Math.Max(gt[3], y1), Math.Max(gt[0], x1), Math.Min(gt[3], y1));

                var wkt = dataset.GetProjection();
                var sr = new OSGeo.OSR.SpatialReference(string.Empty);
                sr.ImportFromWkt(ref wkt);
                sr.ExportToProj4(out proj4);
                sr.Dispose();

                var band = dataset.GetRasterBand(1);
                try
                {
                    var values = new float[width * height];
                    band.ReadRaster(0, 0, width, height, values, width, height, 0, 0);
                    band.GetNoDataValue(out var noData, out var hasNoData);
                    if (hasNoData != 0)
                    {
                        for (var i = 0; i < values.Length; i++)
                        {
                            if (values[i] == (float)noData)
                            {
                                values[i] = float.NaN;
                            }
                        }
                    }

                    return values;
                }
                finally
                {
                    band.Dispose();
                }
            }
            finally
            {
                dataset.Dispose();
            }
        }

        private void Mode_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_style == null)
            {
                return;
            }

            // The document never changes, so this SetStyleAsync only re-applies the
            // registrations - no tile is rebuilt for the switch.
            _style.RemoveVectorField(_field).AddParticleFlow(_field);
            _ = _basemap.SetStyleAsync(_style);
            Status.Text = "NDFD surface wind - 65k particles advected, color is speed";
        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (_style == null)
            {
                return;
            }

            _style.RemoveParticleFlow(_field).AddVectorField(_field, new VectorFieldStyle { MaxArrows = 3000 });
            _ = _basemap.SetStyleAsync(_style);
            Status.Text = "The same field as magnitude-scaled arrows";
        }
    }
}
