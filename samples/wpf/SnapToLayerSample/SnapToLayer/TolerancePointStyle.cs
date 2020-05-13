using System.Collections.Generic;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace SnapToLayer
{
    class TolerancePointStyle : PointStyle
    {
        private float tolerance;
        private DistanceUnit toleranceUnit;
        private ToleranceCoordinates toleranceType;

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

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInThisLayer, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (Feature feature in features)
            {

                //Draws the vertex.
                PointShape pointShape = (PointShape)feature.GetShape();
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

    }
}
