using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show a vector basemap: ThinkGeo Cloud in light or dark, or any MVT server from its style.json URL.
    /// </summary>
    public partial class VectorMapProviders : IDisposable
    {
        private bool _initialized;
        private int _request;

        public VectorMapProviders()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.CenterPoint = new PointShape(-10779700, 3912000);
            Map.CurrentScale = 18100;
            await ShowChosenAsync();
        }

        private async void Provider_Checked(object sender, RoutedEventArgs e)
        {
            if (_initialized) await ShowChosenAsync();
        }

        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            await ShowChosenAsync();
        }

        private async Task ShowChosenAsync()
        {
            var request = ++_request;
            CustomPanel.Visibility = CustomServer.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            Map.Overlays.Clear();
            Status.Text = string.Empty;

            try
            {
                if (ThinkGeoLight.IsChecked == true)
                    ShowThinkGeoCloud(ThinkGeoVectorStyles.Light);
                else if (ThinkGeoDark.IsChecked == true)
                    ShowThinkGeoCloud(ThinkGeoVectorStyles.Dark);
                else
                    await ShowMvtServerAsync(request);
            }
            catch (Exception exception)
            {
                if (request == _request) Status.Text = "Unavailable: " + exception.Message;
            }

            _ = Map.RefreshAsync();
        }

        private void ShowThinkGeoCloud(string styleUri)
        {
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey, styleUri));
            Status.Text = nameof(ThinkGeoVectorTileOverlay);
        }

        private async Task ShowMvtServerAsync(int request)
        {
            var mvtLayer = new MvtTilesAsyncLayer(StyleUrl.Text.Trim());
            await mvtLayer.OpenAsync();
            if (request != _request) return;

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(mvtLayer);
            Map.Overlays.Add(layerOverlay);
            Map.CurrentExtent = mvtLayer.GetBoundingBox();
            Status.Text = nameof(MvtTilesAsyncLayer);
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
