/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public partial class AddAnInMemoryMarkerOverlay : UserControl
    {
        public AddAnInMemoryMarkerOverlay()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-17336118, 20037508, 11623981, -16888303);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            InMemoryMarkerOverlay markerOverlay = new InMemoryMarkerOverlay();
            markerOverlay.MapControl = winformsMap1;
            markerOverlay.Columns.Add(new FeatureSourceColumn("Name"));
            markerOverlay.ZoomLevelSet.ZoomLevel01.DefaultPointMarkerStyle.Image = Properties.Resources.AQUA;
            markerOverlay.ZoomLevelSet.ZoomLevel01.DefaultPointMarkerStyle.Width = 20;
            markerOverlay.ZoomLevelSet.ZoomLevel01.DefaultPointMarkerStyle.Height = 34;
            markerOverlay.ZoomLevelSet.ZoomLevel01.DefaultPointMarkerStyle.YOffset = -17;
            markerOverlay.ZoomLevelSet.ZoomLevel01.DefaultPointMarkerStyle.ToolTipText = "This is [#Name#].";
            markerOverlay.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            winformsMap1.Overlays.Add("MarkerOverlay", markerOverlay);

            Feature newFeature = new Feature(-10606588, 4715285);
            newFeature.ColumnValues.Add("Name", "Lawrence");

            markerOverlay.FeatureSource.BeginTransaction();
            markerOverlay.FeatureSource.AddFeature(newFeature);
            markerOverlay.FeatureSource.CommitTransaction();

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private GroupBox gbxDescrition;
        private WinformsMap winformsMap1;
        private Label label1;

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
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.gbxDescrition.SuspendLayout();
            this.SuspendLayout();
            //
            // gbxDescrition
            //
            this.gbxDescrition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxDescrition.Controls.Add(this.label1);
            this.gbxDescrition.Location = new System.Drawing.Point(430, -1);
            this.gbxDescrition.Name = "gbxDescrition";
            this.gbxDescrition.Size = new System.Drawing.Size(307, 55);
            this.gbxDescrition.TabIndex = 4;
            this.gbxDescrition.TabStop = false;
            this.gbxDescrition.Text = "Description";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "This sample shows how to add an InMemoryMarkerOverlay\r\n and set its styles. Mouse hover to show its tooltip.";
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
            // AddMyOwnCustomDataToAFeatureLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxDescrition);
            this.Controls.Add(this.winformsMap1);
            this.Name = "AddMyOwnCustomDataToAFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.Form_Load);
            this.gbxDescrition.ResumeLayout(false);
            this.gbxDescrition.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}