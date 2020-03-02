using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class DisplayASimpleMap : UserControl
    {
        public DisplayASimpleMap()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.DecimalDegree;
            LayerOverlay utahWaterOverlay = new LayerOverlay() { TileType = TileType.MultiTile };
            utahWaterOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214))));

            ShapeFileFeatureLayer utahWaterShapeLayer = new ShapeFileFeatureLayer(SampleHelper.Get("UtahWater.shp"));
            utahWaterShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 136, 162, 227));
            utahWaterShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            utahWaterOverlay.Layers.Add("UtahWaterShapes", utahWaterShapeLayer);
            Map1.Overlays.Add("UtahWaterOverlay", utahWaterOverlay);

            Map1.CurrentExtent = new RectangleShape(-113.11473388671875, 41.949816894531253, -111.08226318359375, 40.499621582031253);

            Map1.Refresh();
        }
    }
}