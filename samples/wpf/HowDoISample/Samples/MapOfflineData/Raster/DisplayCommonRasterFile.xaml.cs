using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display common raster layers on the map
    /// </summary>
    public partial class DisplayCommonRasterFile : IDisposable
    {
        private LayerOverlay rasterOverlay;
        private SkiaRasterLayer skiaRasterLayer;
        private WpfRasterLayer wpfRasterLayer;

        public DisplayCommonRasterFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the common raster layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
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

            // Create an overlay for the raster layers
            rasterOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            MapView.Overlays.Add(rasterOverlay);

            // Path to the raster file
            string rasterFileRelativePath = "./Data/Jpg/m_3309650_sw_14_1_20160911_20161121.jpg";
            string rasterFileAbsolutePath = Path.GetFullPath(rasterFileRelativePath);

            // Initialize SkiaRasterLayer and WpfRasterLayer
            skiaRasterLayer = new SkiaRasterLayer(rasterFileRelativePath);
            wpfRasterLayer = new WpfRasterLayer(rasterFileAbsolutePath);

            // Set default layer to SkiaRasterLayer
            rasterOverlay.Layers.Add(skiaRasterLayer);

            // Set the map view current extent to a slightly zoomed in area of the image.
            MapView.CurrentExtent = new RectangleShape(-10783910.2966461, 3917274.29233111, -10777309.4670677, 3912119.9131963);

            await MapView.RefreshAsync();
        }

        private async void SwitchRasterLayer_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (rasterOverlay == null) return;
            var selectedRadioButton = sender as System.Windows.Controls.RadioButton;
            
            rasterOverlay.Layers.Clear();

            if (selectedRadioButton != null)
            {
                switch (selectedRadioButton.Tag)
                {
                    case "SkiaRasterLayer":
                        rasterOverlay.Layers.Add(skiaRasterLayer);
                        //MapView.CurrentExtent = skiaRasterLayer.GetBoundingBox();
                        break;
                    case "WpfRasterLayer":
                        rasterOverlay.Layers.Add(wpfRasterLayer);
                        //MapView.CurrentExtent = wpfRasterLayer.GetBoundingBox();
                        break;
                }
            }

            await MapView.RefreshAsync();
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