using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.Layers
{
    public class CustomInmemoryFeatureSource : InMemoryFeatureSource
    {
        private int gridSizeInPixel;

        private CustomInmemoryFeatureSource()
            : this(new FeatureSourceColumn[] { }, new Feature[] { })
        { }

        public CustomInmemoryFeatureSource(IEnumerable<FeatureSourceColumn> featureSourceColumns)
            : this(featureSourceColumns, new Feature[] { })
        { }

        public CustomInmemoryFeatureSource(IEnumerable<FeatureSourceColumn> featureSourceColumns, IEnumerable<Feature> features)
            : base(featureSourceColumns, features)
        {
            gridSizeInPixel = 0;
        }

        public int GridSizeInPixel
        {
            get { return gridSizeInPixel; }
            set { gridSizeInPixel = value; }
        }

        protected override Collection<Feature> GetFeaturesForDrawingCore(RectangleShape boundingBox, double screenWidth, double screenHeight, IEnumerable<string> returningColumnNames)
        {
            Collection<Feature> drawingFeatures = base.GetFeaturesForDrawingCore(boundingBox, screenWidth, screenHeight, returningColumnNames);
            Collection<Feature> simplifiedDrawingFeautres = new Collection<Feature>();
            if (GridSizeInPixel < 1)
            {
                simplifiedDrawingFeautres = drawingFeatures;
            }
            else
            {
                Dictionary<string, bool> grids = new Dictionary<string, bool>();
                RectangleShape layerBoundingBox = new RectangleShape(-180, 90, 180, -90);
                double resolutionX = boundingBox.Width / screenWidth * GridSizeInPixel;
                double resolutionY = boundingBox.Height / screenHeight * GridSizeInPixel;

                foreach (var feature in drawingFeatures)
                {
                    byte[] wkb = feature.GetWellKnownBinary();
                    double x = BitConverter.ToDouble(wkb, 5);
                    double y = BitConverter.ToDouble(wkb, 13);

                    int gridCol = Convert.ToInt32(Math.Floor((x - layerBoundingBox.UpperLeftPoint.X) / resolutionX));
                    int gridRow = Convert.ToInt32(Math.Floor((layerBoundingBox.UpperLeftPoint.Y - y) / resolutionY));

                    if (!grids.ContainsKey($"{gridCol}-{gridRow}"))
                    {
                        simplifiedDrawingFeautres.Add(feature);
                        grids.Add($"{gridCol}-{gridRow}", true);
                    }
                }
            }
            return simplifiedDrawingFeautres;
        }
    }
}
