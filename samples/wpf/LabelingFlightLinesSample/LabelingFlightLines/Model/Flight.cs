using System;
using ThinkGeo.MapSuite.Shapes;

namespace LabelingFlightLines
{
    public class Flight
    {
        private string name;
        private string flightNumber;
        private string date;
        private string flightTime;
        private string originAddress;
        private PointShape originLocation;
        private string destinationAddress;
        private PointShape destinationLocation;
        private PointShape flightLineCenter;
        private LineShape flightLineShape;

        public Flight()
        { }

        public string Key
        {
            get { return Name + "-" + FlightNumber; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public string FlightTime
        {
            get { return flightTime; }
            set { flightTime = value; }
        }

        public string OriginAddress
        {
            get { return originAddress; }
            set { originAddress = value; }
        }

        public PointShape OriginLocation
        {
            get { return originLocation; }
            set { originLocation = value; }
        }

        public string DestinationAddress
        {
            get { return destinationAddress; }
            set { destinationAddress = value; }
        }

        public PointShape DestinationLocation
        {
            get { return destinationLocation; }
            set { destinationLocation = value; }
        }

        public PointShape FlightLineCenter
        {
            get { return flightLineCenter; }
            set { flightLineCenter = value; }
        }

        public LineShape FlightLineShape
        {
            get { return flightLineShape; }
            set { flightLineShape = value; }
        }

        public MultilineShape GetArc(double offset)
        {
            RectangleShape rectangle = new RectangleShape(-180, 90, 180, -90);
            if (rectangle.Contains(destinationLocation) && rectangle.Contains(originLocation))
            {
                return GetArc(1000, offset);
            }
            else
            {
                return null;
            }
        }

        private MultilineShape GetArc(int count, double offset)
        {
            //Create a stylized arc with the given offset to allow the lines to be visible next to each other

            double radiansFactor = Math.PI / 180;
            double degreesFactor = 180 / Math.PI;

            double newX = 0;
            double newY = 0;

            double long1 = originLocation.X * radiansFactor;
            double lat1 = originLocation.Y * radiansFactor;
            double long2 = destinationLocation.X * radiansFactor;
            double lat2 = destinationLocation.Y * radiansFactor;

            double x1 = Math.Cos(long1) * Math.Cos(lat1);
            double x2 = Math.Cos(long2) * Math.Cos(lat2);
            double y1 = Math.Sin(long1) * Math.Cos(lat1);
            double y2 = Math.Sin(long2) * Math.Cos(lat2);
            double z1 = Math.Sin(lat1);
            double z2 = Math.Sin(lat2);

            double alpha = Math.Acos((x1 * x2) + (y1 * y2) + (z1 * z2));

            if (alpha == 0)
            {
                return new MultilineShape(new LineShape[] { new LineShape(new Vertex[] { new Vertex(originLocation), new Vertex(destinationLocation) }) });
            }

            double x3 = (x2 - (x1 * Math.Cos(alpha))) / Math.Sin(alpha);
            double y3 = (y2 - (y1 * Math.Cos(alpha))) / Math.Sin(alpha);
            double z3 = (z2 - (z1 * Math.Cos(alpha))) / Math.Sin(alpha);

            MultilineShape returnMultiLine = new MultilineShape();
            LineShape lineShape1 = new LineShape();
            LineShape lineShape2 = new LineShape();
            lineShape1.Vertices.Add(new Vertex(originLocation.X, originLocation.Y));
            bool isDisconnected = false;
            for (int i = 1; i <= count; i++)
            {
                double xbm = (x1 * Math.Cos((i * alpha) / (count + 1))) + (x3 * Math.Sin((i * alpha) / (count + 1)));
                double ybm = (y1 * Math.Cos((i * alpha) / (count + 1))) + (y3 * Math.Sin((i * alpha) / (count + 1)));
                double zbm = (z1 * Math.Cos((i * alpha) / (count + 1))) + (z3 * Math.Sin((i * alpha) / (count + 1)));

                if (ybm < 0 && xbm < 0)
                {
                    newX = Math.Atan(ybm / xbm) * degreesFactor - 180;
                }
                else if (ybm > 0 && xbm < 0)
                {
                    newX = Math.Atan(ybm / xbm) * degreesFactor + 180;
                }
                else
                {
                    newX = Math.Atan(ybm / xbm) * degreesFactor;
                }
                newY = Math.Asin(zbm) * degreesFactor + ((-1) * (offset / Math.Pow(count / 2, 2)) * Math.Pow(i - count / 2, 2) + offset);
                if ((i > 2) && (isDisconnected == false))
                {
                    double lastDistance = Math.Sqrt(Math.Pow((lineShape1.Vertices[i - 2].X - lineShape1.Vertices[i - 3].X), 2) + Math.Pow((lineShape1.Vertices[i - 2].Y - lineShape1.Vertices[i - 3].Y), 2));
                    double currentDistance = Math.Sqrt(Math.Pow((newX - lineShape1.Vertices[i - 2].X), 2) + Math.Pow((newY - lineShape1.Vertices[i - 2].Y), 2));
                    if (currentDistance > (lastDistance * 6))
                    {
                        isDisconnected = true;
                    }
                }
                if (isDisconnected == false)
                {
                    lineShape1.Vertices.Add(new Vertex(newX, newY));
                }
                else
                {
                    lineShape2.Vertices.Add(new Vertex(newX, newY));
                }
            }

            if (isDisconnected == false)
            {
                lineShape1.Vertices.Add(new Vertex(destinationLocation.X, destinationLocation.Y));
                returnMultiLine.Lines.Add(lineShape1);
            }
            else
            {
                lineShape2.Vertices.Add(new Vertex(destinationLocation.X, destinationLocation.Y));
                returnMultiLine.Lines.Add(lineShape1);
                returnMultiLine.Lines.Add(lineShape2);
            }
            return returnMultiLine;
        }
    }
}
