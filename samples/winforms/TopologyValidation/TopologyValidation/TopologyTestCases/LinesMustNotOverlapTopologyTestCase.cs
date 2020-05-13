using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class LinesMustNotOverlapTopologyTestCase : OneInputLayerTopologyTestCase
    {
        public LinesMustNotOverlapTopologyTestCase()
            : this("LinesMustNotOverlapTopologyTestCase")
        { }

        public LinesMustNotOverlapTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.Line;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.LinesMustNotOverlap(InputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
