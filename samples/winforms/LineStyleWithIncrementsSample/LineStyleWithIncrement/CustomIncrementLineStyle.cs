using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace LineStyleWithIncrement
{
    class CustomIncrementLineStyle : LineStyle
    {
        private enum Side
        {
            Undefined = 0,
            Left = 2,
            Right = 3,
            Middle = 4
        }


        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInThisLayer, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInAllLayers)
        {
            LineShape drawingLineShape; 
            PointShape linePoint;
            LineBaseShape lineShape; 
            LineStyle lineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.SimpleColors.Black, 1f, false);
            LineShape tangentLineShape; 
            ScreenPointF screenPointF; 
            double angle, decDistance = 0; 
            
            foreach (Feature feature in features)
            {
                drawingLineShape = (LineShape)feature.GetShape();

                while (!(decDistance > drawingLineShape.GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer)))
                {
                    linePoint = drawingLineShape.GetPointOnALine(StartingPoint.FirstPoint, decDistance, GeographyUnit.Meter, DistanceUnit.Kilometer);

                    tangentLineShape = GetTangentForLinePosition(drawingLineShape, decDistance);
                    angle = GetAngleFromTwoVertices(tangentLineShape.Vertices[0], tangentLineShape.Vertices[1]);

                    angle += 90.0;
                    if (angle >= 360.0) { angle = angle - 180;}

                    screenPointF = ExtentHelper.ToScreenCoordinate(canvas.CurrentWorldExtent, linePoint, (float)canvas.Width, (float)canvas.Height);  

                    canvas.DrawText("    " + decDistance.ToString(), new GeoFont("Arial", 12, DrawingFontStyles.Bold),
                        new GeoSolidBrush(GeoColor.StandardColors.Black), new GeoPen(GeoColor.StandardColors.White),
                        new ScreenPointF[] { screenPointF }, DrawingLevel.LabelLevel, 0f, 0f, Convert.ToSingle(angle));

                    double dblTranslateAngle = GetOrthogonalFromVertex(tangentLineShape.Vertices[0], tangentLineShape.Vertices[1], Side.Right);

                    double worldDist = ExtentHelper.GetWorldDistanceBetweenTwoScreenPoints
                    (canvas.CurrentWorldExtent, 0, 0, 5, 0, canvas.Width, canvas.Height, GeographyUnit.Meter, DistanceUnit.Meter);

                    PointShape pointShape2 = (PointShape)BaseShape.TranslateByDegree
                        (linePoint, worldDist, dblTranslateAngle, GeographyUnit.Meter, DistanceUnit.Meter);

                    lineShape = new LineShape(new Vertex[] {
		            new Vertex(linePoint),
		            new Vertex(pointShape2) });

                    lineStyle.Draw(new BaseShape[] { lineShape }, canvas, labelsInThisLayer, labelsInAllLayers);
                    decDistance += 0.1;
                }
                lineStyle.Draw(features, canvas, labelsInThisLayer, labelsInAllLayers);
             }
        }

        private LineShape GetTangentForLinePosition(LineShape lineShape, double stationKM)
        {
            const double OFFSET_KM = 0.000000001;
            PointShape tangentPointShape1, tangentPointShape2; 
            double offsetTangentStart = OFFSET_KM;
            double offsetTangentEnd = OFFSET_KM;
            LineShape tangentLineShape; 

            if (stationKM == 0.0) { offsetTangentStart = 0.0; }

            if (stationKM == lineShape.GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer)) { offsetTangentEnd = 0.0; }

            tangentPointShape1 = lineShape.GetPointOnALine(StartingPoint.FirstPoint, stationKM - offsetTangentStart,
                                                                                        GeographyUnit.Meter, DistanceUnit.Kilometer);
            tangentPointShape2 = lineShape.GetPointOnALine(StartingPoint.FirstPoint, stationKM + offsetTangentEnd,
                                                                                        GeographyUnit.Meter, DistanceUnit.Kilometer);
            tangentLineShape = new LineShape(new Vertex[] { new Vertex(tangentPointShape1), new Vertex(tangentPointShape2) });

            return tangentLineShape;
        }

        private double GetAngleFromTwoVertices(Vertex b, Vertex c)
        {
            double result = 0;
            double alpha = 0;
            double tangentAlpha = (c.Y - b.Y) / (c.X - b.X);
            double Peta = Math.Atan(tangentAlpha);

            if (c.X > b.X) {  alpha = 90 + (Peta * (180 / Math.PI));}
            else if (c.X < b.X) { alpha = 270 + (Peta * (180 / Math.PI)); }
            else
            {
                if (c.Y > b.Y){ alpha = 0;}
                if (c.Y < b.Y) { alpha = 180; }
            }

            double offset = 0;
            if (b.X > c.X){ offset = 90; }
            else { offset = -90; }

            result = alpha + offset;
            return result;
        }

        private double GetOrthogonalFromVertex(Vertex vertex1, Vertex vertex2, Side currentSide)
        {
            double alpha = 0.0;

            if (currentSide == Side.Left | currentSide == Side.Right)
            {
                alpha = GetTangent(vertex1, vertex2);

                if (currentSide == Side.Right)
                {
                    if (alpha < 180.0) { alpha += 180.0; }
                    else { alpha -= 180.0; }
                }
            }
            return alpha;
        }

        private double GetTangent(Vertex vertex1, Vertex vertex2)
        {
            double tangentAlpha = 0;
            double offset = 0;
            double deltaY = 0;
            double deltaX = 0;
            double alpha = -1.0;

            if ((vertex1.X != vertex2.X))
            {
                offset = 0.0;
                deltaY = vertex1.Y - vertex2.Y;
                deltaX = vertex2.X - vertex1.X;

                if (Math.Sign(deltaY) == 0.0)
                {
                    if (deltaX > 0.0) { offset = Math.PI * 0.5; }
                    else { offset = Math.PI * -0.5; }
                }
                else
                {
                    if (Math.Sign(deltaX) > 0.0) {offset = 0;}
                    else { offset = Math.PI;}
                }

                tangentAlpha = (deltaY / deltaX);
                alpha = (Math.Atan(tangentAlpha) + offset) * 180.0 / Math.PI;

                if ((alpha < 0.0)) { alpha += 360.0; }
            }
            else
            {
                if ((vertex1.Y > vertex2.Y)) { alpha = 90.0; }
                else { alpha = 270.0; }
            }

            return alpha;
        }
     }
}
