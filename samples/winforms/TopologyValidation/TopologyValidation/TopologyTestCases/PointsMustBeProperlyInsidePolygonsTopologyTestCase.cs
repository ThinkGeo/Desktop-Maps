using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class PointsMustBeProperlyInsidePolygonsTopologyTestCase : PointAndPolygonInputLayersTopologyTestCase
    {
        public PointsMustBeProperlyInsidePolygonsTopologyTestCase()
            : this("PointsMustBeProperlyInsidePolygonsTopologyTestCase")
        { }

        public PointsMustBeProperlyInsidePolygonsTopologyTestCase(string name)
            : base(name)
        { }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.PointsMustBeProperlyInsidePolygons(FirstInputFeatureLayer.InternalFeatures, SecondInputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
