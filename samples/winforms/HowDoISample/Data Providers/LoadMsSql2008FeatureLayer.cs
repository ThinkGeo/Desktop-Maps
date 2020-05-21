using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class LoadMsSql2008FeatureLayer : UserControl
    {
        public LoadMsSql2008FeatureLayer()
        {
            InitializeComponent();
        }

        private void LoadAMSSQL2008FeatureLayer_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.SpecialDataBaseResult;
        }

        private void LoadSQL2008Layer()
        {
            Controls.Clear();
            Controls.Add(winformsMap1);

            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;

            winformsMap1.CurrentExtent = new RectangleShape(-126.4, 48.8, -67.0, 19.0);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            string connectString = "Data Source=192.168.0.58,1041;Initial Catalog=DatabaseName;Persist Security Info=True;User ID=username;Password=password";
            SqlServerFeatureLayer sqlServerFeatureLayer = new SqlServerFeatureLayer(connectString, "states", "recid");
            sqlServerFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            sqlServerFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("sqlServerFeatureLayer", sqlServerFeatureLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private PictureBox pictureBox1;
        private Npgsql.NpgsqlDataAdapter npgsqlDataAdapter1;
        private MapView winformsMap1 = new MapView();

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.npgsqlDataAdapter1 = new Npgsql.NpgsqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::ThinkGeo.UI.WinForms.HowDoI.Properties.Resources.SpecialDataBaseResult;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(740, 528);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // npgsqlDataAdapter1
            // 
            this.npgsqlDataAdapter1.DeleteCommand = null;
            this.npgsqlDataAdapter1.InsertCommand = null;
            this.npgsqlDataAdapter1.SelectCommand = null;
            this.npgsqlDataAdapter1.UpdateCommand = null;
            // 
            // LoadMsSql2008FeatureLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Name = "LoadMsSql2008FeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.LoadAMSSQL2008FeatureLayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}