using System.Collections.Generic;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.Styles
{
    internal class WorldStreetsCompositeStyle : CompositeStyle
    {
        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInThisLayer, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInAllLayers)
        {
            Collection<Feature> tempFeatures = new Collection<Feature>();

            foreach (var feature in features)
            {
                tempFeatures.Add(feature);
                base.DrawCore(tempFeatures, canvas, labelsInThisLayer, labelsInAllLayers);
                tempFeatures.Remove(feature);
            }
        }
    }
}