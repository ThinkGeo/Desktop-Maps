using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to improve performance by locally caching map tiles
    /// </summary>
    public partial class CachingMapTilesSample : UserControl, IDisposable
    {
        ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay;

        public CachingMapTilesSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

            await mapView.RefreshAsync();
        }

        /// <summary>
        /// Create a new tile cache on the Cloud Maps overlay. Cached images will be saved on the file system in the bin folder.
        /// </summary>
        private void UseCache_Checked(object sender, RoutedEventArgs e)
        {
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache("cache", "thinkgeo_vector_light", RasterTileFormat.Png);
        }

        /// <summary>
        /// Remove the tile cache by setting it to null. Note that this does not remove the cached images on the file system.
        /// </summary>
        private void UseCache_Unchecked(object sender, RoutedEventArgs e)
        {
            thinkGeoCloudVectorMapsOverlay.TileCache = null;
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
