using System;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to automatically reproject a raster layer using the ProjectionConverter class
    /// </summary>
    public partial class ProjectARaster : IDisposable
    {
        public ProjectARaster()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set the Map Unit to meters (Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

                // Create an overlay that we can add layers to, and add it to the MapView
                var layerOverlay = new LayerOverlay();
                MapView.Overlays.Add(layerOverlay);

                // Reproject a raster layer and set the extent
                await ReprojectRasterLayerAsync(layerOverlay);
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Use the ProjectionConverter class to reproject a raster layer
        /// </summary>
        private async Task ReprojectRasterLayerAsync(LayerOverlay layerOverlay)
        {
            var worldRasterLayer = new GeoTiffRasterLayer(@"./Data/GeoTiff/World.tif");

            // Create a new ProjectionConverter to convert between World Geodetic System (4326) and US National Atlas Equal Area (2163)
            ProjectionConverter projectionConverter = new GdalProjectionConverter(4326, 2163);
            worldRasterLayer.ImageSource.ProjectionConverter = projectionConverter;

            layerOverlay.Layers.Clear();
            layerOverlay.Layers.Add("World", worldRasterLayer);

            // Set the map to the extent of the raster layer and refresh the map
            worldRasterLayer.Open();
            var worldRasterLayerBBox = worldRasterLayer.GetBoundingBox();
            MapView.CenterPoint = worldRasterLayerBBox.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(worldRasterLayerBBox, MapView.ActualWidth, MapView.MapUnit);
            worldRasterLayer.Close();
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