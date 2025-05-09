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
        private void MapView_Loaded(object sender, RoutedEventArgs e)
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
            MapView.CenterPoint = new PointShape(-82.30999, 24.89084);
            MapView.CurrentScale = 1156400;

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