using System;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class QueryAreaChangedMapModelEventArgs : EventArgs
    {
        private Feature queryFeature;

        public QueryAreaChangedMapModelEventArgs()
            : this(null)
        {
        }

        public QueryAreaChangedMapModelEventArgs(Feature queryFeature)
        {
            this.queryFeature = queryFeature;
        }

        public Feature QueryFeature
        {
            get { return queryFeature; }
            set { queryFeature = value; }
        }
    }
}
