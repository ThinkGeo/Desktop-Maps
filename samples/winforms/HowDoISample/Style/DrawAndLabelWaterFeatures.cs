using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DrawAndLabelWaterFeatures : UserControl
    {
        public DrawAndLabelWaterFeatures()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer utahWaterShapeLayer = new ShapeFileFeatureLayer(SampleHelper.Get("UtahWater_3857.shp"));
            utahWaterShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            utahWaterShapeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 136, 162, 227)));
            utahWaterShapeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(TextStyle.CreateSimpleTextStyle("Landname", "Arial", 6, DrawingFontStyles.Italic, GeoColors.Navy));

            LayerOverlay utahWaterOverlay = new LayerOverlay();
            utahWaterOverlay.Layers.Add("UtahWaterShapes", utahWaterShapeLayer);
            mapView.Overlays.Add("UtahWaterOverlay", utahWaterOverlay);

            mapView.CurrentExtent = new RectangleShape(-12591875, 5153465, -12365621, 4938814);
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