using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace Quickstart
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
            // This ThinkGeo Cloud test key is exclusively for demonstration purposes and is limited to requesting base map 
            // tiles only. Do not use it in your own applications, as it may be restricted or disabled without prior notice. 
            // Please visit https://cloud.thinkgeo.com to create a free key for your own use.
            ThinkGeoCloudRasterMapsOverlay thinkGeoOverlay = new ThinkGeoCloudRasterMapsOverlay("USlbIyO5uIMja2y0qoM21RRM6NBXUad4hjK3NBD6pD0~", "f6OJsvCDDzmccnevX55nL7nXpPDXXKANe5cN6czVjCH0s8jhpCH-2A~~", ThinkGeoCloudRasterMapsMapType.Light);
            Map1.Overlays.Add(thinkGeoOverlay);

            Map1.MapUnit = GeographyUnit.Meter;
            Map1.MinimumScale = 20;
            Map1.ZoomLevelSet = GetCustomZoomLevelSet();
            Map1.CurrentExtent = new RectangleShape(-100000000, 100000000, 100000000, -100000000);
        }

        public static ZoomLevelSet GetCustomZoomLevelSet()
        {
            ThinkGeoCloudMapsZoomLevelSet thinkGeoCloudMapsZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            ThinkGeoCloudMapsZoomLevelSet customZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            customZoomLevelSet.CustomZoomLevels.Clear();

            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[3].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[4].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[5].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[6].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[7].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[8].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[9].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[10].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[11].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[12].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[13].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[14].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[15].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[16].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[17].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[18].Scale));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[19].Scale));

            double zoomlevel17Scale = thinkGeoCloudMapsZoomLevelSet.CustomZoomLevels[19].Scale;
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(zoomlevel17Scale / 2));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(zoomlevel17Scale / 4));
            customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(zoomlevel17Scale / 8));

            return customZoomLevelSet;
        }
    }
}
