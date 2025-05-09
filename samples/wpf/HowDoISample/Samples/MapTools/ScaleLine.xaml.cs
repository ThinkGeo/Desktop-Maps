using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render a ScaleLine on the map in a variety of different of styles.
    /// </summary>
    public partial class ScaleLine : IDisposable
    {
        public ScaleLine()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10778000, 3912000);
            MapView.CurrentScale = 77000;

            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Enable the ScaleLine and add it to the MapView (default: bottom left)
        /// </summary>
        private void DisplayScaleLine_Checked(object sender, RoutedEventArgs e)
        {
            MapView.MapTools.ScaleLine.IsEnabled = true;
        }

        /// <summary>
        /// Disable the ScaleLine and remove it from the MapView
        /// </summary>
        private void DisplayScaleLine_Unchecked(object sender, RoutedEventArgs e)
        {
            MapView.MapTools.ScaleLine.IsEnabled = false;
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