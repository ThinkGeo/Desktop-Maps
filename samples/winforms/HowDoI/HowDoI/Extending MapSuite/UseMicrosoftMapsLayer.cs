using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;
using BingMapsMapType = ThinkGeo.MapSuite.Layers.BingMapsMapType;

namespace CSHowDoISamples
{
    public class UseMicrosoftMapsLayer : UserControl
    {
        public UseMicrosoftMapsLayer()
        {
            InitializeComponent();
        }

        private void UseMicrosoftMapsLayer_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.BingMapsResult;
        }

        private void LoadBingMapsLayer()
        {
            // Please set your own information about those parameters below.
            string applicationID = "Your ApplicationID";
            string cacheDirectory = @"c:\temp\";
            BingMapsLayer worldLayer = new BingMapsLayer(applicationID, BingMapsMapType.Road, cacheDirectory);

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);
            winformsMap1.CurrentExtent = new RectangleShape(-10000000, 10000000, 10000000, -10000000);

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private PictureBox pictureBox1;
        private WinformsMap winformsMap1 = new WinformsMap();

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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            //
            // pictureBox1
            //
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(734, 525);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            //
            // UseMicrosoftMapsLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Name = "UseMicrosoftMapsLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UseMicrosoftMapsLayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}