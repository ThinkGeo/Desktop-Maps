using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DrawFeaturesBasedOnValues : UserControl
    {
        public DrawFeaturesBasedOnValues()
        {
            InitializeComponent();
        }

        private void DrawFeaturesBasedOnValues_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldCapitalsLayer = new ShapeFileFeatureLayer(SampleHelper.Get("capital.shp"));
            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Draw features based on values
            ValueStyle valueStyle = new ValueStyle();
            valueStyle.ColumnName = "POP_RANK";
            valueStyle.ValueItems.Add(new ValueItem("1", PointStyle.CreateCompoundPointStyle(PointSymbolType.Square, GeoColors.White, GeoColors.Black, 1F, 10F, PointSymbolType.Square, GeoColors.Navy, GeoColors.Transparent, 0F, 6F)));
            valueStyle.ValueItems.Add(new ValueItem("2", PointStyle.CreateCompoundPointStyle(PointSymbolType.Square, GeoColors.White, GeoColors.Black, 1F, 6F, PointSymbolType.Square, GeoColors.Maroon, GeoColors.Transparent, 0F, 2F)));
            valueStyle.ValueItems.Add(new ValueItem("3", PointStyle.CreateSimpleCircleStyle(GeoColors.White, 3.2F, GeoColors.Black, 1F)));
            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldCaptials", worldCapitalsLayer);
            winformsMap1.Overlays.Add(worldOverlay);

            //winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.Default;
            winformsMap1.CurrentExtent = new RectangleShape(-143.4, 109.3, 116.7, -76.3);
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
            this.gbxDescrition.Location = new System.Drawing.Point(524, 3);
            this.gbxDescrition.Name = "gbxDescrition";
            this.gbxDescrition.Size = new System.Drawing.Size(213, 44);
            this.gbxDescrition.TabIndex = 4;
            this.gbxDescrition.TabStop = false;
            this.gbxDescrition.Text = "Description";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Draw capitals based on different values";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 5;
            this.winformsMap1.Text = "winformsMap1";
            //
            // DrawFeaturesBasedOnValues
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxDescrition);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DrawFeaturesBasedOnValues";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DrawFeaturesBasedOnValues_Load);
            this.gbxDescrition.ResumeLayout(false);
            this.gbxDescrition.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}