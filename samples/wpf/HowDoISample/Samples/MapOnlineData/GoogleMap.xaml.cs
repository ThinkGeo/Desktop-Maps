using System;
using System.Diagnostics;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a Google Maps Layer on the map
    /// </summary>
    public partial class GoogleMap : IDisposable
    {

        private bool _initialized;
        public GoogleMap()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // Set the map's unit of measurement to meters(Spherical Mercator)
            Map.MapUnit = GeographyUnit.Meter;

            // Add a simple background overlay
            Map.BackgroundOverlay.BackgroundBrush = GeoBrushes.AliceBlue;

            // Set the map extent
            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 77000;
        }

        /// <summary>
        /// Add the Google Maps Layer to the map
        /// </summary>
        private void BtnActivate_Click(object sender, RoutedEventArgs e)
        {
            // Clear the current overlay
            Map.Overlays.Clear();

            // Create a new overlay that will hold our new layer and add it to the map.
            var worldOverlay = new GoogleMapsOverlay(TxtApiKey.Text, string.Empty);
            Map.Overlays.Add("WorldOverlay", worldOverlay);

            // Set the current extent to the whole world.
            Map.CenterPoint = new PointShape(0, 0);
            Map.CurrentScale = 105721100;

            _ = Map.RefreshAsync();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}