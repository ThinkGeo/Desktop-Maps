﻿using System;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class FindTheDistanceBetweenTwoFeatures : UserControl
    {
        public FindTheDistanceBetweenTwoFeatures()
        {
            InitializeComponent();
        }

        private void FindTheDistanceFromOneFeatureToAnother_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-128.31093750000002, 95.25, 131.84531249999998, -63.65625);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            InMemoryFeatureLayer usInMemoryLayer = new InMemoryFeatureLayer();
            usInMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.PointType = PointType.Image;
            usInMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.Image = new GeoImage(SampleHelper.Get("United States.png"));
            usInMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            usInMemoryLayer.InternalFeatures.Add("US", new Feature(-98.58, 39.57, "1"));

            InMemoryFeatureLayer chinaInMemoryLayer = new InMemoryFeatureLayer();
            chinaInMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.PointType = PointType.Image;
            chinaInMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.Image = new GeoImage(SampleHelper.Get("China.png"));
            chinaInMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            chinaInMemoryLayer.InternalFeatures.Add("CHINA", new Feature(104.72, 34.45, "2"));

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            staticOverlay.Layers.Add("USInMemoryFeatureLayer", usInMemoryLayer);
            staticOverlay.Layers.Add("ChinaInMemoryFeatureLayer", chinaInMemoryLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.Refresh();
        }

        private void btnGetDistance_Click(object sender, EventArgs e)
        {
            InMemoryFeatureLayer usInMemoryLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("USInMemoryFeatureLayer");
            InMemoryFeatureLayer chinaInMemoryLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("ChinaInMemoryFeatureLayer");

            BaseShape usShape = usInMemoryLayer.InternalFeatures["US"].GetShape();
            BaseShape chinaShape = chinaInMemoryLayer.InternalFeatures["CHINA"].GetShape();

            double distance = usShape.GetDistanceTo(chinaShape, GeographyUnit.DecimalDegree, DistanceUnit.Kilometer);
            txtDistance.Text = string.Format(CultureInfo.InvariantCulture, "{0 :N4} Km", distance);
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private GroupBox groupBox1;
        private Button btnGetDistance;
        private MapView winformsMap1;
        private TextBox txtDistance;

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
            this.btnGetDistance = new System.Windows.Forms.Button();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnGetDistance);
            this.groupBox1.Controls.Add(this.txtDistance);
            this.groupBox1.Location = new System.Drawing.Point(525, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 65);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Distance From US To China:";
            //
            // btnGetDistance
            //
            this.btnGetDistance.Location = new System.Drawing.Point(136, 21);
            this.btnGetDistance.Name = "btnGetDistance";
            this.btnGetDistance.Size = new System.Drawing.Size(70, 23);
            this.btnGetDistance.TabIndex = 24;
            this.btnGetDistance.Text = "Get";
            this.btnGetDistance.UseVisualStyleBackColor = true;
            this.btnGetDistance.Click += new System.EventHandler(this.btnGetDistance_Click);
            //
            // txtDistance
            //
            this.txtDistance.Location = new System.Drawing.Point(22, 24);
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.ReadOnly = true;
            this.txtDistance.Size = new System.Drawing.Size(103, 20);
            this.txtDistance.TabIndex = 20;
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 6;
            this.winformsMap1.Text = "winformsMap1";
            //
            // FindTheDistanceBetweenTwoFeatures
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "FindTheDistanceBetweenTwoFeatures";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.FindTheDistanceFromOneFeatureToAnother_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}