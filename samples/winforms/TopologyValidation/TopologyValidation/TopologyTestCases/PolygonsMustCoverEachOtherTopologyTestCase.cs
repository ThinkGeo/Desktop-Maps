using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class PolygonsMustCoverEachOtherTopologyTestCase : TwoInputLayersTopologyTestCase
    {
        public PolygonsMustCoverEachOtherTopologyTestCase()
            : this("PolygonsMustCoverEachOtherTopologyTestCase")
        { }

        public PolygonsMustCoverEachOtherTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.PolygonAndPolygon;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;
            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.PolygonsMustCoverEachOther(FirstInputFeatureLayer.InternalFeatures, SecondInputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }
    }
}
