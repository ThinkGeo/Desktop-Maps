using System.Collections.Generic;
using ThinkGeo.Core;

namespace NauticalChartsViewer
{
    public class FeatureInfo : ObservableObject
    {
        private Dictionary<string, string> columnValues;
        private string id;
        private string layerName;
        private string objectClass;
        private WellKnownType geometry;
        private double area = double.MaxValue;

        public FeatureInfo(Feature feature, NauticalChartsFeatureDescription description, string layerName, double area)
        {
            this.id = feature.Id;
            this.layerName = layerName;
            this.geometry = feature.GetWellKnownType();

            // Show the plain-language object class and attributes translated by the SDK's
            // S-57 catalogue (NauticalChartsFeatureLayer.GetFeatureDescription) instead of the
            // raw OBJL / coded attributes and ISO 8211 record fields.
            this.objectClass = string.IsNullOrEmpty(description.ObjectClassAcronym)
                ? "Unknown"
                : string.IsNullOrEmpty(description.ObjectClassName)
                    ? description.ObjectClassAcronym
                    : description.ObjectClassName + " (" + description.ObjectClassAcronym + ")";

            Dictionary<string, string> temp = new Dictionary<string, string>();
            foreach (NauticalChartsFeatureAttribute attribute in description.Attributes)
            {
                if (!string.IsNullOrEmpty(attribute.Value))
                {
                    temp[attribute.Name] = attribute.Value;
                }
            }

            this.columnValues = temp;
            this.area = area;
        }

        public string Id
        {
            get { return id; }
        }

        public string LayerName
        {
            get { return layerName; }
        }

        public string ObjectClass
        {
            get { return objectClass; }
        }

        public double Area
        {
            get { return area; }
        }

        public WellKnownType Geometry
        {
            get { return geometry; }
        }

        public Dictionary<string, string> ColumnValues
        {
            get { return columnValues; }
        }
    }
}
