/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows;
using System.Windows.Media.Imaging;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace MapTouch
{
    public partial class MainWindow : Window
    {
        private SimpleMarkerOverlay markerOverlay;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.Meter;
            Map1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            Map1.Overlays.Add(baseOverlay);

            var overlay = new LayerOverlay();
            var shpLayer = new ShapeFileFeatureLayer(@"..\..\Data\cities_a.shp");
            shpLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.City1;
            shpLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            overlay.Layers.Add(shpLayer);
            Map1.Overlays.Add(overlay);

            markerOverlay = new SimpleMarkerOverlay();
            Map1.Overlays.Add(markerOverlay);

            shpLayer.Open();
            Map1.CurrentExtent = shpLayer.GetBoundingBox();
            shpLayer.Close();
            Map1.Refresh();
        }

        private void Map1_MapTap(object sender, MapTapWpfMapEventArgs e)
        {
            Marker marker = new Marker(e.WorldX, e.WorldY);
            marker.ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute));
            marker.Width = 20;
            marker.Height = 34;
            marker.YOffset = -17;

            markerOverlay.Markers.Add(marker);
            markerOverlay.Refresh();
        }
    }
}
