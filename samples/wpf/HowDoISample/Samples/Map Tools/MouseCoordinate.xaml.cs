using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class MouseCoordinate : UserControl
    {
        public MouseCoordinate()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-17336118, 20037508, 11623981, -16888303);
            mapView.MapTools.MouseCoordinate.IsEnabled = true;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            mapView.Refresh();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            mapView.MapTools.MouseCoordinate.CustomFormatted -= new System.EventHandler<CustomFormattedMouseCoordinateMapToolEventArgs>(MouseCoordinate_CustomMouseCoordinateFormat);
            RadioButton button = (RadioButton)sender;
            switch (button.Tag.ToString())
            {
                case "0":
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.LongitudeLatitude;
                    break;

                case "1":
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.LatitudeLongitude;
                    break;

                case "2":
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.DegreesMinutesSeconds;
                    break;

                case "3":
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.Custom;
                    mapView.MapTools.MouseCoordinate.CustomFormatted += new System.EventHandler<CustomFormattedMouseCoordinateMapToolEventArgs>(MouseCoordinate_CustomMouseCoordinateFormat);
                    break;
            }
        }

        private void MouseCoordinate_CustomMouseCoordinateFormat(object sender, CustomFormattedMouseCoordinateMapToolEventArgs e)
        {
            ((MouseCoordinateMapTool)sender).Foreground = new SolidColorBrush(Colors.Red);
            e.Result = string.Format(CultureInfo.InvariantCulture, "{0},{1}", e.WorldCoordinate.X.ToString("N0"), e.WorldCoordinate.Y.ToString("N0"));
        }
    }
}