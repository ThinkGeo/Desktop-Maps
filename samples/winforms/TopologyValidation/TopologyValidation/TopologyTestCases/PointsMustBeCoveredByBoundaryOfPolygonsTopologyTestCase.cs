using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class PointsMustBeCoveredByBoundaryOfPolygonsTopologyTestCase : PointAndPolygonInputLayersTopologyTestCase
    {
        public PointsMustBeCoveredByBoundaryOfPolygonsTopologyTestCase()
            : this("PointsMustBeCoveredByBoundaryOfPolygonsTopologyTestCase")
        { }

        public PointsMustBeCoveredByBoundaryOfPolygonsTopologyTestCase(string name)
            : base(name)
        { }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.PointsMustBeCoveredByBoundaryOfPolygons(FirstInputFeatureLayer.InternalFeatures, SecondInputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
