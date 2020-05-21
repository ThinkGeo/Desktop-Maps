﻿using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ScaleUpAndDownAFeature : UserControl
    {
        public ScaleUpAndDownAFeature()
        {
            InitializeComponent();
        }

        private void ScaleUpAndDownAFeature_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.White);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.Open();
            Feature feature = worldLayer.QueryTools.GetFeatureById("135", new string[0]);
            worldLayer.Close();

            InMemoryFeatureLayer inMemoryLayer = new InMemoryFeatureLayer();
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.Green));
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.Green;
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            inMemoryLayer.InternalFeatures.Add("135", feature);

            LayerOverlay inMemoryOverlay = new LayerOverlay();
            inMemoryOverlay.Layers.Add("InMemoryFeatureLayer", inMemoryLayer);
            winformsMap1.Overlays.Add("InMemoryOverlay", inMemoryOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-165.946875, 86.4359375, -35.86875, -6.3765625);
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
        private MapView winformsMap1;
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
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
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