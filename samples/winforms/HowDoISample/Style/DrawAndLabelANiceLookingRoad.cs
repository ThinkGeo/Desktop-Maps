using System;
using System.Windows.Forms;
using ThinkGeo.Core; 
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DrawAndLabelANiceLookingRoad : UserControl
    {
        public DrawAndLabelANiceLookingRoad()
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
            austinStreetsLabelLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(TextStyle.CreateSimpleTextStyle("FENAME", "Arial", 9, DrawingFontStyles.Regular, GeoColors.Black, 0, 0));
            austinStreetsLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214))));
            staticOverlay.Layers.Add("AustinStreetsShapeLayer", austinStreetsShapeLayer);
            staticOverlay.Layers.Add("AustinStreetsLabelLayer", austinStreetsLabelLayer);
            mapView.Overlays.Add(staticOverlay);

            mapView.CurrentExtent = new RectangleShape(-10881385, 3542247, -10880501, 3541517);
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