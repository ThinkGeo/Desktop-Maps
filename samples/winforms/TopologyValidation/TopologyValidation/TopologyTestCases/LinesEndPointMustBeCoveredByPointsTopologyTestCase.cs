using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class LinesEndPointMustBeCoveredByPointsTopologyTestCase : PointAndLineInputLayersTopologyTestCase
    {
        public LinesEndPointMustBeCoveredByPointsTopologyTestCase()
            : this("LinesEndPointMustBeCoveredByPointsTopologyTestCase")
        { }

        public LinesEndPointMustBeCoveredByPointsTopologyTestCase(string name)
            : base(name)
        { }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.LinesEndPointMustBeCoveredByPoints(SecondInputFeatureLayer.InternalFeatures, FirstInputFeatureLayer.InternalFeatures);
            }
            
            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (Feature shape in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(shape);
            }
        }
    }
}
