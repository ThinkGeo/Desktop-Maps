using System;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A WMTS server through the renderer.
    /// </summary>
    public partial class WMTS
    {

        private bool _initialized;

        public WMTS()
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
            // The source is the data level: opening it negotiates the server's
            // capabilities, and TileMatrixSetName picks the web-mercator grid.
            var source = new WmtsRasterTileSource(new Uri("https://wmts.geo.admin.ch/EPSG/3857/1.0.0"),
                null, WmtsServerEncodingType.Restful)
            {
                ActiveLayerName = "ch.swisstopo.pixelkarte-farbe-pk25.noscale",
                ActiveStyleName = "default",
                OutputFormat = "image/png",
                TileMatrixSetName = "3857_19",
            };

            // One call puts the service on the map - no style JSON is written or needed.
            Map.Basemap = new GpuBasemap(new MapStyle());
            Map.Basemap.AddRasterSource("wmts", source);

            // Fit the view to the extent the server declares for this layer.
            // (This public server can be slow.)
            await source.OpenAsync();
            var boundingBox = source.GetBoundingBox();
            Map.CenterPoint = boundingBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, boundingBox, Map.MapWidth, Map.MapHeight);
            await Map.RefreshAsync();
        }
    }
}