using System.Drawing;
using ThinkGeo.MapSuite.Core;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class ThematicLegendItem : MyLegendItem
    {
        private ClassBreak startClassBreak;
        private ClassBreak endClassBreak;

        public ThematicLegendItem(ClassBreak startClassBreak, ClassBreak endClassBreak)
            : base()
        {
            StartClassBreak = startClassBreak;
            EndClassBreak = endClassBreak;
        }

        private ThematicLegendItem()
            : this(null, null)
        { }

        public ClassBreak StartClassBreak
        {
            get { return startClassBreak; }
            set { startClassBreak = value; }
        }

        public ClassBreak EndClassBreak
        {
            get { return endClassBreak; }
            set { endClassBreak = value; }
        }

        protected override void DrawCore(GeoCanvas geoCanvas, Style style)
        {
            Bitmap bitmap = new Bitmap(35, 20);
            ClassBreakStyle classBreakStyle = style as ClassBreakStyle;
            RectangleShape worldExtent = new RectangleShape(-180, 90, 180, -90);

            // Draw Icon
            geoCanvas.BeginDrawing(bitmap, worldExtent, GeographyUnit.DecimalDegree);
            if (StartClassBreak.DefaultAreaStyle != null)
            {
                StartClassBreak.DefaultAreaStyle.Draw(new BaseShape[] { worldExtent }, geoCanvas, new System.Collections.ObjectModel.Collection<SimpleCandidate>(), new System.Collections.ObjectModel.Collection<SimpleCandidate>());
            }
            geoCanvas.DrawArea(worldExtent, new GeoPen(GeoColor.FromHtml("#cccccc"), 2), DrawingLevel.LabelLevel);
            geoCanvas.EndDrawing();

            this.Image = bitmap;
            if (EndClassBreak != null)
            {
                this.Title = string.Format("{0:#,0.####} ~ {1:#,0.####}",
                    MapSuiteSampleHelper.GetFormatedStringForLegendItem(classBreakStyle.ColumnName, StartClassBreak.Value),
                    MapSuiteSampleHelper.GetFormatedStringForLegendItem(classBreakStyle.ColumnName, EndClassBreak.Value));
            }
            else
            {
                this.Title = string.Format("> {0:#,0.####}",
                    MapSuiteSampleHelper.GetFormatedStringForLegendItem(classBreakStyle.ColumnName, StartClassBreak.Value));
            }
        }
    }
}