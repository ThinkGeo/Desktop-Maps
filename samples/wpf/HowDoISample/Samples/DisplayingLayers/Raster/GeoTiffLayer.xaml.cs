using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class GeoTiffLayer : UserControl
    {
        public GeoTiffLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            LayerOverlay layerOverlay = new LayerOverlay();
            mapView.Overlays.Add(layerOverlay);

            GeoTiffRasterLayer geoTiffRasterLayer = new GeoTiffRasterLayer("../../../Data/GeoTiff/World.tif");
            geoTiffRasterLayer.Open();

            layerOverlay.Layers.Add(geoTiffRasterLayer);

            mapView.CurrentExtent = new RectangleShape(-90.5399054799761, 68.8866552710533, 57.5181302343096, -43.7137911575181);

            mapView.Refresh();
        }
    }
}
