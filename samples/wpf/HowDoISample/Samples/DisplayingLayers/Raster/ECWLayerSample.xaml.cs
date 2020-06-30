using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
using System.Diagnostics;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class ECWLayerSample : UserControl
    {
        public ECWLayerSample()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay layerOverlay = new LayerOverlay();
            mapView.Overlays.Add(layerOverlay);

            // Create the new layer and dd the layer to the overlay we created earlier.
            EcwRasterLayer ecwRasterLayer = new EcwRasterLayer("../../../Data/Ecw/World.ecw");           
            layerOverlay.Layers.Add(ecwRasterLayer);

            // Set the map view current extent to a slightly zoomed in area of the image.
            mapView.CurrentExtent = new RectangleShape(-90.5399054799761, 68.8866552710533, 57.5181302343096, -43.7137911575181);

            // Refresh the map.
            mapView.Refresh();
        }
    }
}
