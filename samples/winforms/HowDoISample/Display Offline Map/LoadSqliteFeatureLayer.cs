using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class LoadSqliteFeatureLayer : UserControl
    {
        public LoadSqliteFeatureLayer()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Please spcify SQLite file for displaying SqliteFeatureLayer");
            return;

            var connectionString = @"Data Source=C:\Test Data\Sqlite\Mapping.sqlite";
            var ne_road10m_linestring = new SqliteFeatureLayer(connectionString, "Segments", "geomID", "geom");
            ne_road10m_linestring.Name = ne_road10m_linestring.TableName;
            ne_road10m_linestring.WhereClause = $"WHERE ReplicationState = 1";
            ne_road10m_linestring.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(LineStyle.CreateSimpleLineStyle(GeoColors.Black, 2, true));
            ne_road10m_linestring.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(ne_road10m_linestring);

            mapView.Overlays.Add(layerOverlay);
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-8336043.617221244, 5212990.090742311, -8328829.872716907, 5207266.868281254);
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