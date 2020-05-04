using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for DisplayAShapeFileSimpleMap.xaml
    /// </summary>
    public partial class DisplayAShapeFileSimpleMap : UserControl
    {
        public DisplayAShapeFileSimpleMap()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.Meter;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            Map1.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            //LayerOverlay utahWaterOverlay = new LayerOverlay() { TileType = TileType.MultiTile };
            //utahWaterOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214))));

            //ShapeFileFeatureLayer utahWaterShapeLayer = new ShapeFileFeatureLayer(SampleHelper.Get("UtahWater.shp"));
            //utahWaterShapeLayer.ZoomLevelSet.Load(SampleHelper.Get("utahwater.json"));

            //utahWaterOverlay.Layers.Add("UtahWaterShapes", utahWaterShapeLayer);
            //Map1.Overlays.Add("UtahWaterOverlay", utahWaterOverlay);

            //Map1.CurrentExtent = new RectangleShape(-113.11473388671875, 41.949816894531253, -111.08226318359375, 40.499621582031253);

            Map1.Refresh();
        }
    }
}
