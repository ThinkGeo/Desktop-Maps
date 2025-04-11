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
        public ThinkGeoVectorMap()
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

            // Set the map zoom level set to the Cloud Maps zoom level set.
            MapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Create the layer overlay with some additional settings and add to the map.
            var cloudOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                //TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add("Cloud Overlay", cloudOverlay);

            // Add Scale Line Adornment Layer
            var scaleLineAdornmentLayer = new ScaleLineAdornmentLayer
            {
                XOffsetInPixel = 20,
                YOffsetInPixel = -10,
                Projection = new Projection(3857)
            };

            // Add ScaleBarAdornmentLayer
            var scaleBarAdornmentLayer = new ScaleBarAdornmentLayer
            {
                XOffsetInPixel = 10,
                YOffsetInPixel = -50,
                Projection = new Projection(3857)
            };

            var adornmentOverlay = new AdornmentOverlay();
            adornmentOverlay.Layers.Add(scaleLineAdornmentLayer);
            adornmentOverlay.Layers.Add(scaleBarAdornmentLayer);
            MapView.Overlays.Add("AdornmentOverlay", adornmentOverlay);

            // Set the current extent to a neighborhood in Frisco Texas.
            MapView.CenterPoint = new PointShape(-10779700, 3912000);
            MapView.CurrentScale = 18100;

            _ = MapView.RefreshAsync();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void rbMapType_Checked(object sender, RoutedEventArgs e)
        {
            var button = (RadioButton)sender;
            if (!MapView.Overlays.Contains("Cloud Overlay")) return;
            var cloudOverlay = (ThinkGeoCloudVectorMapsOverlay)MapView.Overlays["Cloud Overlay"];

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