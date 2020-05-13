namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class EarthquakeFeatureSourceFilterModel
    {
        private double depthRangeMaximum;
        private double depthRangeMinimum;
        private double displayDepthRangeEnd;
        private double displayDepthRangeStart;
        private double displayMagnitudeRangeEnd;
        private double displayMagnitudeRangeStart;
        private double displayYearRangeEnd;
        private double displayYearRangeStart;
        private double magnitudeRangeMaximum;
        private double magnitudeRangeMinimum;
        private double yearRangeMaximum;
        private double yearRangeMinimum;

        public EarthquakeFeatureSourceFilterModel()
        {
            magnitudeRangeMinimum = 0;
            magnitudeRangeMaximum = 12;
            depthRangeMinimum = 0;
            depthRangeMaximum = 300;
            yearRangeMinimum = 1568;
            yearRangeMaximum = 2010;

            displayDepthRangeEnd = 300;
            displayDepthRangeStart = 0;
            displayMagnitudeRangeEnd = 12;
            displayMagnitudeRangeStart = 0;
            displayYearRangeEnd = 2010;
            displayYearRangeStart = 1568;
        }

        public double DepthRangeMaximum
        {
            get { return depthRangeMaximum; }
            set { depthRangeMaximum = value; }
        }

        public double DepthRangeMinimum
        {
            get { return depthRangeMinimum; }
            set { depthRangeMinimum = value; }
        }

        public double DisplayDepthRangeEnd
        {
            get { return displayDepthRangeEnd; }
            set { displayDepthRangeEnd = value; }
        }

        public double DisplayDepthRangeStart
        {
            get { return displayDepthRangeStart; }
            set { displayDepthRangeStart = value; }
        }

        public double DisplayMagnitudeRangeEnd
        {
            get { return displayMagnitudeRangeEnd; }
            set { displayMagnitudeRangeEnd = value; }
        }

        public double DisplayMagnitudeRangeStart
        {
            get { return displayMagnitudeRangeStart; }
            set { displayMagnitudeRangeStart = value; }
        }

        public double DisplayYearRangeEnd
        {
            get { return displayYearRangeEnd; }
            set { displayYearRangeEnd = value; }
        }

        public double DisplayYearRangeStart
        {
            get { return displayYearRangeStart; }
            set { displayYearRangeStart = value; }
        }

        public double MagnitudeRangeMaximum
        {
            get { return magnitudeRangeMaximum; }
            set { magnitudeRangeMaximum = value; }
        }

        public double MagnitudeRangeMinimum
        {
            get { return magnitudeRangeMinimum; }
            set { magnitudeRangeMinimum = value; }
        }

        public double YearRangeMaximum
        {
            get { return yearRangeMaximum; }
            set { yearRangeMaximum = value; }
        }

        public double YearRangeMinimum
        {
            get { return yearRangeMinimum; }
            set { yearRangeMinimum = value; }
        }
    }
}