using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a MrSid Layer on the map
    /// </summary>
    public partial class DisplayMrSidFile : IDisposable
    {

        private bool _initialized;
        public DisplayMrSidFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the MrSid layer to the map
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            Map.MapUnit = GeographyUnit.DecimalDegree;

            // Create a new overlay that will hold our new layer and add it to the map.
            var layerOverlay = new LayerOverlay();
            Map.Overlays.Add(layerOverlay);

            // Create the new layer and dd the layer to the overlay we created earlier.
            var mrSidRasterLayer = new MrSidGdalRasterLayer("./Data/MrSid/World.sid");
            layerOverlay.Layers.Add(mrSidRasterLayer);

            // Set the map view current extent to a slightly zoomed in area of the image.
            Map.CenterPoint = new PointShape(-16.51088, 12.58643);
            Map.CurrentScale = 66139200;

            _ = Map.RefreshAsync();
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