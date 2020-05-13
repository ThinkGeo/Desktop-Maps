using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class PolygonsMustNotOverlapTopologyTestCase : OneInputLayerTopologyTestCase
    {
        public PolygonsMustNotOverlapTopologyTestCase()
            : this("PolygonsMustNotOverlapTopologyTestCase")
        { }

        public PolygonsMustNotOverlapTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.Polygon;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.PolygonsMustNotOverlap(InputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();

            foreach (Feature item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
