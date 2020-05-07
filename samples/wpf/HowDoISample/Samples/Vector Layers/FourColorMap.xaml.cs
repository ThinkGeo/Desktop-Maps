using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for FourColorMap.xaml
    /// </summary>
    public partial class FourColorMap : UserControl
    {
        private ShapeFileFeatureLayer shapeFileLayer;
        private InMemoryFeatureLayer fourColorLayer;

        public FourColorMap()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            LayerOverlay layerOverlay = new LayerOverlay();
            shapeFileLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            shapeFileLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColors.Black, 1), new GeoSolidBrush(GeoColors.LightSkyBlue));
            shapeFileLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            shapeFileLayer.Open();
            layerOverlay.Layers.Add(shapeFileLayer);

            fourColorLayer = new InMemoryFeatureLayer();
            ValueStyle valueStyle = new ValueStyle { ColumnName = "Color" };
            valueStyle.ValueItems.Add(new ValueItem("1", AreaStyle.CreateSimpleAreaStyle(GeoColors.LightGreen)));
            valueStyle.ValueItems.Add(new ValueItem("2", AreaStyle.CreateSimpleAreaStyle(GeoColors.LightSkyBlue)));
            valueStyle.ValueItems.Add(new ValueItem("3", AreaStyle.CreateSimpleAreaStyle(GeoColors.Yellow)));
            valueStyle.ValueItems.Add(new ValueItem("4", AreaStyle.CreateSimpleAreaStyle(GeoColors.LightPink)));
            fourColorLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);
            fourColorLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(fourColorLayer);

            mapView.Overlays.Add(layerOverlay);

            mapView.CurrentExtent = new RectangleShape(-174, 117, 180, -120);
            mapView.Refresh();
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
                        var fourColorFeatures = MapUtil.BuildFourColorColumn("Color", features);
                        foreach (Feature feature in fourColorFeatures)
                        {
                            fourColorLayer.InternalFeatures.Add(feature);
                        }
                        Dispatcher.Invoke(() =>
                        {
                            progressPanel.Visibility = Visibility.Hidden;
                            mapView.Refresh();
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

            mapView.Refresh();
        }
    }
}
