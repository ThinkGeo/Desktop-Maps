using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.Layers
{
    public class ClusterFeatureLayer : FeatureLayer
    {
        private int diameter;
        private int radius;
        private int legendItemHeight = 20;
        private int legendItemWidth = 190;
        private int cingulaWidth = 70;
        private Collection<FeatureSourceColumn> clusterColumns;
        private static Collection<ColumnCluster> columnClusters;

        public ClusterFeatureLayer()
            : this(new FeatureSourceColumn[] { }, new Feature[] { })
        { }

        public ClusterFeatureLayer(IEnumerable<FeatureSourceColumn> clusterColumns)
          : this(clusterColumns, new Feature[] { })
        { }

        public ClusterFeatureLayer(IEnumerable<FeatureSourceColumn> clusterColumns, IEnumerable<Feature> features, int radius = 20)
            : base()
        {
            FeatureSource = new ClusterFeatureSource(features);

            diameter = radius * 2;
            this.clusterColumns = new Collection<FeatureSourceColumn>();
            foreach (var column in clusterColumns)
                this.clusterColumns.Add(column);

            columnClusters = new Collection<ColumnCluster>();
        }

        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public Collection<FeatureSourceColumn> ClusterColumns
        {
            get { return clusterColumns; }
        }

        public GeoCollection<Feature> InternalFeatures
        {
            get { return ((ClusterFeatureSource)FeatureSource).InternalFeatures; }
        }

        public override bool HasBoundingBox
        {
            get { return true; }
        }

        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            try
            {
                CollectColumnClusters();
                RectangleShape marginWorldExtent = ExtentHelper.ApplyDrawingMarginToExtent(canvas.CurrentWorldExtent, DrawingMarginInPixel, canvas.Width, canvas.Height);
                Collection<Feature> features = FeatureSource.GetFeaturesForDrawing(marginWorldExtent, canvas.Width, canvas.Height, ReturningColumnsType.AllColumns);
                foreach (var feature in features)
                {
                    using (Bitmap bitmap = DrawPieChart(feature))
                    {
                        GeoImage pieChartImage = new GeoImage(bitmap);
                        PointShape centerPoint = feature.GetShape().GetCenterPoint();
                        canvas.DrawWorldImageWithoutScaling(pieChartImage, centerPoint.X, centerPoint.Y, DrawingLevel.LevelOne, -Radius, Radius, 0);

                        bitmap.Dispose();
                        pieChartImage.Dispose();
                    }
                }
                features.Clear();
            }
            finally
            { }
        }

        private void CollectColumnClusters()
        {
            for (int i = 0; i < clusterColumns.Count; i++)
            {
                if (columnClusters.Any(c => c.ColumnName.Equals(clusterColumns[i].ColumnName)))
                    continue;

                ColumnCluster tempColumnCluster = null;
                if (clusterColumns[i].TypeName.Equals("Number"))
                {
                    tempColumnCluster = new NumberColumnCluster(clusterColumns[i].ColumnName, GeoColor.GetRandomGeoColor(RandomColorType.Bright));
                    foreach (var feature in InternalFeatures)
                    {
                        string columnValueString = feature.ColumnValues.ContainsKey(clusterColumns[i].ColumnName) ? feature.ColumnValues[clusterColumns[i].ColumnName] : null;
                        double columnValue;
                        if (columnValueString != null && double.TryParse(columnValueString, out columnValue))
                            ((NumberColumnCluster)tempColumnCluster).Values.Add(columnValue);
                    }
                }
                else
                {
                    tempColumnCluster = new StringColumnCluster(clusterColumns[i].ColumnName, GeoColor.GetRandomGeoColor(RandomColorType.Bright));
                    foreach (var feature in InternalFeatures)
                    {
                        string columnValue = feature.ColumnValues.ContainsKey(clusterColumns[i].ColumnName) ? feature.ColumnValues[clusterColumns[i].ColumnName] : null;
                        if (columnValue != null)
                            ((StringColumnCluster)tempColumnCluster).Values.Add(columnValue);
                    }
                }


                if (tempColumnCluster.BaseColor == null || columnClusters.Any(c => c.BaseColor.Equals(tempColumnCluster.BaseColor)))
                {
                    tempColumnCluster.BaseColor = GeoColor.GetRandomGeoColor(RandomColorType.Bright);
                }
                columnClusters.Add(tempColumnCluster);
            }
        }

        public GeoImage GetLegendImage()
        {
            CollectColumnClusters();
            Bitmap bitmap = new Bitmap(legendItemWidth, legendItemHeight * columnClusters.Count);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < columnClusters.Count; i++)
                {
                    Point lableOffset = new Point(0, legendItemHeight * i);
                    string legendName = GetLegendName(columnClusters[i].ColumnName);
                    graphics.DrawString(legendName, new Font("Calibri", 10), new SolidBrush(Color.Black), lableOffset);

                    int offsetX = legendItemWidth - cingulaWidth;
                    int offsetY = legendItemHeight * i + legendItemHeight / 2;
                    var colors = columnClusters[i].GetColors();
                    int xDuration = cingulaWidth / colors.Count;
                    foreach (var color in colors)
                    {
                        Pen pen = new Pen(ColorTranslator.FromHtml(color.HtmlColor), 10);
                        Point colorBarStartPoint = new Point(offsetX, offsetY);
                        offsetX += xDuration;
                        Point colorBarEndPoint = new Point(offsetX, offsetY);
                        graphics.DrawLine(pen, colorBarStartPoint, colorBarEndPoint);
                    }
                }

                graphics.Dispose();
            }

            return new GeoImage(bitmap);
        }

        private string GetLegendName(string columnName)
        {
            int count = 16;
            int length = columnName.Length;

            string legendName = columnName;
            if (length > count)
            {
                legendName = legendName.Substring(length - count, count);
            }

            return legendName;
        }

        private Bitmap DrawPieChart(Feature feature)
        {
            Bitmap bitmap = new Bitmap(diameter, diameter);
            using (Graphics graphis = Graphics.FromImage(bitmap))
            {
                int columnIndex = 0;
                var pieChartBounds = new Rectangle(new Point(0, 0), new Size(diameter, diameter));
                var drawingColumns = feature.ColumnValues.Where(c => clusterColumns.Any(b => b.ColumnName.Equals(c.Key)));
                float angle = Convert.ToSingle(360D / drawingColumns.Count());
                foreach (var column in drawingColumns)
                {
                    var geoColor = columnClusters.First(c => c.ColumnName.Equals(column.Key)).GetColor(column.Value);
                    var brush = new SolidBrush(ColorTranslator.FromHtml(geoColor.HtmlColor));
                    graphis.FillPie(brush, pieChartBounds, angle * columnIndex, angle);
                    columnIndex++;
                }

                graphis.Dispose();
            }

            return bitmap;
        }
    }
}
