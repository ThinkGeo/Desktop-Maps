/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

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
    public partial class ChangeTheLookOfAFeatureLayer : UserControl
    {
        public ChangeTheLookOfAFeatureLayer()
        {
            InitializeComponent();
        }

        private void ChangeVactorLayerStyle_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-15495673, 20037508, 13458526, -20037508);
            winformsMap1.Refresh();
        }

        private void btnStyle1_Click(object sender, EventArgs e)
        {
            winformsMap1.FindFeatureLayer("WorldLayer").ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country2;

            winformsMap1.Refresh(winformsMap1.Overlays["WorldOverlay"]);
        }

        private void btnStyle2_Click(object sender, EventArgs e)
        {
            winformsMap1.FindFeatureLayer("WorldLayer").ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;

            winformsMap1.Refresh(winformsMap1.Overlays["WorldOverlay"]);
        }

        private WinformsMap winformsMap1;

        #region Component Designer generated code

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStyle1 = new System.Windows.Forms.Button();
            this.btnStyle2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // btnStyle1
            //
            this.btnStyle1.Location = new System.Drawing.Point(21, 19);
            this.btnStyle1.Name = "btnStyle1";
            this.btnStyle1.Size = new System.Drawing.Size(75, 23);
            this.btnStyle1.TabIndex = 1;
            this.btnStyle1.Text = "Style1";
            this.btnStyle1.UseVisualStyleBackColor = true;
            this.btnStyle1.Click += new System.EventHandler(this.btnStyle1_Click);
            //
            // btnStyle2
            //
            this.btnStyle2.Location = new System.Drawing.Point(102, 19);
            this.btnStyle2.Name = "btnStyle2";
            this.btnStyle2.Size = new System.Drawing.Size(75, 23);
            this.btnStyle2.TabIndex = 2;
            this.btnStyle2.Text = "Style2";
            this.btnStyle2.UseVisualStyleBackColor = true;
            this.btnStyle2.Click += new System.EventHandler(this.btnStyle2_Click);
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnStyle1);
            this.groupBox1.Controls.Add(this.btnStyle2);
            this.groupBox1.Location = new System.Drawing.Point(542, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 56);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 4;
            this.winformsMap1.Text = "winformsMap1";
            //
            // ChangeTheLookOfAFeatureLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "ChangeTheLookOfAFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ChangeVactorLayerStyle_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code

        private Button btnStyle1;
        private Button btnStyle2;
        private GroupBox groupBox1;

        #endregion Component Designer generated code
    }
}