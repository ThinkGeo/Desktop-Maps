using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class PointsMustBeCoveredByEndPointOfLinesTopologyTestCase : PointAndLineInputLayersTopologyTestCase
    {
        public PointsMustBeCoveredByEndPointOfLinesTopologyTestCase()
            : this("PointsMustBeCoveredByEndPointOfLinesTopologyTestCase")
        { }

        public PointsMustBeCoveredByEndPointOfLinesTopologyTestCase(string name)
            : base(name)
        { }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.PointsMustBeCoveredByEndPointOfLines(FirstInputFeatureLayer.InternalFeatures, SecondInputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
