using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ConvertAFeatureToAndFromWkt : UserControl
    {
        public ConvertAFeatureToAndFromWkt()
        {
            InitializeComponent();
        }

        private void ConvertAFeatureToAndFromWkt_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-118.64296875000002, 85.93359375, 140.45859374999998, -64.18359375);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorlOverlay", worldOverlay);

            winformsMap1.Refresh();
        }

        private void convertWktToFeature_Click(object sender, EventArgs e)
        {
            Feature feature = new Feature(wktTextBox.Text);

            byte[] wellKnownBinary = feature.GetWellKnownBinary();
            wkbResultTextBox.Text = Convert.ToBase64String(wellKnownBinary);
        }

        private void convertFeatureToWkt_Click(object sender, EventArgs e)
        {
            byte[] wellKnownBinary = Convert.FromBase64String(wkbTextBox.Text);
            Feature feature = new Feature(wellKnownBinary);

            wktResultTextBox.Text = feature.GetWellKnownText();
        }

        #region Component Designer generated code

        private TextBox wktTextBox;
        private GroupBox groupBox2;
        private Label label5;
        private TextBox wktResultTextBox;
        private Button convertWkbToFeature;
        private Label label6;
        private TextBox wkbTextBox;
        private Button convertFeatureToWkb;
        private TextBox wkbResultTextBox;
        private Label label7;
        private GroupBox groupBox3;
        private Label label8;
        private MapView winformsMap1;
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
            this.wktTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.wktResultTextBox = new System.Windows.Forms.TextBox();
            this.convertWkbToFeature = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.wkbTextBox = new System.Windows.Forms.TextBox();
            this.convertFeatureToWkb = new System.Windows.Forms.Button();
            this.wkbResultTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
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
            // groupBox2
            //
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.wktResultTextBox);
            this.groupBox2.Controls.Add(this.convertWkbToFeature);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.wkbTextBox);
            this.groupBox2.Location = new System.Drawing.Point(377, 426);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 99);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Binary To Text";
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(221, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Well Known Text";
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
            this.convertWkbToFeature.Click += new System.EventHandler(this.convertFeatureToWkt_Click);
            //
            // label6
            //
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Binary (Base64 encoded):";
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
            // convertFeatureToWkb
            //
            this.convertFeatureToWkb.Location = new System.Drawing.Point(141, 46);
            this.convertFeatureToWkb.Name = "convertFeatureToWkb";
            this.convertFeatureToWkb.Size = new System.Drawing.Size(70, 23);
            this.convertFeatureToWkb.TabIndex = 11;
            this.convertFeatureToWkb.Text = "Convert";
            this.convertFeatureToWkb.UseVisualStyleBackColor = true;
            this.convertFeatureToWkb.Click += new System.EventHandler(this.convertWktToFeature_Click);
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
            // label7
            //
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Well Known Text";
            //
            // groupBox3
            //
            this.groupBox3.Controls.Add(this.wktTextBox);
            this.groupBox3.Controls.Add(this.convertFeatureToWkb);
            this.groupBox3.Controls.Add(this.wkbResultTextBox);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(3, 426);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(371, 96);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Text To Binary";
            //
            // label8
            //
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(214, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Binary (Base64 encoded):";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(737, 427);
            this.winformsMap1.TabIndex = 21;
            this.winformsMap1.Text = "winformsMap1";
            //
            // ConvertAFeatureToAndFromWkt
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.winformsMap1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Name = "ConvertAFeatureToAndFromWkt";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ConvertAFeatureToAndFromWkt_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}