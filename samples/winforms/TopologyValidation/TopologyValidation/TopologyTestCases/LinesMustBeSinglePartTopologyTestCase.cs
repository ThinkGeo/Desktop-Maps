using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class LinesMustBeSinglePartTopologyTestCase : OneInputLayerTopologyTestCase
    {
        public LinesMustBeSinglePartTopologyTestCase()
            : this("LinesMustBeSinglePartTopologyTestCase")
        { }

        public LinesMustBeSinglePartTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.Line;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.LinesMustBeSinglePart(InputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
