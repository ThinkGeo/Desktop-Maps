using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class EarthquakeHeatFeatureLayer : FeatureLayer
    {
        private HeatLayer heatLayer;

        public EarthquakeHeatFeatureLayer()
            : this(null)
        { }

        public EarthquakeHeatFeatureLayer(FeatureSource featureSource)
            : base()
        {
            FeatureSource = featureSource;
        }

        public new FeatureSource FeatureSource
        {
            get { return base.FeatureSource; }
            set
            {
                base.FeatureSource = value;
                heatLayer = new HeatLayer(value);
            }
        }

        public HeatStyle HeatStyle
        {
            get { return heatLayer.HeatStyle; }
            set { heatLayer.HeatStyle = value; }
        }

        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            try
            {
                heatLayer.Draw(canvas, labelsInAllLayers);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}