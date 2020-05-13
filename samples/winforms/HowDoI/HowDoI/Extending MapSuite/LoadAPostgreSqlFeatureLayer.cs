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
    public class LoadAPostgreSqlFeatureLayer : UserControl
    {
        public LoadAPostgreSqlFeatureLayer()
        {
            InitializeComponent();
        }

        private void LoadAPostgreSqlFeatureLayer_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.SpecialDataBaseResult;
        }

        private void LoadPostgreLayer()
        {
            Controls.Clear();
            Controls.Add(winformsMap1);

            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;

            winformsMap1.CurrentExtent = new RectangleShape(-126.4, 48.8, -67.0, 19.0);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            string connectString = "Server=192.168.0.235;User Id=userId;Password=password;DataBase=postgis;";
            PostgreSqlFeatureLayer postgreLayer = new PostgreSqlFeatureLayer(connectString, "new_states", "recid");
            postgreLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;
            postgreLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("PostgreLayer", postgreLayer);
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
            this.lblDescription = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            //
            // lblDescription
            //
            this.lblDescription.BackColor = System.Drawing.Color.LightYellow;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblDescription.Location = new System.Drawing.Point(5, 5);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(300, 50);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "To use the PostgreSqlFeatureLayer functions, you have to reference [Install-Path]" +
                "\\Developer Reference\\Spatial Extensions\\Postgre Extension\\PostgreExtension.dll.";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // pictureBox1
            //
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::CSHowDoISamples.Properties.Resources.SpecialDataBaseResult;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(740, 528);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            //
            // LoadAPostgreSqlFeatureLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.pictureBox1);
            this.Name = "LoadAPostgreSqlFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.LoadAPostgreSqlFeatureLayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }

        private Label lblDescription;

        #endregion Component Designer generated code
    }
}