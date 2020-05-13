using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.Sample
{
    public class AnnotationStyle : PositionStyle
    {
        public AnnotationStyle()
            : this("Annotation")
        { }

        public AnnotationStyle(string textColumnName)
        {
            TextColumnName = textColumnName;

            DrawingFontStyles fontStyles = DrawingFontStyles.Regular;
            fontStyles = fontStyles | DrawingFontStyles.Bold;
            fontStyles = fontStyles | DrawingFontStyles.Italic;
            fontStyles = fontStyles | DrawingFontStyles.Underline;
            Font = new GeoFont("Arial", 16, fontStyles);
            TextSolidBrush = new GeoSolidBrush(GeoColors.DarkRed);
            PointPlacement = PointPlacement.Center;
            DuplicateRule = LabelDuplicateRule.UnlimitedDuplicateLabels;
            OverlappingRule = LabelOverlappingRule.AllowOverlapping;
            Mask = AreaStyles.CreateSimpleAreaStyle(GeoColors.Blue, GeoColors.Black, 2);
            MaskType = MaskType.RoundedCorners;
            MaskMargin = 10;
            LeaderLineStyle = new LineStyle(new GeoPen(GeoColors.Black, 2));
        }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (var feature in features)
            {
                if (feature.GetWellKnownType() == WellKnownType.Line)
                {
                    LineShape lineShape = (LineShape)feature.GetShape();
                    Feature newFeature = new Feature(lineShape.Vertices[0]);
                    newFeature.ColumnValues.Add(TextColumnName, feature.ColumnValues[TextColumnName]);
                    var labelingCandidates = GetLabelingCandidates(newFeature, canvas);
                    foreach (var labelingCandidate in labelingCandidates)
                    {
                        PolygonShape maskArea = ConvertPolygonShapeToWorldCoordinate(labelingCandidate.ScreenArea, canvas.CurrentWorldExtent, canvas.Width, canvas.Height);
                        PointShape closestPoint = maskArea.GetClosestPointTo(new PointShape(lineShape.Vertices[1]), canvas.MapUnit);
                        if (closestPoint != null)
                        {
                            lineShape.Vertices[0] = new Vertex(closestPoint.X, closestPoint.Y);
                            LeaderLineStyle.Draw(new LineShape[] { lineShape }, canvas, labelsInThisLayer, labelsInAllLayers);
                        }

                        DrawMask(labelingCandidate, canvas, labelsInThisLayer, labelsInAllLayers);

                        foreach (LabelInformation labelInfo in labelingCandidate.LabelInformation)
                        {
                            var textPathInScreen = new ScreenPointF((float)labelInfo.PositionInScreenCoordinates.X, (float)labelInfo.PositionInScreenCoordinates.Y);
                            canvas.DrawText(labelInfo.Text, Font, TextSolidBrush, HaloPen, new ScreenPointF[] { textPathInScreen }, DrawingLevel, 0, 0, (float)labelInfo.RotationAngle, DrawingTextAlignment.Default);
                        }
                    }
                }
            }
        }

        private PolygonShape ConvertPolygonShapeToWorldCoordinate(PolygonShape simplyPolygon, RectangleShape currentWorldExtent, float canvasWidth, float canvasHeight)
        {
            double upperLeftX = currentWorldExtent.UpperLeftPoint.X;
            double upperLeftY = currentWorldExtent.UpperLeftPoint.Y;
            double extentWidth = currentWorldExtent.Width;
            double extentHeight = currentWorldExtent.Height;

            double widthFactor = extentWidth / canvasWidth;
            double heightFactor = extentHeight / canvasHeight;

            int count = simplyPolygon.InnerRings.Count + 1;
            RingShape ringShape = null;

            int verticesCount = 0;

            for (int i = 0; i < count; i++)
            {
                ringShape = (i == 0) ? simplyPolygon.OuterRing : simplyPolygon.InnerRings[i - 1];

                verticesCount += ringShape.Vertices.Count;
            }

            byte[] wellKnownBinary = new byte[9 + count * 4 + verticesCount * 16];
            byte[] header = new byte[5] { 1, 3, 0, 0, 0 };
            CopyToArray(header, wellKnownBinary, 0);
            CopyToArray(BitConverter.GetBytes(count), wellKnownBinary, 5);
            int index = 9;

            for (int i = 0; i < count; i++)
            {
                ringShape = (i == 0) ? simplyPolygon.OuterRing : simplyPolygon.InnerRings[i - 1];

                CopyToArray(BitConverter.GetBytes(ringShape.Vertices.Count), wellKnownBinary, index);
                index += 4;

                for (int j = 0; j < ringShape.Vertices.Count; j++)
                {
                    double pointX = ringShape.Vertices[j].X;
                    double pointY = ringShape.Vertices[j].Y;

                    double worldPointX = pointX * widthFactor + upperLeftX;
                    double worldPointY = upperLeftY - pointY * heightFactor;

                    CopyToArray(BitConverter.GetBytes(worldPointX), wellKnownBinary, index);
                    index += 8;
                    CopyToArray(BitConverter.GetBytes(worldPointY), wellKnownBinary, index);
                    index += 8;
                }
            }

            return new PolygonShape(wellKnownBinary);
        }

        private void CopyToArray(byte[] sourceArray, byte[] destinateArray, long destinateIndex)
        {
            for (int i = 0; i < sourceArray.Length; i++)
            {
                destinateArray[destinateIndex + i] = sourceArray[i];
            }
        }
    }
}
