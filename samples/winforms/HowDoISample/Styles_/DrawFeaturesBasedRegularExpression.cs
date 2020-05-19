using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DrawFeaturesBasedRegularExpression : UserControl
    {
        public DrawFeaturesBasedRegularExpression()
        {
            InitializeComponent();
        }

        private void DrawFeaturesBasedRegexExpression_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.SHP"));
            worldLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green)));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Draw features based regular expression
            RegexStyle regexStyle = new RegexStyle();
            regexStyle.ColumnName = "CNTRY_NAME";
            regexStyle.RegexItems.Add(new RegexItem(".*land", new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.Green)))));
            worldLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(regexStyle);

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(worldOverlay);

            //winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.Default;
            winformsMap1.CurrentExtent = new RectangleShape(-75.0, 83.0, 176.0, -50.0);
            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private GroupBox gbxDescrition;
        private Label label1;
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
            this.gbxDescrition = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.gbxDescrition.SuspendLayout();
            this.SuspendLayout();
            //
            // gbxDescrition
            //
            this.gbxDescrition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxDescrition.Controls.Add(this.label1);
            this.gbxDescrition.Location = new System.Drawing.Point(552, 3);
            this.gbxDescrition.Name = "gbxDescrition";
            this.gbxDescrition.Size = new System.Drawing.Size(185, 77);
            this.gbxDescrition.TabIndex = 3;
            this.gbxDescrition.TabStop = false;
            this.gbxDescrition.Text = "Description";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "We highlight the countries which \r\nthe name is end with \"land\" \r\nby regular expre" +
                "ssion. Such as \r\nGreenland, New Zealand. Thailand.";
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
            // DrawFeaturesBasedRegularExpression
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxDescrition);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DrawFeaturesBasedRegularExpression";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DrawFeaturesBasedRegexExpression_Load);
            this.gbxDescrition.ResumeLayout(false);
            this.gbxDescrition.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}