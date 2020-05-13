using System;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace DefaultValueStyle
{
    class CustomDefaultValueStyle : ValueStyle
    {
        private Style defaultStyle = null;
        public Style DefaultStyle
        {
            get
            {
                return defaultStyle;
            }
            set
            {
                defaultStyle = value;
            }
        }

        protected override void DrawCore(System.Collections.Generic.IEnumerable<Feature> features, GeoCanvas canvas, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInThisLayer, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (Feature feature in features)
            {
                string fieldValue = feature.ColumnValues[this.ColumnName].Trim();
                ValueItem valueItem = GetValueItem(fieldValue);

                Feature[] tmpFeatures = new Feature[1] { feature };
                if (valueItem.CustomStyles.Count == 0)
                {
                    if (!string.IsNullOrEmpty(valueItem.Value))
                    {
                        valueItem.DefaultAreaStyle.Draw(tmpFeatures, canvas, labelsInThisLayer, labelsInAllLayers);
                        valueItem.DefaultLineStyle.Draw(tmpFeatures, canvas, labelsInThisLayer, labelsInAllLayers);
                        valueItem.DefaultPointStyle.Draw(tmpFeatures, canvas, labelsInThisLayer, labelsInAllLayers);
                    }
                    else
                    {
                        defaultStyle.Draw(tmpFeatures, canvas, labelsInThisLayer, labelsInAllLayers);
                    }
                    valueItem.DefaultTextStyle.Draw(tmpFeatures, canvas, labelsInThisLayer, labelsInAllLayers);
                }
                else
                {
                    foreach (Style style in valueItem.CustomStyles)
                    {
                        style.Draw(tmpFeatures, canvas, labelsInThisLayer, labelsInAllLayers);
                    }
                }
            }
        }

        private ValueItem GetValueItem(string columnValue)
        {
            ValueItem result = null;

            foreach (ValueItem valueItem in ValueItems)
            {
                if (string.Compare(columnValue, valueItem.Value, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    result = valueItem;
                    break;
                }
            }

            if (result == null)
            {
                result = new ValueItem();
            }

            return result;
        }
    }
}
