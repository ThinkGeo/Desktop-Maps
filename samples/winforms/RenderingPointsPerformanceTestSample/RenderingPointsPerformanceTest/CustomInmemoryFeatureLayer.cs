using System.Collections.Generic;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.Layers
{
    public class CustomInmemoryFeatureLayer : InMemoryFeatureLayer
    {
        public CustomInmemoryFeatureLayer()
            : this(new FeatureSourceColumn[] { }, new Feature[] { })
        { }

        public CustomInmemoryFeatureLayer(IEnumerable<FeatureSourceColumn> featureSourceColumns)
           : this(featureSourceColumns, new Feature[] { })
        {
        }

        public CustomInmemoryFeatureLayer(IEnumerable<FeatureSourceColumn> featureSourceColumns, IEnumerable<Feature> features)
            : base()
        {
            FeatureSource = new CustomInmemoryFeatureSource(featureSourceColumns, features);
        }

        public int GridSizeInPixel
        {
            get { return ((CustomInmemoryFeatureSource)FeatureSource).GridSizeInPixel; }
            set { ((CustomInmemoryFeatureSource)FeatureSource).GridSizeInPixel = value; }
        }
    }
}
