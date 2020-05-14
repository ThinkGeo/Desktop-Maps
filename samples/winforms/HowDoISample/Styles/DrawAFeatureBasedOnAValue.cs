using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DrawAFeatureBasedOnAValue : UserControl
    {
        public DrawAFeatureBasedOnAValue()
        {
            InitializeComponent();
        }

        private void DrawAFeatureBasedOnAValue_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green)));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Draw a feature based on a value
            ValueStyle valueStyle = new ValueStyle();
            valueStyle.ColumnName = "CNTRY_NAME";
            valueStyle.ValueItems.Add(new ValueItem("United States", new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.Green)))));

            worldLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("worldOverlay", worldOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-165.946875, 86.4359375, -35.86875, -6.3765625);
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
            this.gbxDescrition.Location = new System.Drawing.Point(555, 3);
            this.gbxDescrition.Name = "gbxDescrition";
            this.gbxDescrition.Size = new System.Drawing.Size(182, 57);
            this.gbxDescrition.TabIndex = 3;
            this.gbxDescrition.TabStop = false;
            this.gbxDescrition.Text = "Descrption";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "The feature where CNTRY_NAME \r\nis equal to \"United States\".\"\r\n";
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
            // DrawAFeatureBasedOnAValue
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxDescrition);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DrawAFeatureBasedOnAValue";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DrawAFeatureBasedOnAValue_Load);
            this.gbxDescrition.ResumeLayout(false);
            this.gbxDescrition.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}