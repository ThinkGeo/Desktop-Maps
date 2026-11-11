using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// An OGC API Features collection merged into the basemap's style and drawn by the
    /// renderer - the named places of Spain from IGN, as dots with labels.
    /// </summary>
    public partial class OGCAPIFeatureServer
    {
        private bool _initialized;

        public OGCAPIFeatureServer()
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
            // Below zoom 12 no tile asks the service: a tile there would be a
            // bounding-box query for the places of a whole province.
            var places = new FeatureSourceVectorTileSource { MinDataZoom = 12 };
            places.FeatureSources.Add("places", new OgcApiAsyncFeatureSource(
                "https://api-features.ign.es", "namedplace")
            {
                ProjectionConverter = new ProjectionConverter(4326, 3857)
            });

            var basemapTiles = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) };
            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, basemapTiles);
            style.AddStyle((string)Resources["StyleJson"], places);
            Map.Basemap = new GpuBasemap(style);

            Map.CenterPoint = new PointShape(235690, 5057360);
            Map.CurrentScale = 36200;
            await Map.RefreshAsync();
        }
    }
}
