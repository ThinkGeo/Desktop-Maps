using System;
using System.Collections.Generic;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class StyleUpdatedDataSelectorUserControlEventArgs : EventArgs
     {
        private DemographicStyleBuilderType demographicStyleBuilderType;
        private List<StyleSelectorUserControl> activatedStyleSelectors;

        public StyleUpdatedDataSelectorUserControlEventArgs()
            : this(DemographicStyleBuilderType.PieChart)
        { }

        public StyleUpdatedDataSelectorUserControlEventArgs(DemographicStyleBuilderType demographicStyleBuilderType)
            : base()
        {
            this.demographicStyleBuilderType = demographicStyleBuilderType;
            this.activatedStyleSelectors = new List<StyleSelectorUserControl>();
        }

        public List<StyleSelectorUserControl> ActivatedStyleSelectors
        {
            get { return activatedStyleSelectors; }
        }

        public DemographicStyleBuilderType DemographicStyleBuilderType
        {
            get { return demographicStyleBuilderType; }
            set { demographicStyleBuilderType = value; }
        }
    }
}
