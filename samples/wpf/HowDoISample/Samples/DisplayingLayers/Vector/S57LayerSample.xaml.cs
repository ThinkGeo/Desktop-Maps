using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
using System;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display an S57 Layer on the map
    /// </summary>
    public partial class S57LayerSample : UserControl, IDisposable
    {
        public S57LayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, add the S57 layer to the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay chartOverlay = new LayerOverlay();

            // Currently this layer only works in single tile mode at the moment.
            // If you use multi tile not all of the data may load in.
            chartOverlay.TileType = TileType.SingleTile;
            
            // Add the chart to the overlay for display
            mapView.Overlays.Add(chartOverlay);

            // Create the new layer.
            NauticalChartsFeatureLayer nauticalLayer = new NauticalChartsFeatureLayer(@"../../../Data/S57/US1GC09M/US1GC09M.000");

            // Add the layer to the overlay we created earlier.
            chartOverlay.Layers.Add("Charts", nauticalLayer);         

            // Set the current extent to a portion of the data
            mapView.CurrentExtent = new RectangleShape(-83.79534200990409, 25.87521424320395, -80.82463888490409, 23.90646424320395);
            
            // Refresh the map.
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
