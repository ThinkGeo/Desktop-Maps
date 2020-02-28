using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class LoadAHeatLayer : UserControl
    {
        public LoadAHeatLayer()
        {
            InitializeComponent();
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.MapTools.MouseCoordinate.IsEnabled = true;
            mapView.CurrentExtent = new RectangleShape(-14070784, 6240993, -7458406, 2154936);
            mapView.MaximumScale = 36911986.875;

            ThinkGeoCloudRasterMapsOverlay ThinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(SampleHelper.Get("cities_e_3857.shp"));
            shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new HeatStyle(10, 75, DistanceUnit.Kilometer));
            shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(shapeFileFeatureLayer);
            layerOverlay.OverlayCanvas.Opacity = .8f;
            mapView.Overlays.Add(layerOverlay);

            mapView.Refresh();
        }
    }
}