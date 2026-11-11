using System;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// NASA's view of the planet two days ago, over the basemap: the VIIRS true-color
    /// mosaic from NASA GIBS, a WMS service that re-renders the whole Earth every day.
    /// </summary>
    public partial class WMS
    {
        private bool _initialized;

        public WMS()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            _ = BuildMapAsync();
        }

        private async Task BuildMapAsync()
        {
            var imagery = new WmsRasterTileSource(new Uri("https://gibs.earthdata.nasa.gov/wms/epsg3857/best/wms.cgi"));
            imagery.Client.OutputFormat = "image/png";
            imagery.Client.ActiveLayerNames.Add("VIIRS_NOAA20_CorrectedReflectance_TrueColor");
            // Two days back: the newest day's mosaic can still be empty while it is assembled.
            imagery.Client.Parameters["TIME"] = DateTime.UtcNow.AddDays(-2).ToString("yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture);
            // A server that needs credentials gets them on every request here, for example:
            // imagery.SendingHttpRequest += (_, e) => e.HttpRequestMessage.Headers.Authorization =
            //     new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "your-token");

            // The basemap's own layers first, then the imagery inserted into them -
            // above the fills, below the lines and labels. Nothing is reordered: the
            // slot is found by walking the layers the style already declared.
            var basemapTiles = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) };
            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Dark, basemapTiles);
            style.AddRaster(imagery, slot: StyleLayerSlot.AboveFills);
            Map.Basemap = new GpuBasemap(style);

            // North America, at a scale where a day of weather is the subject.
            Map.CenterPoint = new PointShape(-10500000, 4500000);
            Map.CurrentScale = 25000000;

            await Map.RefreshAsync();
        }
    }
}