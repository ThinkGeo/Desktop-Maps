using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a GeoTiff Layer on the map
    /// </summary>
    public partial class DisplayGdalFile : IDisposable
    {
        public DisplayGdalFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the GeoTiff layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.DecimalDegree;
            MapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            // Create the new layer and dd the layer to the overlay we created earlier.
            var worldLayer = new GdalRasterLayer("./Data/GeoTiff/World.tif")
            {
                LowerThreshold = 0,
                UpperThreshold = double.MaxValue
            };

            worldLayer.Open();
            MapView.CurrentExtent = worldLayer.GetBoundingBox();
            worldLayer.Close();

            // Create a new overlay that will hold our new layer and add it to the map.
            var staticOverlay = new LayerOverlay
            {
                DrawingExceptionMode = DrawingExceptionMode.DrawException
            };

            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            MapView.Overlays.Add(staticOverlay);

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