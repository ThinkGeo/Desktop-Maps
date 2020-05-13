using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace MultipleLabels
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
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214));

            ShapeFileFeatureLayer austinStreetsLayer = new ShapeFileFeatureLayer(@"../../Data/Austinstreets.shp");
            austinStreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.White, 9.2F, GeoColor.StandardColors.DarkGray, 12.2F, true));
            austinStreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(WorldStreetsTextStyles.Poi("[FENAME] [FETYPE]  [FRADDL]-[TOADDL]", 8, -12));
            austinStreetsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay austinStreetsOverlay = new LayerOverlay();
            austinStreetsOverlay.Layers.Add("AustinStreetsLayer", austinStreetsLayer);
            winformsMap1.Overlays.Add("AustinStreetsOverlay", austinStreetsOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-97.7456279238844, 30.3027064993117, -97.7420552214766, 30.3006304695341);

            winformsMap1.Refresh();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            ShapeFileFeatureLayer austinStreetsLayer = ((LayerOverlay)winformsMap1.Overlays["AustinStreetsOverlay"]).Layers["AustinStreetsLayer"] as ShapeFileFeatureLayer;
            austinStreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            austinStreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.White, 9.2F, GeoColor.StandardColors.DarkGray, 12.2F, true));

            if (rbnSingleStyle.Checked)
            {
                austinStreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(WorldStreetsTextStyles.Poi("[FENAME] [FETYPE]  [FRADDL]-[TOADDL]", 8, -12));
            }
            else
            {
                TextStyle primaryTextStyle = WorldStreetsTextStyles.Poi("[FENAME] [FETYPE]", 8, -12);
                primaryTextStyle.XOffsetInPixel = 0;                

                TextStyle secondaryTextStyle = WorldStreetsTextStyles.Poi("[FRADDL]-[TOADDL]", 8, -12);
                secondaryTextStyle.YOffsetInPixel =  15;
                secondaryTextStyle.Font = new GeoFont("Arial", 7);
                secondaryTextStyle.Mask = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 1);
                secondaryTextStyle.OverlappingRule = LabelOverlappingRule.AllowOverlapping;
                secondaryTextStyle.DuplicateRule = LabelDuplicateRule.UnlimitedDuplicateLabels;

                austinStreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(primaryTextStyle);
                austinStreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(secondaryTextStyle);
            }

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
    }
}
