using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class LinesMustNotHaveDanglesTopologyTestCase : OneInputLayerTopologyTestCase
    {
        public LinesMustNotHaveDanglesTopologyTestCase()
            : this("LinesMustNotHaveDanglesTopologyTestCase")
        { }

        public LinesMustNotHaveDanglesTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.Line;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;

            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.LinesMustNotHaveDangles(InputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
