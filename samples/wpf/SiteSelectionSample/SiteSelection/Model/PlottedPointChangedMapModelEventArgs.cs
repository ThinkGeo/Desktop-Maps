using System;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.SiteSelection
{
    [Serializable]
    public class PlottedPointChangedMapModelEventArgs : EventArgs
    {
        private PointShape plottedPoint;

        public PlottedPointChangedMapModelEventArgs()
            : this(null)
        { }

        public PlottedPointChangedMapModelEventArgs(PointShape plottedPoint)
        {
            this.plottedPoint = plottedPoint;
        }

        public PointShape PlottedPoint
        {
            get { return plottedPoint; }
        }
    }
}