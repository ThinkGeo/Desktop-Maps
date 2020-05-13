using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class HideOrShowARasterLayer : UserControl
    {
        public HideOrShowARasterLayer()
        {
            InitializeComponent();
        }

        private void ShowHideRasterLayer_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-118.098, 84.3, 118.098, -84.3);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);
            winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.None;

            EcwRasterLayer ecwImageLayer = new EcwRasterLayer(Samples.RootDirectory + @"Data\World.ecw");
            ecwImageLayer.UpperThreshold = double.MaxValue;
            ecwImageLayer.LowerThreshold = 0;

            LayerOverlay imageOverlay = new LayerOverlay();
            imageOverlay.Layers.Add("EcwImageLayer", ecwImageLayer);
            winformsMap1.Overlays.Add("ImageOverlay", imageOverlay);

            winformsMap1.Refresh();
        }

        private void cbxShowLayer_CheckedChanged(object sender, EventArgs e)
        {
            winformsMap1.FindRasterLayer("EcwImageLayer").IsVisible = cbxShowLayer.Checked;

            winformsMap1.Refresh(winformsMap1.Overlays["ImageOverlay"]);
        }

        private WinformsMap winformsMap1;

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxShowLayer = new System.Windows.Forms.CheckBox();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbxShowLayer);
            this.groupBox1.Location = new System.Drawing.Point(594, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(143, 51);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // cbxShowLayer
            //
            this.cbxShowLayer.AutoSize = true;
            this.cbxShowLayer.Checked = true;
            this.cbxShowLayer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxShowLayer.Location = new System.Drawing.Point(17, 19);
            this.cbxShowLayer.Name = "cbxShowLayer";
            this.cbxShowLayer.Size = new System.Drawing.Size(114, 17);
            this.cbxShowLayer.TabIndex = 0;
            this.cbxShowLayer.Text = "Show Image Layer";
            this.cbxShowLayer.UseVisualStyleBackColor = false;
            this.cbxShowLayer.CheckedChanged += new System.EventHandler(this.cbxShowLayer_CheckedChanged);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 6;
            this.winformsMap1.Text = "winformsMap1";
            //
            // HideOrShowARasterLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "HideOrShowARasterLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ShowHideRasterLayer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        private GroupBox groupBox1;
        private CheckBox cbxShowLayer;

        #endregion Component Designer generated code
    }
}