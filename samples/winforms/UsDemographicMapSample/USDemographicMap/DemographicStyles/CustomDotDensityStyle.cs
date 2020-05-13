using System;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    // We custom this DotDensity Style to enhance its drawing on the legend.
    [Serializable]
    internal class CustomDotDensityStyle : DotDensityStyle
    {
        private int drawingPointsNumber;

        public CustomDotDensityStyle()
            : base()
        { }

        public int DrawingPointsNumber
        {
            get { return drawingPointsNumber; }
            set { drawingPointsNumber = value; }
        }

        protected override void DrawSampleCore(GeoCanvas canvas, DrawingRectangleF drawingExtent)
        {
            base.DrawSampleCore(canvas, drawingExtent);
            PointShape upperLeftPoint = ExtentHelper.ToWorldCoordinate(canvas.CurrentWorldExtent, drawingExtent.CenterX - drawingExtent.Width / 2, drawingExtent.CenterY - drawingExtent.Height / 2, canvas.Width, canvas.Height);
            PointShape lowerRightPoint = ExtentHelper.ToWorldCoordinate(canvas.CurrentWorldExtent, drawingExtent.CenterX + drawingExtent.Width / 2, drawingExtent.CenterY + drawingExtent.Height / 2, canvas.Width, canvas.Height);
            RectangleShape rectangle = new RectangleShape(upperLeftPoint, lowerRightPoint);
            rectangle.ScaleDown(10);

            // Here draw the points on Legend Image
            Random random = new Random(DateTime.Now.Millisecond);
            Collection<BaseShape> drawingPoints = new Collection<BaseShape>();
            for (int i = 0; i < DrawingPointsNumber; i++)
            {
                double x = rectangle.LowerLeftPoint.X + random.NextDouble() * (rectangle.Width);
                double y = rectangle.LowerLeftPoint.Y + random.NextDouble() * (rectangle.Height);
                drawingPoints.Add(new PointShape(x, y));
            }
            TextStyle textStyle = new TextStyle(DrawingPointsNumber.ToString(), new GeoFont("Arial", 20, DrawingFontStyles.Bold), new GeoSolidBrush(GeoColor.FromArgb(180, GeoColor.FromHtml("#d3d3d3"))));
            textStyle.DrawSample(canvas, drawingExtent);
            CustomPointStyle.Draw(drawingPoints, canvas, new Collection<SimpleCandidate>(), new Collection<SimpleCandidate>());
        }
    }
}