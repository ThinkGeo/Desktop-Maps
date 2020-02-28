using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class PanZoomBar : UserControl
    {
        public PanZoomBar()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-17336118, 20037508, 11623981, -16888303);
            mapView.MapTools.PanZoomBar.IsEnabled = true;
            mapView.MapTools.PanZoomBar.DisplayZoomBarText = ZoomBarTextDisplayMode.Display;

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.DeepOcean)));
            worldOverlay.Layers.Add(worldLayer);
            mapView.Overlays.Add(worldOverlay);

            mapView.Refresh();
        }

        private void chbVisibility_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (mapView != null)
            {
                mapView.MapTools.PanZoomBar.IsEnabled = (bool)checkBox.IsChecked;
            }
        }

        private void chbTrackLevel_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (mapView != null)
            {
                mapView.MapTools.PanZoomBar.DisplayZoomBarText =
                    (bool)checkBox.IsChecked ? ZoomBarTextDisplayMode.Display : ZoomBarTextDisplayMode.None;
            }
        }
    }
}