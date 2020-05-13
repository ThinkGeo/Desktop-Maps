using System.Collections.Generic;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    // We custom this class just make it a bit easier to use.
    public class PieZedGraphStyle : ZedGraphStyle
    {
        private Dictionary<string, GeoColor> pieSlices;

        public PieZedGraphStyle()
        {
            pieSlices = new Dictionary<string, GeoColor>();
        }

        public Dictionary<string, GeoColor> PieSlices
        {
            get { return pieSlices; }
        }
    }
}