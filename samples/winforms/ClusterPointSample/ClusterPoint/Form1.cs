/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace ClusterPoint
{
    public partial class Form1 : Form
    {
        private ClassBreakStyle classBreakStyle;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            ClusterPointStyle clusterPointStyle = new ClusterPointStyle();
            clusterPointStyle.MinimumFeaturesPerCellToCluster = 1;
            clusterPointStyle.TextStyle = TextStyles.CreateSimpleTextStyle("FeatureCount", "Arail", 10, DrawingFontStyles.Regular, GeoColor.SimpleColors.Black);
            clusterPointStyle.TextStyle.PointPlacement = PointPlacement.Center;
            clusterPointStyle.TextStyle.OverlappingRule = LabelOverlappingRule.AllowOverlapping;
            clusterPointStyle.DrawingClusteredFeature += ClusterPointStyle_DrawingClusteredFeature;

            ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer("../../AppData/usEarthquake.shp");
            shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(clusterPointStyle);
            shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(shapeFileFeatureLayer);
            winformsMap1.Overlays.Add(layerOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-14471534, 8399738, -6679169, 1689200);
            winformsMap1.Refresh();
        }

        private void ClusterPointStyle_DrawingClusteredFeature(object sender, DrawingClusteredFeatureClusterPointStyleEventArgs e)
        {
            classBreakStyle = classBreakStyle ?? GetClassBreakStyle("FeatureCount");
            e.Styles.Add(classBreakStyle);
        }

        private ClassBreakStyle GetClassBreakStyle(string columnName)
        {
            ClassBreakStyle classBreakStyle = new ClassBreakStyle();
            classBreakStyle.ColumnName = columnName;

            PointStyle pointStyle1 = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(250, 100, 200, 100)), new GeoPen(GeoColor.FromArgb(100, 120, 200, 120), 5), 8);
            classBreakStyle.ClassBreaks.Add(new ClassBreak(1, pointStyle1));

            PointStyle pointStyle2 = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(250, 222, 226, 153)), new GeoPen(GeoColor.FromArgb(100, 222, 226, 153), 8), 15);
            classBreakStyle.ClassBreaks.Add(new ClassBreak(2, pointStyle2));

            PointStyle pointStyle3 = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(250, 255, 183, 76)), new GeoPen(GeoColor.FromArgb(100, 255, 183, 76), 10), 25);
            classBreakStyle.ClassBreaks.Add(new ClassBreak(5, pointStyle3));

            PointStyle pointStyle4 = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(250, 243, 193, 26)), new GeoPen(GeoColor.FromArgb(100, 243, 193, 26), 15), 35);
            classBreakStyle.ClassBreaks.Add(new ClassBreak(10, pointStyle4));

            PointStyle pointStyle5 = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(250, 245, 7, 10)), new GeoPen(GeoColor.FromArgb(100, 245, 7, 10), 15), 40);
            classBreakStyle.ClassBreaks.Add(new ClassBreak(100, pointStyle5));

            PointStyle pointStyle6 = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(250, 245, 7, 10)), new GeoPen(GeoColor.FromArgb(100, 245, 7, 10), 20), 50);
            classBreakStyle.ClassBreaks.Add(new ClassBreak(500, pointStyle6));

            return classBreakStyle;
        }
    }
}
