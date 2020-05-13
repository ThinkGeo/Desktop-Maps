using System;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public abstract class PointAndPolygonInputLayersTopologyTestCase : TwoInputLayersTopologyTestCase
    {
        protected PointAndPolygonInputLayersTopologyTestCase()
            : this("PointAndPolygonInputLayersTopologyTestCase")
        { }

        protected PointAndPolygonInputLayersTopologyTestCase(string name)
            : base(name)
        {
            SecondInputFeatureLayer.ZoomLevelSet = FirstInputFeatureLayer.ZoomLevelSet;
            TestCaseInputType = TestCaseInputType.PointAndPolygon;
        }

        protected override void AddInputShapeCore(BaseShape shape)
        {
            WellKnownType type = shape.GetWellKnownType();
            if (type == WellKnownType.Point || type == WellKnownType.Multipoint)
            {
                FirstInputFeatureLayer.InternalFeatures.Add(shape.GetFeature());
            }
            else if (type == WellKnownType.Polygon || type == WellKnownType.Multipolygon)
            {
                SecondInputFeatureLayer.InternalFeatures.Add(shape.GetFeature());
            }
            else
            {
                throw new Exception("The input shape is wrong");
            }
        }
    }
}
