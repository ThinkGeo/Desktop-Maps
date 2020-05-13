using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class PolygonsMustBeCoveredByFeatureClassOfPolygonsTopologyTestCase : TwoInputLayersTopologyTestCase
    {
        public PolygonsMustBeCoveredByFeatureClassOfPolygonsTopologyTestCase()
            : this("PolygonsMustBeCoveredByFeatureClassOfPolygonsTopologyTestCase")
        { }

        public PolygonsMustBeCoveredByFeatureClassOfPolygonsTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.PolygonAndPolygon;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.PolygonsMustBeCoveredByFeatureClassOfPolygons(FirstInputFeatureLayer.InternalFeatures, SecondInputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (Feature shape in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(shape);
            }
        }
    }
}
