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
        public ResizeTheMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map and a shapefile with simple data to work with
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
            MapView.CenterPoint = new PointShape(-10336000, 5260000);
            MapView.CurrentScale = 37000000;

            _ = MapView.RefreshAsync();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            switch (radioButton.Content)
            {
                case "PreserveScale":
                    MapView.MapResizeMode = MapResizeMode.PreserveScale;
                    break;
                case "PreserveScaleAndCenter":
                    MapView.MapResizeMode = MapResizeMode.PreserveScaleAndCenter;
                    break;
                case "PreserveExtent":
                    MapView.MapResizeMode = MapResizeMode.PreserveExtent;
                    break;
            }
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