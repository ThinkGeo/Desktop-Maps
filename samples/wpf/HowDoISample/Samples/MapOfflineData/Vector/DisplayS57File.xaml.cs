using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display an S57 Layer on the map
    /// </summary>
    public partial class DisplayS57File : IDisposable
    {
        public DisplayS57File()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the S57 layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // It is important to set the map unit first to either feet, meters or decimal degrees.
                MapView.MapUnit = GeographyUnit.DecimalDegree;

                // Create a new overlay that will hold our new layer and add it to the map.
                var chartOverlay = new LayerOverlay();

                // Currently this layer only works in single tile mode at the moment.
                // If you use multi tile not all the data may load in.
                chartOverlay.TileType = TileType.SingleTile;

                // Add the chart to the overlay for display
                MapView.Overlays.Add(chartOverlay);

                // Create the new layer.
                var nauticalLayer = new NauticalChartsFeatureLayer(@"./Data/S57/US1GC09M/US1GC09M.000");

                // Add the layer to the overlay we created earlier.
                chartOverlay.Layers.Add("Charts", nauticalLayer);

                // Set the current extent to a portion of the data
                MapView.CurrentExtent = new RectangleShape(-83.79534200990409, 25.87521424320395, -80.82463888490409, 23.90646424320395);

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