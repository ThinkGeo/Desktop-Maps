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
    /// The perspective camera over a vector or a raster basemap, with the vector style's own buildings on or off.
    /// </summary>
    public partial class PerspectiveTilt
    {
        private static readonly RectangleShape MidtownManhattan =
            new RectangleShape(-8_236_900, 4_975_700, -8_235_100, 4_974_330);

        private static readonly PointShape SanFranciscoBay = new PointShape(-13_630_000, 4_540_000);

        private GpuBasemap _basemap;
        private bool _ready;

        public PerspectiveTilt()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            Map.IsTiltEnabled = true;
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_ready || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _ready = true;
            await ShowBasemapAsync();
        }

        private async void Basemap_Checked(object sender, RoutedEventArgs e)
        {
            if (_ready) await ShowBasemapAsync();
        }

        private void BuildingCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (_basemap == null) return;

            // A renderer switch, not a restyle: no tile is rebuilt.
            _basemap.IsExtrusionDrawingEnabled = BuildingCheckBox.IsChecked == true;
            _ = Map.RefreshAsync();
        }

        private async Task ShowBasemapAsync()
        {
            var vector = TiltVector.IsChecked == true;

            // A raster basemap and a vector one are different styles over different
            // sources, so this is the one case where the basemap really is rebuilt.
            _basemap = new GpuBasemap(vector
                ? new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) })
                : new MapStyle().AddRaster(new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoCloudRasterMapsMapType.Dark_V2_X1), sourceId: "thinkgeo"));
            _basemap.IsExtrusionDrawingEnabled = BuildingCheckBox.IsChecked == true;
            Map.Basemap = _basemap;

            if (vector)
            {
                Map.CurrentExtent = MidtownManhattan;
                await Map.RefreshAsync();
                try
                {
                    await Map.TiltToAsync(50, 600);
                }
                catch (OperationCanceledException)
                {
                    // The other basemap was picked before the tilt finished.
                }
            }
            else
            {
                // Raster tiles carry their labels as pixels: a zoom of their own draws them
                // crisp, a scale between two shrinks or stretches them.
                Map.CenterPoint = SanFranciscoBay;
                Map.CurrentScale = new ZoomLevelSet().ZoomLevel12.Scale;
                await Map.RefreshAsync();
            }
        }
    }
}
