using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Draw a world vector tile map in different projections with a ProjectionConverter on the layer.
    /// </summary>
    public partial class ProjectTheWorld : IDisposable
    {
        private const string MgaZone55 = "+proj=utm +zone=55 +south +ellps=GRS80 +towgs84=0,0,0,0,0,0,0 +units=m +no_defs";
        private const string AlbersEqualAreaConic = "+proj=aea +lat_1=20 +lat_2=60 +lat_0=40 +lon_0=-96 +x_0=0 +y_0=0 +ellps=GRS80 +datum=NAD83 +units=m +no_defs";
        private const string PolarStereographic = "+proj=stere +lat_0=90 +lat_ts=70 +lon_0=-45 +k=1 +x_0=0 +y_0=0 +a=6378273 +b=6356889.449 +units=m +no_defs";
        private const string CylindricalEqualArea = "+proj=cea +lon_0=0 +x_0=0 +y_0=0 +lat_ts=45 +ellps=WGS84 +datum=WGS84 +units=m +no_defs";

        private bool _initialized;
        private MvtTilesAsyncLayer _worldLayer;

        public ProjectTheWorld()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.DecimalDegree;
            Map.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#42B2EE"));

            _worldLayer = new MvtTilesAsyncLayer("https://tiles.preludemaps.com/styles/WorldStreets_Light/style.json")
            {
                ProjectionConverter = new ProjectionConverter(3857, 4326)
            };
            var layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(_worldLayer);
            Map.Overlays.Add(layerOverlay);

            Map.CenterPoint = new PointShape(5.92651, 14.58364);
            Map.CurrentScale = 147648000;

            _ = Map.RefreshAsync();
        }

        private void Projection_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            switch ((string)((RadioButton)sender).Tag)
            {
                case "mga55":
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, MgaZone55);
                    Map.MapUnit = GeographyUnit.Meter;
                    Map.CenterPoint = new PointShape(-945060, 7010600);
                    Map.CurrentScale = 18489350;
                    break;
                case "albers":
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, AlbersEqualAreaConic);
                    Map.MapUnit = GeographyUnit.Meter;
                    Map.CenterPoint = new PointShape(-107570, -246560);
                    Map.CurrentScale = 36978690;
                    break;
                case "polar":
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, PolarStereographic);
                    Map.MapUnit = GeographyUnit.Meter;
                    Map.CenterPoint = new PointShape(176160, -818530);
                    Map.CurrentScale = 73957380;
                    break;
                case "cylindrical":
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, CylindricalEqualArea);
                    Map.MapUnit = GeographyUnit.Meter;
                    Map.CenterPoint = new PointShape(-1152760, 796980);
                    Map.CurrentScale = 147648000;
                    break;
                default:
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, 4326);
                    Map.MapUnit = GeographyUnit.DecimalDegree;
                    Map.CenterPoint = new PointShape(5.92651, 14.58364);
                    Map.CurrentScale = 147648000;
                    break;
            }
            _worldLayer.ProjectionConverter.Open();

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
