using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace ClassBreakStyleSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClassBreakStyle statesStyle;
        private ClassBreakStyle citiesStyle;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize the ThinkGeo UI map control and set the initial view 
            // to the continental United States.
            wpfMap1.MapUnit = GeographyUnit.DecimalDegree;
            wpfMap1.CurrentExtent = new RectangleShape(-140.712890625, 62.490234375, -54.228515625, 10.72265625);

            // Create a LayerOverlay for our two layers (U.S. states and major cities).
            var layerOverlay = new LayerOverlay();
            layerOverlay.DrawingQuality = DrawingQuality.HighQuality;
            wpfMap1.Overlays.Add(layerOverlay);

            // Create a ShapeFileFeatureLayer for the states of the U.S.
            var statesLayer = new ShapeFileFeatureLayer(@"..\..\App_Data\USStates.shp");
            layerOverlay.Layers.Add(statesLayer);

            // Create a ShapeFileFeatureLayer for the major U.S. cities.
            var citiesLayer = new ShapeFileFeatureLayer(@"..\..\App_Data\MajorCities.shp");
            layerOverlay.Layers.Add(citiesLayer);

            // Use a ClassBreakStyle to colorize each state differently depending on 
            // the range into which its population falls.  This value is found in the 
            // states ShapeFile DBF in the column named "POP1990".
            statesStyle = new ClassBreakStyle("POP1990");
            statesStyle.ClassBreaks.Add(
                new ClassBreak(value: 0, areaStyle: AreaStyles.CreateSimpleAreaStyle(
                    fillBrushColor: GeoColors.LightGray,
                    outlinePenColor: GeoColors.DarkGray,
                    outlinePenWidth: 1)
                )
            );
            statesStyle.ClassBreaks.Add(
                new ClassBreak(value: 1000000, areaStyle: AreaStyles.CreateSimpleAreaStyle(
                    fillBrushColor: GeoColors.LightBlue,
                    outlinePenColor: GeoColors.CornflowerBlue,
                    outlinePenWidth: 1)
                )
            );
            statesStyle.ClassBreaks.Add(
                new ClassBreak(value: 3500000, areaStyle: AreaStyles.CreateSimpleAreaStyle(
                    fillBrushColor: GeoColors.SkyBlue,
                    outlinePenColor: GeoColors.DeepSkyBlue,
                    outlinePenWidth: 1)
                )
            );
            // Add stateStyle to the statesLayer and apply the style to all zoom levels.
            statesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(statesStyle);
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            
            // Use a ClassBreakStyle to give each major city a different symbol and color
            // depending on the range into which its population falls.  This value is found 
            // in the major cities ShapeFile DBF in the column named "POP2000".
            citiesStyle = new ClassBreakStyle("POP2000");
            citiesStyle.ClassBreaks.Add(
                new ClassBreak(value: 0, pointStyle: PointStyles.CreateSimpleSquareStyle(
                    fillColor: GeoColors.White,
                    size: 6,
                    outlineColor: GeoColors.Black,
                    outlineWidth: 1)
                )
            );
            citiesStyle.ClassBreaks.Add(
                new ClassBreak(value: 550000, pointStyle: PointStyles.CreateSimpleCircleStyle(
                    fillColor: GeoColors.LightOrange,
                    size: 8,
                    outlineColor: GeoColors.Black,
                    outlineWidth: 1)
                )
            );
            citiesStyle.ClassBreaks.Add(
                new ClassBreak(value: 1200000, pointStyle: PointStyles.CreateSimpleStarStyle(
                    fillColor: GeoColors.Yellow,
                    size: 18,
                    outlineColor: GeoColors.Black,
                    outlineWidth: 3)
                )
            );
            // Add citiesStyle to the citiesLayer and apply the style to all zoom levels.
            citiesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(citiesStyle);
            citiesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }
    }
}