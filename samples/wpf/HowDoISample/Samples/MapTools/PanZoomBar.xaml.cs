using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render a PanZoomBar on the map.
    /// </summary>
    public partial class PanZoomBar : IDisposable
    {
        private bool _initialized;

        public PanZoomBar()
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

            MapView.MapTools.PanZoomBar.IsEnabled = true;

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10778000, 3912000);
            MapView.CurrentScale = 77000;

            _initialized = true;
            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Enable the PanZoomBar and remove it from the MapView
        /// </summary>
        private void DisplayPanZoomBar_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            MapView.MapTools.PanZoomBar.IsEnabled = true;
        }

        /// <summary>
        /// Disable the PanZoomBar and remove it from the MapView
        /// </summary>
        private void DisplayPanZoomBar_Unchecked(object sender, RoutedEventArgs e)
        {
            MapView.MapTools.PanZoomBar.IsEnabled = false;
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