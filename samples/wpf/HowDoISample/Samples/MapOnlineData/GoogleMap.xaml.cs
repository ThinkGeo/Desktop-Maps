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
        public GoogleMap()
        {
            InitializeComponent();
        }

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
        /// Add the Google Maps Layer to the map
        /// </summary>
        private void BtnActivate_Click(object sender, RoutedEventArgs e)
        {
            // Sets the map zoom level set to the Google maps zoom level set.
            MapView.ZoomLevelSet = new GoogleMapsZoomLevelSet();

            // Clear the current overlay
            MapView.Overlays.Clear();

            // Create a new overlay that will hold our new layer and add it to the map.
            var worldOverlay = new GoogleMapsOverlay(TxtApiKey.Text, string.Empty);
            MapView.Overlays.Add("WorldOverlay", worldOverlay);

            // Set the current extent to the whole world.
            MapView.CenterPoint = new PointShape(0, 0);
            MapView.CurrentScale = 105721100;

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