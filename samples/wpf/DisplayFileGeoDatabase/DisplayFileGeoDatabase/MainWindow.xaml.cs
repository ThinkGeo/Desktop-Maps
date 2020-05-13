using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace DisplayFileGeoDatabase
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            map.MapUnit = GeographyUnit.DecimalDegree;

            LayerOverlay layerOverlay = new LayerOverlay();

            FileGeoDatabaseFeatureLayer fileGeoDatabaseFeatureLayer = new FileGeoDatabaseFeatureLayer("../../AppData/Shapes.gdb", "states");
            fileGeoDatabaseFeatureLayer.Open();
            AreaStyle areaStyle = new AreaStyle();
            areaStyle.FillSolidBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214));
            areaStyle.OutlinePen = new GeoPen(GeoColor.FromArgb(255, 118, 138, 69), 1);
            areaStyle.OutlinePen.DashStyle = LineDashStyle.Solid;
            fileGeoDatabaseFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = areaStyle;
            fileGeoDatabaseFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(fileGeoDatabaseFeatureLayer);
            map.Overlays.Add(layerOverlay);

            map.CurrentExtent = fileGeoDatabaseFeatureLayer.GetBoundingBox();
            map.Refresh();
        }
    }
}
