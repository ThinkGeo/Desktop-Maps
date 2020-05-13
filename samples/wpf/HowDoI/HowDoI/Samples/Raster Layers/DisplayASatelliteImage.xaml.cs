using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class DisplayASatelliteImage : UserControl
    {
        public DisplayASatelliteImage()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = new RectangleShape(-180, 90, 180, -90);

            EcwRasterLayer ecwImageLayer = new EcwRasterLayer(SampleHelper.Get("World.ecw"));
            ecwImageLayer.UpperThreshold = double.MaxValue;
            ecwImageLayer.LowerThreshold = 0;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("EcwImageLayer", ecwImageLayer);
            mapView.Overlays.Add(staticOverlay);

            mapView.Refresh();
        }
    }
}