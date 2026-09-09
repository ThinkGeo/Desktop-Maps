using System.Windows;
using ThinkGeo.Core;
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

        private static readonly RectangleShape SanFranciscoBay =
            new RectangleShape(-13_660_000, 4_565_000, -13_600_000, 4_515_000);

        private bool _ready;

        public PerspectiveTilt()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            Map.TiltInteractionEnabled = true;

            // The built-in tilt bar: hidden by default, shown on the right when enabled.
            Map.MapTools.TiltBar.IsEnabled = true;
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ready) return;

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
                : new MapStyle().AddRaster(new ThinkGeoRasterTileSource(SampleShared.CloudApiKey), "thinkgeo"));

            Map.CurrentExtent = vector ? Manhattan : SanFranciscoBay;
            await Map.RefreshAsync();
        }
    }
}
