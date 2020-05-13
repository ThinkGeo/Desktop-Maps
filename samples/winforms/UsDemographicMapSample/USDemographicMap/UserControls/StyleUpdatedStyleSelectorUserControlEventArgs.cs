using System;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class StyleUpdatedStyleSelectorUserControlEventArgs : EventArgs
    {
        private DemographicStyleBuilderType demographicStyleBuilderType;

        public StyleUpdatedStyleSelectorUserControlEventArgs()
            : this(DemographicStyleBuilderType.PieChart)
        { }

        public StyleUpdatedStyleSelectorUserControlEventArgs(DemographicStyleBuilderType demographicStyleBuilderType)
            : base()
        {
            this.demographicStyleBuilderType = demographicStyleBuilderType;
        }

        public DemographicStyleBuilderType DemographicStyleBuilderType
        {
            get { return demographicStyleBuilderType; }
            set { demographicStyleBuilderType = value; }
        }
    }
}