using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateAGraticuleAdornmentLayer : UserControl
    {
        public CreateAGraticuleAdornmentLayer()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            GraticuleFeatureLayer graticuleAdornmentLayer = new GraticuleFeatureLayer();
            graticuleAdornmentLayer.GraticuleLineStyle.OuterPen.Color = GeoColor.FromArgb(125, GeoColors.Navy);

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("graticule", graticuleAdornmentLayer);
            mapView.Overlays.Add(layerOverlay);

            mapView.CurrentExtent = new RectangleShape(-100, 50, 100, -50);
            mapView.Refresh();
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