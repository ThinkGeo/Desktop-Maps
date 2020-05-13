/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace DisplayMsSqlDatabase
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
            map.MapUnit = GeographyUnit.Meter;
            map.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay worldOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            map.Overlays.Add(worldOverlay);

            LayerOverlay layerOverlay = new LayerOverlay();
            string connectString = "Data Source=MsSqlServer;Initial Catalog=DatabaseName;Persist Security Info=True;User ID=username;Password=password";
            MsSqlFeatureLayer sqlLayer = new MsSqlFeatureLayer(connectString, "tableName", "featureId");
            sqlLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(new GeoImage(@"..\..\AppData\marker.png"));
            sqlLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(sqlLayer);
            map.Overlays.Add(layerOverlay);

            sqlLayer.Open();
            map.CurrentExtent = sqlLayer.GetBoundingBox();
            map.Refresh();
        }
    }
}
