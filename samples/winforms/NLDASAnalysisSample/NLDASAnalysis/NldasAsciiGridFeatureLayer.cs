using ThinkGeo.MapSuite.Layers;

namespace NLDASAnalysis
{
    public class NldasAsciiGridFeatureLayer : FeatureLayer
    {
        public NldasAsciiGridFeatureLayer(string gridPathFilename)
            :base()
        {
            FeatureSource = new NldasAsciiGridFeatureSource(gridPathFilename);
        }
    }
}
