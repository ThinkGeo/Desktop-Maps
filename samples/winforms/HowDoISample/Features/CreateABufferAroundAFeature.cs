﻿using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateABufferAroundAFeature : UserControl
    {
        public CreateABufferAroundAFeature()
        {
            InitializeComponent();
        }

        private void CreateABufferAroundAFeature_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-135.224054810107, 62.2893787272533, -58.8379651537011, 7.78687151295263);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.White);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.Open();
            Feature feature = worldLayer.QueryTools.GetFeatureById("137", new string[0]);
            worldLayer.Close();

            InMemoryFeatureLayer inMemoryLayer = new InMemoryFeatureLayer();
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.Green));
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.DeepOcean;
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            inMemoryLayer.InternalFeatures.Add("POLYGON", feature);

            InMemoryFeatureLayer bufferLayer = new InMemoryFeatureLayer();
            bufferLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.Green));
            bufferLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay inmemoryOverlay = new LayerOverlay();
            inmemoryOverlay.Layers.Add("InMemoryFeatureLayer", inMemoryLayer);
            winformsMap1.Overlays.Add("InmemoryOverlay", inmemoryOverlay);

            LayerOverlay bufferOverlay = new LayerOverlay();
            bufferOverlay.Layers.Add("BufferLayer", bufferLayer);
            winformsMap1.Overlays.Add("BufferOverlay", bufferOverlay);

            winformsMap1.Refresh();
        }

        private void btnBuffer_Click(object sender, EventArgs e)
        {
            InMemoryFeatureLayer inMemoryLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("InMemoryFeatureLayer");
            InMemoryFeatureLayer bufferLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("BufferLayer");

            AreaBaseShape baseShape = (AreaBaseShape)inMemoryLayer.InternalFeatures["POLYGON"].GetShape();
            MultipolygonShape bufferedShape = baseShape.Buffer(100, 8, BufferCapType.Butt, GeographyUnit.DecimalDegree, DistanceUnit.Kilometer);
            Feature bufferFeature = new Feature(bufferedShape.GetWellKnownBinary(), "Buffer1");

            bufferLayer.InternalFeatures.Clear();
            bufferLayer.InternalFeatures.Add("BufferFeature", bufferFeature);

            winformsMap1.Refresh(winformsMap1.Overlays["BufferOverlay"]);
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private GroupBox groupBox1;
        private MapView winformsMap1;
        private Button btnBuffer;

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
            this.btnBuffer = new System.Windows.Forms.Button();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnBuffer);
            this.groupBox1.Location = new System.Drawing.Point(640, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(97, 55);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // btnBuffer
            //
            this.btnBuffer.Location = new System.Drawing.Point(13, 20);
            this.btnBuffer.Name = "btnBuffer";
            this.btnBuffer.Size = new System.Drawing.Size(75, 23);
            this.btnBuffer.TabIndex = 0;
            this.btnBuffer.Text = "Buffer";
            this.btnBuffer.UseVisualStyleBackColor = true;
            this.btnBuffer.Click += new System.EventHandler(this.btnBuffer_Click);
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
            // CreateABufferAroundAFeature
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "CreateABufferAroundAFeature";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.CreateABufferAroundAFeature_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}