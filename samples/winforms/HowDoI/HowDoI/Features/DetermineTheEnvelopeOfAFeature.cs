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
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class DetermineTheEnvelopeOfAFeature : UserControl
    {
        public DetermineTheEnvelopeOfAFeature()
        {
            InitializeComponent();
        }

        private void DetermineTheEnvelopOfAFeature_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.StandardColors.White);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            // Setup the BoundingBox InMemoryFeatureLayer.
            InMemoryFeatureLayer boundingBoxLayer = new InMemoryFeatureLayer();
            boundingBoxLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillSolidBrush.Color = GeoColor.FromArgb(100, GeoColor.SimpleColors.Green);
            boundingBoxLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColor.SimpleColors.Green;
            boundingBoxLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;


            LayerOverlay boundingboxOverlay = new LayerOverlay();
            boundingboxOverlay.Layers.Add("BoundingBoxLayer", boundingBoxLayer);
            winformsMap1.Overlays.Add("BoundingBoxOverlay", boundingboxOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-14774792, 5894765, -7534677, 33744);

            winformsMap1.Refresh();
        }

        private void btnGetEnvelope_Click(object sender, EventArgs e)
        {
            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");
            worldLayer.Open();
            RectangleShape usBoundingBox = worldLayer.QueryTools.GetFeatureById("137", new string[0]).GetBoundingBox();
            worldLayer.Close();

            InMemoryFeatureLayer boundingBoxLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("BoundingBoxLayer");
            if (!boundingBoxLayer.InternalFeatures.Contains("BoundingBox"))
            {
                boundingBoxLayer.InternalFeatures.Add("BoundingBox", new Feature(usBoundingBox.GetWellKnownBinary(), "BoundingBox"));
            }

            winformsMap1.Refresh(winformsMap1.Overlays["BoundingBoxOverlay"]);
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private Button btnGetEnvelope;
        private WinformsMap winformsMap1;
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
            this.btnGetEnvelope = new System.Windows.Forms.Button();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnGetEnvelope);
            this.groupBox1.Location = new System.Drawing.Point(600, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 59);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // btnGetEnvelope
            //
            this.btnGetEnvelope.Location = new System.Drawing.Point(13, 19);
            this.btnGetEnvelope.Name = "btnGetEnvelope";
            this.btnGetEnvelope.Size = new System.Drawing.Size(118, 23);
            this.btnGetEnvelope.TabIndex = 1;
            this.btnGetEnvelope.Text = "Get Envelope";
            this.btnGetEnvelope.UseVisualStyleBackColor = true;
            this.btnGetEnvelope.Click += new System.EventHandler(this.btnGetEnvelope_Click);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 9;
            this.winformsMap1.Text = "winformsMap1";
            //
            // DetermineTheEnvelopeOfAFeature
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DetermineTheEnvelopeOfAFeature";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DetermineTheEnvelopOfAFeature_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}