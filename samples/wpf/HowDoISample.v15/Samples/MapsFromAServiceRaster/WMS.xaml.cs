using System;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// NASA's view of the planet yesterday, over the basemap: the VIIRS true-color
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
            var imagery = new WmsRasterTileSource(new Uri("https://gibs.earthdata.nasa.gov/wms/epsg3857/best/wms.cgi"))
            {
                OutputFormat = "image/png"
            };
            imagery.Parameters.Add("LAYERS", "VIIRS_SNPP_CorrectedReflectance_TrueColor");
            // Yesterday: today's mosaic is still being assembled as the satellite flies.
            imagery.Parameters.Add("TIME", DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture));

            // The basemap's own layers first, then the imagery inserted into them -
            // above the fills, below the lines and labels. Nothing is reordered: the
            // slot is found by walking the layers the style already declared.
            var basemapTiles = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);
            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, basemapTiles);
            // A slot is found by walking the layers already loaded, so the document
            // must be open before inserting into it.
            await style.OpenAsync();
            style.InsertRasterAt(StyleLayerSlot.AboveFills, imagery);
            Map.Basemap = new GpuBasemap(style);

            // North America, at a scale where a day of weather is the subject.
            Map.CenterPoint = new PointShape(-10500000, 4500000);
            Map.CurrentScale = 25000000;

            await Map.RefreshAsync();
        }
    }
}