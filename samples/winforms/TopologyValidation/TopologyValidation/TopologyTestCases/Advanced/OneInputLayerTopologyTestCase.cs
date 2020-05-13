using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace TopologyValidation
{
    public abstract class OneInputLayerTopologyTestCase : TopologyTestCase
    {
        private string shapePathFileName;
        private InMemoryFeatureLayer inputFeatureLayer;

        public InMemoryFeatureLayer InputFeatureLayer
        {
            get { return inputFeatureLayer; }
        }

        public string ShapePathFileName
        {
            get { return shapePathFileName; }
            set { shapePathFileName = value; }
        }

        protected OneInputLayerTopologyTestCase()
            : this("OneInputLayerTopologyTestCase")
        { }

        protected OneInputLayerTopologyTestCase(string name)
            : base(name)
        {
            inputFeatureLayer = new InMemoryFeatureLayer();
            inputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(102, 0, 0, 255), GeoColor.FromArgb(255, 0, 0, 255), 2);
            inputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromArgb(102, 0, 0, 255), 2, true);
            inputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.FromArgb(102, 0, 0, 255), 12);
            inputFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            Layers = new Collection<InMemoryFeatureLayer>();
            Layers.Add(inputFeatureLayer);
            Layers.Add(OutputFeatureLayer);
        }

        protected override void AddInputShapeCore(BaseShape shape)
        {
            inputFeatureLayer.InternalFeatures.Add(shape.GetFeature());
        }

        protected override void FromXmlCore(XmlNode node)
        {
            Collection<Feature> inputFeatures = new Collection<Feature>();
            Collection<Feature> outputFeatures = new Collection<Feature>();

            XmlNode nodeTypeName = node.SelectSingleNode("./TypeName");
            XmlNode nodeName = node.SelectSingleNode("./Name");
            XmlNode nodeDescription = node.SelectSingleNode("./Description");
            XmlNode nodeInput = node.SelectSingleNode("./Input");
            XmlNode nodeID = node.SelectSingleNode("./ID");

            XmlNodeList nodesWKT = nodeInput.SelectNodes("./WKT");
            foreach (XmlNode nodeShape in nodesWKT)
            {
                string wkt = nodeShape.InnerText;
                inputFeatures.Add(new Feature(wkt));
            }

            XmlNode nodeOutput = node.SelectSingleNode("./Output");
            nodesWKT = nodeOutput.SelectNodes("./WKT");
            foreach (XmlNode nodeShape in nodesWKT)
            {
                string wkt = nodeShape.InnerText;
                outputFeatures.Add(new Feature(wkt));
            }

            this.Name = nodeName.InnerText;
            this.Id = nodeID.InnerText;
            this.Description = nodeDescription.InnerText;
            foreach (Feature feature in inputFeatures)
            {
                this.InputFeatureLayer.InternalFeatures.Add(feature);
            }

            foreach (Feature feature in outputFeatures)
            {
                this.OutputFeatureLayer.InternalFeatures.Add(feature);
            }
        }

        protected override void ToXmlCore(string path)
        {
            XmlDocument doc = new XmlDocument();
            int count = 1;
            string tempPath = path + ".xml";
            while (File.Exists(tempPath))
            {
                tempPath = path + "_" + count + ".xml";
                count++;
            }

            XmlElement caseNode = doc.CreateElement("Case");
            doc.AppendChild(caseNode);

            XmlElement typeNameNode = doc.CreateElement("TypeName");
            typeNameNode.InnerText = this.GetType().ToString();
            caseNode.AppendChild(typeNameNode);

            XmlElement idNode = doc.CreateElement("ID");
            idNode.InnerText = Guid.NewGuid().ToString();
            caseNode.AppendChild(idNode);

            XmlElement nameNode = doc.CreateElement("Name");
            nameNode.InnerText = Name + (count > 1 ? "_" + (count - 1) : "");
            caseNode.AppendChild(nameNode);

            XmlElement desNode = doc.CreateElement("Description");
            desNode.InnerText = Description;
            caseNode.AppendChild(desNode);

            XmlElement inNode = doc.CreateElement("Input");
            caseNode.AppendChild(inNode);
            foreach (var feature in InputFeatureLayer.InternalFeatures)
            {
                XmlElement wktNode = doc.CreateElement("WKT");
                wktNode.InnerText = feature.GetWellKnownText();
                inNode.AppendChild(wktNode);
            }

            XmlElement outNode = doc.CreateElement("Output");
            caseNode.AppendChild(outNode);
            foreach (var feature in OutputFeatureLayer.InternalFeatures)
            {
                XmlElement wktNode = doc.CreateElement("WKT");
                wktNode.InnerText = feature.GetWellKnownText();
                outNode.AppendChild(wktNode);
            }

            doc.Save(tempPath);
        }

        protected override void ClearCore()
        {
            base.ClearCore();
            shapePathFileName = string.Empty;
            inputFeatureLayer.InternalFeatures.Clear();
        }

        protected override RectangleShape GetBoundingBoxCore()
        {
            RectangleShape boundingBox;
            if (!LoadingFromShapeFile)
            {
                InputFeatureLayer.Open();
                boundingBox = InputFeatureLayer.GetBoundingBox();
                InputFeatureLayer.Close();
            }
            else
            {
                ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(shapePathFileName);
                shapeFileFeatureLayer.Open();
                boundingBox = shapeFileFeatureLayer.GetBoundingBox();
                shapeFileFeatureLayer.Close();
            }

            return boundingBox;
        }

        protected override void DrawCore(WinformsMap winformsMap)
        {
            LayerOverlay layerOverlay = null;
            if (!LoadingFromShapeFile)
            {
                layerOverlay = new LayerOverlay();

                foreach (Layer layer in Layers)
                {
                    layerOverlay.Layers.Add(layer);
                }
            }
            else
            {
                ShapeFileFeatureLayer.BuildIndexFile(ShapePathFileName, BuildIndexMode.DoNotRebuild);
                ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(ShapePathFileName);
                shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(102, 0, 0, 255), GeoColor.FromArgb(255, 0, 0, 255), 2);
                shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromArgb(102, 0, 0, 255), 2, true);
                shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.FromArgb(102, 0, 0, 255), 12);
                shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                layerOverlay = new LayerOverlay();
                layerOverlay.Layers.Add(shapeFileFeatureLayer);
                layerOverlay.Layers.Add(OutputFeatureLayer);
                winformsMap.CurrentExtent = GetBoundingBox();
            }

            winformsMap.Overlays.Clear();
            winformsMap.Overlays.Add(layerOverlay);
            winformsMap.Refresh();
        }

        protected override TopologyTestCase CloneCore()
        {
            OneInputLayerTopologyTestCase newObject = (OneInputLayerTopologyTestCase)Activator.CreateInstance(GetType());
            for (int i = 0; i < Layers.Count; i++)
            {
                for (int j = 0; j < Layers[i].InternalFeatures.Count; j++)
                {
                    newObject.Layers[i].InternalFeatures.Add(Layers[i].InternalFeatures[j]);
                }
            }
            return newObject;
        }

        protected override TopologyTestCase GenerateTestCaseWithinExtentCore(RectangleShape extent)
        {
            if (!OutputFeatureLayer.IsOpen)
            {
                OutputFeatureLayer.Open();
            }
            Collection<Feature> outputFeatures = OutputFeatureLayer.QueryTools.GetFeaturesInsideBoundingBox(extent, ReturningColumnsType.NoColumns);
            Collection<Feature> inputFeatures = null;
            if (!LoadingFromShapeFile)
            {
                if (!InputFeatureLayer.IsOpen)
                {
                    InputFeatureLayer.Open();
                }

                inputFeatures = InputFeatureLayer.QueryTools.GetFeaturesInsideBoundingBox(extent, ReturningColumnsType.NoColumns);
            }
            else
            {
                ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(ShapePathFileName);
                shapeFileFeatureLayer.Open();
                inputFeatures = shapeFileFeatureLayer.QueryTools.GetFeaturesInsideBoundingBox(extent, ReturningColumnsType.NoColumns);
                shapeFileFeatureLayer.Close();
            }
            OneInputLayerTopologyTestCase topologyTestCase = (OneInputLayerTopologyTestCase)Activator.CreateInstance(GetType());
            foreach (Feature feature in outputFeatures)
            {
                topologyTestCase.OutputFeatureLayer.InternalFeatures.Add(feature);
            }

            foreach (Feature feature in inputFeatures)
            {
                topologyTestCase.InputFeatureLayer.InternalFeatures.Add(feature);
            }

            return topologyTestCase;
        }
    }
}
