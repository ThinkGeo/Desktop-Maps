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
    /// The hosted vector maps, each a style document: ThinkGeo Cloud binds its keyed
    /// tile source to a published style, OpenFreeMap and any other MVT server are a
    /// style URL alone. The keyed commercial providers are a page of their style URLs.
    /// </summary>
    public partial class VectorMapProviders
    {
        private readonly ThinkGeoVectorTileSource _thinkGeo =
            new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) };

        private bool _ready;

        public VectorMapProviders()
        {
            InitializeComponent();
            Map.MapUnit = GeographyUnit.Meter;
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_ready || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _ready = true;
            Map.CenterPoint = new PointShape(-10779700, 3912000);
            Map.CurrentScale = 18100;
            await ShowChosenAsync();
        }

        private async void Provider_Checked(object sender, RoutedEventArgs e)
        {
            if (_ready) await ShowChosenAsync();
        }

        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            if (_ready) await ShowChosenAsync();
        }

        /// <summary>
        /// A new provider is a new style over the same map: the basemap is restyled in
        /// place and the camera holds. Light and dark share the one ThinkGeo tile source.
        /// </summary>
        private async Task ShowChosenAsync()
        {
            CustomPanel.Visibility = CustomServer.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            if (OtherProviders.IsChecked == true)
            {
                OtherProvidersPage.Content ??= new OtherVectorProviders();
                OtherProvidersPage.Visibility = Visibility.Visible;
                Status.Text = string.Empty;
                return;
            }

            OtherProvidersPage.Visibility = Visibility.Collapsed;
            try
            {
                var style = ThinkGeoLight.IsChecked == true ? new MapStyle(ThinkGeoVectorStyles.Light, _thinkGeo)
                    : ThinkGeoDark.IsChecked == true ? new MapStyle(ThinkGeoVectorStyles.Dark, _thinkGeo)
                    : new MapStyle(ChosenStyleUri());
                if (Map.Basemap == null)
                {
                    Map.Basemap = new GpuBasemap(style);
                }
                else
                {
                    await Map.Basemap.SetStyleAsync(style);
                }

                // A server whose coverage is unknown opens on the whole world.
                if (CustomServer.IsChecked == true)
                {
                    Map.CurrentExtent = MaxExtents.SphericalMercator;
                }

                Status.Text = ThinkGeoLight.IsChecked == true || ThinkGeoDark.IsChecked == true
                    ? nameof(ThinkGeoVectorTileSource)
                    : ChosenStyleUri();
            }
            catch (Exception exception)
            {
                Status.Text = "Unavailable: " + exception.Message.Split('\n')[0];
            }

            await Map.RefreshAsync();
        }

        private string ChosenStyleUri()
        {
            if (OpenFreeMapLiberty.IsChecked == true) return "https://tiles.openfreemap.org/styles/liberty";
            if (OpenFreeMapPositron.IsChecked == true) return "https://tiles.openfreemap.org/styles/positron";
            if (OpenFreeMapDark.IsChecked == true) return "https://tiles.openfreemap.org/styles/dark";
            return StyleUriBox.Text.Trim();
        }
    }
}
