using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class PolygonsMustContainPointTopologyTestCase : PointAndPolygonInputLayersTopologyTestCase
    {
        public PolygonsMustContainPointTopologyTestCase()
            : this("PolygonsMustContainPointTopologyTestCase")
        { }

        public PolygonsMustContainPointTopologyTestCase(string name)
            : base(name)
        { }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;

            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.PolygonsMustContainPoint(SecondInputFeatureLayer.InternalFeatures, FirstInputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (Feature shape in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(shape);
            }
        }
    }
}
