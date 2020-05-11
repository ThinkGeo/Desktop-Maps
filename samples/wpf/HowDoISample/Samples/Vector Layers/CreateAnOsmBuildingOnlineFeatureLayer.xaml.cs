using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for CreateAnOsmBuildingOnlineFeatureLayer.xaml
    /// </summary>
    public partial class CreateAnOsmBuildingOnlineFeatureLayer : UserControl
    {
        public CreateAnOsmBuildingOnlineFeatureLayer()
        {
            InitializeComponent();
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            OsmBuildingOnlineFeatureLayer buildingShapeLayer = new OsmBuildingOnlineFeatureLayer();
            buildingShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.WhiteSmoke, GeoColors.DarkGray);
            buildingShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            buildingShapeLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            buildingShapeLayer.Open();

            LayerOverlay buildingOverlay = new LayerOverlay();
            buildingOverlay.Layers.Add(buildingShapeLayer);
            mapView.Overlays.Add(buildingOverlay);

            mapView.CurrentExtent = new RectangleShape(1524042.13444618, 6636760.00025701, 1525246.01463038, 6635889.33690951);
            mapView.Refresh();
        }
    }
}
