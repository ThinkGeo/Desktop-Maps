using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class LoadAnOracleFeatureLayer : UserControl
    {
        public LoadAnOracleFeatureLayer()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Please spcify connection for displaying OracleFeatureLayer");
            return;

            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = new RectangleShape(-180, 90, 180, -90);

            string connectString = "User ID=userid;Password=password;Data Source=192.168.0.178/orcl;";
            OracleFeatureLayer oracleLayer = new OracleFeatureLayer(connectString);
            oracleLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 1);
            oracleLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColor.FromArgb(255, 169, 195, 221), 2F, GeoColors.Black, 4F, false);
            oracleLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            oracleLayer.FeatureIdFieldName = "ID";
            oracleLayer.ActiveLayer = "COUNTRY02";
            oracleLayer.EnableEmbeddedStyle = false;

            LayerOverlay oracleOverlay = new LayerOverlay();
            oracleOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.ShallowOcean)));
            oracleOverlay.Layers.Add("OracleLayer", oracleLayer);
            mapView.Overlays.Add("OracleOverlay", oracleOverlay);
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