using System;
using ThinkGeo.MapSuite.Layers;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class DataSelectedCustomDataSelectorUserControlEventArgs : EventArgs
    {
        public DataSelectedCustomDataSelectorUserControlEventArgs()
            : this(null)
        { }

        public DataSelectedCustomDataSelectorUserControlEventArgs(ShapeFileFeatureLayer shapeFileFeatureLayer)
        {
            ShapeFileFeatureLayer = shapeFileFeatureLayer;
        }

        public ShapeFileFeatureLayer ShapeFileFeatureLayer { get; set; }
    }
}