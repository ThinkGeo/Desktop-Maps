using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ThinkGeo.MapSuite.Core
{
    public class OneWayLineStyle : LineStyle
    {
        private string oneWayColumnName;
        private string oneWayValue;
        private GeoImage geoImage;

        public OneWayLineStyle()
            : this(string.Empty, string.Empty, new GeoImage())
        { }

        public OneWayLineStyle(string oneWayColumnName, string oneWayValue, GeoImage geoImage)
        {
            this.oneWayColumnName = oneWayColumnName;
            this.oneWayValue = oneWayValue;
            this.geoImage = geoImage;
        }

        public string OneWayColumnName
        {
            get { return oneWayColumnName; }
            set { oneWayColumnName = value; }
        }

        public string OneWayValue
        {
            get { return oneWayValue; }
            set { oneWayValue = value; }
        }

        public GeoImage GeoImage
        {
            get { return geoImage; }
            set { geoImage = value; }
        }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (Feature feature in features)
            {
                string currentOneWayValue = Convert.ToString(feature.ColumnValues[oneWayColumnName]);

                if (currentOneWayValue == oneWayValue)
                {
                    LineShape lineShape = new LineShape();

                    if (feature.GetShape() is MultilineShape)
                    {
                        MultilineShape multilineShape = (MultilineShape)feature.GetShape();
                        lineShape = GetLongestSegment(multilineShape);
                    }
                    else
                    {
                        lineShape = GetLongestSegment((LineShape)feature.GetShape());
                    }

                    double angle = GetAngleFromTwoVertices(lineShape.Vertices[0], lineShape.Vertices[1]);

                    PointShape pointShape = lineShape.GetPointOnALine(StartingPoint.FirstPoint, 50);

                    canvas.DrawWorldImage(geoImage, pointShape.X, pointShape.Y, geoImage.GetWidth(), geoImage.GetHeight(), DrawingLevel.LevelOne, 0, 0, (float)angle);
                }
            }
        }

        private LineShape GetLongestSegment(LineShape lineShape)
        {
            LineShape resultLineShape = new LineShape();
            PointShape pointShape1 = new PointShape(lineShape.Vertices[0]);
            PointShape pointShape2 = new PointShape(lineShape.Vertices[1]);
            resultLineShape.Vertices.Add(lineShape.Vertices[0]);
            resultLineShape.Vertices.Add(lineShape.Vertices[1]);
            double longestDist = pointShape1.GetDistanceTo(pointShape2, GeographyUnit.Meter, DistanceUnit.Meter);

            for (int i = 0; i < lineShape.Vertices.Count - 2; i++)
            {
                pointShape1 = new PointShape(lineShape.Vertices[i]);
                pointShape2 = new PointShape(lineShape.Vertices[i + 1]);
                double currentDist = pointShape1.GetDistanceTo(pointShape2, GeographyUnit.Meter, DistanceUnit.Meter);

                if (currentDist > longestDist)
                {
                    resultLineShape.Vertices.Clear();
                    resultLineShape.Vertices.Add(lineShape.Vertices[i]);
                    resultLineShape.Vertices.Add(lineShape.Vertices[i + 1]);
                    longestDist = currentDist;
                }
            }

            return resultLineShape;
        }

        private LineShape GetLongestSegment(MultilineShape multiLineShape)
        {
            LineShape resultLineShape = new LineShape();
            double longestDist = 0;

            foreach (LineShape lineShape in multiLineShape.Lines)
            {
                for (int i = 0; i < lineShape.Vertices.Count - 2; i++)
                {
                    PointShape pointShape1 = new PointShape(lineShape.Vertices[i]);
                    PointShape pointShape2 = new PointShape(lineShape.Vertices[i + 1]);
                    double currentDist = pointShape1.GetDistanceTo(pointShape2, GeographyUnit.Meter, DistanceUnit.Meter);

                    if (currentDist > longestDist)
                    {
                        resultLineShape.Vertices.Clear();
                        resultLineShape.Vertices.Add(lineShape.Vertices[i]);
                        resultLineShape.Vertices.Add(lineShape.Vertices[i + 1]);
                        longestDist = currentDist;
                    }
                }
            }

            return resultLineShape;
        }

        private double GetAngleFromTwoVertices(Vertex b, Vertex c)
        {
            float xDiff = (float)c.X - (float)b.X;
            float yDiff = (float)c.Y - (float)b.Y; 
            return Math.Atan2(yDiff, xDiff) * (180 / Math.PI);
        }

        protected override Collection<string> GetRequiredColumnNamesCore()
        {
            Collection<string> columns = base.GetRequiredColumnNamesCore();

            if (!columns.Contains(oneWayColumnName))
            {
                columns.Add(oneWayColumnName);
            }

            return columns;
        }
    }
}