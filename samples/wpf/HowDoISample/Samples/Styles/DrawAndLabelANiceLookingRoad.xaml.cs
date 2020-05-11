using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class DrawAndLabelANiceLookingRoad : UserControl
    {
        public DrawAndLabelANiceLookingRoad()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            ShapeFileFeatureLayer austinStreetsShapeLayer = new ShapeFileFeatureLayer(SampleHelper.Get("austinstreets_3857.shp"));
            austinStreetsShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            austinStreetsShapeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(LineStyle.CreateSimpleLineStyle(GeoColors.White, 9.2F, GeoColors.DarkGray, 12.2F, true));

            ShapeFileFeatureLayer austinStreetsLabelLayer = new ShapeFileFeatureLayer(SampleHelper.Get("austinstreets_3857.shp"));
            austinStreetsLabelLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(TextStyle.CreateSimpleTextStyle("FENAME", "Arial", 9, DrawingFontStyles.Regular, GeoColors.Black, 0, 0));
            austinStreetsLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214))));
            staticOverlay.Layers.Add("AustinStreetsShapeLayer", austinStreetsShapeLayer);
            staticOverlay.Layers.Add("AustinStreetsLabelLayer", austinStreetsLabelLayer);
            mapView.Overlays.Add(staticOverlay);

            mapView.CurrentExtent = new RectangleShape(-10881385, 3542247, -10880501, 3541517);

            mapView.Refresh();
        }
    }
}