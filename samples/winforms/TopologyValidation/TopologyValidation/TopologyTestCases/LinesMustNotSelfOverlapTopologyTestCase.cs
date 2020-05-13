using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class LinesMustNotSelfOverlapTopologyTestCase : OneInputLayerTopologyTestCase
    {
        public LinesMustNotSelfOverlapTopologyTestCase()
            : this("LinesMustNotSelfOverlapTopologyTestCase")
        { }

        public LinesMustNotSelfOverlapTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.Line;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.LinesMustNotSelfOverlap(InputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
