using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace BuildingStyle_Wpf
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
            Map1.CurrentExtent = new RectangleShape(1524789, 6636424, 1525257, 6636089);

            string buildingFilePath = @"..\..\..\BuildingData\gis_osm_buildings_900913.shp";
            ShapeFileFeatureLayer buildingShapeLayer = new ShapeFileFeatureLayer(buildingFilePath);
            buildingShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new BuildingAreaStyle();
            buildingShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay buildingOverlay = new LayerOverlay();
            buildingOverlay.TileType = TileType.SingleTile;
            buildingOverlay.Layers.Add(buildingShapeLayer);
            Map1.Overlays.Add(buildingOverlay);

            Map1.Refresh();
        }
    }
}
