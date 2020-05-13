using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace FourColorMap
{
    public partial class MainWindow : Window
    {
        private ShapeFileFeatureLayer shapeFileLayer;
        private InMemoryFeatureLayer fourColorLayer;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadMap(object sender, RoutedEventArgs e)
        {
            map.MapUnit = GeographyUnit.DecimalDegree;

            LayerOverlay layerOverlay = new LayerOverlay();

            shapeFileLayer = new ShapeFileFeatureLayer("../../AppData/cntry02.shp");
            shapeFileLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColors.Black, 1), new GeoSolidBrush(GeoColors.LightSkyBlue));
            shapeFileLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(shapeFileLayer);

            fourColorLayer = new InMemoryFeatureLayer();
            ValueStyle valueStyle = new ValueStyle { ColumnName = "Color" };
            valueStyle.ValueItems.Add(new ValueItem("1", AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.LightGreen)));
            valueStyle.ValueItems.Add(new ValueItem("2", AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.LightSkyBlue)));
            valueStyle.ValueItems.Add(new ValueItem("3", AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.Yellow)));
            valueStyle.ValueItems.Add(new ValueItem("4", AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.LightPink)));
            fourColorLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);
            fourColorLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(fourColorLayer);

            map.Overlays.Add(layerOverlay);
            map.CurrentExtent = new RectangleShape(-174, 117, 180, -120);
            map.Refresh();
        }

        private void FourColorMapCheckBoxClick(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            if (checkBox.IsChecked == true)
            {
                if (fourColorLayer.InternalFeatures.Count == 0)
                {
                    progressPanel.Visibility = Visibility.Visible;
                    Task.Factory.StartNew(() =>
                     {
                         var features = shapeFileLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);
                         var fourColorFeatures = ExtendedHelper.BuildFourColorColumn("Color", features);
                         foreach (Feature feature in fourColorFeatures)
                         {
                             fourColorLayer.InternalFeatures.Add(feature);
                         }
                         Dispatcher.Invoke(() =>
                         {
                             progressPanel.Visibility = Visibility.Hidden;
                             map.Refresh();
                         });
                     });
                }
                shapeFileLayer.IsVisible = false;
                fourColorLayer.IsVisible = true;
            }
            else
            {
                shapeFileLayer.IsVisible = true;
                fourColorLayer.IsVisible = false;
            }

            map.Refresh();
        }
    }
}
