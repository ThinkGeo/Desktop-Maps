using System.Collections.Generic;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.Layers
{
    public class ClusterFeatureSource : FeatureSource
    {
        private GeoCollection<Feature> internalFeatures;

        public ClusterFeatureSource()
            : this(new Feature[] { })
        { }

        public ClusterFeatureSource( IEnumerable<Feature> features)
            : base()
        {
            InMemoryFeatureSourceConstructor(features);
        }

        public ClusterFeatureSource(IEnumerable<BaseShape> shapes)
            : base()
        {
            Collection<Feature> features = new Collection<Feature>();
            foreach (BaseShape shape in shapes)
            {
                features.Add(new Feature(shape));
            }

            InMemoryFeatureSourceConstructor(features);
        }

        public GeoCollection<Feature> InternalFeatures
        {
            get { return internalFeatures; }
            set { internalFeatures = value; }
        }

        protected override Collection<Feature> GetAllFeaturesCore(IEnumerable<string> returningColumnNames)
        {
            Collection<Feature> returnFeatures = new Collection<Feature>();
            foreach (Feature feature in internalFeatures)
            {
                foreach (string columnName in returningColumnNames)
                {
                    if (!feature.ColumnValues.ContainsKey(columnName))
                        feature.ColumnValues.Add(columnName, "");
                }
                returnFeatures.Add(feature);
            }

            return returnFeatures;
        }

        private void InMemoryFeatureSourceConstructor(IEnumerable<Feature> features)
        {
            this.internalFeatures = new GeoCollection<Feature>();
            foreach (Feature feature in features)
            {
                this.internalFeatures.Add(feature.Id, feature);
            }
        }
    }
}
