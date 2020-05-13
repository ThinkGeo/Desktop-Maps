using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class PolygonsBoundaryMustBeCoveredByBoundaryOfPolygonsTopologyTestCase : TwoInputLayersTopologyTestCase
    {
        public PolygonsBoundaryMustBeCoveredByBoundaryOfPolygonsTopologyTestCase()
            : this("PolygonsBoundaryMustBeCoveredByBoundaryOfPolygonsTopologyTestCase")
        { }

        public PolygonsBoundaryMustBeCoveredByBoundaryOfPolygonsTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.PolygonAndPolygon;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;

            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.PolygonsBoundaryMustBeCoveredByBoundaryOfPolygons(FirstInputFeatureLayer.InternalFeatures, SecondInputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
