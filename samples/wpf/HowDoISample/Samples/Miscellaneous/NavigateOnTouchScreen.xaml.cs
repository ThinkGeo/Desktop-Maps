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
        public NavigateOnTouchScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            MapView.CenterPoint = new PointShape(0, 0);
            MapView.CurrentScale = 100000000;

            MapView.IsManipulationEnabled = true;

            _ = MapView.RefreshAsync();
        }

        private void CheckBox_Toggled(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox?.IsChecked == null) return;

            if (checkBox.IsChecked.Value)
                MapView.TrackOverlay.TrackMode = TrackMode.Freehand;
            else
                MapView.TrackOverlay.TrackMode = TrackMode.None;

        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
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