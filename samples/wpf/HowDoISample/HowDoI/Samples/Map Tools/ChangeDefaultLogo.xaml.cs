using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class ChangeDefaultLogo : UserControl
    {
        public ChangeDefaultLogo()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            mapView.CurrentExtent = new RectangleShape(-17336118, 20037508, 11623981, -16888303);
            mapView.MapTools.Logo.Source = new BitmapImage(new Uri("/Resources/ThinkGeoLogo.png", UriKind.RelativeOrAbsolute));

            ThinkGeoCloudRasterMapsOverlay worldOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(worldOverlay);

            mapView.Refresh();
        }
    }
}