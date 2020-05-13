using System;
using System.Collections.Generic;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace WeatherLineStyle
{
    class customGeoImageLineStyle : LineStyle
    {

        private GeoImage geoImage;
        private int spacing;
        private LineStyle lineStyle;
        public enum SymbolSide { Right, Left };
        private SymbolSide side;

        public customGeoImageLineStyle(LineStyle LineStyle, GeoImage GeoImage, int Spacing, SymbolSide Side)
        {
            this.lineStyle = LineStyle;
            this.geoImage = GeoImage;
            this.geoImage = GeoImage;
            this.spacing = Spacing;
            this.side = Side;
        }

        public LineStyle LineStyle
        {
            get { return lineStyle; }
            set { lineStyle = value; }
        }

        public GeoImage GeoImage
        {
            get { return geoImage; }
            set { geoImage = value; }
        }

        //Spacing in screen coordinate between each GeoImage on the line.
        public int Spacing
        {
            get { return spacing; }
            set { spacing = value; }
        }

        //Side according to the line direction for orienting the icon.
        public SymbolSide Side
        {
            get { return side; }
            set { side = value;}
        }
        
        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInThisLayer, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInAllLayers)
        {
           PointStyle pointStyle = new PointStyle(geoImage);
           
           foreach (Feature feature in features)
            {
                LineShape lineShape = (LineShape)feature.GetShape();
                lineStyle.Draw(new BaseShape[] { lineShape }, canvas, labelsInThisLayer, labelsInAllLayers);

                double totalDist = 0;
                for (int i = 0; i < lineShape.Vertices.Count - 1; i++)
                {
                    PointShape pointShape1 = new PointShape(lineShape.Vertices[i]);
                    PointShape pointShape2 = new PointShape(lineShape.Vertices[i + 1]);

                    LineShape tempLineShape = new LineShape();
                    tempLineShape.Vertices.Add(lineShape.Vertices[i]);
                    tempLineShape.Vertices.Add(lineShape.Vertices[i + 1]);

                    double angle = GetAngleFromTwoVertices(lineShape.Vertices[i], lineShape.Vertices[i + 1]);
                    
                    //Left side
                    if (side == SymbolSide.Left)
                    {
                        if (angle >= 270) { angle = angle - 180; }
                    }
                    //Right side
                    else
                    {
                        if (angle <= 90) { angle = angle + 180; }
                    }
                    pointStyle.RotationAngle = (float)angle;
                   
                    float screenDist = ExtentHelper.GetScreenDistanceBetweenTwoWorldPoints(canvas.CurrentWorldExtent, pointShape1, 
                                                                                        pointShape2, canvas.Width, canvas.Height);
                    double currentDist = Math.Round(pointShape1.GetDistanceTo(pointShape2, canvas.MapUnit, DistanceUnit.Meter), 2);
                    double worldInterval = (currentDist * spacing) / screenDist;

                    while (totalDist <= currentDist)
                    {
                        PointShape tempPointShape = tempLineShape.GetPointOnALine(StartingPoint.FirstPoint, totalDist, canvas.MapUnit, DistanceUnit.Meter);
                        pointStyle.Draw(new BaseShape[] { tempPointShape }, canvas, labelsInThisLayer, labelsInAllLayers);
                        totalDist = totalDist + worldInterval;
                    }

                    totalDist = totalDist - currentDist;
                 }
            }
            
        }

        private double GetAngleFromTwoVertices(Vertex b, Vertex c)
        {
            double result;
            double alpha = 0;
            double tangentAlpha = (c.Y - b.Y) / (c.X - b.X);
            double Peta = Math.Atan(tangentAlpha);

            if (c.X > b.X)
            {
                alpha = 90 + (Peta * (180 / Math.PI));
            }
            else if (c.X < b.X)
            {
                alpha = 270 + (Peta * (180 / Math.PI));
            }
            else
            {
                if (c.Y > b.Y) alpha = 0;
                if (c.Y < b.Y) alpha = 180;
            }

            double offset;
            if (b.X > c.X)
            { offset = 90; }
            else { offset = -90; }

            result = alpha + offset;

            return result;
        }
    }
}
