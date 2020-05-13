using System.Collections.ObjectModel;
using System.Collections.Generic;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite.Drawing;

namespace VertexTolerance
{
    class VertexToleranceEditInteractiveOverlay : EditInteractiveOverlay
    {
        private PointStyle controlPointStyle;
        private PointStyle draggedControlPointStyle;
        private double tolerance;
        private DistanceUnit toleranceUnit;
        private GeographyUnit geographyUnit;

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

        //Toerance distance to keep on the control points
        public double Tolerance
        {
            get { return tolerance; }
            set { tolerance = value; }
        }

        public DistanceUnit ToleranceUnit
        {
            get { return toleranceUnit; }
            set { toleranceUnit = value; }
        }

        //Overrides the MoveVertexCore to have the logic to not allow dragged control within the tolerance area.
        protected override Feature MoveVertexCore(Feature sourceFeature, PointShape sourceControlPoint, PointShape targetControlPoint)
        {
            VertexMovingEditInteractiveOverlayEventArgs vertexMovingEditInteractiveOverlayEventArgs = new VertexMovingEditInteractiveOverlayEventArgs(false, sourceFeature, new Vertex(targetControlPoint));

            ExistingControlPointsLayer.Open();
            Collection<Feature> controlPoints = ExistingControlPointsLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);

            EllipseShape toleranceEllipse = null;
            //Loops thru the control points.
            foreach (Feature feature in controlPoints)
            {
                //Checks the distance of dragged control point to the other control points to know if within tolerance or not and
                //gets the tolerance ellipseShape.
                PointShape pointShape = (PointShape)feature.GetShape();
                double Dist = pointShape.GetDistanceTo(targetControlPoint, geographyUnit, toleranceUnit);

                if (Dist <= Tolerance)
                {
                    toleranceEllipse = new EllipseShape(pointShape, tolerance, geographyUnit, toleranceUnit);
                    break;
                }
            }

            //If within tolerance. 
            if (toleranceEllipse != null)
            {
                //If within tolerance gets other EllipseShapes within tolerance.
                EllipseShape[] neighborEllipseShapes = GetNeighborEllipseShapes(toleranceEllipse);
                //Unions and gets the LineShape (perimeter) of the unioned MultipolygonShape. 
                //Then we get the closet point to mouse pointer on the lineShape to have the effect of the dragged control point
                //staying outside the tolerance.
                MultipolygonShape unionMultiPolygonShape = PolygonShape.Union(neighborEllipseShapes);
                LineShape ellipseLineShape = ToLineShape(unionMultiPolygonShape);
                PointShape onEllipsePointShape = ellipseLineShape.GetClosestPointTo(targetControlPoint, geographyUnit);

                vertexMovingEditInteractiveOverlayEventArgs.MovingVertex = new Vertex(onEllipsePointShape);
            }
            return base.MoveVertexCore(sourceFeature, sourceControlPoint, new PointShape(vertexMovingEditInteractiveOverlayEventArgs.MovingVertex));
        }

        private EllipseShape[] GetNeighborEllipseShapes(EllipseShape ellipseShape)
        {
            ExistingControlPointsLayer.Open();
            Collection<Feature> controlPoints = ExistingControlPointsLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);

            List<EllipseShape> neighborEllipseShapes = new List<EllipseShape>();
            //Loops thru the control points.
            foreach (Feature feature in controlPoints)
            {
                //Looks at the value of "state" for the non dragged point controls.
                PointShape pointShape = (PointShape)feature.GetShape();
                double Dist = pointShape.GetDistanceTo(ellipseShape, geographyUnit, toleranceUnit);
                if (Dist <= Tolerance)
                {
                    EllipseShape neighborEllipseShape = new EllipseShape(pointShape, tolerance, GeographyUnit.DecimalDegree, toleranceUnit);
                    neighborEllipseShapes.Add(neighborEllipseShape);
                }
            }
            return neighborEllipseShapes.ToArray();
        }

        private LineShape ToLineShape(MultipolygonShape multiPolygonShape)
        {
            LineShape lineShape = new LineShape();
            foreach (Vertex vertex in multiPolygonShape.Polygons[0].OuterRing.Vertices)
            {
                lineShape.Vertices.Add(vertex);
            }
            return lineShape;
        }

        //Overrides the DrawCore function to draw the Edit Layers, the vertices and tolerance ellipses,
        //and the control points.
        protected override void DrawCore(GeoCanvas canvas)
        {
            //Sets the geography Unit used in FindNearestSnappingPoint function
            geographyUnit = canvas.MapUnit;

            //Draws the Edit Shapes as default.
            Collection<SimpleCandidate> labelsInAllLayers = new Collection<SimpleCandidate>();
            EditShapesLayer.Open();
            EditShapesLayer.Draw(canvas, labelsInAllLayers);
            canvas.Flush();

            //Draws the control points. 
            ExistingControlPointsLayer.Open();
            Collection<Feature> controlPoints = ExistingControlPointsLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);

            //Loops thru the control points.
            foreach (Feature feature in controlPoints)
            {
                //Looks at the value of "state" to draw the control point as dragged or not.
                Feature[] features = new Feature[1] { feature };
                controlPointStyle.Draw(features, canvas, labelsInAllLayers, labelsInAllLayers);

                PointShape pointShape = (PointShape)feature.GetShape();
                //Draws the ellipse.
                EllipseShape ellipseShape = new EllipseShape(pointShape, tolerance, canvas.MapUnit, toleranceUnit);
                canvas.DrawArea(ellipseShape, new GeoPen(GeoColor.StandardColors.Black), DrawingLevel.LevelOne);
            }

            foreach (Feature feature in SelectedControlPointLayer.InternalFeatures)
            {
                Feature[] features = new Feature[1] { feature };
                draggedControlPointStyle.Draw(features, canvas, labelsInAllLayers, labelsInAllLayers);
            }
        }
    }
}
