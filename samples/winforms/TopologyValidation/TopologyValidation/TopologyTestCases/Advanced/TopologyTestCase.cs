using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace TopologyValidation
{
    public abstract class TopologyTestCase
    {
        private string id;
        private string name;
        private string description;
        private bool loadingFromShapeFile;
        private InMemoryFeatureLayer outputFeatureLayer;
        private Collection<InMemoryFeatureLayer> layers;
        private TestCaseInputType testCaseInputType = TestCaseInputType.Default;

        protected TopologyTestCase()
            : this("TopologyTestCase")
        { }

        protected TopologyTestCase(string name)
        {
            this.name = name;
            outputFeatureLayer = new InMemoryFeatureLayer();
            outputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(new GeoColor(180, GeoColor.StandardColors.Red), GeoColor.StandardColors.Black);
            outputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(new GeoColor(180, GeoColor.StandardColors.Red), 2, true);
            outputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(new GeoColor(180, GeoColor.StandardColors.Red), 12);
            outputFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            layers = new Collection<InMemoryFeatureLayer>();
            layers.Add(OutputFeatureLayer);
        }

        public TestCaseInputType TestCaseInputType
        {
            get { return testCaseInputType; }
            set { testCaseInputType = value; }
        }

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public InMemoryFeatureLayer OutputFeatureLayer
        {
            get
            {
                return outputFeatureLayer;
            }
            protected set
            {
                outputFeatureLayer = value;
            }
        }


        public Collection<InMemoryFeatureLayer> Layers
        {
            get
            {
                return layers;
            }
            protected set
            {
                layers = value;
            }
        }

        public bool LoadingFromShapeFile
        {
            get
            {
                return loadingFromShapeFile;
            }
            set
            {
                loadingFromShapeFile = value;
            }
        }

        public void GenerateTestResult()
        {
            GenerateTestResultCore();
        }

        protected abstract void GenerateTestResultCore();

        public TopologyTestCase Clone()
        {
            return CloneCore();
        }

        protected abstract TopologyTestCase CloneCore();

        public void AddInputShape(BaseShape shape)
        {
            AddInputShapeCore(shape);
        }

        protected abstract void AddInputShapeCore(BaseShape shape);

        public void FromXml(XmlNode node)
        {
            FromXmlCore(node);
        }

        protected abstract void FromXmlCore(XmlNode node);

        public void ToXml(string path)
        {
            ToXmlCore(path);
        }

        protected abstract void ToXmlCore(string path);

        public void Clear()
        {
            ClearCore();
        }

        protected virtual void ClearCore()
        {
            loadingFromShapeFile = false;
            OutputFeatureLayer.InternalFeatures.Clear();
        }

        public RectangleShape GetBoundingBox()
        {
            return GetBoundingBoxCore();
        }

        protected abstract RectangleShape GetBoundingBoxCore();

        public TopologyTestCase GenerateTestCaseWithinExtent(RectangleShape extent)
        {
            return GenerateTestCaseWithinExtentCore(extent);
        }

        protected abstract TopologyTestCase GenerateTestCaseWithinExtentCore(RectangleShape extent);

        public void Draw(WinformsMap winformsMap)
        {
            DrawCore(winformsMap);
        }

        protected abstract void DrawCore(WinformsMap winformsMap);

        public virtual string GetOutputInString()
        {
            StringBuilder sb = new StringBuilder();
            string result = string.Empty;
            if (OutputFeatureLayer.InternalFeatures.Count > 0)
            {
                foreach (Feature feature in OutputFeatureLayer.InternalFeatures)
                {
                    sb.AppendLine(feature.GetWellKnownText() + ";");
                }
                result = sb.ToString();
                result = result.Remove(result.LastIndexOf(";"));
            }
            return result;
        }
    }
}
