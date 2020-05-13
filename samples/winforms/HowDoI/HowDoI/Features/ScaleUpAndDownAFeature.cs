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
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class ScaleUpAndDownAFeature : UserControl
    {
        public ScaleUpAndDownAFeature()
        {
            InitializeComponent();
        }

        private void ScaleUpAndDownAFeature_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.StandardColors.White);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");
            worldLayer.Open();
            Feature feature = worldLayer.QueryTools.GetFeatureById("135", new string[0]);
            worldLayer.Close();

            InMemoryFeatureLayer inMemoryLayer = new InMemoryFeatureLayer();
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillSolidBrush.Color = GeoColor.FromArgb(100, GeoColor.SimpleColors.Green);
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColor.SimpleColors.Green;
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            inMemoryLayer.InternalFeatures.Add("135", feature);

            LayerOverlay inMemoryOverlay = new LayerOverlay();
            inMemoryOverlay.Layers.Add("InMemoryFeatureLayer", inMemoryLayer);
            winformsMap1.Overlays.Add("InMemoryOverlay", inMemoryOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-18473122, 20037508, -3992891, -711306);
            winformsMap1.Refresh();
        }

        private void btnScaleUp_Click(object sender, EventArgs e)
        {
            UpdateFeatureByScale(25, true);
        }

        private void btnScaleDown_Click(object sender, EventArgs e)
        {
            UpdateFeatureByScale(20, false);
        }

        private void UpdateFeatureByScale(double percentage, bool isScaleUp)
        {
            InMemoryFeatureLayer inMemoryLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("InMemoryFeatureLayer");
            inMemoryLayer.Open();
            inMemoryLayer.EditTools.BeginTransaction();
            if (isScaleUp)
            {
                inMemoryLayer.EditTools.ScaleUp("135", percentage);
            }
            else
            {
                inMemoryLayer.EditTools.ScaleDown("135", percentage);
            }
            inMemoryLayer.EditTools.CommitTransaction();
            inMemoryLayer.Close();

            winformsMap1.Refresh(winformsMap1.Overlays["InMemoryOverlay"]);
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private GroupBox groupBox1;
        private Button btnScaleDown;
        private WinformsMap winformsMap1;
        private Button btnScaleUp;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnScaleDown = new System.Windows.Forms.Button();
            this.btnScaleUp = new System.Windows.Forms.Button();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnScaleDown);
            this.groupBox1.Controls.Add(this.btnScaleUp);
            this.groupBox1.Location = new System.Drawing.Point(638, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(97, 91);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // btnScaleDown
            //
            this.btnScaleDown.Location = new System.Drawing.Point(13, 54);
            this.btnScaleDown.Name = "btnScaleDown";
            this.btnScaleDown.Size = new System.Drawing.Size(75, 23);
            this.btnScaleDown.TabIndex = 1;
            this.btnScaleDown.Text = "Scale Down";
            this.btnScaleDown.UseVisualStyleBackColor = true;
            this.btnScaleDown.Click += new System.EventHandler(this.btnScaleDown_Click);
            //
            // btnScaleUp
            //
            this.btnScaleUp.Location = new System.Drawing.Point(13, 20);
            this.btnScaleUp.Name = "btnScaleUp";
            this.btnScaleUp.Size = new System.Drawing.Size(75, 23);
            this.btnScaleUp.TabIndex = 0;
            this.btnScaleUp.Text = "Scale Up";
            this.btnScaleUp.UseVisualStyleBackColor = true;
            this.btnScaleUp.Click += new System.EventHandler(this.btnScaleUp_Click);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 8;
            this.winformsMap1.Text = "winformsMap1";
            //
            // ScaleUpAndDownAFeature
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "ScaleUpAndDownAFeature";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ScaleUpAndDownAFeature_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}