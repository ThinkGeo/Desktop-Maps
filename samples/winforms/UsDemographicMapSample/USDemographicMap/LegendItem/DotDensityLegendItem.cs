using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using ThinkGeo.MapSuite.Core;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class DotDensityLegendItem : MyLegendItem
    {
        private int dorCount;
        private GeoBrush labelBrush;
        private GeoFont labelFont;

        public DotDensityLegendItem()
            : this(50)
        { }

        public DotDensityLegendItem(int dotCount)
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

        protected override void DrawCore(GeoCanvas geoCanvas, Style style)
        {
            Bitmap bitmap = new Bitmap(50, 50);

            CustomDotDensityStyle dotDensityStyle = style as CustomDotDensityStyle;
            RectangleShape drawingExtent = new RectangleShape(-180, 90, 180, -90);

            // Draw Icon Outline
            geoCanvas.BeginDrawing(bitmap, drawingExtent, GeographyUnit.DecimalDegree);
            geoCanvas.DrawArea(drawingExtent, new GeoPen(GeoColor.FromHtml("#cccccc"), 2), DrawingLevel.LevelOne);

            // Draw Icon points
            Random random = new Random(DateTime.Now.Millisecond);
            Collection<BaseShape> mockupPoints = new Collection<BaseShape>();
            for (int i = 0; i < DotCount; i++)
            {
                double x = random.NextDouble() * (drawingExtent.LowerRightPoint.X - drawingExtent.LowerLeftPoint.X) + drawingExtent.LowerLeftPoint.X;
                double y = random.NextDouble() * (drawingExtent.UpperLeftPoint.Y - drawingExtent.LowerLeftPoint.Y) + drawingExtent.LowerLeftPoint.Y;
                mockupPoints.Add(new PointShape(x, y));
            }
            dotDensityStyle.CustomPointStyle.Draw(mockupPoints, geoCanvas, new Collection<SimpleCandidate>(), new Collection<SimpleCandidate>());

            // Draw Icon Label
            geoCanvas.DrawText(DotCount.ToString("N0"), LabelFont, LabelBrush, null, new ScreenPointF[] { new ScreenPointF(20f, 20f) }, DrawingLevel.LabelLevel, 0, 0, 0);
            geoCanvas.EndDrawing();

            Title = string.Format(CultureInfo.InvariantCulture, "{0:0.####}", MapSuiteSampleHelper.GetFormatedStringForLegendItem(dotDensityStyle.ColumnName, (DotCount / dotDensityStyle.PointToValueRatio)));
            Image = bitmap;
        }
    }
}