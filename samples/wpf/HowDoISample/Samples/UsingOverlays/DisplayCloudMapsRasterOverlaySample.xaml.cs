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
            MapView.CenterPoint = new PointShape(-10777420, 3911000);
            MapView.CurrentScale = 49300;
        }

        /// <summary>
        /// Create a ThinkGeo Cloud Maps raster overlay and add it to the map view.
        /// </summary>
        private void DisplayRasterCloudMaps_Click(object sender, RoutedEventArgs e)
        {
            var thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Hybrid2_V2_X1,
            };
            MapView.Overlays.Add(thinkGeoCloudRasterMapsOverlay);
            _ = MapView.RefreshAsync();
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