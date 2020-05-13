using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public abstract class LineAndPolygonInputLayersTopologyTestCase : TwoInputLayersTopologyTestCase
    {
        protected LineAndPolygonInputLayersTopologyTestCase()
            : this("LineAndPolygonInputLayersTopologyTestCase")
        { }

        protected LineAndPolygonInputLayersTopologyTestCase(string name)
            : base(name)
        {
            SecondInputFeatureLayer.ZoomLevelSet = FirstInputFeatureLayer.ZoomLevelSet;
            TestCaseInputType = TestCaseInputType.LineAndPolygon;
        }

        protected override void AddInputShapeCore(BaseShape shape)
        {
            WellKnownType type = shape.GetWellKnownType();
            if (type == WellKnownType.Line || type == WellKnownType.Multiline)
            {
                FirstInputFeatureLayer.InternalFeatures.Add(shape.GetFeature());
            }
            else if (type == WellKnownType.Polygon || type == WellKnownType.Multipolygon)
            {
                SecondInputFeatureLayer.InternalFeatures.Add(shape.GetFeature());
            }
        }
    }
}
