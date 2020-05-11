using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for CreateATinyGeoFeatureLayer.xaml
    /// </summary>
    public partial class CreateATinyGeoFeatureLayer : UserControl
    {
        public CreateATinyGeoFeatureLayer()
        {
            InitializeComponent();
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            var tinyGeoFeatureLayer = new TinyGeoFeatureLayer(SampleHelper.Get("Frisco.tgeo"));
            tinyGeoFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Black, 1, true);
            tinyGeoFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            tinyGeoFeatureLayer.Open();
            var currentExtent = tinyGeoFeatureLayer.GetBoundingBox();
            tinyGeoFeatureLayer.Close();

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(tinyGeoFeatureLayer);

            mapView.Overlays.Add(layerOverlay);
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = currentExtent;
            mapView.Refresh();
        }
    }
}
