using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DisplayWMSMap : UserControl
    {
        public DisplayWMSMap()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = new RectangleShape(-143.4, 109.3, 116.7, -76.3);

            WmsOverlay wmsOverlay = new WmsOverlay();
            wmsOverlay.ServerUri = new Uri("http://ows.mundialis.de/services/service");
            wmsOverlay.Parameters.Add("layers", "OSM-WMS");
            wmsOverlay.Parameters.Add("STYLES", "default");
            wmsOverlay.Parameters.Add("CRS", "3857");
            mapView.Overlays.Add(wmsOverlay);
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