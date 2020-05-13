using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace  LabelingBasedOnSize
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-120,86,112,-54);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            //Layer for simply displaying countries as a simple area style.
            ShapeFileFeatureLayer worldShapeLayer = new ShapeFileFeatureLayer("../../data/Countries02.shp");
            worldShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.LightGoldenrodYellow, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green));
            worldShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Layer for displaying the country labels using ClassBreakStyle to have the label size proportinal to the country size.
            ShapeFileFeatureLayer worldLabelLayer = new ShapeFileFeatureLayer("../../data/Countries02.shp");
            
            //For Zoom Levels 01 to 03, displays the country labels for countries above 500,000 sqkm in area with two classes.
            ClassBreakStyle classBreakStyle1 = new ClassBreakStyle("SQKM");
            TextStyle textStyle1a = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 9, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 2, 0, 0);
            TextStyle textStyle1b = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 12, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 3, 0, 0);
            classBreakStyle1.ClassBreaks.Add(new ClassBreak(500000, textStyle1a));
            classBreakStyle1.ClassBreaks.Add(new ClassBreak(5000000, textStyle1b));
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakStyle1);
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level03;

            //For Zoom Levels 04 to 05, displays the country labels for countries above 100,000 sqkm in area with three classes.
            ClassBreakStyle classBreakStyle2 = new ClassBreakStyle("SQKM");
            TextStyle textStyle2a = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 9, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 2, 0, 0);
            TextStyle textStyle2b = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 11, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 2, 0, 0);
            TextStyle textStyle2c = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 14, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 3, 0, 0);
            classBreakStyle2.ClassBreaks.Add(new ClassBreak(100000, textStyle2a));
            classBreakStyle2.ClassBreaks.Add(new ClassBreak(500000, textStyle2b));
            classBreakStyle2.ClassBreaks.Add(new ClassBreak(3000000, textStyle2c));
            worldLabelLayer.ZoomLevelSet.ZoomLevel04.CustomStyles.Add(classBreakStyle2);
            worldLabelLayer.ZoomLevelSet.ZoomLevel04.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level05;

            //For Zoom Levels 06 to 07, displays the country labels for countries above 20,000 sqkm in area with four classes.
            ClassBreakStyle classBreakStyle3 = new ClassBreakStyle("SQKM");
            TextStyle textStyle3a = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 9, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 2, 0, 0);
            TextStyle textStyle3b = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 12, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 2, 0, 0);
            TextStyle textStyle3c = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 14, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 2, 0, 0);
            TextStyle textStyle3d = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 18, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 3, 0, 0);
            classBreakStyle3.ClassBreaks.Add(new ClassBreak(20000, textStyle3a));
            classBreakStyle3.ClassBreaks.Add(new ClassBreak(100000, textStyle3b));
            classBreakStyle3.ClassBreaks.Add(new ClassBreak(500000, textStyle3c));
            classBreakStyle3.ClassBreaks.Add(new ClassBreak(3000000, textStyle3d));
            worldLabelLayer.ZoomLevelSet.ZoomLevel06.CustomStyles.Add(classBreakStyle3);
            worldLabelLayer.ZoomLevelSet.ZoomLevel06.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level07;

            //For Zoom Levels 08 to 20, displays the country labels for all countries with five classes.
            ClassBreakStyle classBreakStyle4 = new ClassBreakStyle("SQKM");
            TextStyle textStyle4a = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 9, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 2, 0, 0);
            TextStyle textStyle4b = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 12, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 2, 0, 0);
            TextStyle textStyle4c = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 14, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 2, 0, 0);
            TextStyle textStyle4d = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 18, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 2, 0, 0);
            TextStyle textStyle4e = TextStyles.CreateSimpleTextStyle("CNTRY_NAME", "Arial", 22, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.StandardColors.White, 3, 0, 0);
            classBreakStyle4.ClassBreaks.Add(new ClassBreak(0, textStyle4a));
            classBreakStyle4.ClassBreaks.Add(new ClassBreak(20000, textStyle4b));
            classBreakStyle4.ClassBreaks.Add(new ClassBreak(100000, textStyle4c));
            classBreakStyle4.ClassBreaks.Add(new ClassBreak(500000, textStyle4d));
            classBreakStyle4.ClassBreaks.Add(new ClassBreak(3000000, textStyle4e));
            worldLabelLayer.ZoomLevelSet.ZoomLevel08.CustomStyles.Add(classBreakStyle4);
            worldLabelLayer.ZoomLevelSet.ZoomLevel08.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
           
            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldShapeLayer", worldShapeLayer);
            staticOverlay.Layers.Add("WorldLabelLayer", worldLabelLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.Refresh();
        }

        private void winformsMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Displays the X and Y in screen coordinates.
            statusStrip1.Items["toolStripStatusLabelScreen"].Text = "X:" + e.X + " Y:" + e.Y;

            //Gets the PointShape in world coordinates from screen coordinates.
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, new ScreenPointF(e.X, e.Y), winformsMap1.Width, winformsMap1.Height);

            //Displays world coordinates.
            statusStrip1.Items["toolStripStatusLabelWorld"].Text = "(world) X:" + Math.Round(pointShape.X, 4) + " Y:" + Math.Round(pointShape.Y, 4);
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
