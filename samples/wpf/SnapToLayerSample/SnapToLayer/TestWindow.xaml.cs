using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace SnapToLayer
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
            wpfMap1.MapUnit = GeographyUnit.Feet;
            wpfMap1.CurrentExtent = new RectangleShape(2468268,7111899,2469766,7110811);
            BackgroundOverlay backGroundOverlay = new BackgroundOverlay();
            backGroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.StandardColors.LightGoldenrodYellow);
            wpfMap1.BackgroundOverlay = backGroundOverlay;

            //Backgrounbd layer for streets.
            ShapeFileFeatureLayer shapefileFeatureLayer1 = new ShapeFileFeatureLayer(@"../../Data/Streets.shp");
            shapefileFeatureLayer1.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.LocalRoad3;
            shapefileFeatureLayer1.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.LocalRoad3("ROAD_NAME");
            shapefileFeatureLayer1.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("Streets", shapefileFeatureLayer1);

            wpfMap1.Overlays.Add(layerOverlay);


            //inMemoryFeatureLayer used to be snapped to.
            InMemoryFeatureLayer polygonInMemoryFeatureLayer = new InMemoryFeatureLayer();
            polygonInMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Park1;
            polygonInMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle
                (GeoColor.StandardColors.Transparent, 25, GeoColor.StandardColors.Black);
            polygonInMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            InMemoryFeatureLayer pointInMemoryFeatureLayer = new InMemoryFeatureLayer();
            TolerancePointStyle tolerancePointStyle = new TolerancePointStyle();
            tolerancePointStyle.Tolerance = 25;
            tolerancePointStyle.ToleranceType = ToleranceCoordinates.Screen;

            pointInMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(tolerancePointStyle);
            pointInMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureSource shapeFileFeatureSource = new ShapeFileFeatureSource(@"..\..\data\Parks.shp");
            shapeFileFeatureSource.Open();
            Collection<Feature> features = shapeFileFeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns);

            foreach (Feature feature in features)
            {
                polygonInMemoryFeatureLayer.InternalFeatures.Add(feature);
                MultipolygonShape multipolygonShape = (MultipolygonShape)feature.GetShape();
                foreach (PolygonShape polygonShape in multipolygonShape.Polygons)
                {
                    foreach (Vertex vertex in polygonShape.OuterRing.Vertices)
                    {
                        pointInMemoryFeatureLayer.InternalFeatures.Add(new Feature(new PointShape(vertex)));
                    }
                }
            }
            shapeFileFeatureSource.Close();

            LayerOverlay inMemoryOverlay = new LayerOverlay();
            inMemoryOverlay.Layers.Add("InMemoryFeatureLayer", polygonInMemoryFeatureLayer);
            inMemoryOverlay.Layers.Add("PointInMemoryFeatureLayer", pointInMemoryFeatureLayer);
            wpfMap1.Overlays.Add("InMemoryOverlay", inMemoryOverlay);

            //SnapToLayerEditInteractiveOverlay to snap dragged control point to nearest vertex of layer if within tolerance.
            SnapToLayerEditInteractiveOverlay snapToLayerEditInteractiveOverlay = new SnapToLayerEditInteractiveOverlay();
           

            LineShape lineShape = new LineShape();
            lineShape.Vertices.Add(new Vertex(2468665, 7111734));
            lineShape.Vertices.Add(new Vertex(2468560, 7111376));
            lineShape.Vertices.Add(new Vertex(2468569, 7110947));
            snapToLayerEditInteractiveOverlay.EditShapesLayer.InternalFeatures.Add("MultiLine", new Feature(lineShape));

            
            //Sets the PointStyle for the non dragged control points.
            snapToLayerEditInteractiveOverlay.ControlPointStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.StandardColors.PaleGoldenrod), new GeoPen(GeoColor.StandardColors.Black), 8);
            //Sets the PointStyle for the dragged control points.
            snapToLayerEditInteractiveOverlay.DraggedControlPointStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.StandardColors.Red), new GeoPen(GeoColor.StandardColors.Orange, 2), 10);

            snapToLayerEditInteractiveOverlay.ToSnapInMemoryFeatureLayer = polygonInMemoryFeatureLayer;

            //Example using Screen (Pixel) coordinates for tolerance.
            snapToLayerEditInteractiveOverlay.ToleranceType = ToleranceCoordinates.Screen;
            snapToLayerEditInteractiveOverlay.Tolerance = 25;

            //Example using World coordinates for tolerance.
            //snapToLayerEditInteractiveOverlay.ToleranceType = ToleranceCoordinates.World;
            //snapToLayerEditInteractiveOverlay.Tolerance = 150;
            //snapToLayerEditInteractiveOverlay.ToleranceUnit = DistanceUnit.Meter;

            snapToLayerEditInteractiveOverlay.CalculateAllControlPoints();

            wpfMap1.EditOverlay = snapToLayerEditInteractiveOverlay;

            wpfMap1.TrackOverlay.TrackMode = TrackMode.None;
          
            wpfMap1.Refresh();
        }

       
        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);
            
            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.Width, (float)wpfMap1.Height);

            textBox1.Text = "X: " + Math.Round(pointShape.X,2) + 
                          "  Y: " + Math.Round(pointShape.Y,2);

           }
    }
}
