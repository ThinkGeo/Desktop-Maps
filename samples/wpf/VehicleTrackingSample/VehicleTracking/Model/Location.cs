using System;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    /// <summary>
    /// This class specify the basic information for a location.
    /// </summary>
    public class Location
    {
        private double speed;
        private double latitude;
        private double longitude;
        private DateTime dateTime;

        public Location()
            : this(0, 0, 0, DateTime.Now)
        { }

        public Location(double longitude, double latitude, double speed, DateTime dateTime)
        {
            Longitude = longitude;
            Latitude = latitude;
            Speed = speed;
            DateTime = dateTime;
        }

        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        public PointShape GetLocation()
        {
            return new PointShape(Longitude, Latitude);
        }
    }
}
