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
    public abstract class TwoInputLayersTopologyTestCase : TopologyTestCase
    {
        private InMemoryFeatureLayer firstInputFeatureLayer;
        private InMemoryFeatureLayer secondInputFeatureLayer;
        private string firstShapeFilePathName;
        private string secondShapeFilePathName;
        private InputFeatureLayerType inputFeatureLayerType;

        public InputFeatureLayerType InputFeatureLayerType
        {
            get { return inputFeatureLayerType; }
            set { inputFeatureLayerType = value; }
        }
        public InMemoryFeatureLayer FirstInputFeatureLayer
        {
            get { return firstInputFeatureLayer; }
        }

        public InMemoryFeatureLayer SecondInputFeatureLayer
        {
            get { return secondInputFeatureLayer; }
        }

        public string FirstShapeFilePathName
        {
            get { return firstShapeFilePathName; }
            set { firstShapeFilePathName = value; }
        }

        public string SecondShapeFilePathName
        {
            get { return secondShapeFilePathName; }
            set { secondShapeFilePathName = value; }
        }

        protected TwoInputLayersTopologyTestCase()
            : this("TwoInputLayersTopologyTestCase")
        { }

        protected TwoInputLayersTopologyTestCase(string name)
            : base(name)
        {
            firstInputFeatureLayer = new InMemoryFeatureLayer();
            secondInputFeatureLayer = new InMemoryFeatureLayer();

            firstInputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(102, 0, 0, 255), GeoColor.FromArgb(255, 0, 0, 255), 2);
            firstInputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromArgb(102, 0, 0, 255), 2, true);
            firstInputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.FromArgb(102, 0, 0, 255), 12);
            firstInputFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            secondInputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(102, 0, 255, 0), GeoColor.FromArgb(255, 0, 255, 0), 2);
            secondInputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromArgb(102, 0, 255, 0), 2, true);
            secondInputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.FromArgb(102, 0, 255, 0), 12);
            secondInputFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            Layers = new Collection<InMemoryFeatureLayer>();

            Layers.Add(firstInputFeatureLayer);
            Layers.Add(secondInputFeatureLayer);
            Layers.Add(OutputFeatureLayer);
        }


        protected override void AddInputShapeCore(BaseShape shape)
        {
            if (InputFeatureLayerType == InputFeatureLayerType.First)
            {
                FirstInputFeatureLayer.InternalFeatures.Add(shape.GetFeature());
            }
            else if (InputFeatureLayerType == InputFeatureLayerType.Second)
            {
                SecondInputFeatureLayer.InternalFeatures.Add(shape.GetFeature());
            }
            else
            {
                throw new Exception("The InputType is wrong");
            }
        }

        protected override RectangleShape GetBoundingBoxCore()
        {
            RectangleShape boundingBox = null;
            if (!LoadingFromShapeFile)
            {
                if (firstInputFeatureLayer.InternalFeatures.Count > 0)
                {
                    firstInputFeatureLayer.Open();
                    boundingBox = firstInputFeatureLayer.GetBoundingBox();
                }

                if (secondInputFeatureLayer.InternalFeatures.Count > 0)
                {
                    secondInputFeatureLayer.Open();
                    if (boundingBox != null)
                    {
                        boundingBox.ExpandToInclude(secondInputFeatureLayer.GetBoundingBox());
                    }
                    else
                    {
                        boundingBox = secondInputFeatureLayer.GetBoundingBox();
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(firstShapeFilePathName))
                {
                    ShapeFileFeatureLayer coveredShapeFileFeatureLayer = new ShapeFileFeatureLayer(firstShapeFilePathName);
                    coveredShapeFileFeatureLayer.Open();
                    boundingBox = coveredShapeFileFeatureLayer.GetBoundingBox();
                    coveredShapeFileFeatureLayer.Close();
                }
                if (!string.IsNullOrEmpty(secondShapeFilePathName))
                {
                    ShapeFileFeatureLayer coveringShapeFileFeatureLayer = new ShapeFileFeatureLayer(secondShapeFilePathName);
                    coveringShapeFileFeatureLayer.Open();
                    if (boundingBox == null)
                    {
                        boundingBox = coveringShapeFileFeatureLayer.GetBoundingBox();
                    }
                    else
                    {
                        boundingBox.ExpandToInclude(coveringShapeFileFeatureLayer.GetBoundingBox());
                    }
                    coveringShapeFileFeatureLayer.Close();
                }
            }
            return boundingBox;
        }

        protected override TopologyTestCase CloneCore()
        {
            TwoInputLayersTopologyTestCase newObject = (TwoInputLayersTopologyTestCase)Activator.CreateInstance(GetType());
            for (int i = 0; i < Layers.Count; i++)
            {
                for (int j = 0; j < Layers[i].InternalFeatures.Count; j++)
                {
                    newObject.Layers[i].InternalFeatures.Add(Layers[i].InternalFeatures[j]);
                }
            }
            return newObject;
        }

        protected override void ClearCore()
        {
            base.ClearCore();
            firstInputFeatureLayer.InternalFeatures.Clear();
            secondInputFeatureLayer.InternalFeatures.Clear();
        }

        protected override TopologyTestCase GenerateTestCaseWithinExtentCore(RectangleShape extent)
        {
            OutputFeatureLayer.Open();
            firstInputFeatureLayer.Open();
            secondInputFeatureLayer.Open();

            Collection<Feature> outputFeatures = OutputFeatureLayer.QueryTools.GetFeaturesInsideBoundingBox(extent, ReturningColumnsType.NoColumns);
            Collection<Feature> inputPointFeatures = null;
            Collection<Feature> inputPolygonFeatures = null;

            if (!LoadingFromShapeFile)
            {
                inputPointFeatures = firstInputFeatureLayer.QueryTools.GetFeaturesInsideBoundingBox(extent, ReturningColumnsType.NoColumns);
                inputPolygonFeatures = secondInputFeatureLayer.QueryTools.GetFeaturesInsideBoundingBox(extent, ReturningColumnsType.NoColumns);
            }
            else
            {
                ShapeFileFeatureLayer pointShapeFileFeatureLayer = new ShapeFileFeatureLayer(firstShapeFilePathName);
                pointShapeFileFeatureLayer.Open();
                inputPointFeatures = pointShapeFileFeatureLayer.QueryTools.GetFeaturesInsideBoundingBox(extent, ReturningColumnsType.NoColumns);

                ShapeFileFeatureLayer polygonShapeFileFeatureLayer = new ShapeFileFeatureLayer(secondShapeFilePathName);
                polygonShapeFileFeatureLayer.Open();
                inputPointFeatures = polygonShapeFileFeatureLayer.QueryTools.GetFeaturesInsideBoundingBox(extent, ReturningColumnsType.NoColumns);
            }
            TwoInputLayersTopologyTestCase topologyTestCase = (TwoInputLayersTopologyTestCase)Activator.CreateInstance(GetType());
            foreach (Feature feature in outputFeatures)
            {
                topologyTestCase.OutputFeatureLayer.InternalFeatures.Add(feature);
            }

            foreach (Feature feature in inputPointFeatures)
            {
                topologyTestCase.FirstInputFeatureLayer.InternalFeatures.Add(feature);
            }

            foreach (Feature feature in inputPolygonFeatures)
            {
                topologyTestCase.SecondInputFeatureLayer.InternalFeatures.Add(feature);
            }

            return topologyTestCase;
        }

        protected override void FromXmlCore(XmlNode node)
        {
            XmlNode nodeTypeName = node.SelectSingleNode("./TypeName");

            Collection<Feature> outputFeatures = new Collection<Feature>();
            Collection<Feature> inputPointFeatures = new Collection<Feature>();
            Collection<Feature> inputPolygonFeatures = new Collection<Feature>();

            XmlNode nodeID = node.SelectSingleNode("./ID");
            XmlNode nodeName = node.SelectSingleNode("./Name");
            XmlNode nodeDescription = node.SelectSingleNode("./Description");
            XmlNode nodeInput = node.SelectSingleNode("./Input");

            XmlNodeList nodesWKT;

            XmlNode nodePointShapes = nodeInput.SelectSingleNode("./FirstInputShapes");
            if (nodePointShapes != null)
            {
                nodesWKT = nodePointShapes.SelectNodes("./WKT");
                foreach (XmlNode nodeShape in nodesWKT)
                {
                    string wkt = nodeShape.InnerText;
                    inputPointFeatures.Add(new Feature(wkt));
                }
            }
            XmlNode nodePolygonShapes = nodeInput.SelectSingleNode("./SecondInputShapes");
            if (nodePolygonShapes != null)
            {
                nodesWKT = nodePolygonShapes.SelectNodes("./WKT");
                foreach (XmlNode nodeShape in nodesWKT)
                {
                    string wkt = nodeShape.InnerText;
                    inputPolygonFeatures.Add(new Feature(wkt));
                }
            }

            XmlNode nodeOutput = node.SelectSingleNode("./Output");
            nodesWKT = nodeOutput.SelectNodes("./WKT");
            foreach (XmlNode nodeShape in nodesWKT)
            {
                string wkt = nodeShape.InnerText;
                outputFeatures.Add(new Feature(wkt));
            }

            this.Name = nodeName.InnerText;
            this.Description = nodeDescription.InnerText;
            this.Id = nodeID.InnerText;
            foreach (Feature feature in inputPointFeatures)
            {
                this.FirstInputFeatureLayer.InternalFeatures.Add(feature);
            }

            foreach (Feature feature in inputPolygonFeatures)
            {
                this.SecondInputFeatureLayer.InternalFeatures.Add(feature);
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
            typeNameNode.InnerText = GetType().ToString();
            caseNode.AppendChild(typeNameNode);

            XmlElement idNode = doc.CreateElement("ID");
            idNode.InnerText = Guid.NewGuid().ToString();
            caseNode.AppendChild(idNode);

            XmlElement nameNode = doc.CreateElement("Name");
            nameNode.InnerText = Name + (count > 1 ? "_" + (count - 1) : ""); ;
            caseNode.AppendChild(nameNode);

            XmlElement desNode = doc.CreateElement("Description");
            desNode.InnerText = Description;
            caseNode.AppendChild(desNode);

            XmlElement inNode = doc.CreateElement("Input");
            caseNode.AppendChild(inNode);

            XmlElement inCoveredNode = doc.CreateElement("FirstInputShapes");
            inNode.AppendChild(inCoveredNode);
            foreach (var feature in FirstInputFeatureLayer.InternalFeatures)
            {
                XmlElement wktNode = doc.CreateElement("WKT");
                wktNode.InnerText = feature.GetWellKnownText();
                inCoveredNode.AppendChild(wktNode);
            }

            XmlElement inCoverringNode = doc.CreateElement("SecondInputShapes");
            inNode.AppendChild(inCoverringNode);
            foreach (var feature in SecondInputFeatureLayer.InternalFeatures)
            {
                XmlElement wktNode = doc.CreateElement("WKT");
                wktNode.InnerText = feature.GetWellKnownText();
                inCoverringNode.AppendChild(wktNode);
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

        protected override void DrawCore(WinformsMap winformsMap)
        {
            LayerOverlay layerOverlay = new LayerOverlay();
            if (!LoadingFromShapeFile)
            {
                foreach (var item in Layers)
                {
                    layerOverlay.Layers.Add(item);
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(firstShapeFilePathName))
                {
                    ShapeFileFeatureLayer.BuildIndexFile(firstShapeFilePathName, BuildIndexMode.DoNotRebuild);
                    ShapeFileFeatureLayer firstInputShapeFileFeatureLayer = new ShapeFileFeatureLayer(firstShapeFilePathName);
                    firstInputShapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(102, 0, 255, 0), GeoColor.FromArgb(255, 0, 255, 0), 2);
                    firstInputShapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromArgb(102, 0, 255, 0), 2, true);
                    firstInputShapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.FromArgb(102, 0, 255, 0), 12);
                    firstInputShapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                    layerOverlay.Layers.Add(firstInputShapeFileFeatureLayer);
                }

                if (!String.IsNullOrEmpty(secondShapeFilePathName))
                {
                    ShapeFileFeatureLayer.BuildIndexFile(secondShapeFilePathName, BuildIndexMode.DoNotRebuild);
                    ShapeFileFeatureLayer secondInputShapeFileFeatureLayer = new ShapeFileFeatureLayer(secondShapeFilePathName);
                    secondInputShapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(102, 0, 0, 255), GeoColor.FromArgb(255, 0, 0, 255), 2);
                    secondInputShapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromArgb(102, 0, 0, 255), 2, true);
                    secondInputShapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.FromArgb(102, 0, 0, 255), 12);
                    secondInputShapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                    layerOverlay.Layers.Add(secondInputShapeFileFeatureLayer);
                }
                layerOverlay.Layers.Add(OutputFeatureLayer);
            }
            winformsMap.Overlays.Clear();
            winformsMap.Overlays.Add(layerOverlay);
            winformsMap.Refresh();
        }
    }
}
