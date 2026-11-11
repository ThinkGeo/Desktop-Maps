using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A WFS V2 service merged into the basemap's style and drawn by the renderer -
    /// the Dutch national cadastral parcels, from PDOK.
    /// </summary>
    public partial class WFS
    {
        private bool _initialized;

        public WFS()
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
            // Below zoom 15 no tile asks the service: a tile there would be a
            // bounding-box query for tens of thousands of parcels.
            var parcels = new FeatureSourceVectorTileSource { MinDataZoom = 15 };

            // The service's own CRS is EPSG:28992, but it also publishes EPSG:3857 -
            // ask it for web mercator rather than converting afterwards: the server
            // holds the datum transformation for its national grid, and a generic
            // client-side conversion lands the parcels about 170 m off the basemap.
            parcels.FeatureSources.Add("parcels", new WfsV2AsyncFeatureSource(
                "https://service.pdok.nl/kadaster/kadastralekaart/wfs/v5_0",
                "kadastralekaart:Perceel")
            {
                TimeoutInSeconds = 120,
                Crs = "urn:ogc:def:crs:EPSG::3857",
            });

            var basemapTiles = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) };
            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, basemapTiles);
            style.AddStyle((string)Resources["StyleJson"], parcels);
            Map.Basemap = new GpuBasemap(style);

            Map.CenterPoint = new PointShape(544950, 6867650);
            Map.CurrentScale = 4000;
            await Map.RefreshAsync();
        }
    }
}
