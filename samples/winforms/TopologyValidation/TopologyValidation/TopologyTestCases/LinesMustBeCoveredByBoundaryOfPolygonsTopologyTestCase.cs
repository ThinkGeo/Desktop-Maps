using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class LinesMustBeCoveredByBoundaryOfPolygonsTopologyTestCase : LineAndPolygonInputLayersTopologyTestCase
    {
        public LinesMustBeCoveredByBoundaryOfPolygonsTopologyTestCase()
            : this("LinesMustBeCoveredByBoundaryOfPolygonsTopologyTestCase")
        { }

        public LinesMustBeCoveredByBoundaryOfPolygonsTopologyTestCase(string name)
            : base(name)
        { }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;

            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.LinesMustBeCoveredByBoundaryOfPolygons(FirstInputFeatureLayer.InternalFeatures, SecondInputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
