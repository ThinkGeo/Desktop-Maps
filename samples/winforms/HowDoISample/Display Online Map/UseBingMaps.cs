using System;
using System.Windows.Forms;
using ThinkGeo.Core; 
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class UseBingMaps : UserControl
    {
        public UseBingMaps()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-15612805, 7675440, -5819082, 1746373);
            mapView.ZoomLevelSet = new BingMapsZoomLevelSet();

            string bingMapsApplicationId = string.Empty;
            if (bingMapsApplicationId == string.Empty)
            {
                MessageBox.Show("A Bing Map Application ID is required for displaying Bing Maps");
                return;
            }
            BingMapsOverlay bingMapsOverlay = new BingMapsOverlay(bingMapsApplicationId, BingMapsMapType.AerialWithLabels);
            mapView.Overlays.Add(bingMapsOverlay);
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