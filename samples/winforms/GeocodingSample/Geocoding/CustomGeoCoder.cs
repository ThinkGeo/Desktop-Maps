using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.MapSuiteGeocoder;

namespace HowDoISamples
{
    public class CustomGeoCoder : Geocoder
    {
        private string indexDataPath;

        public CustomGeoCoder() :this(string.Empty)
        { 
        }

        public string DataPath
        {
            get
            {
                return indexDataPath;
            }
            set
            {
                indexDataPath = value;
            }
        }

        public CustomGeoCoder(string dataPath)
            : base()
        {
            this.indexDataPath = dataPath;
            matchingPlugInCollection = new Collection<MatchingPlugIn>();
        }

        protected override MatchResult MatchCore(string sourceText)
        {
            MatchResult result = base.MatchCore(sourceText);
            foreach (MatchItem item in result.MatchItems)
            {
                item.MatchPairs = NormalizeCoordinates(item.MatchPairs);
            }
            return result;
        }
    }
}
