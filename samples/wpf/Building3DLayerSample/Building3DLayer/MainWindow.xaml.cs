using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace Building3DLayer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            map.MapUnit = GeographyUnit.Meter;
            map.CurrentExtent = new RectangleShape(1526609.33727945, 6639059.07699771, 1527211.27737155, 6638623.74532396);

            OpenStreetMapOverlay osmOverlay = new OpenStreetMapOverlay();
            map.Overlays.Add(osmOverlay);

            Proj4Projection proj4Projection = new Proj4Projection();
            proj4Projection.InternalProjectionParametersString = Proj4Projection.GetDecimalDegreesParametersString();
            proj4Projection.ExternalProjectionParametersString = Proj4Projection.GetGoogleMapParametersString();
            proj4Projection.Open();

            BuildingOverlay buildingsOverlay = new BuildingOverlay();
            OsmBuildingOnlineFeatureSource osmBuildingOnlineFeatureSource = new OsmBuildingOnlineFeatureSource();
            osmBuildingOnlineFeatureSource.Projection = proj4Projection;
            osmBuildingOnlineFeatureSource.Open();
            buildingsOverlay.BuildingFeatureSource = osmBuildingOnlineFeatureSource;
            buildingsOverlay.TileType = TileType.SingleTile;
            map.Overlays.Add(buildingsOverlay);
            map.Refresh();
        }
    }
}
