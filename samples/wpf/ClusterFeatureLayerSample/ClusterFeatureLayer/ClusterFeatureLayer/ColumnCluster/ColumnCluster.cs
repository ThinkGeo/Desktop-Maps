using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Drawing;

namespace ThinkGeo.MapSuite.Layers
{
    public abstract class ColumnCluster
    {
        private GeoColor baseColor;
        public string ColumnName { get; set; }

        public ColumnCluster()
            : this(string.Empty, null)
        { }

        public ColumnCluster(string columnName, GeoColor geoColor)
        {
            ColumnName = columnName;
            baseColor = geoColor;
        }

        public GeoColor BaseColor
        {
            get { return baseColor; }
            set { baseColor = value; }
        }

        public abstract GeoColor GetColor(object value);

        public abstract Collection<GeoColor> GetColors();
    }
}
