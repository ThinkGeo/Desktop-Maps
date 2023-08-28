using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for ShapefileLayer.xaml
    /// </summary>
    public partial class OfflineCloudMapsVectorLayerSample : UserControl, IDisposable
    {
        public OfflineCloudMapsVectorLayerSample()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a new overlay that will hold our new layer
            LayerOverlay layerOverlay = new LayerOverlay();

            // Create the background world maps using vector tiles stored locally in our MBTiles file and also set the styling though a json file
            ThinkGeoMBTilesLayer mbTilesLayer = new ThinkGeoMBTilesLayer(@"./Data/Mbtiles/Frisco.mbtiles", new Uri(@"./Data/Json/thinkgeo-world-streets-dark.json",UriKind.Relative));
            layerOverlay.Layers.Add(mbTilesLayer);

            // Add the overlay to the map
            mapView.Overlays.Add(layerOverlay);

            // Set the current extent of the map to an area in the data
            mapView.CurrentExtent = new RectangleShape(-10785086.173498387, 3913489.693302595, -10779919.030415015, 3910065.3144544438);

            // Refresh the map.
            await mapView.RefreshAsync();            
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
