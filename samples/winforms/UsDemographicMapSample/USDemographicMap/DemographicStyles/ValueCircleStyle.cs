using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    // This custom style is for displaying circles with different sizes based on the value.
    public class ValueCircleStyle : Style
    {
        private string columnName;
        private double baseScale;
        private double minValidValue;
        private double maxValidValue;
        private double drawingRadiusRatio;
        private double maxCircleRadiusInDefaultZoomLevel;
        private double minCircleRadiusInDefaultZoomLevel;
        private GeoColor innerColor;
        private GeoColor outerColor;
        private ZoomLevel defaultZoomLevel;

        public ValueCircleStyle()
            : base()
        {
            ZoomLevelSet zoomLevelSet = new ZoomLevelSet();
            defaultZoomLevel = zoomLevelSet.ZoomLevel04;
            baseScale = zoomLevelSet.ZoomLevel05.Scale;

            drawingRadiusRatio = 1;
            outerColor = GeoColor.FromArgb(255, 10, 20, 255);
            innerColor = GeoColor.FromArgb(100, 10, 20, 255);
            minCircleRadiusInDefaultZoomLevel = 10;
            maxCircleRadiusInDefaultZoomLevel = 100;
        }

        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }

        public ZoomLevel DefaultZoomLevel
        {
            get { return defaultZoomLevel; }
            set { defaultZoomLevel = value; }
        }

        public double DrawingRadiusRatio
        {
            get { return drawingRadiusRatio; }
            set { drawingRadiusRatio = value; }
        }

        public GeoColor InnerColor
        {
            get { return innerColor; }
            set { innerColor = value; }
        }

        // The data might be dramatically different but we don't want any circle be super large
        // on the map, so here we have this property to identify the max circle we can have on map.
        public double MaxCircleAreaInDefaultZoomLevel
        {
            get { return maxCircleRadiusInDefaultZoomLevel; }
            set { maxCircleRadiusInDefaultZoomLevel = value; }
        }

        public double MaxValidValue
        {
            get { return maxValidValue; }
            set { maxValidValue = value; }
        }

        // The data might be dramatically different but we don't want any circle be super tiny
        // on the map, so here we have this property to identify the min circle we can have on map.
        public double MinCircleAreaInDefaultZoomLevel
        {
            get { return minCircleRadiusInDefaultZoomLevel; }
            set { minCircleRadiusInDefaultZoomLevel = value; }
        }

        public double MinValidValue
        {
            get { return minValidValue; }
            set { minValidValue = value; }
        }

        public GeoColor OuterColor
        {
            get { return outerColor; }
            set { outerColor = value; }
        }

        public double BasedScale
        {
            get { return baseScale; }
        }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            double ratio = (maxValidValue - minValidValue) / (maxCircleRadiusInDefaultZoomLevel - minCircleRadiusInDefaultZoomLevel);

            // here we calculate the size of each circle based on every feature's value.
            foreach (Feature feature in features)
            {
                double value = 0;
                if (!double.TryParse(feature.ColumnValues[columnName], out value))
                {
                    continue;
                }

                if (value > maxValidValue || value < minValidValue)
                {
                    continue;
                }

                double drawingDefaultCircleArea = (value - minValidValue) / ratio + minCircleRadiusInDefaultZoomLevel;

                double graphArea = drawingDefaultCircleArea * defaultZoomLevel.Scale / baseScale * drawingRadiusRatio;
                double graphHeght = Math.Sqrt(graphArea / Math.PI);

                PointShape center = feature.GetShape().GetCenterPoint();
                canvas.DrawEllipse(center, (float)(graphHeght * 2), (float)(graphHeght * 2), new GeoPen(outerColor, 1), new GeoSolidBrush(innerColor), DrawingLevel.LevelOne);
            }
        }

        protected override Collection<string> GetRequiredColumnNamesCore()
        {
            Collection<string> requiredFieldNames = new Collection<string>();
            requiredFieldNames.Add(columnName);

            return requiredFieldNames;
        }
    }
}