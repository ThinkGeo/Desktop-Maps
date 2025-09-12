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
        private bool _initialized;

        public ThinkGeoRasterMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.Meter;

            // Create the layer overlay with some additional settings and add to the map.
            var cloudOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Hybrid2_V2_X1
            };
            MapView.Overlays.Add("Cloud Overlay", cloudOverlay);

            // Set the current extent to a neighborhood in Frisco Texas.
            MapView.CenterPoint = new PointShape(-10779700, 3912000);
            MapView.CurrentScale = 18100;

            _initialized = true;
            _ = MapView.RefreshAsync();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void rbMapType_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

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
                    cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Aerial2_V2_X1;
                    break;
                case "Hybrid":
                    cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid2_V2_X1;
                    break;
            }
            _ = MapView.RefreshAsync();
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