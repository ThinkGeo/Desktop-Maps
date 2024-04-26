using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// This sample shows how to refresh points on the map based on some outside event.
    /// </summary>
    public partial class GetSnapShotSample
    {
        public GetSnapShotSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
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

            // set the map extent to Frisco, TX
            MapView.CurrentExtent = new RectangleShape(-10810995, 3939081, -10747552, 3884429);

            // Add a marker in the center of the map. 
            var simpleMarkerOverlay = new SimpleMarkerOverlay();
            var marker = new Marker(MapView.CurrentExtent.GetCenterPoint());
            simpleMarkerOverlay.Markers.Add(marker);
            MapView.Overlays.Add(simpleMarkerOverlay);

            await MapView.RefreshAsync();
        }
        private void btnGetSnapshot_Click(object sender, RoutedEventArgs e)
        {
            var snapShot = MapView.GetSnapshot();
            snapShot.Save(@".\snapshot.png");

            var fullPath = Path.GetFullPath(@".\snapshot.png");
            MessageBox.Show($"The snapshot image was saved at this path: {fullPath}");
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