using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display an ECW Layer on the map
    /// </summary>
    public partial class DisplayECWFile : IDisposable
    {
        public DisplayECWFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the ECW layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            { 
                // It is important to set the map unit first to either feet, meters or decimal degrees.
                MapView.MapUnit = GeographyUnit.DecimalDegree;

                // Create a new overlay that will hold our new layer and add it to the map.
                var layerOverlay = new LayerOverlay();
                MapView.Overlays.Add(layerOverlay);

                // Create the new layer and dd the layer to the overlay we created earlier.
                var ecwRasterLayer = new EcwGdalRasterLayer("./Data/Ecw/World.ecw");
                layerOverlay.Layers.Add(ecwRasterLayer);

                // Set the map view current extent to a slightly zoomed in area of the image.
                MapView.CenterPoint = new PointShape(-16.51088,12.58643);
                MapView.CurrentScale = 66139200;

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