using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ThinkGeo.MapSuite.Drawing;

namespace ThinkGeo.MapSuite.Layers
{
    public class StringColumnCluster : ColumnCluster
    {
        private List<string> values;

        public StringColumnCluster(string columnName, GeoColor geoColor)
            : base(columnName, geoColor)
        {
            values = new List<string>();
        }

        public List<string> Values
        {
            get { return values; }
        }

        public override GeoColor GetColor(object value)
        {
            var valueGroups = Values.GroupBy(v => v).ToList();
            valueGroups.Sort((compareValue1, compareValue2) =>
            {
                if (compareValue1.Count() < compareValue2.Count()) return -1;
                else if (compareValue1.Count() < compareValue2.Count()) return 0;
                else return 1;
            });
            int steps = valueGroups.Count();
            var colors = GeoColor.GetColorsInHueFamily(BaseColor, steps);
            int index = valueGroups.FindIndex(v => v.Key.Equals(value));
            return colors[index];
        }

        public override Collection<GeoColor> GetColors()
        {
            var valueGroups = Values.GroupBy(v => v);
            return GeoColor.GetColorsInHueFamily(BaseColor, valueGroups.Count());
        }
    }
}
