using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace BuildingOverlay_Wpf
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
            Map1.MapTools.MouseCoordinate.IsEnabled = true;

            string buildingFilePath = @"..\..\..\BuildingData\gis_osm_buildings_900913.shp";
            ShapeFileFeatureSource buildingShapeSource = new ShapeFileFeatureSource(buildingFilePath);
            buildingShapeSource.Open();
            BuildingOverlay overlay = new BuildingOverlay(buildingShapeSource);
            overlay.DrawingQuality = ThinkGeo.MapSuite.Drawing.DrawingQuality.HighSpeed;
            overlay.TileType = TileType.SingleTile;
            Map1.Overlays.Add(overlay);
            Map1.CurrentExtent = new RectangleShape(1524789, 6636424, 1525257, 6636089);
            Map1.Refresh();
        }
    }
}
