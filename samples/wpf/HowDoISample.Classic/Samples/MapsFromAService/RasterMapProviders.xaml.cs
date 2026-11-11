using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show a raster basemap: ThinkGeo Cloud, OpenStreetMap or any XYZ tile server; Azure and Google need your own key.
    /// </summary>
    public partial class RasterMapProviders : IDisposable
    {
        // OpenStreetMap's tile policy asks every application for a user agent that names it.
        private const string UserAgent = "ThinkGeo HowDoI Sample";

        private bool _initialized;

        public RasterMapProviders()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.CenterPoint = new PointShape(-10779700, 3912000);
            Map.CurrentScale = 18100;
            ShowChosen();
        }

        private void Provider_Checked(object sender, RoutedEventArgs e)
        {
            if (_initialized) ShowChosen();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            ShowChosen();
        }

        private void ShowChosen()
        {
            CustomXyzPanel.Visibility = CustomXyz.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            KeyedProvidersPage.Visibility = KeyedProviders.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            Map.Overlays.Clear();
            Status.Text = string.Empty;

            try
            {
                if (ThinkGeoLight.IsChecked == true)
                    ShowThinkGeoCloud(ThinkGeoCloudRasterMapsMapType.Light_V2_X1);
                else if (ThinkGeoDark.IsChecked == true)
                    ShowThinkGeoCloud(ThinkGeoCloudRasterMapsMapType.Dark_V2_X1);
                else if (ThinkGeoAerial.IsChecked == true)
                    ShowThinkGeoCloud(ThinkGeoCloudRasterMapsMapType.Aerial2_V2_X1);
                else if (ThinkGeoHybrid.IsChecked == true)
                    ShowThinkGeoCloud(ThinkGeoCloudRasterMapsMapType.Hybrid2_V2_X1);
                else if (Osm.IsChecked == true)
                    ShowOpenStreetMap();
                else if (CustomXyz.IsChecked == true)
                    ShowXyzServer();
            }
            catch (UriFormatException exception)
            {
                Status.Text = "Not a URL: " + exception.Message;
            }

            _ = Map.RefreshAsync();
        }

        private void ShowThinkGeoCloud(ThinkGeoCloudRasterMapsMapType mapType)
        {
            Map.Overlays.Add(new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = mapType
            });
            Status.Text = nameof(ThinkGeoCloudRasterMapsOverlay);
        }

        private void ShowOpenStreetMap()
        {
            Map.Overlays.Add(new OpenStreetMapOverlay(UserAgent));
            Status.Text = nameof(OpenStreetMapOverlay);
        }

        private void ShowXyzServer()
        {
            // The layer's default cache folder is shared by every URL, so a new URL would show the old tiles.
            var xyzLayer = new OpenStreetMapAsyncLayer(UserAgent) { TileCache = null };
            xyzLayer.CustomServerUris.Add(new Uri(XyzUrl.Text.Trim()));

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(xyzLayer);
            Map.Overlays.Add(layerOverlay);
            Status.Text = nameof(OpenStreetMapAsyncLayer) + ".CustomServerUris";
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
