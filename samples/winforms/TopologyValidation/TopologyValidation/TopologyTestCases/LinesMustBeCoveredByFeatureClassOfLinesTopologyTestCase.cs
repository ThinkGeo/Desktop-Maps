using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class LinesMustBeCoveredByFeatureClassOfLinesTopologyTestCase : TwoInputLayersTopologyTestCase
    {
        public LinesMustBeCoveredByFeatureClassOfLinesTopologyTestCase()
            : this("LinesMustBeCoveredByFeatureClassOfLinesTopologyTestCase")
        { }

        public LinesMustBeCoveredByFeatureClassOfLinesTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.LineAndLine;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;

            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.LinesMustBeCoveredByFeatureClassOfLines(SecondInputFeatureLayer.InternalFeatures, FirstInputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
