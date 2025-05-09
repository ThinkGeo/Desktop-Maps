using System;
using System.Diagnostics;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render Google Maps using the GoogleMapsOverlay.
    /// A Google Maps API key and secret is required.
    /// </summary>
    public partial class DisplayGoogleMapsOverlaySample : IDisposable
    {
        public DisplayGoogleMapsOverlaySample()
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
            MapView.CenterPoint = new PointShape(-10778000, 3912000);
            MapView.CurrentScale = 77000;
        }

        /// <summary>
        /// Create a Google Maps overlay and add it to the map view.
        /// </summary>
        private void DisplayGoogleMaps_Click(object sender, RoutedEventArgs e)
        {
            var googleMapsOverlay = new GoogleMapsOverlay(GoogleApiKey.Text, string.Empty);
            MapView.Overlays.Add(googleMapsOverlay);
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