/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace SelectAndDragFeature
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
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap1.CurrentExtent = new RectangleShape(-4452779, 2273030, 4452779, -2273030);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            PolygonShape polygon1 = new PolygonShape("POLYGON((-2226389 1118889,-1113194 1118889,-1113194 0,-2226389 0,-2226389 1118889))");
            PolygonShape polygon2 = new PolygonShape("POLYGON((1113194 1118889,2226389 1118889,2226389 0,1113194 0,1113194 1118889))");

            wpfMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Add(new Feature(polygon1) { Id = "polygon1" });
            wpfMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Add(new Feature(polygon2) { Id = "polygon2" });
            wpfMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Add(new Feature(getLineshape(polygon1, polygon2)) { Id = "lineshape" });
            wpfMap1.TrackOverlay.TrackShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new MyLineStyle(new GeoPen(GeoColor.SimpleColors.Blue), new GeoPen(), new GeoPen());

            wpfMap1.EditOverlay.EditShapesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new MyLineStyle(new GeoPen(GeoColor.SimpleColors.Blue), new GeoPen(), new GeoPen());
            wpfMap1.EditOverlay.FeatureDragged += new System.EventHandler<FeatureDraggedEditInteractiveOverlayEventArgs>(EditOverlay_FeatureDragged);

            wpfMap1.Refresh();
        }


        void EditOverlay_FeatureDragged(object sender, FeatureDraggedEditInteractiveOverlayEventArgs e)
        {
            wpfMap1.EditOverlay.EditShapesLayer.InternalFeatures.Remove("lineshape");
            wpfMap1.EditOverlay.EditShapesLayer.InternalFeatures.Add("lineshape", new Feature(getLineshape((wpfMap1.EditOverlay.EditShapesLayer.InternalFeatures[0].GetShape() as PolygonShape), (wpfMap1.EditOverlay.EditShapesLayer.InternalFeatures[1].GetShape() as PolygonShape))) { Id = "lineshape" });
        }


        private LineShape getLineshape(PolygonShape polygon1, PolygonShape polygon2)
        {
            LineShape lineToReturn = new LineShape();
            RectangleShape polygon1BoundingBox = polygon1.GetBoundingBox();
            RectangleShape polygon2BoundingBox = polygon2.GetBoundingBox();

            Vertex vertex1 = new Vertex(polygon1BoundingBox.LowerRightPoint.X, (polygon1BoundingBox.UpperRightPoint.Y + polygon1BoundingBox.LowerRightPoint.Y) / 2);
            Vertex vertex2 = new Vertex(polygon2BoundingBox.UpperLeftPoint.X, (polygon2BoundingBox.UpperLeftPoint.Y + polygon2BoundingBox.LowerLeftPoint.Y) / 2);

            lineToReturn.Vertices.Add(vertex1);
            lineToReturn.Vertices.Add(vertex2);

            return lineToReturn;
        }

        private void BtnEditMode_Click(object sender, RoutedEventArgs e)
        {
            wpfMap1.TrackOverlay.TrackMode = TrackMode.None;
            foreach (Feature feature in wpfMap1.TrackOverlay.TrackShapeLayer.InternalFeatures)
            {
                wpfMap1.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
            }

            wpfMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            wpfMap1.EditOverlay.CalculateAllControlPoints();
            wpfMap1.Refresh(new Overlay[] { wpfMap1.EditOverlay, wpfMap1.TrackOverlay });
        }

        private void BtnFinish_Click(object sender, RoutedEventArgs e)
        {
            foreach (Feature feature in wpfMap1.EditOverlay.EditShapesLayer.InternalFeatures)
            { wpfMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Add(feature.Id, feature); }

            wpfMap1.EditOverlay.EditShapesLayer.InternalFeatures.Clear();
            wpfMap1.Refresh(new Overlay[] { wpfMap1.EditOverlay, wpfMap1.TrackOverlay });
        }

        class MyLineStyle : LineStyle
        {

            public MyLineStyle(GeoPen outerPen, GeoPen innerPen, GeoPen centerPen)
                : base(outerPen, innerPen, centerPen)
            { }

            protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
            {
                foreach (Feature feature in features)
                {
                    LineShape line = (LineShape)feature.GetShape();

                    double X1 = line.Vertices[0].X;
                    double Y1 = line.Vertices[0].Y;
                    double X2 = line.Vertices[1].X;
                    double Y2 = line.Vertices[1].Y;

                    double angle = Math.Atan((Y2 - Y1) / (X2 - X1));
                    double openingAngle = Math.PI / 8;
                    double radius = 2;

                    canvas.DrawLine(feature, OuterPen, DrawingLevel.LevelOne);

                    LineShape oLineShape = new LineShape();
                    oLineShape.Vertices.Add(new Vertex(X2 + radius * Math.Cos(Math.PI + angle - openingAngle), Y2 + radius * Math.Sin(Math.PI + angle - openingAngle)));
                    oLineShape.Vertices.Add(new Vertex(X2, Y2));
                    oLineShape.Vertices.Add(new Vertex(X2 + radius * Math.Cos(Math.PI + angle + openingAngle), Y2 + radius * Math.Sin(Math.PI + angle + openingAngle)));
                    canvas.DrawLine(oLineShape, OuterPen, DrawingLevel.LevelOne);
                }
            }
        }
    }
}
