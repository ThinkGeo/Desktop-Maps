using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class LoadAMrSidImage : UserControl
    {
        public LoadAMrSidImage()
        {
            InitializeComponent();
        }

        private void WpfMap_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = new RectangleShape(-118.098, 84.3, 118.098, -84.3);

            MrSidRasterLayer sidImageLayer = new MrSidRasterLayer(SampleHelper.Get("world.sid"));
            sidImageLayer.UpperThreshold = double.MaxValue;
            sidImageLayer.LowerThreshold = 0;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("SidImageLayer", sidImageLayer);
            mapView.Overlays.Add(staticOverlay);

            mapView.Refresh();
        }
    }
}