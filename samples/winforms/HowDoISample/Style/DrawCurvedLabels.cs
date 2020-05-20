using System;
using System.Windows.Forms;
using ThinkGeo.Core; 
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DrawCurvedLabels : UserControl
    {
        public DrawCurvedLabels()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            ShapeFileFeatureLayer austinStreetsShapeLayer = new ShapeFileFeatureLayer(SampleHelper.Get("austinstreets_3857.shp"));
            austinStreetsShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            austinStreetsShapeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(LineStyle.CreateSimpleLineStyle(GeoColors.White, 9.2F, GeoColors.DarkGray, 12.2F, true));

            ShapeFileFeatureLayer austinStreetsLabelLayer = new ShapeFileFeatureLayer(SampleHelper.Get("austinstreets_3857.shp"));
            austinStreetsLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            TextStyle textStyle = TextStyle.CreateSimpleTextStyle("FENAME", "Arial", 9, DrawingFontStyles.Regular, GeoColors.Black, 0, 0);
            textStyle.TextLineSegmentRatio = double.MaxValue;
            textStyle.SplineType = SplineType.StandardSplining;
            austinStreetsLabelLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);

            LayerOverlay austinStreetsOverlay = new LayerOverlay();
            austinStreetsOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214))));
            austinStreetsOverlay.Layers.Add("AustinStreetsShapeLayer", austinStreetsShapeLayer);
            austinStreetsOverlay.Layers.Add("AustinStreetsLabelLayer", austinStreetsLabelLayer);
            mapView.Overlays.Add("AustinStreetsOverlay", austinStreetsOverlay);

            mapView.CurrentExtent = new RectangleShape(-10874598, 3544465, -10872831, 3543004);
        }

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.SuspendLayout();
            //
            // mapView
            //
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.Controls.Add(this.mapView);
            //
            // UserControl
            //
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}