using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class PointsMustBeCoveredByLinesTopologyTestCase : PointAndLineInputLayersTopologyTestCase
    {
        public PointsMustBeCoveredByLinesTopologyTestCase()
            : this("PointsMustBeCoveredByLinesTopologyTestCase")
        { }

        public PointsMustBeCoveredByLinesTopologyTestCase(string name)
            : base(name)
        { }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.PointsMustBeCoveredByLines(FirstInputFeatureLayer.InternalFeatures, SecondInputFeatureLayer.InternalFeatures);
            }
          
            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
