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
            MapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);
        }

        /// <summary>
        /// Add the Google Maps Layer to the map
        /// </summary>
        private async void BtnActivate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Sets the map zoom level set to the Google maps zoom level set.
                MapView.ZoomLevelSet = new GoogleMapsZoomLevelSet();

                // Clear the current overlay
                MapView.Overlays.Clear();

                // Create a new overlay that will hold our new layer and add it to the map.
                var worldOverlay = new GoogleMapsOverlay(TxtApiKey.Text, string.Empty);
                MapView.Overlays.Add("WorldOverlay", worldOverlay);

                // Set the current extent to the whole world.
                MapView.CurrentExtent = new RectangleShape(-10000000, 10000000, 10000000, -10000000);

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
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