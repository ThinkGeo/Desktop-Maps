using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class CommonRasterLayers : UserControl
    {
        public CommonRasterLayers()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {

            mapView.MapUnit = GeographyUnit.Feet;

            LayerOverlay layerOverlay = new LayerOverlay();
            mapView.Overlays.Add(layerOverlay);

            NativeImageRasterLayer commonRasterLayer = new NativeImageRasterLayer("../../../Data/FriscoRaster/m_3309650_sw_14_1_20160911_20161121.jpg");
            commonRasterLayer.Open();

            layerOverlay.Layers.Add(commonRasterLayer);



            mapView.CurrentExtent = new RectangleShape(-10783910.2966461, 3917274.29233111, -10777309.4670677, 3912119.9131963);

            mapView.Refresh();
        }      
        
    }
}
