using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace Buildings3DSample_forWpf
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

        private void Map1_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.Meter;

            string buildingFilePath = @"..\..\AppData\osm_buildings_900913_min.shp";
            ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(buildingFilePath);
            featureSource.Open();
            Map1.CurrentExtent = featureSource.GetFeatureById("1", ReturningColumnsType.NoColumns).GetBoundingBox();

            OsmBuildingOverlay buildingOverlay = new OsmBuildingOverlay();
            buildingOverlay.BuildingFeatureSource = featureSource;
            Map1.Overlays.Add(buildingOverlay);

            Map1.Refresh();
        }
    }
}
