using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class HighlightAFeatureOnTheMap : UserControl
    {
        public HighlightAFeatureOnTheMap()
        {
            InitializeComponent();
        }

        private void HighlightAFeatureOnTheMap_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));

            InMemoryFeatureLayer highlightLayer = new InMemoryFeatureLayer();
            highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(100, GeoColors.DarkGreen));

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            LayerOverlay highlightOverlay = new LayerOverlay();
            highlightOverlay.Layers.Add("HighlightLayer", highlightLayer);
            winformsMap1.Overlays.Add("HighlightOverlay", highlightOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-177.333203125, 92.96484375, 82.823046875, -89.84765625);
            winformsMap1.Refresh();
        }

        private void btnHighlightAFeature_Click(object sender, EventArgs e)
        {
            FeatureLayer worldLayer = winformsMap1.FindFeatureLayer("WorldLayer");
            InMemoryFeatureLayer highlightLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("HighlightLayer");

            worldLayer.Open();
            Feature feature = worldLayer.QueryTools.GetFeatureById("135", new string[0]);
            worldLayer.Close();

            highlightLayer.InternalFeatures.Clear();
            highlightLayer.InternalFeatures.Add("135", feature);

            winformsMap1.Refresh(winformsMap1.Overlays["HighlightOverlay"]);
        }

        private MapView winformsMap1;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnHighlightAFeature = new System.Windows.Forms.Button();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.btnHighlightAFeature);
            this.groupBox1.Location = new System.Drawing.Point(597, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(143, 57);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // btnHighlightAFeature
            //
            this.btnHighlightAFeature.Location = new System.Drawing.Point(15, 19);
            this.btnHighlightAFeature.Name = "btnHighlightAFeature";
            this.btnHighlightAFeature.Size = new System.Drawing.Size(112, 23);
            this.btnHighlightAFeature.TabIndex = 0;
            this.btnHighlightAFeature.Text = "Highlight A Feature";
            this.btnHighlightAFeature.UseVisualStyleBackColor = true;
            this.btnHighlightAFeature.Click += new System.EventHandler(this.btnHighlightAFeature_Click);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 3;
            this.winformsMap1.Text = "winformsMap1";
            //
            // HighlightAFeatureOnTheMap
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "HighlightAFeatureOnTheMap";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.HighlightAFeatureOnTheMap_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private GroupBox groupBox1;
        private Button btnHighlightAFeature;

        #endregion Component Designer generated code
    }
}