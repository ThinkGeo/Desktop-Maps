using System.Collections.ObjectModel;
using System.Xml;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public class LinesMustNotSelfIntersectTopologyTestCase : OneInputLayerTopologyTestCase
    {
        public LinesMustNotSelfIntersectTopologyTestCase()
            : this("LinesMustNotSelfIntersectTopologyTestCase")
        { }

        public LinesMustNotSelfIntersectTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.Line;
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> errorShapes = null;
            if (!LoadingFromShapeFile)
            {
                errorShapes = TopologyValidator.LinesMustNotSelfIntersect(InputFeatureLayer.InternalFeatures);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            foreach (var item in errorShapes)
            {
                OutputFeatureLayer.InternalFeatures.Add(item);
            }
        }

        protected override void FromXmlCore(XmlNode node)
        {
            base.FromXmlCore(node);
        }
    }
}
