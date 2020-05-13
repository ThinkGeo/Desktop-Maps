using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class CreateAGraticuleAdornmentLayer : UserControl
    {
        public CreateAGraticuleAdornmentLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            GraticuleFeatureLayer graticuleAdornmentLayer = new GraticuleFeatureLayer();
            graticuleAdornmentLayer.GraticuleLineStyle.OuterPen.Color = GeoColor.FromArgb(125, GeoColors.Navy);

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("graticule", graticuleAdornmentLayer);
            mapView.Overlays.Add(layerOverlay);

            mapView.CurrentExtent = new RectangleShape(-100, 50, 100, -50);
            mapView.Refresh();
        }
    }
}