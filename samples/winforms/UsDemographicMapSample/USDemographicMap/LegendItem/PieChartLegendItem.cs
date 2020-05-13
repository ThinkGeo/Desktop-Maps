using System.Drawing;
using ThinkGeo.MapSuite.Core;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class PieChartLegendItem : MyLegendItem
    {
        private AreaStyle areaStyle;

        public PieChartLegendItem()
            : this(null, string.Empty)
        { }

        public PieChartLegendItem(AreaStyle areaStyle, string title)
            : base()
        {
            AreaStyle = areaStyle;
            Title = title;
        }

        public AreaStyle AreaStyle
        {
            get { return areaStyle; }
            set { areaStyle = value; }
        }

        protected override void DrawCore(GeoCanvas geoCanvas, Style style)
        {
            Bitmap bitmap = new Bitmap(35, 20);
            RectangleShape worldExtent = new RectangleShape(-180, 90, 180, -90);

            // Draw Icon
            geoCanvas.BeginDrawing(bitmap, worldExtent, GeographyUnit.DecimalDegree);
            AreaStyle.Draw(new BaseShape[] { worldExtent }, geoCanvas, new System.Collections.ObjectModel.Collection<SimpleCandidate>(), new System.Collections.ObjectModel.Collection<SimpleCandidate>());
            geoCanvas.DrawArea(worldExtent, new GeoPen(GeoColor.FromHtml("#cccccc"), 2), DrawingLevel.LabelLevel);
            geoCanvas.EndDrawing();

            this.Image = bitmap;
        }
    }
}