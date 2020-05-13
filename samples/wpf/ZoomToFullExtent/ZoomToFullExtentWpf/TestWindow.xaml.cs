using System.Collections.ObjectModel;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace ZoomToFullExtentWpf
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            wpfMap1.MapUnit = GeographyUnit.DecimalDegree;
            wpfMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            //Adds countries and capitals shapefiles.
            ShapeFileFeatureLayer Layer1 = new ShapeFileFeatureLayer(@"..\..\Data\countries02.shp");
            Layer1.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.LightGreen, GeoColor.StandardColors.Black);
            Layer1.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer Layer2 = new ShapeFileFeatureLayer(@"..\..\Data\worldcapitals.shp");
            Layer2.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateCompoundPointStyle(PointSymbolType.Square, GeoColor.StandardColors.White, GeoColor.StandardColors.Black, 1F, 10F, PointSymbolType.Square, GeoColor.StandardColors.Navy, GeoColor.StandardColors.Transparent, 0F, 6F);
            Layer2.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("Layer1", Layer1);
            layerOverlay.Layers.Add("Layer2", Layer2);

            wpfMap1.Overlays.Add("Layers", layerOverlay);

            wpfMap1.CurrentExtent = GetFullExtent(layerOverlay.Layers);

            wpfMap1.Refresh();
        }

        //Function for getting the extent based on a collection of layers.
        //It gets the overall extent of all the layers.
        private RectangleShape GetFullExtent(GeoCollection<Layer> Layers)
        {
            Collection<BaseShape> rectangleShapes = new Collection<BaseShape>();

            foreach (Layer layer in Layers)
            {
                layer.Open();
                if (layer.HasBoundingBox == true)
                    rectangleShapes.Add(layer.GetBoundingBox());
            }
            return ExtentHelper.GetBoundingBoxOfItems(rectangleShapes);
        }
    }
}
