using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;


namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class S57Layer : UserControl
    {
        public S57Layer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            LayerOverlay chartOverlay = new LayerOverlay();
            mapView.Overlays.Add(chartOverlay);

            NauticalChartsFeatureLayer nauticalLayer = new NauticalChartsFeatureLayer(@"../../../Data/S57/US1GC09M/US1GC09M.000");

            chartOverlay.Layers.Add("Charts", nauticalLayer);
            chartOverlay.TileType = TileType.SingleTile;

            mapView.Refresh();
        }
    }
}
