using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class PolygonsMustNotHaveGapsTopologyTestCase : OneInputLayerTopologyTestCase
    {
        public PolygonsMustNotHaveGapsTopologyTestCase()
            : this("PolygonsMustNotHaveGapsTopologyTestCase")
        { }

        public PolygonsMustNotHaveGapsTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.Polygon;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.PolygonsMustNotHaveGaps(InputFeatureLayer.InternalFeatures);
            }
           
            OutputFeatureLayer.InternalFeatures.Clear();

            foreach (Feature shape in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(shape);
            }
        }
    }
}
