using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class LinesMustNotIntersectOrTouchInteriorTopologyTestCase : OneInputLayerTopologyTestCase
    {
        public LinesMustNotIntersectOrTouchInteriorTopologyTestCase()
            : this("LinesMustNotIntersectOrTouchInteriorTopologyTestCase")
        { }

        public LinesMustNotIntersectOrTouchInteriorTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.Line;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.LinesMustNotIntersectOrTouchInterior(InputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
