using System;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.Layers
{
    public class HereMapRealTimeTrafficLayer : FeatureLayer
    {
        private bool enableEmbeddedStyle;
        private ClassBreakStyle classBreakStyle;

        public HereMapRealTimeTrafficLayer()
            : this(null, string.Empty, string.Empty)
        { }

        public HereMapRealTimeTrafficLayer(Uri serverUri, string appId, string appCode)
        {
            enableEmbeddedStyle = true;

            classBreakStyle = new ClassBreakStyle();
            classBreakStyle.ColumnName = "jf";
            classBreakStyle.ClassBreaks.Add(new ClassBreak(0, new LineStyle(new GeoPen(GeoColor.SimpleColors.Green, 2.0f))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(4, new LineStyle(new GeoPen(GeoColor.SimpleColors.Yellow, 2.0f))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(8, new LineStyle(new GeoPen(GeoColor.SimpleColors.Red, 2.0f))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(10, new LineStyle(new GeoPen(GeoColor.SimpleColors.Black, 2.0f))));

            FeatureSource = new HereMapRealTimeTrafficSource(serverUri, appId, appCode);
        }

        public bool EnableEmbeddedStyle
        {
            get { return enableEmbeddedStyle; }
            set { enableEmbeddedStyle = value; }
        }

        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            if (enableEmbeddedStyle)
            {
                var drawingFeatures = FeatureSource.GetFeaturesForDrawing(canvas.CurrentWorldExtent, canvas.Width, canvas.Height, ReturningColumnsType.AllColumns);
                classBreakStyle.Draw(drawingFeatures, canvas, new Collection<SimpleCandidate>(), labelsInAllLayers);
            }
            else
                base.DrawCore(canvas, labelsInAllLayers);
        }
    }
}
