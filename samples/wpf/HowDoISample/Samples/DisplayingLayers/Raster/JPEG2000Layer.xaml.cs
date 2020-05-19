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
    public partial class JPEG2000Layer : UserControl
    {
        public JPEG2000Layer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Feet;

            LayerOverlay layerOverlay = new LayerOverlay();
            mapView.Overlays.Add(layerOverlay);

            MrSidRasterLayer jp2000RasterLayer = new MrSidRasterLayer("../../../Data/FriscoRaster/m_3309650_sw_14_1_20160911_20161121.jp2");
            jp2000RasterLayer.Open();

            layerOverlay.Layers.Add(jp2000RasterLayer);

            mapView.CurrentExtent = new RectangleShape(-10783910.2966461, 3917274.29233111, -10777309.4670677, 3912119.9131963);

            mapView.Refresh();
        }
    }
}
