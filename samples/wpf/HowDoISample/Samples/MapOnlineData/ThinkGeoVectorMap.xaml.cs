using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a CloudMapsVector Layer on the map
    /// </summary>
    public partial class ThinkGeoVectorMap : IDisposable
    {
        private bool _initialized;

        public ThinkGeoVectorMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            Map.MapUnit = GeographyUnit.Meter;

            // Create the layer overlay with some additional settings and add to the map.
            var cloudOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                //TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add("Cloud Overlay", cloudOverlay);

            // Set the current extent to a neighborhood in Frisco Texas.
            Map.CenterPoint = new PointShape(-10779700, 3912000);
            Map.CurrentScale = 18100;

            _initialized = true;
            _ = Map.RefreshAsync();
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
            if (!Map.Overlays.Contains("Cloud Overlay")) return;
            var cloudOverlay = (ThinkGeoCloudVectorMapsOverlay)Map.Overlays["Cloud Overlay"];

            switch (button.Content.ToString())
            {
                case "Light":
                    cloudOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Light;
                    break;
                case "Dark":
                    cloudOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Dark;
                    break;
                case "TransparentBackground":
                    cloudOverlay.MapType = ThinkGeoCloudVectorMapsMapType.TransparentBackground;
                    break;
            }
            _ = Map.RefreshAsync();
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