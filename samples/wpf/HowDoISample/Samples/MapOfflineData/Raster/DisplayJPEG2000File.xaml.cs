using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a JPEG2000 Layer on the map
    /// </summary>
    public partial class DisplayJPEG2000File : IDisposable
    {
        public DisplayJPEG2000File()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the JPEG2000 layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // It is important to set the map unit first to either feet, meters or decimal degrees.
                MapView.MapUnit = GeographyUnit.Meter;

                // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
                var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudVectorMapsMapType.Light,
                    // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                    TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
                };
                MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

                // Create a new overlay that will hold our new layer and add it to the map.
                var layerOverlay = new LayerOverlay();
                MapView.Overlays.Add(layerOverlay);

                // Create the new layer and dd the layer to the overlay we created earlier.
                var jp2000RasterLayer = new MrSidGdalRasterLayer("./Data/Jp2/m_3309650_sw_14_1_20160911_20161121.jp2");
                layerOverlay.Layers.Add(jp2000RasterLayer);

                // Set the map view current extent to a slightly zoomed in area of the image.
                MapView.CurrentExtent = new RectangleShape(-10783910.2966461, 3917274.29233111, -10777309.4670677, 3912119.9131963);

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
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