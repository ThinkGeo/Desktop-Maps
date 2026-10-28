using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// The perspective camera over a basemap, vector or raster. The tilt interaction is
    /// built into the MapView - this sample adds no mouse code at all: right-drag tilts,
    /// left-drag pans with the grabbed ground staying under the cursor, alt and
    /// left-drag rotates, the wheel zooms.
    /// </summary>
    public partial class PerspectiveTilt
    {
        private static readonly RectangleShape Manhattan =
            new RectangleShape(-8_240_000, 4_975_000, -8_228_000, 4_965_000);

        private static readonly PointShape SanFranciscoBay = new PointShape(-13_630_000, 4_540_000);

        private bool _ready;

        public PerspectiveTilt()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            Map.IsTiltInteractionEnabled = true;

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

        private async System.Threading.Tasks.Task ShowBasemapAsync()
        {
            var vector = TiltVector.IsChecked == true;

            // A raster basemap and a vector one are different styles over different
            // sources, so this is the one case where the basemap really is rebuilt.
            Map.Basemap = new GpuBasemap(vector
                ? new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey))
                : new MapStyle().AddRaster(new ThinkGeoRasterTileSource(SampleShared.CloudApiKey), sourceId: "thinkgeo"));

            if (vector)
            {
                Map.CurrentExtent = Manhattan;
            }
            else
            {
                // Raster tiles carry their labels as pixels: a zoom of their own draws them
                // crisp, a scale between two shrinks or stretches them.
                Map.CenterPoint = SanFranciscoBay;
                Map.CurrentScale = new ZoomLevelSet().ZoomLevel12.Scale;
            }
            await Map.RefreshAsync();
        }
    }
}
