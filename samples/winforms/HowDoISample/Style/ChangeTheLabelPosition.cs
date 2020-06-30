using System;
using System.Windows.Forms;
using ThinkGeo.Core; 
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ChangeTheLabelPosition : UserControl
    {
        public ChangeTheLabelPosition()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));

            ShapeFileFeatureLayer statesLayer = new ShapeFileFeatureLayer(SampleHelper.Get("USStates_3857.shp"));
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer worldLabelLayer = new ShapeFileFeatureLayer(SampleHelper.Get("USStates_3857.shp"));
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyle.CreateSimpleTextStyle("STATE_NAME", "Arial", 7, DrawingFontStyles.Bold, GeoColor.FromArgb(255, 91, 91, 91));
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.TextPlacement = TextPlacement.Center;
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.OverlappingRule = LabelOverlappingRule.NoOverlapping;
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.DuplicateRule = LabelDuplicateRule.NoDuplicateLabels;
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            worldLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.LabelPositions.Add("33", new WorldLabelingCandidate("Kansas State", new PointShape(-91.3969, 28.1016)));
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.LabelPositions.Add("4", new WorldLabelingCandidate("North Dakota State", new PointShape(-101.09, 51.11)));
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.LabelPositions.Add("24", new WorldLabelingCandidate("California State", new PointShape(-126.2, 36.27)));

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.ShallowOcean)));
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            staticOverlay.Layers.Add("StatesLayer", statesLayer);
            staticOverlay.Layers.Add("worldLabelLayer", worldLabelLayer);
            mapView.Overlays.Add(staticOverlay);

            mapView.CurrentExtent = new RectangleShape(-14070784, 6240993, -7458406, 2154936);
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