using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to resize the map while preserving the world extent with various map resize modes.
    /// </summary>
    public partial class ResizeTheMap : IDisposable
    {
        private bool _initialized;

        public ResizeTheMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map and a shapefile with simple data to work with
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // Set the map's unit of measurement to meters(Spherical Mercator)
            Map.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            Map.CenterPoint = new PointShape(-10336000, 5260000);
            Map.CurrentScale = 37000000;

            _ = Map.RefreshAsync();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            switch (radioButton.Content)
            {
                case "PreserveScale":
                    Map.MapResizeMode = MapResizeMode.PreserveScale;
                    break;
                case "PreserveScaleAndCenter":
                    Map.MapResizeMode = MapResizeMode.PreserveScaleAndCenter;
                    break;
                case "PreserveExtent":
                    Map.MapResizeMode = MapResizeMode.PreserveExtent;
                    break;
            }
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
