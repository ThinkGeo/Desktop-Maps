using System.Collections.Generic;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace MultiLineLabeling
{
    class MultiLineTextStyle : TextStyle
    {
        public MultiLineTextStyle(string textColumnName, GeoFont textFont, GeoSolidBrush textSolidBrush)

        : base(textColumnName, textFont, textSolidBrush)
            {
                base.SplineType = SplineType.StandardSplining;
            }

        //The strategy for displaying the label at each LineShape making upo the MultilineShape is to treat each LineShape as its own feature.
        protected override void DrawCore(System.Collections.Generic.IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            Collection<Feature> featuresForDrawing = new Collection<Feature>();
            foreach (Feature feature in features)
            {
                if (feature.GetWellKnownType() == WellKnownType.Multiline)
                {
                    MultilineShape multiline = (MultilineShape)(feature.GetShape());

                    foreach (LineShape line in multiline.Lines)
                    {
                        Feature newFeature = new Feature(line);
                        CopyColumnValues(newFeature, feature);
                        featuresForDrawing.Add(newFeature);
                    }
                }
                else
                {
                    featuresForDrawing.Add(feature);
                }
            }

            base.DrawCore(featuresForDrawing, canvas, labelsInThisLayer, labelsInAllLayers);
        }

        private static void CopyColumnValues(Feature targetFeature, Feature sourceFeature)
        {
            targetFeature.ColumnValues.Clear();
            foreach (KeyValuePair<string, string> item in sourceFeature.ColumnValues)
            {
                targetFeature.ColumnValues.Add(item.Key, item.Value);
            }
        }

    }
}
