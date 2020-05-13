using System;
using System.Drawing;
using ThinkGeo.MapSuite.Core;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class ValueCircleLegendItem : MyLegendItem
    {
        private double circleArea;

        public ValueCircleLegendItem(float circleArea)
            : base()
        {
            CircleArea = circleArea;
        }

        private ValueCircleLegendItem()
            : this(20)
        { }

        public double CircleArea
        {
            get { return circleArea; }
            set { circleArea = value; }
        }

        protected override void DrawCore(GeoCanvas geoCanvas, Style style)
        {
            Bitmap bitmap = new Bitmap(50, 50);
            RectangleShape worldExtent = new RectangleShape(-180, 90, 180, -90);
            ValueCircleStyle valueCircleStyle = style as ValueCircleStyle;

            double radius = Math.Sqrt(circleArea / Math.PI);

            // Draw Icon
            geoCanvas.BeginDrawing(bitmap, worldExtent, GeographyUnit.DecimalDegree);
            if (valueCircleStyle != null)
            {
                geoCanvas.DrawEllipse(new PointShape(0, 0), (float)(radius * 2), (float)(radius * 2), new GeoPen(new GeoSolidBrush(valueCircleStyle.OuterColor)), new GeoSolidBrush(valueCircleStyle.InnerColor), DrawingLevel.LevelOne);
            }

            geoCanvas.DrawArea(worldExtent, new GeoPen(GeoColor.FromHtml("#cccccc"), 2), DrawingLevel.LabelLevel);
            geoCanvas.EndDrawing();

            //Calculate the value of radius
            double drawingRadius = circleArea / valueCircleStyle.DrawingRadiusRatio * valueCircleStyle.BasedScale / valueCircleStyle.DefaultZoomLevel.Scale;
            double dCircleArea = valueCircleStyle.MaxCircleAreaInDefaultZoomLevel - valueCircleStyle.MinCircleAreaInDefaultZoomLevel;
            double dValue = valueCircleStyle.MaxValidValue - valueCircleStyle.MinValidValue;
            double ratio = dValue / dCircleArea;
            double resultValue = (drawingRadius - valueCircleStyle.MinCircleAreaInDefaultZoomLevel) * ratio + valueCircleStyle.MinValidValue;

            if (resultValue > 0)
            {
                Image = bitmap;
                Title = MapSuiteSampleHelper.GetFormatedStringForLegendItem(valueCircleStyle.ColumnName, resultValue);
            }
            else
            {
                Image = null;
                Title = null;
            }
        }
    }
}