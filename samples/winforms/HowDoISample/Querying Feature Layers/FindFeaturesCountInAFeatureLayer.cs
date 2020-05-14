using System;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class FindFeaturesCountInAFeatureLayer : UserControl
    {
        public FindFeaturesCountInAFeatureLayer()
        {
            InitializeComponent();
        }

        private void FindFeaturesCountnAFeatureLayer_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);
            winformsMap1.Refresh();
        }

        private void btnGetCount_Click(object sender, EventArgs e)
        {
            FeatureLayer worldLayer = winformsMap1.FindFeatureLayer("WorldLayer");
            worldLayer.Open();
            txtCount.Text = worldLayer.QueryTools.GetCount().ToString(CultureInfo.InvariantCulture);
            worldLayer.Close();
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private GroupBox groupBox1;
        private Button btnGetCount;
        private MapView winformsMap1;
        private TextBox txtCount;

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
            this.btnGetCount = new System.Windows.Forms.Button();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnGetCount);
            this.groupBox1.Controls.Add(this.txtCount);
            this.groupBox1.Location = new System.Drawing.Point(571, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 70);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Feature Count";
            //
            // btnGetCount
            //
            this.btnGetCount.Location = new System.Drawing.Point(94, 27);
            this.btnGetCount.Name = "btnGetCount";
            this.btnGetCount.Size = new System.Drawing.Size(57, 23);
            this.btnGetCount.TabIndex = 24;
            this.btnGetCount.Text = "Get";
            this.btnGetCount.UseVisualStyleBackColor = true;
            this.btnGetCount.Click += new System.EventHandler(this.btnGetCount_Click);
            //
            // txtCount
            //
            this.txtCount.Location = new System.Drawing.Point(16, 30);
            this.txtCount.Name = "txtCount";
            this.txtCount.ReadOnly = true;
            this.txtCount.Size = new System.Drawing.Size(60, 20);
            this.txtCount.TabIndex = 20;
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
            // FindFeaturesCountInAFeatureLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "FindFeaturesCountInAFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.FindFeaturesCountnAFeatureLayer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}