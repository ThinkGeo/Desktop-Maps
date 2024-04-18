using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a MrSid Layer on the map
    /// </summary>
    public partial class MrSidLayerSample : IDisposable
    {
        public MrSidLayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the MrSid layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.DecimalDegree;

            // Create a new overlay that will hold our new layer and add it to the map.
            var layerOverlay = new LayerOverlay();
            MapView.Overlays.Add(layerOverlay);

            // Create the new layer and dd the layer to the overlay we created earlier.
            var mrSidRasterLayer = new MrSidRasterLayer("./Data/MrSid/World.sid");
            layerOverlay.Layers.Add(mrSidRasterLayer);

            // Set the map view current extent to a slightly zoomed in area of the image.
            MapView.CurrentExtent = new RectangleShape(-90.5399054799761, 68.8866552710533, 57.5181302343096, -43.7137911575181);

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