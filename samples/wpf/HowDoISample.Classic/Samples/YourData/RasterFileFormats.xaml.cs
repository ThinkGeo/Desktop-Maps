using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show a raster file - GeoTIFF, anything GDAL reads, ECW, MrSID, JPEG 2000, or a plain image with a world file.
    /// </summary>
    public partial class RasterFileFormats : IDisposable
    {
        private const string AerialPhoto = "m_3309650_sw_14_1_20160911_20161121";

        private bool _initialized;
        private int _request;

        public RasterFileFormats()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);
            await ShowChosenAsync();
        }

        private async void Format_Checked(object sender, RoutedEventArgs e)
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
                var isAerialPhoto = Jpeg2000.IsChecked == true || Jpeg.IsChecked == true || WpfTiff.IsChecked == true;
                var layer = CreateChosenLayer();
                var extent = await Task.Run(() =>
                {
                    layer.Open();
                    return layer.GetBoundingBox();
                });
                if (request != _request) return;

                // The world files are in decimal degrees; the aerial photo is in Spherical Mercator, like the basemap.
                Map.MapUnit = isAerialPhoto ? GeographyUnit.Meter : GeographyUnit.DecimalDegree;
                if (isAerialPhoto)
                {
                    Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));
                }

                var rasterOverlay = new LayerOverlay();
                rasterOverlay.Layers.Add(layer);
                Map.Overlays.Add(rasterOverlay);
                Map.CurrentExtent = extent;
                Status.Text = layer.GetType().Name;
            }
            catch (Exception exception)
            {
                if (request != _request) return;
                Status.Text = "Unavailable: " + exception.Message;
            }

            _ = Map.RefreshAsync();
        }

        private RasterLayer CreateChosenLayer()
        {
            if (Gdal.IsChecked == true) return new GdalRasterLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Gdal", "FdoGdal.tif"));
            if (Ecw.IsChecked == true) return new EcwGdalRasterLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Ecw", "World.ecw"));
            if (MrSid.IsChecked == true) return new MrSidGdalRasterLayer(Path.Combine(AppContext.BaseDirectory, "Data", "MrSid", "World.sid"));
            if (Jpeg2000.IsChecked == true) return new Jpeg2000GdalRasterLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Jp2", AerialPhoto + ".jp2"));
            if (Jpeg.IsChecked == true) return new SkiaRasterLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Jpg", AerialPhoto + ".jpg"));
            if (WpfTiff.IsChecked == true) return new WpfRasterLayer(Path.Combine(AppContext.BaseDirectory, "Data", "GeoTiff", AerialPhoto + ".tif"));
            return new GeoTiffRasterLayer(Path.Combine(AppContext.BaseDirectory, "Data", "GeoTiff", "World.tif"));
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
