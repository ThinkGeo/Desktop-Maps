using System;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
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
            Map.CurrentScale = 91000;
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
        private IRasterTileSource OpenChosen()
        {
            if (ThinkGeoLight.IsChecked == true)
                return new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoRasterMapType.Light);
            if (ThinkGeoDark.IsChecked == true)
                return new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoRasterMapType.Dark);
            if (ThinkGeoAerial.IsChecked == true)
                return new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoRasterMapType.Aerial);
            if (ThinkGeoHybrid.IsChecked == true)
                return new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoRasterMapType.Hybrid);

            // OpenStreetMap's tile policy asks every application for a descriptive user agent.
            return new OpenStreetMapRasterTileSource("ThinkGeo HowDoI Sample (support@thinkgeo.com)");
        }
    }
}
