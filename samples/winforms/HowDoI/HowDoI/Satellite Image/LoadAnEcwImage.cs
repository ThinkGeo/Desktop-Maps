using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class LoadAnEcwImage : UserControl
    {
        public LoadAnEcwImage()
        {
            InitializeComponent();
        }

        private void LoadAnEcwImage_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-118.098, 84.3, 118.098, -84.3);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            EcwRasterLayer ecwImageLayer = new EcwRasterLayer(Samples.RootDirectory + @"Data\World.ecw");
            ecwImageLayer.UpperThreshold = double.MaxValue;
            ecwImageLayer.LowerThreshold = 0;

            LayerOverlay imageOverlay = new LayerOverlay();
            imageOverlay.Layers.Add("EcwImageLayer", ecwImageLayer);
            winformsMap1.Overlays.Add(imageOverlay);

            winformsMap1.Refresh();
        }

        private WinformsMap winformsMap1;

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
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.SuspendLayout();
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 0;
            this.winformsMap1.Text = "winformsMap1";
            //
            // LoadAnEcwImage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.winformsMap1);
            this.Name = "LoadAnEcwImage";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.LoadAnEcwImage_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}