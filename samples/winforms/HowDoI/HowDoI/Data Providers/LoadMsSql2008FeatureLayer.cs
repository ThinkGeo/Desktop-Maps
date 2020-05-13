using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
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
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            string connectString = "Data Source=192.168.0.58,1041;Initial Catalog=DatabaseName;Persist Security Info=True;User ID=username;Password=password";
            MsSqlFeatureLayer sql2008Layer = new MsSqlFeatureLayer(connectString, "states", "recid");
            sql2008Layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;
            sql2008Layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("Sql2008Layer", sql2008Layer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private PictureBox pictureBox1;
        private WinformsMap winformsMap1 = new WinformsMap();

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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            //
            // pictureBox1
            //
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::CSHowDoISamples.Properties.Resources.SpecialDataBaseResult;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(740, 528);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
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