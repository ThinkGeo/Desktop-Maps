using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render Google Maps using the GoogleMapsOverlay.
    /// A Google Maps API key and secret is required.
    /// </summary>
    public partial class DisplayGoogleMapsOverlaySample : UserControl, IDisposable
    {
        public DisplayGoogleMapsOverlaySample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with a background overlay and set the map's extent to Frisco, Tx.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add a simple background overlay
            mapView.BackgroundOverlay.BackgroundBrush = GeoBrushes.AliceBlue;

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);
        }

        /// <summary>
        /// Create a Google Maps overlay and add it to the map view.
        /// </summary>
        private void DisplayGoogleMaps_Click(object sender, RoutedEventArgs e)
        {
            GoogleMapsOverlay googleMapsOverlay = new GoogleMapsOverlay(googleApiKey.Text, googleSigningSecret.Text);
            mapView.Overlays.Add(googleMapsOverlay);
            mapView.Refresh();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

    }
}
