using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class LinesMustNotOverlapWithLinesTopologyTestCase : TwoInputLayersTopologyTestCase
    {
        public LinesMustNotOverlapWithLinesTopologyTestCase()
            : this("LinesMustNotOverlapWithLinesTopologyTestCase")
        { }

        public LinesMustNotOverlapWithLinesTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.LineAndLine;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = new Collection<Feature>();
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.LinesMustNotOverlapWithLines(FirstInputFeatureLayer.InternalFeatures, SecondInputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
