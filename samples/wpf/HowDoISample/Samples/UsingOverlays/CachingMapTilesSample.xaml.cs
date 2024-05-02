using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to improve performance by locally caching map tiles
    /// </summary>
    public partial class CachingMapTilesSample : IDisposable
    {
        ThinkGeoCloudVectorMapsOverlay _thinkGeoCloudVectorMapsOverlay;

        public CachingMapTilesSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            _thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
            };
            MapView.Overlays.Add(_thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            MapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

            await MapView.RefreshAsync();
        }

        /// <summary>
        /// Create a new tile cache on the Cloud Maps overlay. Cached images will be saved on the file system in the bin folder.
        /// </summary>
        private void UseCache_Checked(object sender, RoutedEventArgs e)
        {
            _thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache("cache", "thinkgeo_vector_light", RasterTileFormat.Png);
        }

        /// <summary>
        /// Remove the tile cache by setting it to null. Note that this does not remove the cached images on the file system.
        /// </summary>
        private void UseCache_Unchecked(object sender, RoutedEventArgs e)
        {
            _thinkGeoCloudVectorMapsOverlay.TileCache = null;
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