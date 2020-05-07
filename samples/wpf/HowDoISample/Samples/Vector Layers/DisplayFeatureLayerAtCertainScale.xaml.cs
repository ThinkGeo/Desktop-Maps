using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class DisplayFeatureLayerAtCertainScale : UserControl
    {
        public DisplayFeatureLayerAtCertainScale()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-20037508, 17821912, -2226390, -2273031);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.Name = "Countries02";
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer statesLayer = new ShapeFileFeatureLayer(SampleHelper.Get("USStates_3857.shp"));
            statesLayer.Name = "USStates";
            statesLayer.ZoomLevelSet.ZoomLevel04.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 2);
            statesLayer.ZoomLevelSet.ZoomLevel04.DefaultAreaStyle.OutlinePen.LineJoin = DrawingLineJoin.Round;
            statesLayer.ZoomLevelSet.ZoomLevel04.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer citiesLayer = new ShapeFileFeatureLayer(SampleHelper.Get("MajorCities_3857.shp"));
            citiesLayer.Name = "MajorCities";
            citiesLayer.ZoomLevelSet.ZoomLevel06.DefaultPointStyle = PointStyle.CreateCompoundCircleStyle(GeoColors.White, 10F, GeoColors.Black, 1F, GeoColors.Black, 7F);
            citiesLayer.ZoomLevelSet.ZoomLevel06.DefaultTextStyle = TextStyle.CreateSimpleTextStyle("areaname", "Arial", 10, DrawingFontStyles.Regular, GeoColors.Black, 0, -8);
            citiesLayer.ZoomLevelSet.ZoomLevel06.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.ShallowOcean)));
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            worldOverlay.Layers.Add("StatesLayer", statesLayer);
            worldOverlay.Layers.Add("CitiesLayer", citiesLayer);
            mapView.Overlays.Add("WorldOverlay", worldOverlay);

            mapView.Refresh();
        }

        private void btnLow_Click(object sender, RoutedEventArgs e)
        {
            ZoomLevelSet zoomLevelSet = new ZoomLevelSet();
            mapView.CurrentExtent = MapUtil.ZoomToScale(zoomLevelSet.ZoomLevel06.Scale, mapView.CurrentExtent, GeographyUnit.Meter, (float)mapView.ActualWidth, (float)mapView.ActualHeight);
            mapView.Refresh();
        }

        private void btnNormal_Click(object sender, RoutedEventArgs e)
        {
            ZoomLevelSet zoomLevelSet = new ZoomLevelSet();
            mapView.CurrentExtent = MapUtil.ZoomToScale(zoomLevelSet.ZoomLevel05.Scale, mapView.CurrentExtent, GeographyUnit.Meter, (float)mapView.ActualWidth, (float)mapView.ActualHeight);
            mapView.Refresh();
        }

        private void btnHigh_Click(object sender, RoutedEventArgs e)
        {
            ZoomLevelSet zoomLevelSet = new ZoomLevelSet();
            mapView.CurrentExtent = MapUtil.ZoomToScale(zoomLevelSet.ZoomLevel03.Scale, mapView.CurrentExtent, GeographyUnit.Meter, (float)mapView.ActualWidth, (float)mapView.ActualHeight);
            mapView.Refresh();
        }
    }
}