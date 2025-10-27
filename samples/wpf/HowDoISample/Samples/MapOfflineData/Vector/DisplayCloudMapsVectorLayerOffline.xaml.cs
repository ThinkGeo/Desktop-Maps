using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for ShapefileLayer.xaml
    /// </summary>
    public partial class DisplayCloudMapsVectorLayerOffline : IDisposable
    {
        public DisplayCloudMapsVectorLayerOffline()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.Meter;

            // Create a new overlay that will hold our new layer
            var layerOverlay = new LayerOverlay();

            // Create the background world maps using vector tiles stored locally in our MBTiles file and also set the styling though a json file
            var mbTilesLayer = new ThinkGeoMBTilesLayer(@"./Data/MBTiles/Frisco.mbtiles", new Uri(@"./Data/Json/thinkgeo-world-streets-dark.json", UriKind.Relative));
            layerOverlay.Layers.Add(mbTilesLayer);

            // Add the overlay to the map
            MapView.Overlays.Add(layerOverlay);

            // Set the current extent of the map to an area in the data
            MapView.CenterPoint = new PointShape(-10782500, 3911780);
            MapView.CurrentScale = 18100;

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