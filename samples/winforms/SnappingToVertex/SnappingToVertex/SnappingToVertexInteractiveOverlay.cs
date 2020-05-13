using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace SnappingToVertex
{
    class SnappingToVertexInteractiveOverlay : EditInteractiveOverlay
    {
        private InteractionArguments arguments;
        private double tolerance;
        private DistanceUnit toleranceUnit;
        bool inControlPointDraggingMode;
        
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

        public SnappingToVertexInteractiveOverlay()
            : base()
        {
        }

        protected override InteractiveResult MouseDownCore(InteractionArguments interactionArguments)
        {
            arguments = interactionArguments;
            return base.MouseDownCore(interactionArguments);
        }

        protected override InteractiveResult MouseUpCore(InteractionArguments interactionArguments)
        {
            //Sets inControlPointDraggingMode to false to show the tolerance circle when not dragging vertex.
            inControlPointDraggingMode = false;
            return base.MouseUpCore(interactionArguments);
        }

        protected override Feature SetSelectedControlPointCore(PointShape targetPointShape, double searchingTolerance)
        {
            //If the mouse pointer is within the tolerance of a vertex, allows the dragging of the vertex.
            //If not, the map will pan normally.
            Collection<Feature> existingControlPoints = ExistingControlPointsLayer.FeatureSource.GetFeaturesNearestTo(targetPointShape, GeographyUnit.Meter, 1, ReturningColumnsType.AllColumns);
            if (existingControlPoints.Count == 1)
            {
                PointShape existingControlPointShape = (PointShape)existingControlPoints[0].GetShape();
                double Distance = existingControlPointShape.GetDistanceTo(targetPointShape, arguments.MapUnit, toleranceUnit);

                if (Distance <= tolerance)
                {
                    ControlPointType = ControlPointType.Vertex;
                    if (arguments != null)
                    {
                        //Snapps the location of the mouse pointer to the location of the nearest vertex.
                        ScreenPointF controlPoint = ExtentHelper.ToScreenCoordinate(arguments.CurrentExtent, existingControlPoints[0], arguments.MapWidth, arguments.MapHeight);
                        ScreenPointF targetPoint = ExtentHelper.ToScreenCoordinate(arguments.CurrentExtent, targetPointShape, arguments.MapWidth, arguments.MapHeight);
                        Cursor.Position = new Point(Cursor.Position.X + (int)(controlPoint.X - targetPoint.X), Cursor.Position.Y + (int)(controlPoint.Y - targetPoint.Y));
                    }
                }

                inControlPointDraggingMode = true;
                return existingControlPoints[0];
            }

            return base.SetSelectedControlPointCore(targetPointShape, searchingTolerance);
        }

        protected override void DrawCore(GeoCanvas canvas)
        {
            base.DrawCore(canvas);

            //If not dragging a vertex, show tolerance as circles around the vertices.
            if (!inControlPointDraggingMode)
            {
                Collection<Feature> controlPoints = ExistingControlPointsLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);

                foreach (Feature feature in controlPoints)
                {
                    PointShape pointShape = (PointShape)feature.GetShape();
                    EllipseShape ellipseShape = new EllipseShape(pointShape, tolerance, canvas.MapUnit, toleranceUnit);
                    canvas.DrawArea(ellipseShape, new GeoPen(GeoColor.StandardColors.Black), DrawingLevel.LevelFour);
                }
            }
       }
    }
    
}
