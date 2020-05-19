using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateATinyGeoFeatureLayer : UserControl
    {
        public CreateATinyGeoFeatureLayer()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            var tinyGeoFeatureLayer = new TinyGeoFeatureLayer(SampleHelper.Get("Frisco.tgeo"));
            tinyGeoFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Black, 1, true);
            tinyGeoFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            tinyGeoFeatureLayer.Open();
            var currentExtent = tinyGeoFeatureLayer.GetBoundingBox();
            tinyGeoFeatureLayer.Close();

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(tinyGeoFeatureLayer);

            mapView.Overlays.Add(layerOverlay);
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = currentExtent;
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