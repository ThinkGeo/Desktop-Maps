using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class LinesMustNotIntersectTopologyTestCase : OneInputLayerTopologyTestCase
    {
        public LinesMustNotIntersectTopologyTestCase()
            : this("LinesMustNotIntersectTopologyTestCase")
        { }

        public LinesMustNotIntersectTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.Line;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.LinesMustNotIntersect(InputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
