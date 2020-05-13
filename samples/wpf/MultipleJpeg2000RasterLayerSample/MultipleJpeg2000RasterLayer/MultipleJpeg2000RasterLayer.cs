using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.Layers
{
    public class MultipleJpeg2000RasterLayer : Layer
    {
        private Collection<Jpeg2000RasterLayer> featureLayers;

        public override bool HasBoundingBox
        {
            get
            {
                return true;
            }
        }

        public MultipleJpeg2000RasterLayer()
        {
            featureLayers = new Collection<Jpeg2000RasterLayer>();
        }

        public MultipleJpeg2000RasterLayer(IEnumerable<string> imagePathFilenames)
        {
            featureLayers = new Collection<Jpeg2000RasterLayer>();
            foreach (var imagePathFilename in imagePathFilenames)
            {
                featureLayers.Add(new Jpeg2000RasterLayer(imagePathFilename));
            }
        }

        public MultipleJpeg2000RasterLayer(IEnumerable<string> imagePathFilenames, IEnumerable<string> worldfilePathFilenames)
        {
            featureLayers = new Collection<Jpeg2000RasterLayer>();
            for (int i = 0; i < imagePathFilenames.Count(); i++)
            {
                featureLayers.Add(new Jpeg2000RasterLayer(imagePathFilenames.ElementAt(i), worldfilePathFilenames.ElementAt(i)));
            }
        }

        public MultipleJpeg2000RasterLayer(IEnumerable<string> imagePathFilenames, IEnumerable<RectangleShape> imageExtents)
        {
            featureLayers = new Collection<Jpeg2000RasterLayer>();
            for (int i = 0; i < imagePathFilenames.Count(); i++)
            {
                featureLayers.Add(new Jpeg2000RasterLayer(imagePathFilenames.ElementAt(i), imageExtents.ElementAt(i)));
            }

        }

        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (var featureLayer in featureLayers)
            {
                featureLayer.Draw(canvas, labelsInAllLayers);
            }
        }

        protected override void OpenCore()
        {
            foreach (var featureLayer in featureLayers)
            {
                featureLayer.Open();
            }
        }

        protected override bool IsOpenCore
        {
            get
            {
                base.IsOpenCore = true;
                foreach (var featureLayer in featureLayers)
                {
                    if (!featureLayer.IsOpen) { base.IsOpenCore = false; }
                }
                return base.IsOpenCore;
            }

            set
            {
                base.IsOpenCore = value;
            }
        }

        protected override void CloseCore()
        {
            if (IsOpen)
            {
                foreach (var featureLayer in featureLayers)
                {
                    featureLayer.Close();
                }
            }
        }

        protected override Layer CloneDeepCore()
        {
            MultipleJpeg2000RasterLayer newLayer = new MultipleJpeg2000RasterLayer();
            foreach (var featureLayer in featureLayers)
            {
                newLayer.featureLayers.Add(featureLayer.CloneDeep() as Jpeg2000RasterLayer);
            }
            return newLayer;
        }

        protected override RectangleShape GetBoundingBoxCore()
        {
            RectangleShape boundingBox = null;
            foreach (var featureLayer in featureLayers)
            {
                if (boundingBox == null)
                {
                    boundingBox = featureLayer.GetBoundingBox();
                }
                else
                {
                    boundingBox.ExpandToInclude(featureLayer.GetBoundingBox());
                }
            }

            return boundingBox;
        }
    }
}
