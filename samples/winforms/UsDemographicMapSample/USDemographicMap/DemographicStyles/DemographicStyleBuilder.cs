using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    // We have the StyleBuilder to generate different styles
    public abstract class DemographicStyleBuilder
    {
        private int opacity;
        private GeoColor color;
        private Collection<string> selectedColumns;

        protected DemographicStyleBuilder()
            : this(new Collection<string>())
        { }

        protected DemographicStyleBuilder(IEnumerable<string> selectedColumns)
        {
            this.Opacity = 100;
            this.color = GeoColor.FromHtml("#F1F369");
            this.selectedColumns = new Collection<string>(selectedColumns.ToList());
        }

        // The generated style would based on the columns we selected here.
        // Multiple columns are required For PieChart style, only one column is needed for other styles in this sample.
        public Collection<string> SelectedColumns
        {
            get { return selectedColumns; }
        }

        public GeoColor Color
        {
            get { return color; }
            set { color = value; }
        }

        public int Opacity
        {
            get { return opacity; }
            protected set { opacity = value; }
        }

        public Collection<Style> GetStyles(FeatureSource featureSource)
        {
            return GetStylesCore(featureSource);
        }

        protected abstract Collection<Style> GetStylesCore(FeatureSource featureSource);
    }
}