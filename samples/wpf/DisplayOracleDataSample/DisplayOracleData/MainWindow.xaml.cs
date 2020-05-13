using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;
using ThinkGeo.MapSuite.Drawing;

namespace MapSuite_Desktop_for_WPF_App1
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

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            map.MapUnit = GeographyUnit.DecimalDegree;

            // Please update the connecting string, table, feature by your data.
            var oracleFeatureLayer = new OracleFeatureLayer("User ID=system;Password=tg2017;Data Source=192.168.0.101/orcl;", "AUSTINSTREETS1", "ID");
            oracleFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(new GeoSolidBrush(GeoColor.SimpleColors.Copper), 2.0f));
            oracleFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            oracleFeatureLayer.Open();
            map.CurrentExtent = oracleFeatureLayer.GetBoundingBox();
            oracleFeatureLayer.Close();

            LayerOverlay overlay = new LayerOverlay();
            overlay.Layers.Add(oracleFeatureLayer);

            map.Overlays.Add(overlay);
            map.Refresh();
        }
    }
}
