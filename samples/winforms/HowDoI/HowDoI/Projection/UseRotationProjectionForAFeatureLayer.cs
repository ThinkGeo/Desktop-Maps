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
    public class UseRotationProjectionForAFeatureLayer : UserControl
    {
        private RotationProjection rotateProjection;

        public UseRotationProjectionForAFeatureLayer()
        {
            InitializeComponent();
        }

        private void UseRotationProjectionForAFeatureLayer_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            rotateProjection = new RotationProjection();
            worldLayer.FeatureSource.Projection = rotateProjection;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.None;
            winformsMap1.CurrentExtent = rotateProjection.GetUpdatedExtent(new RectangleShape(-20037508, 17821912, 20037508, -20037508));

            winformsMap1.Refresh();
        }

        private void btnRotateCounterclockwise_Click(object sender, EventArgs e)
        {
            rotateProjection.Angle += 20;
            winformsMap1.CurrentExtent = rotateProjection.GetUpdatedExtent(winformsMap1.CurrentExtent);

            winformsMap1.Refresh(winformsMap1.Overlays["WorldOverlay"]);
        }

        private void btnRotateClockwise_Click(object sender, EventArgs e)
        {
            rotateProjection.Angle -= 20;
            winformsMap1.CurrentExtent = rotateProjection.GetUpdatedExtent(winformsMap1.CurrentExtent);

            winformsMap1.Refresh(winformsMap1.Overlays["WorldOverlay"]);
        }

        #region Component Designer generated code

        private Button btnRotateCounterclockwise;
        private Button btnRotateClockwise;
        private GroupBox groupBox1;
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
            this.btnRotateCounterclockwise = new System.Windows.Forms.Button();
            this.btnRotateClockwise = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // btnRotateCounterclockwise
            //
            this.btnRotateCounterclockwise.Location = new System.Drawing.Point(12, 19);
            this.btnRotateCounterclockwise.Name = "btnRotateCounterclockwise";
            this.btnRotateCounterclockwise.Size = new System.Drawing.Size(99, 23);
            this.btnRotateCounterclockwise.TabIndex = 1;
            this.btnRotateCounterclockwise.Text = "Counterclockwise";
            this.btnRotateCounterclockwise.UseVisualStyleBackColor = true;
            this.btnRotateCounterclockwise.Click += new System.EventHandler(this.btnRotateCounterclockwise_Click);
            //
            // btnRotateClockwise
            //
            this.btnRotateClockwise.Location = new System.Drawing.Point(124, 19);
            this.btnRotateClockwise.Name = "btnRotateClockwise";
            this.btnRotateClockwise.Size = new System.Drawing.Size(99, 23);
            this.btnRotateClockwise.TabIndex = 2;
            this.btnRotateClockwise.Text = "Clockwise";
            this.btnRotateClockwise.UseVisualStyleBackColor = true;
            this.btnRotateClockwise.Click += new System.EventHandler(this.btnRotateClockwise_Click);
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.btnRotateCounterclockwise);
            this.groupBox1.Controls.Add(this.btnRotateClockwise);
            this.groupBox1.Location = new System.Drawing.Point(499, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 55);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rotation";
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
            // UseRotationProjectionForAFeatureLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "UseRotationProjectionForAFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UseRotationProjectionForAFeatureLayer_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}