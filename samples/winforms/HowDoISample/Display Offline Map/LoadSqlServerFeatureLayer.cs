using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class LoadSqlServerFeatureLayer : UserControl
    {
        public LoadSqlServerFeatureLayer()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Please spcify connection for displaying SqlServerFeatureLayer");
            return;

            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = new RectangleShape(-126.4, 48.8, -67.0, 19.0);

            string connectionString = "Server=127.0.0.1;Initial Catalog=spatialdatabase;User ID=user;Password=password";
            SqlServerFeatureLayer layer = new SqlServerFeatureLayer(connectionString, "COUNTRY02", "RECID");
            layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Green, GeoColors.Red);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay sql2008Overlay = new LayerOverlay();
            sql2008Overlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.ShallowOcean)));
            sql2008Overlay.Layers.Add(layer);
            mapView.Overlays.Add(sql2008Overlay);
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