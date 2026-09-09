using System;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// The hosted vector maps, each a style document: ThinkGeo Maps binds its keyed
    /// tile source to a published style, OpenFreeMap and any other MVT server are a
    /// style URL alone. The keyed commercial providers are a page of their style URLs.
    /// </summary>
    public partial class VectorMapProviders
    {
        private ThinkGeoVectorTileSource _thinkGeo;
        private bool _ready;

        public VectorMapProviders()
        {
            InitializeComponent();
            Map.MapUnit = GeographyUnit.Meter;
            Map.TiltInteractionEnabled = true;
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ready) return;

            _ready = true;
            Map.CenterPoint = new PointShape(-10779700, 3912000);
            Map.CurrentScale = 18100;
            Map.TiltAngle = 45;
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
        /// Light and dark are two documents over the same ThinkGeo tiles, so they restyle
        /// in place; any other server is a different source, and that is a new basemap.
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
                var thinkGeo = ThinkGeoLight.IsChecked == true || ThinkGeoDark.IsChecked == true;
                if (thinkGeo && _thinkGeo != null)
                {
                    var restyle = new MapStyle().AddStyle(ThinkGeoLight.IsChecked == true ? ThinkGeoVectorStyles.Light : ThinkGeoVectorStyles.Dark, _thinkGeo);
                    await Map.Basemap.SetStyleAsync(restyle);
                }
                else
                {
                    _thinkGeo = thinkGeo ? new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) : null;
                    var style = thinkGeo
                        ? new MapStyle().AddStyle(ThinkGeoLight.IsChecked == true ? ThinkGeoVectorStyles.Light : ThinkGeoVectorStyles.Dark, _thinkGeo)
                        : new MapStyle().AddStyle(ChosenStyleUri());
                    Map.Basemap = new GpuBasemap(await style.OpenAsync());
                }

                // A server whose coverage is unknown opens on the whole world.
                if (CustomServer.IsChecked == true)
                {
                    Map.CurrentExtent = MaxExtents.SphericalMercator;
                }

                Status.Text = thinkGeo ? nameof(ThinkGeoVectorTileSource) : ChosenStyleUri();
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
