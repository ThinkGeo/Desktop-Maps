using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class PolygonsMustNotOverlapWithPolygonsTopologyTestCase : TwoInputLayersTopologyTestCase
    {
        public PolygonsMustNotOverlapWithPolygonsTopologyTestCase()
            : this("PolygonsMustNotOverlapWithPolygonsTopologyTestCase")
        { }

        public PolygonsMustNotOverlapWithPolygonsTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.PolygonAndPolygon;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.PolygonsMustNotOverlapWithPolygons(FirstInputFeatureLayer.InternalFeatures, SecondInputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
