using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// This sample shows how to refresh points on the map based on some outside event.
    /// </summary>
    public partial class GetMapSnapShot
    {

        private bool _initialized;
        public GetMapSnapShot()
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

            var simpleMarkerOverlay = new SimpleMarkerOverlay();
            Map.Overlays.Add(simpleMarkerOverlay);

            // set the map extent to Frisco, TX
            Map.CenterPoint = new PointShape(-10779270, 3911750);
            Map.CurrentScale = 288900;

            // Add a marker in the center of the map. 
            var marker = new Marker(Map.CenterPoint);
            simpleMarkerOverlay.Markers.Add(marker);

            _ = Map.RefreshAsync();
        }
        private void btnGetSnapshot_Click(object sender, RoutedEventArgs e)
        {
            var snapShot = Map.GetSnapshot();
            snapShot.Save(@".\snapshot.png");

            var fullPath = Path.GetFullPath(@".\snapshot.png");
            MessageBox.Show($"The snapshot image was saved at this path: {fullPath}");
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