using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class DrawCurvedLabels : UserControl
    {
        public DrawCurvedLabels()
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
            austinStreetsLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            TextStyle textStyle = TextStyle.CreateSimpleTextStyle("FENAME", "Arial", 9, DrawingFontStyles.Regular, GeoColors.Black, 0, 0);
            textStyle.TextLineSegmentRatio = double.MaxValue;
            textStyle.SplineType = SplineType.StandardSplining;
            austinStreetsLabelLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);

            LayerOverlay austinStreetsOverlay = new LayerOverlay();
            austinStreetsOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214))));
            austinStreetsOverlay.Layers.Add("AustinStreetsShapeLayer", austinStreetsShapeLayer);
            austinStreetsOverlay.Layers.Add("AustinStreetsLabelLayer", austinStreetsLabelLayer);
            mapView.Overlays.Add("AustinStreetsOverlay", austinStreetsOverlay);

            mapView.CurrentExtent = new RectangleShape(-10874598, 3544465, -10872831, 3543004);

            mapView.Refresh();
        }
    }
}