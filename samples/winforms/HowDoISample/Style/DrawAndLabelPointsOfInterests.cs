using System;
using System.Windows.Forms;
using ThinkGeo.Core; 
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DrawAndLabelPointsOfInterests : UserControl
    {
        public DrawAndLabelPointsOfInterests()
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
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 2);
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.LineJoin = DrawingLineJoin.Round;
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer majorCitiesShapeLayer = new ShapeFileFeatureLayer(SampleHelper.Get("MajorCities_3857.shp"));
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateCompoundCircleStyle(GeoColors.White, 10F, GeoColors.Black, 1F, GeoColors.Black, 7F);
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyle.CreateSimpleTextStyle("AREANAME", "Arial", 10, DrawingFontStyles.Regular, GeoColors.Black, 0, -8);
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.TextPlacement = TextPlacement.AutoPlacement;
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.ShallowOcean)));
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            staticOverlay.Layers.Add("StatesLayer", statesLayer);
            staticOverlay.Layers.Add("MajorCitiesShapeLayer", majorCitiesShapeLayer);
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