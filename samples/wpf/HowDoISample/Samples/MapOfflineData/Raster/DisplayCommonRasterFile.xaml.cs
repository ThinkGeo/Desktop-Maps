using System;
using System.Windows;
using System.Windows.Controls;
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
        private void MapView_Loaded(object sender, RoutedEventArgs e)
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
            string skiaRasterFilePath = "./Data/Jpg/m_3309650_sw_14_1_20160911_20161121.jpg";
            string wpfRasterFilePath = "./Data/GeoTiff/m_3309650_sw_14_1_20160911_20161121.tif";

            // Initialize SkiaRasterLayer and WpfRasterLayer
            skiaRasterLayer = new SkiaRasterLayer(skiaRasterFilePath);
            wpfRasterLayer = new WpfRasterLayer(wpfRasterFilePath);

            // Set default layer to SkiaRasterLayer
            rasterOverlay.Layers.Add(skiaRasterLayer);

            // Set the map view current extent to a slightly zoomed in area of the image.
            MapView.CenterPoint = new PointShape(-10780600, 3914700);
            MapView.CurrentScale = 27250;

            _ = MapView.RefreshAsync();
        }

        private void SwitchRasterLayer_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (rasterOverlay == null) return;
            var selectedRadioButton = sender as RadioButton;

            rasterOverlay.Layers.Clear();

            if (selectedRadioButton != null)
            {
                switch (selectedRadioButton.Tag)
                {
                    case "SkiaRasterLayer":
                        rasterOverlay.Layers.Add(skiaRasterLayer);
                        break;
                    case "WpfRasterLayer":
                        rasterOverlay.Layers.Add(wpfRasterLayer);
                        break;
                }
            }

            _ = MapView.RefreshAsync();
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