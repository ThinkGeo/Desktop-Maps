using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class OpenStreetMapLayer : UserControl
    {
        public OpenStreetMapLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            var openStreetMapLayer = new ThinkGeo.Core.OpenStreetMapLayer("ThinkGeo Samples/12.0 (http://thinkgeo.com/; system@thinkgeo.com)");
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileWidth = 256;
            layerOverlay.TileHeight = 256;

            layerOverlay.Layers.Add(openStreetMapLayer);
            mapView.Overlays.Add(layerOverlay);

            mapView.ZoomLevelSet = new OpenStreetMapsZoomLevelSet();
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-10789388.4602951, 3923878.18083465, -10768258.7082788, 3906668.46719412);
            mapView.Refresh();
        }
    }
}
