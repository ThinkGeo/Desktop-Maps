using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace MultipleDotDensityStyles
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
            //Sets the correct map unit and the extent of the map.
            wpfMap1.MapUnit = GeographyUnit.DecimalDegree;
            wpfMap1.CurrentExtent = new RectangleShape(-128,51,-65,19);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            //Adds the country shapefile for background.
            ShapeFileFeatureLayer countriesLayer = new ShapeFileFeatureLayer(@"..\..\Data\countries02.shp");
            countriesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.LightGray, GeoColor.StandardColors.Black);
            countriesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

           //Adds the countries shapefile for background and the DotDensityStyles.
            ShapeFileFeatureLayer statesLayer = new ShapeFileFeatureLayer(@"..\..\Data\USStates.shp");
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Adds the first style as a regular AreaStyle to the custom styles for the background of states.
            statesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new AreaStyle(new GeoPen(GeoColor.StandardColors.Black, 1), new GeoSolidBrush(GeoColor.StandardColors.LightGray)));

            int alpha = 180;
            //Adds the first DotDensityStyle to the custom styles.
            DotDensityStyle dotDensityStyle = new DotDensityStyle();
            dotDensityStyle.ColumnName = "AGE_UNDER5";
            // (1 / 100,000) = 0.00001. It means that every dot represents 100,000 persons under 5 years old.
            dotDensityStyle.PointToValueRatio = 0.00001;  
            dotDensityStyle.CustomPointStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(alpha, GeoColor.StandardColors.Green)), 6);
            statesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(dotDensityStyle);

            //Adds the first DotDensityStyle to the custom styles.
            DotDensityStyle dotDensityStyle1 = new DotDensityStyle();
            dotDensityStyle1.ColumnName = "AGE_5_17";
            // (1 / 100,000) = 0.00001. It means that every dot represents 100,000 persons between 5 and 17.
            dotDensityStyle1.PointToValueRatio = 0.00001; 
            dotDensityStyle1.CustomPointStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(alpha, GeoColor.StandardColors.Blue)), 6);
            statesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(dotDensityStyle1);

            //Adds the first DotDensityStyle to the custom styles.
            DotDensityStyle dotDensityStyle2 = new DotDensityStyle();
            dotDensityStyle2.ColumnName = "AGE_18_64";
            // (1 / 100,000) = 0.00001. It means that every dot represents 100,000 persons between 18 and 64.
            dotDensityStyle2.PointToValueRatio = 0.00001; 
            dotDensityStyle2.CustomPointStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(alpha, GeoColor.StandardColors.Red)), 6);
            statesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(dotDensityStyle2);

            //Adds the first DotDensityStyle to the custom styles.
            DotDensityStyle dotDensityStyle3 = new DotDensityStyle();
            dotDensityStyle3.ColumnName = "AGE_65_UP";
            // (1 / 100,000) = 0.00001. It means that every dot represents 100,000 persons more than 65 years old.
            dotDensityStyle3.PointToValueRatio = 0.00001; 
            dotDensityStyle3.CustomPointStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(alpha, GeoColor.StandardColors.Black)), 6);
            statesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(dotDensityStyle3);

            //Adds the ShapeFileFeatureLayer to an LayerOverlay.
            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(countriesLayer);
            layerOverlay.Layers.Add(statesLayer);
           
            wpfMap1.Overlays.Add(layerOverlay);

            wpfMap1.Refresh();
        }

       
        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);
            
            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.Width, (float)wpfMap1.Height);

            textBox1.Text = "X: " + DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(pointShape.X) + 
                          "  Y: " + DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(pointShape.Y);

           }
    }
}
