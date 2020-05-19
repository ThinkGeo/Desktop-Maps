using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class MrSidLayer : UserControl
    {
        public MrSidLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            LayerOverlay layerOverlay = new LayerOverlay();
            mapView.Overlays.Add(layerOverlay);

            MrSidRasterLayer mrSidRasterLayer = new MrSidRasterLayer("../../../Data/MrSid/World.sid");
            mrSidRasterLayer.Open();

            layerOverlay.Layers.Add(mrSidRasterLayer);

            mapView.CurrentExtent = new RectangleShape(-90.5399054799761, 68.8866552710533, 57.5181302343096, -43.7137911575181);

            mapView.Refresh();
        }
    }
}
