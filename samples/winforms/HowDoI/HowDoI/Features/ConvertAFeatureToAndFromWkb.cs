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
    public class ConvertAFeatureToAndFromWkb : UserControl
    {
        public ConvertAFeatureToAndFromWkb()
        {
            InitializeComponent();
        }

        private void ConvertAFeatureToAndFromWkb_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-12933325, 14195325, 16027137, -8510143);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.SimpleColors.Transparent, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            winformsMap1.Refresh();
        }

        private void convertWkbToFeature_Click(object sender, EventArgs e)
        {
            byte[] wellKnownBinary = Convert.FromBase64String(wkbTextBox.Text);
            Feature feature = new Feature(wellKnownBinary);

            wktResultTextBox.Text = feature.GetWellKnownText();
        }

        private void convertFeatureToWkb_Click(object sender, EventArgs e)
        {
            Feature feature = new Feature(wktTextBox.Text);
            byte[] wellKnownBinary = feature.GetWellKnownBinary();

            wkbResultTextBox.Text = Convert.ToBase64String(wellKnownBinary);
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private Label label4;
        private TextBox wktResultTextBox;
        private TextBox wkbResultTextBox;
        private TextBox wktTextBox;
        private Label label2;
        private Button convertFeatureToWkb;
        private Button convertWkbToFeature;
        private Label label3;
        private Label label1;
        private TextBox wkbTextBox;
        private System.ComponentModel.IContainer components = null;
        private WinformsMap winformsMap1;
        private GroupBox groupBox2;

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
            this.label4 = new System.Windows.Forms.Label();
            this.wktResultTextBox = new System.Windows.Forms.TextBox();
            this.convertWkbToFeature = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.wkbTextBox = new System.Windows.Forms.TextBox();
            this.wkbResultTextBox = new System.Windows.Forms.TextBox();
            this.wktTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.convertFeatureToWkb = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.wktResultTextBox);
            this.groupBox1.Controls.Add(this.convertWkbToFeature);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.wkbTextBox);
            this.groupBox1.Location = new System.Drawing.Point(0, 429);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 99);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Binary To Text";
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(203, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Well Known Text";
            //
            // wktResultTextBox
            //
            this.wktResultTextBox.Location = new System.Drawing.Point(218, 34);
            this.wktResultTextBox.Multiline = true;
            this.wktResultTextBox.Name = "wktResultTextBox";
            this.wktResultTextBox.ReadOnly = true;
            this.wktResultTextBox.Size = new System.Drawing.Size(128, 47);
            this.wktResultTextBox.TabIndex = 15;
            //
            // convertWkbToFeature
            //
            this.convertWkbToFeature.Location = new System.Drawing.Point(142, 46);
            this.convertWkbToFeature.Name = "convertWkbToFeature";
            this.convertWkbToFeature.Size = new System.Drawing.Size(70, 23);
            this.convertWkbToFeature.TabIndex = 12;
            this.convertWkbToFeature.Text = "Convert";
            this.convertWkbToFeature.UseVisualStyleBackColor = true;
            this.convertWkbToFeature.Click += new System.EventHandler(this.convertWkbToFeature_Click);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Binary (Base64 encoded):";
            //
            // wkbTextBox
            //
            this.wkbTextBox.Location = new System.Drawing.Point(6, 34);
            this.wkbTextBox.Multiline = true;
            this.wkbTextBox.Name = "wkbTextBox";
            this.wkbTextBox.ReadOnly = true;
            this.wkbTextBox.Size = new System.Drawing.Size(130, 47);
            this.wkbTextBox.TabIndex = 8;
            this.wkbTextBox.Text = "AQEAAAAAAAAAAAAkQAAAAAAAADRA";
            //
            // wkbResultTextBox
            //
            this.wkbResultTextBox.Location = new System.Drawing.Point(217, 37);
            this.wkbResultTextBox.Multiline = true;
            this.wkbResultTextBox.Name = "wkbResultTextBox";
            this.wkbResultTextBox.ReadOnly = true;
            this.wkbResultTextBox.Size = new System.Drawing.Size(137, 44);
            this.wkbResultTextBox.TabIndex = 16;
            //
            // wktTextBox
            //
            this.wktTextBox.Location = new System.Drawing.Point(6, 34);
            this.wktTextBox.Multiline = true;
            this.wktTextBox.Name = "wktTextBox";
            this.wktTextBox.ReadOnly = true;
            this.wktTextBox.Size = new System.Drawing.Size(129, 47);
            this.wktTextBox.TabIndex = 14;
            this.wktTextBox.Text = "POINT(10 20)";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Well Known Text";
            //
            // convertFeatureToWkb
            //
            this.convertFeatureToWkb.Location = new System.Drawing.Point(141, 46);
            this.convertFeatureToWkb.Name = "convertFeatureToWkb";
            this.convertFeatureToWkb.Size = new System.Drawing.Size(70, 23);
            this.convertFeatureToWkb.TabIndex = 11;
            this.convertFeatureToWkb.Text = "Convert";
            this.convertFeatureToWkb.UseVisualStyleBackColor = true;
            this.convertFeatureToWkb.Click += new System.EventHandler(this.convertFeatureToWkb_Click);
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(214, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Binary (Base64 encoded):";
            //
            // groupBox2
            //
            this.groupBox2.Controls.Add(this.wktTextBox);
            this.groupBox2.Controls.Add(this.convertFeatureToWkb);
            this.groupBox2.Controls.Add(this.wkbResultTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(366, 429);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(371, 96);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Text To Binary";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 430);
            this.winformsMap1.TabIndex = 10;
            this.winformsMap1.Text = "winformsMap1";
            //
            // ConvertAFeatureToAndFromWkb
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "ConvertAFeatureToAndFromWkb";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ConvertAFeatureToAndFromWkb_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}