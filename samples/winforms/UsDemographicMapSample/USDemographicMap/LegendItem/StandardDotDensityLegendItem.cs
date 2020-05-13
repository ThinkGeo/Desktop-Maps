using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using ThinkGeo.MapSuite.Core;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class StandardDotDensityLegendItem : LegendItem
    {
        private int dorCount;
        private GeoBrush labelBrush;
        private GeoFont labelFont;

        public StandardDotDensityLegendItem()
            : this(50)
        { }

        public StandardDotDensityLegendItem(int dotCount)
            : base()
        {
            DotCount = dotCount;
            LabelFont = new GeoFont("Arial", 20, DrawingFontStyles.Bold);
            LabelBrush = new GeoSolidBrush(GeoColor.FromArgb(180, GeoColor.FromHtml("#d3d3d3")));
        }

        public int DotCount
        {
            get { return dorCount; }
            set { dorCount = value; }
        }

        public GeoBrush LabelBrush
        {
            get { return labelBrush; }
            set { labelBrush = value; }
        }

        public GeoFont LabelFont
        {
            get { return labelFont; }
            set { labelFont = value; }
        }

        protected override void DrawCore(GeoCanvas adornmentGeoCanvas, Collection<SimpleCandidate> labelsInAllLayers, LegendDrawingParameters legendDrawingParameters)
        {
            base.DrawCore(adornmentGeoCanvas, labelsInAllLayers, legendDrawingParameters);
            //Bitmap bitmap = new Bitmap(50, 50);

            //DotDensityStyle dotDensityStyle = ImageStyle as DotDensityStyle;
            ////RectangleShape drawingExtent = new RectangleShape(-180, 90, 180, -90);
            //dotDensityStyle.DrawSample(adornmentGeoCanvas, new DrawingRectangleF(0, 0, 50, 50));

            //// Draw Icon Outline
            //adornmentGeoCanvas.BeginDrawing(bitmap, drawingExtent, GeographyUnit.DecimalDegree);
            //adornmentGeoCanvas.DrawArea(drawingExtent, new GeoPen(GeoColor.FromHtml("#cccccc"), 2), DrawingLevel.LevelOne);

            //// Draw Icon points
            //Random random = new Random(DateTime.Now.Millisecond);
            //Collection<BaseShape> mockupPoints = new Collection<BaseShape>();
            //for (int i = 0; i < DotCount; i++)
            //{
            //    double x = random.NextDouble() * (drawingExtent.LowerRightPoint.X - drawingExtent.LowerLeftPoint.X) + drawingExtent.LowerLeftPoint.X;
            //    double y = random.NextDouble() * (drawingExtent.UpperLeftPoint.Y - drawingExtent.LowerLeftPoint.Y) + drawingExtent.LowerLeftPoint.Y;
            //    mockupPoints.Add(new PointShape(x, y));
            //}
            //dotDensityStyle.CustomPointStyle.Draw(mockupPoints, adornmentGeoCanvas, new Collection<SimpleCandidate>(), new Collection<SimpleCandidate>());

            //// Draw Icon Label
            //adornmentGeoCanvas.DrawText(DotCount.ToString("N0"), LabelFont, LabelBrush, null, new ScreenPointF[] { new ScreenPointF(20f, 20f) }, DrawingLevel.LabelLevel, 0, 0, 0);
            //adornmentGeoCanvas.EndDrawing();

            //Title = string.Format(CultureInfo.InvariantCulture, "{0:0.####}", MapSuiteSampleHelper.GetFormatedStringForLegendItem(dotDensityStyle.ColumnName, (DotCount / dotDensityStyle.PointToValueRatio)));
            //Image = bitmap;
        }
    }
}