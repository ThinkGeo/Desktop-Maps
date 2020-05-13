using System;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public abstract class PointAndLineInputLayersTopologyTestCase : TwoInputLayersTopologyTestCase
    {
        protected PointAndLineInputLayersTopologyTestCase()
            : this("PointAndLineInputLayersTopologyTestCase")
        { }

        protected PointAndLineInputLayersTopologyTestCase(string name)
            : base(name)
        {
            SecondInputFeatureLayer.ZoomLevelSet = FirstInputFeatureLayer.ZoomLevelSet;
            TestCaseInputType = TestCaseInputType.PointAndLine;
        }

        protected override void AddInputShapeCore(BaseShape shape)
        {
            WellKnownType type = shape.GetWellKnownType();
            if (type == WellKnownType.Point || type == WellKnownType.Multipoint)
            {
                FirstInputFeatureLayer.InternalFeatures.Add(shape.GetFeature());
            }
            else if (type == WellKnownType.Line || type == WellKnownType.Multiline)
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
