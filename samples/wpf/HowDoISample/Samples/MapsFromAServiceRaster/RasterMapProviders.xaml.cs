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
    /// The hosted raster maps: ThinkGeo Cloud and OpenStreetMap, one tile source each -
    /// pick one and the registration is the same line. The commercial providers need
    /// your own API key, so they are a page of their classes and one-line usage rather
    /// than a map.
    /// </summary>
    public partial class RasterMapProviders
    {
        private bool _ready;

        public RasterMapProviders()
        {
            InitializeComponent();
            Map.MapUnit = GeographyUnit.Meter;
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ready) return;

            _ready = true;
            Map.CenterPoint = new PointShape(-10778800, 3915300);
            // Raster tiles carry their labels as pixels: a zoom of their own draws them crisp,
            // a scale between two shrinks or stretches them.
            Map.CurrentScale = new ZoomLevelSet().ZoomLevel13.Scale;
            await ShowChosenAsync();
        }

        private async void Provider_Checked(object sender, RoutedEventArgs e)
        {
            if (_ready) await ShowChosenAsync();
        }

        /// <summary>
        /// A new provider is a new style over the same map: the raster registration is
        /// swapped in place, the camera holds.
        /// </summary>
        private async Task ShowChosenAsync()
        {
            if (KeyedProviders.IsChecked == true)
            {
                KeyedProvidersPage.Content ??= new ThirdPartyBasemaps();
                KeyedProvidersPage.Visibility = Visibility.Visible;
                Status.Text = string.Empty;
                return;
            }

            KeyedProvidersPage.Visibility = Visibility.Collapsed;
            var source = OpenChosen();
            var style = new MapStyle().AddRaster(source);
            if (Map.Basemap == null)
            {
                Map.Basemap = new GpuBasemap(style);
            }
            else
            {
                await Map.Basemap.SetStyleAsync(style);
            }

            Status.Text = source.GetType().Name;
            await Map.RefreshAsync();
        }

        /// <summary>The line that differs: which class serves the tiles.</summary>
        private RasterTileSource OpenChosen()
        {
            if (ThinkGeoLight.IsChecked == true)
                return new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoCloudRasterMapsMapType.Light_V2_X1);
            if (ThinkGeoDark.IsChecked == true)
                return new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoCloudRasterMapsMapType.Dark_V2_X1);
            if (ThinkGeoAerial.IsChecked == true)
                return new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1);
            if (ThinkGeoHybrid.IsChecked == true)
                return new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1);

            // OpenStreetMap's tile policy asks every application for a descriptive user agent.
            return new OpenStreetMapRasterTileSource("ThinkGeo HowDoI Sample (support@thinkgeo.com)");
        }
    }
}
