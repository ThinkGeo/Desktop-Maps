namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class EarthquakeQueryFilter : ViewModelBase
    {
        private double endYearRange;
        private double endDepthRange;
        private double startYearRange;
        private double startDepthRange;
        private double yearRangeMinimum;
        private double yearRangeMaximum;
        private double depthRangeMaximum;
        private double depthRangeMinimum;
        private double endMagnitudeRange;
        private double startMagnitudeRange;
        private double magnitudeRangeMaximum;
        private double magnitudeRangeMinimum;

        public EarthquakeQueryFilter()
        {
            MagnitudeRangeMinimum = 0;
            MagnitudeRangeMaximum = 12;
            DepthRangeMinimum = 0;
            DepthRangeMaximum = 300;
            YearRangeMinimum = 1568;
            YearRangeMaximum = 2010;

            endDepthRange = 300;
            startDepthRange = 0;
            endMagnitudeRange = 12;
            startMagnitudeRange = 0;
            endYearRange = 2010;
            startYearRange = 1568;
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

        public double EndDepthRange
        {
            get { return endDepthRange; }
            set
            {
                endDepthRange = value;
                RaisePropertyChanged(() => EndDepthRange);
            }
        }

        public double StartDepthRange
        {
            get { return startDepthRange; }
            set
            {
                startDepthRange = value;
                RaisePropertyChanged(() => StartDepthRange);
            }
        }

        public double EndMagnitudeRange
        {
            get { return endMagnitudeRange; }
            set
            {
                endMagnitudeRange = value;
                RaisePropertyChanged(() => EndMagnitudeRange);
            }
        }

        public double StartMagnitudeRange
        {
            get { return startMagnitudeRange; }
            set
            {
                startMagnitudeRange = value;
                RaisePropertyChanged(() => StartMagnitudeRange);
            }
        }

        public double EndYearRange
        {
            get { return endYearRange; }
            set
            {
                endYearRange = value;
                RaisePropertyChanged(() => EndYearRange);
            }
        }

        public double StartYearRange
        {
            get { return startYearRange; }
            set
            {
                startYearRange = value;
                RaisePropertyChanged(() => StartYearRange);
            }
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