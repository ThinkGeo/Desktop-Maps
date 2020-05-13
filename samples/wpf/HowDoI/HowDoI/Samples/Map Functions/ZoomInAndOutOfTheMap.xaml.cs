using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for ZoomInAndOutOfTheMap.xaml
    /// </summary>
    public partial class ZoomInAndOutOfTheMap : UserControl
    {
        public ZoomInAndOutOfTheMap()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-14833496, 20037508, 14126965, -20037508);

            ThinkGeoCloudRasterMapsOverlay worldMapKitDesktopOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            //worldMapKitDesktopOverlay.TileType = TileType.SingleTile;
            mapView.Overlays.Add("WorldMapKitWmsOverlay", worldMapKitDesktopOverlay);

            mapView.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Tag.ToString())
            {
                case "Zoom In":
                    mapView.ZoomIn();
                    break;

                case "Zoom Out":
                    mapView.ZoomOut();
                    break;
            }
        }
    }
}