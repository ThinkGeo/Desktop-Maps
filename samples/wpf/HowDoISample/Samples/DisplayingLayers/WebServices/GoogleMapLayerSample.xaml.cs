using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
using System.Diagnostics;
using System;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a Google Maps Layer on the map
    /// </summary>
    public partial class GoogleMapLayerSample : UserControl, IDisposable
    {
        public GoogleMapLayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the Google Maps Layer to the map
        /// </summary>
        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Sets the map zoom level set to the Google maps zoom level set.
            mapView.ZoomLevelSet = new GoogleMapsZoomLevelSet();

            // Clear the current overlay
            mapView.Overlays.Clear();

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay worldOverlay = new LayerOverlay();
            mapView.Overlays.Add("WorldOverlay", worldOverlay);

            // Create the new layer.
            GoogleMapsLayer worldLayer = new GoogleMapsLayer();

            // Add the layer to the overlay we created earlier.
            worldOverlay.Layers.Add("WorldLayer", worldLayer);

            // Set the client ID and Private key from the text box on the sample.  
            worldLayer.ClientId = txtClientId.Text;
            worldLayer.PrivateKey = txtPrivateKey.Text;

            // Set the current extent to the whole world.
            mapView.CurrentExtent = new RectangleShape(-10000000, 10000000, 10000000, -10000000);
           
            // Refresh the map.
            mapView.Refresh();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
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
