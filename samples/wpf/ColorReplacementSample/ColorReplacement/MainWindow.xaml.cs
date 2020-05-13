/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace ColorReplacement
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the Map Unit.
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret") { MapType = ThinkGeoCloudRasterMapsMapType.Aerial };
            wpfMap1.Overlays.Add(baseOverlay);

            // Create a single-tile overlay to hold the lake image.
            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            wpfMap1.Overlays.Add("layerOverlay", layerOverlay);

            // Create a raster layer to load the lake image. 
            RectangleShape targetExtent = new RectangleShape(-11191114.7309915, 4674772.10425472, -11189708.4809915, 4673647.10425472);
            NativeImageRasterLayer rasterLayer = new NativeImageRasterLayer(@"..\..\App_Data\Lake.png", targetExtent);
            layerOverlay.Layers.Add("raster", rasterLayer);

            wpfMap1.CurrentExtent = targetExtent;
            wpfMap1.Refresh();
        }

        private void btnReplace_Click(object sender, RoutedEventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)wpfMap1.Overlays["layerOverlay"];
            NativeImageRasterLayer rasterLayer = (NativeImageRasterLayer)(layerOverlay.Layers["raster"]);

            // Set the ColorMappings. [Note: Before reuser, please clear it]
            rasterLayer.ColorMappings.Clear();
            rasterLayer.ColorMappings.Add(GeoColors.Blue, GeoColors.Green);
            rasterLayer.ColorMappings.Add(GeoColors.Green, GeoColors.Blue);

            rasterLayer.Transparency = 150;

            wpfMap1.Refresh();
        }
    }
}
