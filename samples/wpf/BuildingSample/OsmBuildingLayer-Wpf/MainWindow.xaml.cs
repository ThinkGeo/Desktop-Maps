using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace OsmBuildingLayer_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WpfMap_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.Meter;

            Proj4Projection proj4 = new Proj4Projection();
            proj4.InternalProjectionParametersString = Proj4Projection.GetDecimalDegreesParametersString();
            proj4.ExternalProjectionParametersString = Proj4Projection.GetGoogleMapParametersString();
            proj4.Open();

            OsmBuildingOnlineFeatureLayer buildingShapeLayer = new OsmBuildingOnlineFeatureLayer();
            buildingShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country2;
            buildingShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            buildingShapeLayer.FeatureSource.Projection = proj4;
            buildingShapeLayer.Open();

            LayerOverlay buildingOverlay = new LayerOverlay();
            buildingOverlay.TileType = TileType.MultipleTile;
            buildingOverlay.Layers.Add(buildingShapeLayer);
            Map1.Overlays.Add(buildingOverlay);

            Map1.CurrentExtent = new RectangleShape(1524042.13444618, 6636760.00025701, 1525246.01463038, 6635889.33690951);
            Map1.Refresh();
        }
    }
}
