using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class LoadAPostgreSqlFeatureLayer : UserControl
    {
        public LoadAPostgreSqlFeatureLayer()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Please spcify connection for displaying PostgreSqlFeatureLayer");
            return;

            mapView.MapUnit = GeographyUnit.DecimalDegree;

            mapView.CurrentExtent = new RectangleShape(-126.4, 48.8, -67.0, 19.0);

            string connectString = "Server=192.168.0.235;User Id=userId;Password=password;DataBase=postgis;";
            PostgreSqlFeatureLayer postgreLayer = new PostgreSqlFeatureLayer(connectString, "COUNTRY02", "'RECID'");
            postgreLayer.SchemaName = "public";
            postgreLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            postgreLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay postgreOverlay = new LayerOverlay();
            postgreOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.ShallowOcean)));
            postgreOverlay.Layers.Add("PostgreLayer", postgreLayer);
            mapView.Overlays.Add("PostgreOverlay", postgreOverlay);
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