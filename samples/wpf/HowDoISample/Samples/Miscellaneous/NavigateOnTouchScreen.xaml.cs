using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// This sample shows how to refresh points on the map based on some outside event.
    /// </summary>
    public partial class NavigateOnTouchScreen
    {

        private bool _initialized;
        public NavigateOnTouchScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            Map.CenterPoint = new PointShape(0, 0);
            Map.CurrentScale = 100000000;

            Map.IsManipulationEnabled = true;

            _ = Map.RefreshAsync();
        }

        private void CheckBox_Toggled(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox?.IsChecked == null) return;

            if (checkBox.IsChecked.Value)
                Map.TrackOverlay.TrackMode = TrackMode.Freehand;
            else
                Map.TrackOverlay.TrackMode = TrackMode.None;

        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        // Click handler wired up in XAML above.
        private void OnOpenWebsiteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var url = "https://youtu.be/1LAROjqIrO8?si=O1zdrhpisj1vCIB8";
                // On .NET Core / .NET 5+, UseShellExecute = true is required
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                // Show an error if something goes wrong
                MessageBox.Show($"Unable to open browser:\n{ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}