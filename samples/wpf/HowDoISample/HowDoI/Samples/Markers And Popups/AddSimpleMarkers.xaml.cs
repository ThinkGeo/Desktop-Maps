using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class AddSimpleMarkers : UserControl
    {
        public AddSimpleMarkers()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-17336118, 20037508, 11623981, -16888303);

            ThinkGeoCloudRasterMapsOverlay ThinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudRasterMapsOverlay);

            SimpleMarkerOverlay markerOverlay = new SimpleMarkerOverlay();
            mapView.Overlays.Add("MarkerOverlay", markerOverlay);

            mapView.Refresh();
        }

        private void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            SimpleMarkerOverlay markerOverlay = (SimpleMarkerOverlay)mapView.Overlays["MarkerOverlay"];
            Marker marker = new Marker(e.WorldLocation);
            marker.ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute));
            marker.Width = 20;
            marker.Height = 34;
            marker.YOffset = -17;

            markerOverlay.Markers.Add(marker);
            markerOverlay.Refresh();
        }
    }
}