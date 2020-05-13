using System.Collections.ObjectModel;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace SnapToLayer
{
    public enum ToleranceCoordinates { World, Screen };
    class SnapToLayerEditInteractiveOverlay : EditInteractiveOverlay
    {
        private PointStyle controlPointStyle;
        private PointStyle draggedControlPointStyle;
        private InMemoryFeatureLayer toSnapInMemoryFeatureLayer;
        private float tolerance;
        //private int tolerancePixel;
        private DistanceUnit toleranceUnit;
        private GeographyUnit geographyUnit;

        private RectangleShape currentWorldExtent;
        private float mapWidth;
        private float mapHeight;
        private ToleranceCoordinates toleranceType;



        //Property for the non dragged control point style.
        public PointStyle ControlPointStyle
        {
            get { return controlPointStyle; }
            set { controlPointStyle = value; }
        }

        //Property for the dragged control point style.
        public PointStyle DraggedControlPointStyle
        {
            get { return draggedControlPointStyle; }
            set { draggedControlPointStyle = value; }
        }

        //InMemoryFeatureLayer for the layer to be snapped to.
        public InMemoryFeatureLayer ToSnapInMemoryFeatureLayer
        {
            get { return toSnapInMemoryFeatureLayer; }
            set { toSnapInMemoryFeatureLayer = value; }
        }

        public ToleranceCoordinates ToleranceType
        {
            get { return toleranceType; }
            set { toleranceType = value; }
        }

        public float Tolerance
        {
            get { return tolerance; }
            set { tolerance = value; }
        }

        public DistanceUnit ToleranceUnit
        {
            get { return toleranceUnit; }
            set { toleranceUnit = value; }
        }

        //Overrides the MoveVertexCore to have the logic to check if the dragged control point is within tolerance.
        protected override Feature MoveVertexCore(Feature sourceFeature, PointShape sourceControlPoint, PointShape targetControlPoint)
        {
            VertexMovingEditInteractiveOverlayEventArgs vertexMovingEditInteractiveOverlayEventArgs = new VertexMovingEditInteractiveOverlayEventArgs(false, sourceFeature, new Vertex(targetControlPoint));

            PointShape snapPointShape = null;
            if (toleranceType == ToleranceCoordinates.Screen)
            {
                snapPointShape = FindNearestSnappingPointPixel(targetControlPoint);
            }
            else
            {
                snapPointShape = FindNearestSnappingPoint(targetControlPoint);
            }

            if (snapPointShape != null)
            {
                vertexMovingEditInteractiveOverlayEventArgs.MovingVertex = new Vertex(snapPointShape);
            }

            return base.MoveVertexCore(sourceFeature, sourceControlPoint, new PointShape(vertexMovingEditInteractiveOverlayEventArgs.MovingVertex));
        }

        //Function to find if dragged control point is within the tolerance of a vertex of layer in screen (pixels) coordinates.
        private PointShape FindNearestSnappingPointPixel(PointShape targetPointShape)
        {
            toSnapInMemoryFeatureLayer.Open();
            Collection<Feature> toSnapInMemoryFeatures = toSnapInMemoryFeatureLayer.FeatureSource.GetFeaturesNearestTo(targetPointShape, GeographyUnit.Meter, 1, ReturningColumnsType.AllColumns);
            toSnapInMemoryFeatureLayer.Close();

            if (toSnapInMemoryFeatures.Count == 1)
            {
                PolygonShape polygonShape = (PolygonShape)toSnapInMemoryFeatures[0].GetShape();

                foreach (Vertex vertex in polygonShape.OuterRing.Vertices)
                {
                    PointShape toSnapPointShape = new PointShape(vertex);
                    float screenDistance = ExtentHelper.GetScreenDistanceBetweenTwoWorldPoints(currentWorldExtent, toSnapPointShape, targetPointShape, mapWidth, mapHeight);

                    if (screenDistance <= tolerance)
                    {
                        return new PointShape(toSnapPointShape.X, toSnapPointShape.Y);
                    }
                }
            }
            return null;
        }

        //Function to find if dragged control point is within the tolerance of a vertex of layer in world coordinates.
        private PointShape FindNearestSnappingPoint(PointShape targetPointShape)
        {
            toSnapInMemoryFeatureLayer.Open();
            Collection<Feature> toSnapInMemoryFeatures = toSnapInMemoryFeatureLayer.FeatureSource.GetFeaturesNearestTo(targetPointShape, GeographyUnit.Meter, 1, ReturningColumnsType.AllColumns);
            toSnapInMemoryFeatureLayer.Close();

            if (toSnapInMemoryFeatures.Count == 1)
            {
                PolygonShape polygonShape = (PolygonShape)toSnapInMemoryFeatures[0].GetShape();

                foreach (Vertex vertex in polygonShape.OuterRing.Vertices)
                {
                    PointShape toSnapPointShape = new PointShape(vertex);
                    double Distance = toSnapPointShape.GetDistanceTo(targetPointShape, geographyUnit, toleranceUnit);

                    if (Distance <= tolerance)
                    {
                        return new PointShape(toSnapPointShape.X, toSnapPointShape.Y);
                    }
                }
            }
            return null;
        }

        //Overrides the DrawCore function to draw the Edit Layers, the vertices and tolerance ellipses of layer to snap to,
        //and the control points.
        protected override void DrawCore(GeoCanvas canvas)
        {
            //Sets the geography Unit used in FindNearestSnappingPoint function
            geographyUnit = canvas.MapUnit;
            currentWorldExtent = canvas.CurrentWorldExtent;
            mapWidth = canvas.Width;
            mapHeight = canvas.Height;

            //Draws the Edit Shapes as default.
            Collection<SimpleCandidate> labelsInAllLayers = new Collection<SimpleCandidate>();
            EditShapesLayer.Open();
            EditShapesLayer.Draw(canvas, labelsInAllLayers);
            canvas.Flush();

            //Draw the vertices and tolerance ellipses of layer to snap to.
            toSnapInMemoryFeatureLayer.Open();
            Collection<Feature> toSnapPoints = toSnapInMemoryFeatureLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);
            toSnapInMemoryFeatureLayer.Close();
            foreach (Feature feature in toSnapPoints)
            {
                PolygonShape polygonShape = (PolygonShape)feature.GetShape();

                foreach (Vertex vertex in polygonShape.OuterRing.Vertices)
                {
                    //Draws the vertex.
                    PointShape pointShape = new PointShape(vertex);
                    canvas.DrawEllipse(pointShape, 5, 5, new GeoSolidBrush(GeoColor.StandardColors.Black), DrawingLevel.LevelOne);

                    //Draws the tolerance ellipse.
                    if (toleranceType == ToleranceCoordinates.Screen)
                    {
                        ScreenPointF screenPointF = ExtentHelper.ToScreenCoordinate(canvas.CurrentWorldExtent, pointShape, canvas.Width, canvas.Height);
                        canvas.DrawEllipse(screenPointF, tolerance * 2, tolerance * 2, new GeoPen(GeoColor.StandardColors.Black), new GeoSolidBrush(), DrawingLevel.LevelFour, 0, 0, PenBrushDrawingOrder.PenFirst);
                    }
                    else
                    {
                        EllipseShape ellipseShape = new EllipseShape(pointShape, tolerance, canvas.MapUnit, toleranceUnit);
                        canvas.DrawArea(ellipseShape, new GeoPen(GeoColor.StandardColors.Black), DrawingLevel.LevelOne);
                    }
                }
            }

            //Draws the control points. 
            ExistingControlPointsLayer.Open();
            Collection<Feature> controlPoints = ExistingControlPointsLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);

            ////Loops thru the control points.
            foreach (Feature feature in controlPoints)
            {
                //Looks at the value of "state" to draw the control point as dragged or not.
                Feature[] features = new Feature[1] { feature };
                controlPointStyle.Draw(features, canvas, labelsInAllLayers, labelsInAllLayers);

            }

            foreach (Feature feature in SelectedControlPointLayer.InternalFeatures)
            {
                Feature[] features = new Feature[1] { feature };
                draggedControlPointStyle.Draw(features, canvas, labelsInAllLayers, labelsInAllLayers);
            }
        }
    }
}
