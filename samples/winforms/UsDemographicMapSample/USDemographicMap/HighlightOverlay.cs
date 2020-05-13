using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    // This overlay is to highlight a feature when mouse over.
    public class HighlightOverlay : Overlay
    {
        private InMemoryFeatureLayer highlightFeatureLayer;
        private Feature highlightFeature;

        public HighlightOverlay()
            : base()
        {
            highlightFeatureLayer = new InMemoryFeatureLayer();
            highlightFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(100, GeoColor.FromHtml("#449fbc")), GeoColor.FromHtml("#014576"), 1); ;
            highlightFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            highlightFeatureLayer.Open();
        }

        public Feature HighlightFeature
        {
            get { return highlightFeature; }
        }

        public void UpdateHighlightFeature(FeatureLayer featureLayer, PointShape mouseLocation)
        {
            Collection<Feature> features = featureLayer.QueryTools.GetFeaturesContaining(mouseLocation, ReturningColumnsType.AllColumns);
            highlightFeature = features.Count > 0 ? features[0] : null;
        }

        protected override void DrawCore(GeoCanvas canvas)
        {
            if (highlightFeature != null)
            {
                highlightFeatureLayer.Clear();
                highlightFeatureLayer.InternalFeatures.Add(highlightFeature);
                highlightFeatureLayer.Draw(canvas, new Collection<SimpleCandidate>());
            }
        }
    }
}