using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class LinesMustNotHavePseudonodesTopologyTestCase : OneInputLayerTopologyTestCase
    {
        public LinesMustNotHavePseudonodesTopologyTestCase()
            : this("LinesMustNotHavePseudonodesTopologyTestCase")
        { }

        public LinesMustNotHavePseudonodesTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.Line;            
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.LinesMustNotHavePseudonodes(InputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
