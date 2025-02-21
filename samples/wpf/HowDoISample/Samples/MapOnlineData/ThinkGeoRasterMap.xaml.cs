using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a CloudRasterMaps Layer on the map
    /// </summary>
    public partial class ThinkGeoRasterMap : IDisposable
    {
        public ThinkGeoRasterMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // It is important to set the map unit first to either feet, meters or decimal degrees.
                MapView.MapUnit = GeographyUnit.Meter;

                // Set the map zoom level set to the Cloud Maps zoom level set.
                MapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

                // Create the layer overlay with some additional settings and add to the map.
                var cloudOverlay = new ThinkGeoCloudRasterMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1
                };
                MapView.Overlays.Add("Cloud Overlay", cloudOverlay);

                // Set the current extent to a neighborhood in Frisco Texas.
                MapView.CurrentExtent = new RectangleShape(-10781708.9749424, 3913502.90429046, -10777685.1114043, 3910360.79646662);

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

        private async void rbMapType_Checked(object sender, RoutedEventArgs e)
        {
            try
            { 
                var button = (RadioButton)sender;
                if (!MapView.Overlays.Contains("Cloud Overlay")) return;
                var cloudOverlay = (ThinkGeoCloudRasterMapsOverlay)MapView.Overlays["Cloud Overlay"];

                switch (button.Content.ToString())
                {
                    case "Light":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1;
                        break;
                    case "Dark":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Dark_V2_X1;
                        break;
                    case "Aerial":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1;
                        break;
                    case "Hybrid":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1;
                        break;
                }
                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
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