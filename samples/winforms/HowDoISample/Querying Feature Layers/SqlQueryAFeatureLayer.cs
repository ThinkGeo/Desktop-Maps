using System;
using System.Data;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class SqlQueryAFeatureLayer : UserControl
    {
        public SqlQueryAFeatureLayer()
        {
            InitializeComponent();
        }

        private void SqlQuery_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("USStates.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.LineJoin = DrawingLineJoin.Round;
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-165.7421875, 80.23046875, -35.6640625, 13.08203125);
            winformsMap1.Refresh();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            ShapeFileFeatureLayer worldLayer = (ShapeFileFeatureLayer)winformsMap1.FindFeatureLayer("WorldLayer");

            worldLayer.Open();
            DataTable dataTable = worldLayer.QueryTools.ExecuteQuery(tbxSQL.Text);
            worldLayer.Close();

            dgridResult.DataSource = dataTable;
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.btnExecute = new System.Windows.Forms.Button();
            this.dgridResult = new System.Windows.Forms.DataGridView();
            this.tbxSQL = new System.Windows.Forms.TextBox();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            ((System.ComponentModel.ISupportInitialize)(this.dgridResult)).BeginInit();
            this.SuspendLayout();
            //
            // btnExecute
            //
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecute.Location = new System.Drawing.Point(628, 505);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(84, 23);
            this.btnExecute.TabIndex = 39;
            this.btnExecute.Text = "Execute SQL";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            //
            // dgridResult
            //
            this.dgridResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgridResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridResult.Location = new System.Drawing.Point(0, 381);
            this.dgridResult.Name = "dgridResult";
            this.dgridResult.ReadOnly = true;
            this.dgridResult.Size = new System.Drawing.Size(582, 147);
            this.dgridResult.TabIndex = 38;
            //
            // tbxSQL
            //
            this.tbxSQL.BackColor = System.Drawing.Color.White;
            this.tbxSQL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxSQL.Location = new System.Drawing.Point(588, 386);
            this.tbxSQL.Multiline = true;
            this.tbxSQL.Name = "tbxSQL";
            this.tbxSQL.ReadOnly = true;
            this.tbxSQL.Size = new System.Drawing.Size(149, 115);
            this.tbxSQL.TabIndex = 40;
            this.tbxSQL.Text = "Select * from USStates \r\nWhere PERIMETER > 30\r\nOrder by PERIMETER";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 100;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            this.winformsMap1.Size = new System.Drawing.Size(740, 382);
            this.winformsMap1.TabIndex = 41;
            this.winformsMap1.Text = "winformsMap1";
            // this.winformsMap1.ZoomLevelSnapping = ThinkGeo.UI.WinForms.ZoomLevelSnappingMode.Default;
            this.winformsMap1.ExtentOverlay.ZoomPercentage = 40;
            //
            // SqlQueryAFeatureLayer
            //
            this.Controls.Add(this.winformsMap1);
            this.Controls.Add(this.tbxSQL);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.dgridResult);
            this.Name = "SqlQueryAFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.SqlQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Button btnExecute;
        private DataGridView dgridResult;
        private MapView winformsMap1;
        private TextBox tbxSQL;

        #endregion Component Designer generated code
    }
}