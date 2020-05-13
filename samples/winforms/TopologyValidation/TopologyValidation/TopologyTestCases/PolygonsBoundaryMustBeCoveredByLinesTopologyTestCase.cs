using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class PolygonsBoundaryMustBeCoveredByLinesTopologyTestCase : LineAndPolygonInputLayersTopologyTestCase
    {
        public PolygonsBoundaryMustBeCoveredByLinesTopologyTestCase()
            : this("PolygonsBoundaryMustBeCoveredByLinesTopologyTestCase")
        { }

        public PolygonsBoundaryMustBeCoveredByLinesTopologyTestCase(string name)
            : base(name)
        { }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;

            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.PolygonsBoundaryMustBeCoveredByLines(SecondInputFeatureLayer.InternalFeatures, FirstInputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
