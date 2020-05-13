using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ThinkGeo.MapSuite.Drawing;

namespace ThinkGeo.MapSuite.Layers
{
    public class NumberColumnCluster : ColumnCluster
    {
        private List<double> values;

        public int ColorSteps { get; set; }

        public NumberColumnCluster(string columnName, GeoColor geoColor)
            : base(columnName, geoColor)
        {
            values = new List<double>();
            ColorSteps = 5;
        }

        public List<double> Values
        { get { return values; } }

        public override GeoColor GetColor(object value)
        {
            int index;
            int steps = Values.Distinct().Count();
            values.Sort();
            index = values.IndexOf(Convert.ToDouble(value));
            if (steps <= ColorSteps)
                ColorSteps = steps;
            else
                index = index * ColorSteps / Values.Count;

            var colors = GetColors();

            return colors[ColorSteps - 1 - index];
        }

        public override Collection<GeoColor> GetColors()
        {
            int steps = Values.Distinct().Count();
            if (steps <= ColorSteps)
                ColorSteps = steps;

            return GeoColor.GetColorsInHueFamily(BaseColor, ColorSteps);
        }
    }
}
