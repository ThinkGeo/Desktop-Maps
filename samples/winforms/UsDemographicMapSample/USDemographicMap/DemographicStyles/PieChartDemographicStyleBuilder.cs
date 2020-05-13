using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;
using ZedGraph;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class PieChartDemographicStyleBuilder : DemographicStyleBuilder
    {
        private Collection<string> selectedColumnAliases;
        private Collection<GeoColor> pieColors;

        public PieChartDemographicStyleBuilder()
            : this(new string[] { })
        { }

        public PieChartDemographicStyleBuilder(IEnumerable<string> selectedColumns)
            : base(selectedColumns)
        {
            Opacity = 200;
            Color = GeoColor.SimpleColors.LightBlue;
            selectedColumnAliases = new Collection<string>();
        }

        public Collection<string> SelectedColumnAliases
        {
            get { return selectedColumnAliases; }
        }

        protected override Collection<Style> GetStylesCore(FeatureSource featureSource)
        {
            // here we generated the PieZedGraphStyle.
            PieZedGraphStyle zedGraphStyle = new PieZedGraphStyle();
            zedGraphStyle.ZedGraphDrawing += ZedGraphStyle_ZedGraphDrawing;
            pieColors = GeoColor.GetColorsInQualityFamily(GeoColor.FromArgb(Opacity, Color), SelectedColumns.Count);

            for (int i = 0; i < SelectedColumns.Count; i++)
            {
                zedGraphStyle.RequiredColumnNames.Add(SelectedColumns[i]);
                zedGraphStyle.PieSlices.Add(SelectedColumnAliases[i], pieColors[i]);
            }

            return new Collection<Style>() { zedGraphStyle };
        }

        private void ZedGraphStyle_ZedGraphDrawing(object sender, ZedGraphDrawingEventArgs e)
        {
            ZedGraphControl zedGraph = new ZedGraphControl();
            zedGraph.Size = new Size(100, 100);

            zedGraph.GraphPane.Fill.Type = FillType.None;
            zedGraph.GraphPane.Chart.Fill.Type = FillType.None;

            zedGraph.GraphPane.Border.IsVisible = false;
            zedGraph.GraphPane.Chart.Border.IsVisible = false;
            zedGraph.GraphPane.XAxis.IsVisible = false;
            zedGraph.GraphPane.YAxis.IsVisible = false;
            zedGraph.GraphPane.Legend.IsVisible = false;
            zedGraph.GraphPane.Title.IsVisible = false;

            for (int i = 0; i < SelectedColumns.Count; i++)
            {
                double value = 0;
                if (!double.TryParse(e.Feature.ColumnValues[SelectedColumns[i]], out value))
                {
                    zedGraph.Dispose();
                    return;
                }
                Color color = System.Drawing.Color.FromArgb(pieColors[i].AlphaComponent, pieColors[i].RedComponent, pieColors[i].GreenComponent, pieColors[i].BlueComponent);
                PieItem pieItem = zedGraph.GraphPane.AddPieSlice(value, color, 0.08, "");
                pieItem.LabelDetail.IsVisible = false;
            }
            zedGraph.AxisChange();

            e.GeoImage = new GeoImage(zedGraph.GraphPane.GetImage());
            zedGraph.Dispose();
        }
    }
}