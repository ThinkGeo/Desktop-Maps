using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.Misc
{
    /// <summary>
    /// This sample shows how to refresh points on the map based on some outside event.
    /// </summary>
    public partial class GetSnapShotSample : UserControl
    {
        public GetSnapShotSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10810995, 3939081, -10747552, 3884429);
            
            // Add a marker in the center of the map. 
            var simpleMarkerOverlay = new SimpleMarkerOverlay();
            var marker = new Marker(mapView.CurrentExtent.GetCenterPoint());
            simpleMarkerOverlay.Markers.Add(marker);
            mapView.Overlays.Add(simpleMarkerOverlay);

            await mapView.RefreshAsync();
        }
        private void btnGetSnapshot_Click(object sender, RoutedEventArgs e)
        {
            var snapShot = mapView.GetSnapshot();
            snapShot.Save(@".\snapshot.png");

            var fullPath = Path.GetFullPath(@".\snapshot.png");
            MessageBox.Show($"The snapshot image was saved at this path: {fullPath}");
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
