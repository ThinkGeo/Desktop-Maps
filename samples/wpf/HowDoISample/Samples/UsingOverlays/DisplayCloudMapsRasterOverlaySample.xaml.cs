using System;
using System.Diagnostics;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render ThinkGeo Cloud Maps in raster format.
    /// </summary>
    public partial class DisplayCloudMapsRasterOverlaySample : IDisposable
    {
        public DisplayCloudMapsRasterOverlaySample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with a background overlay and set the map's extent to Frisco, Tx.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add a simple background overlay
            MapView.BackgroundOverlay.BackgroundBrush = GeoBrushes.AliceBlue;

            // Set the map extent
            MapView.CurrentExtent = new RectangleShape(-10782598.9806675, 3915669.09132595, -10772234.1196896, 3906343.77392696);
        }

        /// <summary>
        /// Create a ThinkGeo Cloud Maps raster overlay and add it to the map view.
        /// </summary>
        private async void DisplayRasterCloudMaps_Click(object sender, RoutedEventArgs e)
        {
            var thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1,
            };
            MapView.Overlays.Add(thinkGeoCloudRasterMapsOverlay);
            await MapView.RefreshAsync();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}