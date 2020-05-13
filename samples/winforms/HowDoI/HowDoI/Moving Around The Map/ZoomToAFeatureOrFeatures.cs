/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class ZoomToAFeatureOrFeatures : UserControl
    {
        public ZoomToAFeatureOrFeatures()
        {
            InitializeComponent();
        }

        private void ZoomToAFeatureOrFeatures_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.SimpleColors.Transparent, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-15495673, 20037508, 13458526, -20037508);
            winformsMap1.Refresh();
        }

        private void btnOneFeature_Click(object sender, EventArgs e)
        {
            FeatureLayer worldLayer = winformsMap1.FindFeatureLayer("WorldLayer");
            worldLayer.Open();
            winformsMap1.CurrentExtent = worldLayer.FeatureSource.GetBoundingBoxById("137");
            worldLayer.Close();

            winformsMap1.Refresh();
        }

        private void btnMultipleFeatures_Click(object sender, EventArgs e)
        {
            Collection<string> featureIDs = new Collection<string>();
            featureIDs.Add("63");  // For US
            featureIDs.Add("6");   // For Canada
            featureIDs.Add("137"); // For Mexico

            FeatureLayer worldLayer = winformsMap1.FindFeatureLayer("WorldLayer");
            worldLayer.Open();
            Collection<Feature> features = worldLayer.FeatureSource.GetFeaturesByIds(featureIDs, new string[0]);
            worldLayer.Close();

            winformsMap1.CurrentExtent = ExtentHelper.GetBoundingBoxOfItems(features);

            winformsMap1.Refresh();
        }

        private WinformsMap winformsMap1;

        #region Component Designer generated code

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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnMultipleFeatures = new System.Windows.Forms.Button();
            this.btnOneFeature = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // btnMultipleFeatures
            //
            this.btnMultipleFeatures.Location = new System.Drawing.Point(15, 48);
            this.btnMultipleFeatures.Name = "btnMultipleFeatures";
            this.btnMultipleFeatures.Size = new System.Drawing.Size(141, 23);
            this.btnMultipleFeatures.TabIndex = 6;
            this.btnMultipleFeatures.Text = "Zoom To Multiple Features";
            this.btnMultipleFeatures.UseVisualStyleBackColor = true;
            this.btnMultipleFeatures.Click += new System.EventHandler(this.btnMultipleFeatures_Click);
            //
            // btnOneFeature
            //
            this.btnOneFeature.Location = new System.Drawing.Point(15, 19);
            this.btnOneFeature.Name = "btnOneFeature";
            this.btnOneFeature.Size = new System.Drawing.Size(141, 23);
            this.btnOneFeature.TabIndex = 5;
            this.btnOneFeature.Text = "Zoom To One Feature";
            this.btnOneFeature.UseVisualStyleBackColor = true;
            this.btnOneFeature.Click += new System.EventHandler(this.btnOneFeature_Click);
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnMultipleFeatures);
            this.groupBox1.Controls.Add(this.btnOneFeature);
            this.groupBox1.Location = new System.Drawing.Point(565, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 88);
            this.groupBox1.TabIndex = 7;
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
            this.winformsMap1.TabIndex = 8;
            this.winformsMap1.Text = "winformsMap1";
            //
            // ZoomToAFeatureOrFeatures
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "ZoomToAFeatureOrFeatures";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ZoomToAFeatureOrFeatures_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Button btnMultipleFeatures;
        private Button btnOneFeature;
        private GroupBox groupBox1;

        #endregion Component Designer generated code
    }
}