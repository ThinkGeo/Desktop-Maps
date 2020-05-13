using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class DotDensityDemographicStyleBuilder : DemographicStyleBuilder
    {
        private double dotDensityValue;

        public DotDensityDemographicStyleBuilder()
            : this(new string[] { })
        { }

        public DotDensityDemographicStyleBuilder(IEnumerable<string> selectedColumns)
            : base(selectedColumns)
        {
            Opacity = 255;
            DotDensityValue = 50;
            Color = GeoColor.SimpleColors.DarkRed;
        }

        public double DotDensityValue
        {
            get { return dotDensityValue; }
            set { dotDensityValue = value; }
        }

        protected override Collection<Style> GetStylesCore(FeatureSource featureSource)
        {
            // here we generate CustomDotDensityStyle.
            double totalValue = 0;
            featureSource.Open();
            int featureCount = featureSource.GetCount();
            for (int i = 0; i < featureCount; i++)
            {
                Feature feature = featureSource.GetFeatureById((i + 1).ToString(CultureInfo.InvariantCulture), SelectedColumns);
                double columnValue;

                if (double.TryParse(feature.ColumnValues[SelectedColumns[0]], out columnValue))
                {
                    totalValue += columnValue;
                }
            }
            featureSource.Close();

            CustomDotDensityStyle dotDensityStyle = new CustomDotDensityStyle();
            dotDensityStyle.ColumnName = SelectedColumns[0];
            dotDensityStyle.PointToValueRatio = DotDensityValue / (totalValue / featureCount);
            dotDensityStyle.CustomPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.FromArgb(Opacity, Color), 4);

            return new Collection<Style>() { dotDensityStyle };
        }
    }
}