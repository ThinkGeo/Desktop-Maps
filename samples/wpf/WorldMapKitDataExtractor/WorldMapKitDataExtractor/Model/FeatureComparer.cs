using System.Collections.Generic;
using ThinkGeo.MapSuite.Shapes;

namespace WorldMapKitDataExtractor
{
    class FeatureComparer : IEqualityComparer<Feature>
    {
        public bool Equals(Feature x, Feature y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Feature obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
