using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Reproject the tiles of a raster tile service, here OpenStreetMap, into another projection.
    /// </summary>
    public partial class ReprojectARasterTileService : IDisposable
    {
        private const string PolarStereographic = "+proj=stere +lat_0=90 +lat_ts=70 +lon_0=-45 +k=1 +x_0=0 +y_0=0 +a=6378273 +b=6356889.449 +units=m +no_defs";
        private const string AlbersEqualAreaConic = "+proj=aea +lat_1=20 +lat_2=60 +lat_0=40 +lon_0=-96 +x_0=0 +y_0=0 +ellps=GRS80 +datum=NAD83 +units=m +no_defs";

        // Germany and its neighbours: the zone itself is a strip 6 degrees wide and 84 tall, which
        // fills a map badly.
        private static readonly RectangleShape Utm32NExtent = new RectangleShape(100000, 6200000, 900000, 5200000);

        private bool _initialized;
        private OpenStreetMapAsyncLayer _osmLayer;

        public ReprojectARasterTileService()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            _osmLayer = new OpenStreetMapAsyncLayer();
            var layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(_osmLayer);
            Map.Overlays.Add(layerOverlay);

            _ = ShowProjectionAsync("3857");
        }

        private void Projection_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _ = ShowProjectionAsync((string)((RadioButton)sender).Tag);
        }

        private async Task ShowProjectionAsync(string projection)
        {
            try
            {
                GdalProjectionConverter projectionConverter;
                switch (projection)
                {
                    case "4326":
                        projectionConverter = new GdalProjectionConverter(3857, 4326);
                        Map.MapUnit = GeographyUnit.DecimalDegree;
                        Map.CenterPoint = new PointShape(0.6, 33.8);
                        Map.CurrentScale = 40000000;
                        break;
                    case "25832":
                        projectionConverter = new GdalProjectionConverter(3857, 25832);
                        Map.MapUnit = GeographyUnit.Meter;
                        Map.CenterPoint = Utm32NExtent.GetCenterPoint();
                        Map.CurrentScale = MapUtil.GetScale(GeographyUnit.Meter, Utm32NExtent, Map.MapWidth, Map.MapHeight);
                        break;
                    case "polar":
                        projectionConverter = new GdalProjectionConverter(3857, PolarStereographic);
                        Map.MapUnit = GeographyUnit.Meter;
                        Map.CenterPoint = new PointShape(7273160, -3414200);
                        Map.CurrentScale = 36978700;
                        break;
                    case "albers":
                        projectionConverter = new GdalProjectionConverter(3857, AlbersEqualAreaConic);
                        Map.MapUnit = GeographyUnit.Meter;
                        Map.CenterPoint = new PointShape(8968290, 5188500);
                        Map.CurrentScale = 18489340;
                        break;
                    default:
                        projectionConverter = null;
                        Map.MapUnit = GeographyUnit.Meter;
                        Map.CenterPoint = MaxExtents.OsmMaps.GetCenterPoint();
                        Map.CurrentScale = MapUtil.GetScale(GeographyUnit.Meter, MaxExtents.OsmMaps, Map.MapWidth, Map.MapHeight);
                        break;
                }

                projectionConverter?.Open();
                _osmLayer.ProjectionConverter = projectionConverter;

                // The projected tile cache does not know which projection a tile was cut for,
                // so each projection gets a cache of its own.
                _osmLayer.ProjectedTileCache = new InMemoryRasterTileCache();

                await _osmLayer.CloseAsync();
                await _osmLayer.OpenAsync();
                Status.Text = string.Empty;
                await Map.RefreshAsync();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
