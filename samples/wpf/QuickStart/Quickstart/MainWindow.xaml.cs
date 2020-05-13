using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace Quickstart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Map1_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.DecimalDegree;

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(@"../../Data/Countries02.shp");
            AreaStyle areaStyle = new AreaStyle();
            areaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214));
            areaStyle.OutlinePen = new GeoPen(GeoColor.FromArgb(255, 118, 138, 69), 1);
            areaStyle.OutlinePen.DashStyle = LineDashStyle.Solid;

            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = areaStyle;
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            PointStyle pointStyle = new PointStyle();
            pointStyle.SymbolType = PointSymbolType.Square;
            pointStyle.FillBrush = new GeoSolidBrush(GeoColors.White);
            pointStyle.OutlinePen = new GeoPen(GeoColors.Black, 1);
            pointStyle.SymbolSize = 6;

            PointStyle stackStyle = new PointStyle();
            stackStyle.SymbolType = PointSymbolType.Square;
            stackStyle.FillBrush = new GeoSolidBrush(GeoColors.Maroon);
            stackStyle.OutlinePen = new GeoPen(GeoColors.Transparent, 0);
            stackStyle.SymbolSize = 2;

            pointStyle.CustomPointStyles.Add(stackStyle);
            ShapeFileFeatureLayer capitalLayer = new ShapeFileFeatureLayer(@"../../Data/WorldCapitals.shp");
            // We can customize our own Style. Here we passed in a color and a size.
            capitalLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.White, 7, GeoColors.Brown);
            // The Style we set here is available from ZoomLevel01 to ZoomLevel05. That means if we zoom in a bit more, the appearance we set here will not be visible anymore.
            capitalLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;


            capitalLayer.ZoomLevelSet.ZoomLevel06.DefaultPointStyle = pointStyle;
            // The Style we set here is available from ZoomLevel06 to ZoomLevel20. That means if we zoom out a bit more, the appearance we set here will not be visible any more.
            capitalLayer.ZoomLevelSet.ZoomLevel06.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer capitalLabelLayer = new ShapeFileFeatureLayer(@"../../Data/WorldCapitals.shp");
            // We can customize our own TextStyle. Here we passed in the font, the size, the style and the color.
            GeoFont font = new GeoFont("Arial", 9, DrawingFontStyles.Bold);
            GeoSolidBrush txtBrush = new GeoSolidBrush(GeoColors.Maroon);
            TextStyle textStyle = new TextStyle("CITY_NAME", font, txtBrush);
            textStyle.XOffsetInPixel = 0;
            textStyle.YOffsetInPixel = -6;
            capitalLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyle.CreateSimpleTextStyle("CITY_NAME", "Arial", 8, DrawingFontStyles.Italic, GeoColors.Black, 3, 3);
            // The TextStyle we set here is available from ZoomLevel01 to ZoomLevel05. 
            capitalLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level05;

            capitalLabelLayer.ZoomLevelSet.ZoomLevel06.DefaultTextStyle = textStyle;
            // The TextStyle we set here is available from ZoomLevel06 to ZoomLevel20. 
            capitalLabelLayer.ZoomLevelSet.ZoomLevel06.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.ShallowOcean)));
            layerOverlay.Layers.Add(worldLayer);
            layerOverlay.Layers.Add(capitalLayer);
            layerOverlay.Layers.Add(capitalLabelLayer);

            Map1.Overlays.Add(layerOverlay);
            Map1.CurrentExtent = new RectangleShape(-136.60, 60.06, -53.81, 11.63);
            Map1.Refresh();
        }
    }
}
