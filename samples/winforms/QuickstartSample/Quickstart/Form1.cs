using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace Quickstart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(@"../../Data/Countries02.shp");
            AreaStyle areaStyle = new AreaStyle();
            areaStyle.FillSolidBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214));
            areaStyle.OutlinePen = new GeoPen(GeoColor.FromArgb(255, 118, 138, 69), 1);
            areaStyle.OutlinePen.DashStyle = LineDashStyle.Solid;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = areaStyle;
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer capitalLayer = new ShapeFileFeatureLayer(@"../../Data/WorldCapitals.shp");
            PointStyle pointStyle = new PointStyle();
            pointStyle.SymbolType = PointSymbolType.Square;
            pointStyle.SymbolSolidBrush = new GeoSolidBrush(GeoColor.StandardColors.White);
            pointStyle.SymbolPen = new GeoPen(GeoColor.StandardColors.Black, 1);
            pointStyle.SymbolSize = 6;

            PointStyle stackStyle = new PointStyle();
            stackStyle.SymbolType = PointSymbolType.Square;
            stackStyle.SymbolSolidBrush = new GeoSolidBrush(GeoColor.StandardColors.Maroon);
            stackStyle.SymbolPen = new GeoPen(GeoColor.StandardColors.Transparent, 0);
            stackStyle.SymbolSize = 2;

            pointStyle.CustomPointStyles.Add(stackStyle);
            // We can customize our own Style. Here we passed in a color and a size.
            capitalLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.StandardColors.White, 7, GeoColor.StandardColors.Brown);
            // The Style we set here is available from ZoomLevel01 to ZoomLevel05. That means if we zoom in a bit more, the appearance we set here will not be visible anymore.
            capitalLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level05;

            capitalLayer.ZoomLevelSet.ZoomLevel06.DefaultPointStyle = pointStyle;
            // The Style we set here is available from ZoomLevel06 to ZoomLevel20. That means if we zoom out a bit more, the appearance we set here will not be visible any more.
            capitalLayer.ZoomLevelSet.ZoomLevel06.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer capitalLabelLayer = new ShapeFileFeatureLayer(@"../../Data/WorldCapitals.shp");
            // We can customize our own TextStyle. Here we passed in the font, the size, the style and the color.
            GeoFont font = new GeoFont("Arial", 9, DrawingFontStyles.Bold);
            GeoSolidBrush txtBrush = new GeoSolidBrush(GeoColor.StandardColors.Maroon);
            TextStyle textStyle = new TextStyle("CITY_NAME", font, txtBrush);
            textStyle.XOffsetInPixel = 0;
            textStyle.YOffsetInPixel = -6;
            capitalLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.CreateSimpleTextStyle("CITY_NAME", "Arial", 8, DrawingFontStyles.Italic, GeoColor.StandardColors.Black, 3, 3);
            // The TextStyle we set here is available from ZoomLevel01 to ZoomLevel05. 
            capitalLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level05;

            capitalLabelLayer.ZoomLevelSet.ZoomLevel06.DefaultTextStyle = textStyle;
            // The TextStyle we set here is available from ZoomLevel06 to ZoomLevel20. 
            capitalLabelLayer.ZoomLevelSet.ZoomLevel06.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(worldLayer);
            layerOverlay.Layers.Add(capitalLayer);
            layerOverlay.Layers.Add(capitalLabelLayer);

            winformsMap1.Overlays.Add(layerOverlay);
            winformsMap1.CurrentExtent = new RectangleShape(-136.60, 68.06, -53.81, 11.63);

            winformsMap1.Refresh();
        }
    }
}
