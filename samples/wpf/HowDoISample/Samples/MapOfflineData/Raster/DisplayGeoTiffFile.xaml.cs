using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a GeoTiff Layer on the map
    /// </summary>
    public partial class DisplayGeoTiffFile : IDisposable
    {
        public DisplayGeoTiffFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the GeoTiff layer to the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.DecimalDegree;

            // Create a new overlay that will hold our new layer and add it to the map.
            var layerOverlay = new LayerOverlay();
            MapView.Overlays.Add(layerOverlay);

            // Create the new layer and dd the layer to the overlay we created earlier.
            var geoTiffRasterLayer = new GeoTiffRasterLayer("./Data/GeoTiff/World.tif");
            layerOverlay.Layers.Add(geoTiffRasterLayer);

            // Set the map view current extent to a slightly zoomed in area of the image.
            MapView.CenterPoint = new PointShape(-16.51088, 12.58643);
            MapView.CurrentScale = 66139200;

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