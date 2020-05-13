using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class DrawAndLabelWaterFeatures : UserControl
    {
        public DrawAndLabelWaterFeatures()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            ShapeFileFeatureLayer utahWaterShapeLayer = new ShapeFileFeatureLayer(SampleHelper.Get("UtahWater_3857.shp"));
            utahWaterShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 136, 162, 227));
            utahWaterShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer utahWaterLabelLayer = new ShapeFileFeatureLayer(SampleHelper.Get("UtahWater_3857.shp"));
            utahWaterLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyle.CreateSimpleTextStyle("Landname", "Arial", 6, DrawingFontStyles.Italic, GeoColors.Navy);
            utahWaterLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay utahWaterOverlay = new LayerOverlay();
            utahWaterOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214))));
            utahWaterOverlay.Layers.Add("UtahWaterShapes", utahWaterShapeLayer);
            utahWaterOverlay.Layers.Add("UtahWaterLabels", utahWaterLabelLayer);
            mapView.Overlays.Add("UtahWaterOverlay", utahWaterOverlay);

            mapView.CurrentExtent = new RectangleShape(-12591875, 5153465, -12365621, 4938814);

            mapView.Refresh();
        }
    }
}