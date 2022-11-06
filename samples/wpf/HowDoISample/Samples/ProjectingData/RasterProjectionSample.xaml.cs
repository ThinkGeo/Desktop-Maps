using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.ProjectingData
{
    /// <summary>
    /// Learn how to automatically reproject a raster layer using the ProjectionConverter class
    /// </summary>
    public partial class RasterProjectionSample : UserControl, IDisposable
    {
        public RasterProjectionSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the Map Unit to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create an overlay that we can add layers to, and add it to the MapView
            LayerOverlay layerOverlay = new LayerOverlay();
            mapView.Overlays.Add(layerOverlay);

            // Reproject a raster layer and set the extent
            ReprojectRasterLayer(layerOverlay);
        }

        /// <summary>
        /// Use the ProjectionConverter class to reproject a raster layer
        /// </summary>
        private void ReprojectRasterLayer(LayerOverlay layerOverlay)
        {
            GeoTiffRasterLayer worldRasterLayer = new GeoTiffRasterLayer(@"../../../Data/GeoTiff/World.tif");

            // Create a new ProjectionConverter to convert between World Geodetic System (4326) and US National Atlas Equal Area (2163)
            ProjectionConverter projectionConverter = new UnmanagedProjectionConverter(4326, 2163);
            worldRasterLayer.ImageSource.ProjectionConverter = projectionConverter;

            layerOverlay.Layers.Clear();
            layerOverlay.Layers.Add("World", worldRasterLayer);

            // Set the map to the extent of the raster layer and refresh the map
            worldRasterLayer.Open();
            mapView.CurrentExtent = worldRasterLayer.GetBoundingBox();
            worldRasterLayer.Close();
            mapView.Refresh();
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
