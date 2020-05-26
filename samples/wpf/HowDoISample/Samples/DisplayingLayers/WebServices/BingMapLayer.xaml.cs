using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
 
namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class BingMapLayer : UserControl
    {
        public BingMapLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            mapView.Overlays.Clear();
            // Please set your own information about those parameters below.
            //string applicationID = "Amg9BxyuF81NEyxKm2ESMaoL03MTvaYBV3KOfxpHeXDsEt38DVwK4-SPFPg6qcBp";
            string cacheDirectory = @"c:\temp\";
            BingMapsLayer worldLayer = new BingMapsLayer(txtApplicationID.Text, BingMapsMapType.Road, cacheDirectory);

            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-10000000, 10000000, 10000000, -10000000);
            LayerOverlay worldOverlay = new LayerOverlay() { TileWidth = 256, TileHeight = 256 };
            worldOverlay.Layers.Add("WorldLayer", worldLayer);

            mapView.ZoomLevelSet = new BingMapsZoomLevelSet(256);

            mapView.Overlays.Add("WorldOverlay", worldOverlay);
            mapView.Refresh();
        }
    }
}
