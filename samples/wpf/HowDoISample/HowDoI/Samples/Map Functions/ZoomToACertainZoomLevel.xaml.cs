using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class ZoomToACertainZoomLevel : UserControl
    {
        public ZoomToACertainZoomLevel()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-610949, 15475775, 4818826, 5637066);

            ThinkGeoCloudRasterMapsOverlay worldMapKitDesktopOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(worldMapKitDesktopOverlay);

            mapView.Refresh();
        }

        private void cmbZoomLevel_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                double scale = GetScaleFromZoomLevelSet(cmbZoomLevel.SelectedIndex);
                mapView.ZoomToScale(scale);
            }
        }

        private static double GetScaleFromZoomLevelSet(int comboboxIndex)
        {
            double scale = 0;
            ZoomLevelSet zoomLevelSet = new ZoomLevelSet();

            switch (comboboxIndex)
            {
                case 0:
                    scale = zoomLevelSet.ZoomLevel10.Scale;
                    break;

                case 1:
                    scale = zoomLevelSet.ZoomLevel09.Scale;
                    break;

                case 2:
                    scale = zoomLevelSet.ZoomLevel08.Scale;
                    break;

                case 3:
                    scale = zoomLevelSet.ZoomLevel07.Scale;
                    break;

                case 4:
                    scale = zoomLevelSet.ZoomLevel06.Scale;
                    break;

                case 5:
                    scale = zoomLevelSet.ZoomLevel05.Scale;
                    break;

                case 6:
                    scale = zoomLevelSet.ZoomLevel04.Scale;
                    break;

                case 7:
                    scale = zoomLevelSet.ZoomLevel03.Scale;
                    break;

                case 8:
                    scale = zoomLevelSet.ZoomLevel02.Scale;
                    break;

                default:
                    break;
            }

            return scale;
        }
    }
}