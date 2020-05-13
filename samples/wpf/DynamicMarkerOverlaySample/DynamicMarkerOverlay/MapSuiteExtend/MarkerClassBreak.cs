using System;
using ThinkGeo.MapSuite.Wpf;

namespace DynamicMarkerOverlay
{
    public class MarkerClassBreak
    {
        private double breakValue;
        private PointMarkerStyle defaultMarkerStyle;

        public MarkerClassBreak() : this(Double.MinValue, new PointMarkerStyle())
        { }

        public MarkerClassBreak(double value, PointMarkerStyle markerStyle)
        {
            this.breakValue = value;
            this.DefaultMarkerStyle = markerStyle;
        }

        public double Value
        {
            get { return breakValue; }
            set { breakValue = value; }
        }

        public PointMarkerStyle DefaultMarkerStyle
        {
            get
            {
                if (defaultMarkerStyle == null)
                {
                    defaultMarkerStyle = new PointMarkerStyle();
                }
                return defaultMarkerStyle;
            }
            set
            {
                defaultMarkerStyle = value;
            }
        }
    }
}
