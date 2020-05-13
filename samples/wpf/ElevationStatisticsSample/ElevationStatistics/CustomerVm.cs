namespace ThinkGeo.MapSuite.DebugSamples
{
    public class CustomerVm
    {
        private double elevation;
        private double longitude;
        private double latitude;
        private double distance;

        public CustomerVm()
            : this(-32768, 0, 0, 0)
        {

        }

        public CustomerVm(double elevation, double longitude, double latitude, double distance)
        {
            this.Elevation = elevation;
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Distance = distance;
        }

        public double Elevation
        {
            get { return elevation; }
            set { elevation = value; }
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

        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }
    }
}
